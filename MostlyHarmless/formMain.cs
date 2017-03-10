using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.Main;
using MonoLibUsb;
using System.Threading;
using AForge.Video.DirectShow;
using AForge.Video;
using AForge.Vision;

using MissileSharp;

namespace MostlyHarmless
{
    public partial class formMain : Form
    {
        //global variables
        private CommandCenter _Commander = new CommandCenter(new ThunderMissileLauncher());
        private Bitmap _trackingImage;
        private Bitmap _currentFrame;
        private Bitmap _displayFrame;
        private ParticleFilter pf = null;
        private Thread _RocketTracking;
        VideoCaptureDevice videoSource = null;


        
        
        
        //Thread synchronization
        //Signal that the filter
        private EventWaitHandle _Centered = new EventWaitHandle(false, EventResetMode.ManualReset); 
        //This one is used as a bit of a hack.  If a new frame starts getting drawn before the last one is finished, it throws an exception.
        EventWaitHandle _DrawingDelay = new EventWaitHandle(false, EventResetMode.ManualReset);
        //Signal a new frame is in use
        EventWaitHandle _newFrame = new EventWaitHandle(true, EventResetMode.ManualReset);

        Semaphore _frameLock = new Semaphore(1, 1);
        //Filter is running
        private bool _ParticleFilterRunning = false;
        //Event that signals when the filter is stopping, then again when it's done.
        private EventWaitHandle _Filterstopping = new EventWaitHandle(false, EventResetMode.ManualReset);
        //Never gets set in the code.  This serves the same function Thread.sleep() might, but is a bit more reliable.
        EventWaitHandle Pause = new EventWaitHandle(false, EventResetMode.ManualReset);

        //Centering and guidance instructions
        int[] currentCenter = new int[2];
        int _pixelsToMilliseconds = 2;

        //Particle filter parameters
        int _numParticles = 200;
        double _Threshold = .15;
        float _HoldPercentage = .8f;
        float _Alpha = .05f;

        public formMain()
        {
            InitializeComponent();

        }


        /// <summary>
        /// Start up the webcam sample thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            FilterInfoCollection VideoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice(VideoDevices[0].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            videoSource.Start();

            Thread ResetThread = new Thread(new ThreadStart(ResetCanon));
            ResetThread.Start();

            if(_Commander.IsReady)
            {
                SetStatus("Connected", lblRocketStatus);
            }
            //_FrameProcessing = new Thread(new ThreadStart(ProcessFrame));
            //_FrameProcessing.Start();
        }


        /// <summary>
        /// Positions the Rocket Launcher about in the center of its range.
        /// </summary>
        private void ResetCanon()
        {
            SetStatus("Resetting", lblRocketStatus);
            _Commander.Reset();
            _Commander.Right(3000);
            _Commander.Up(100);
            SetStatus("Ready", lblRocketStatus);
        }

        /// <summary>
        /// Called every time the webcam sends us a new frame.
        /// Most of the UI elements are updated here.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void video_NewFrame(object sender,
        NewFrameEventArgs eventArgs)
        {
            // get new frame
            if (picBoxMain.Image != null)
            {
                picBoxMain.Image.Dispose();
            }
            _frameLock.WaitOne();
            _currentFrame = (Bitmap)eventArgs.Frame.Clone();
            picBoxMain.Image = _currentFrame;
            _frameLock.Release();
            _newFrame.Set();
            // process the frame
            using (Graphics gf = picBoxMain.CreateGraphics())
            {
                if (_ParticleFilterRunning && pf != null)
                {
                    _frameLock.WaitOne();
                    pf.markupImage(gf, currentCenter);
                    float Confidence = pf.getConfidence();
                    int[] center = pf.findCenter();
                    setTbox(Confidence.ToString(), tBoxConfidence);
                    setTbox(center[0].ToString() + "," + center[1].ToString(), tboxCenterCoordinates);
                    if (pf._Centered)
                    {
                        _Centered.Set();
                        SetStatus("Target Locked", lblStatus);
                    }
                    else
                    {
                        SetStatus("Looking for target", lblStatus);
                        _Centered.Reset();
                    }
                    if (!_RocketTracking.IsAlive)
                    {
                        _RocketTracking.Start();
                    }
                    _frameLock.Release();

                }

            }

        }

        /// <summary>
        /// Triggers every time we've got a new target lock, updates the aiming of the rocket launcher to match
        /// </summary>
        private void TrackAndFireThread()
        {
            _frameLock.WaitOne();
            currentCenter[0] = _currentFrame.Size.Width / 2;
            currentCenter[1] = _currentFrame.Size.Height / 2;
            _frameLock.Release();

            while(_ParticleFilterRunning)
            {
                bool centered = _Centered.WaitOne(1000);
                if (centered)
                {
                    int[] center = pf.findCenter();
                    int xoffset = currentCenter[0] - center[0];
                    int yoffset = currentCenter[1] - center[1];
                    currentCenter[0] = center[0];
                    currentCenter[1] = center[1];
                    if (xoffset > 1)
                    {
                        _Commander.Left(xoffset * _pixelsToMilliseconds);
                        Pause.WaitOne(xoffset * 4); //movements get lost if they're sent too closely together
                    }
                    if (xoffset < 1)
                    {
                        xoffset = Math.Abs(xoffset);
                        _Commander.Right(xoffset * _pixelsToMilliseconds);
                        Pause.WaitOne(xoffset * 4); //movements get lost if they're sent too closely together

                    }
                    if (yoffset > 1)
                    {
                        _Commander.Up(yoffset * _pixelsToMilliseconds);
                        Pause.WaitOne(xoffset * 4); //movements get lost if they're sent too closely together

                    }
                    if (yoffset < 1)
                    {
                        yoffset = Math.Abs(yoffset);
                        _Commander.Down(yoffset * _pixelsToMilliseconds);
                        Pause.WaitOne(xoffset * 4); //movements get lost if they're sent too closely together
                    }
                    Pause.WaitOne(500);//Prevent Jerking motions
                }
                

            }
            


        }

        /// <summary>
        /// Thread-safe UI label control.
        /// </summary>
        /// <param name="statusText"></param>
        /// <param name="lblset"></param>
        private void SetStatus(string statusText, Label lblset)
        {
            if (this.lblStatus.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetStatus);
                this.Invoke(d, new object[] { statusText, lblset });
            }
            else
            {
                lblset.Text = statusText;
            }
        }
        delegate void setTboxCallback(string text,TextBox tBox);

        /// <summary>
        /// Thread-safe UI Text box control.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="tBox"></param>
        private void setTbox(string text, TextBox tBox)
        {
            if (this.lblStatus.InvokeRequired)
            {
                setTboxCallback d = new setTboxCallback(setTbox);
                this.Invoke(d, new object[] { text, tBox });
            }
            else
            {
                tBox.Text = text;
            }
        }
        delegate void SetTextCallback(string text, Label lblSet);

        /// <summary>
        /// _DrawingDelay is necessary since trying to display a new frame before the old one
        /// is finished will throw an exception.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            _DrawingDelay.Set();
        }

        /// <summary>
        /// Cleanly shut everything down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ParticleFilterRunning = false;
            Pause.WaitOne(100);
            if (videoSource != null)
            {
                videoSource.NewFrame -= new NewFrameEventHandler(video_NewFrame);
                videoSource.SignalToStop();
                Pause.WaitOne(2000);
                //replaced with Sleep.  WaitforStop hangs for some reason.
                //videoSource.WaitForStop();
                videoSource = null;
            }
            
        }

        /// <summary>
        /// Set a new tracking image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTrackingImage_Click(object sender, EventArgs e)
        {
            try
            {
                _newFrame.WaitOne();
                _newFrame.Reset();
                FileDialog newImage = new OpenFileDialog();
                newImage.Filter = "BMP Files (*.bmp) |*.bmp| JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All files|*.*";
                DialogResult result = newImage.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _trackingImage = new Bitmap(newImage.FileName);
                    Size s = _trackingImage.Size;
                    picBoxTrackingImage.Size = s;
                    picBoxTrackingImage.Image = _trackingImage;
                }
                _newFrame.Set();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                _newFrame.Set();
            }
            

        }
        
        /// <summary>
        /// Start or stop the particle filter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProcessFrame_Click(object sender, EventArgs e)
        {
            try
            {
                _numParticles = int.Parse(tBoxNumParticles.Text);
                _Threshold = double.Parse(tboxThreshold.Text);
                _HoldPercentage = float.Parse(tBoxHoldPercent.Text);
                _Alpha = float.Parse(tBoxAlpha.Text);
                _pixelsToMilliseconds = int.Parse(tBoxPixels.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Invalid value in particle text boxes!");
            }
            if (!_ParticleFilterRunning)
            {
                SetStatus("Starting", lblStatus);
                _ParticleFilterRunning = true;
                Thread FilterThread = new Thread(new ThreadStart(ProcessFrame));
                _RocketTracking = new Thread(new ThreadStart(TrackAndFireThread));
                FilterThread.Start();
                btnProcessFrame.Text = "Stop Filter";
            }
            else
            {
                SetStatus("Off", lblStatus);
                _ParticleFilterRunning = false;
                _Filterstopping.WaitOne();
                btnProcessFrame.Text = "Start Filter";
                pf = null;
            }
        }


        /// <summary>
        /// Run the particle filter on its own independent thread.
        /// </summary>
        private void ProcessFrame()
        {
            
            while (_ParticleFilterRunning)
            {
                if (_currentFrame != null && _trackingImage != null)
                {
                    _newFrame.WaitOne();
                    _newFrame.Reset();
                    _frameLock.WaitOne();
                    var Frame = (Bitmap)_currentFrame.Clone();
                    _frameLock.Release();
                    if (pf == null)
                    {
                        Bitmap Template = (Bitmap)_trackingImage.Clone();
                        pf = new ParticleFilter(_numParticles, Template, Frame, _Threshold, _HoldPercentage, _Alpha);
                    }
                    pf.processFrame(Frame);
                    Frame.Dispose();
                    Pause.WaitOne(100); //Prevents Jerky delayed webcam updates.
                }
            }
            _Filterstopping.Set();
        }

        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread ResetThread = new Thread(new ThreadStart(ResetCanon));
            ResetThread.Start();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                _pixelsToMilliseconds = int.Parse(tBoxPixels.Text);
            }
            catch
            {
                MessageBox.Show("Invalid Pixel per milliseconds entry");
            }
        }

        private void btnFireRocket_Click(object sender, EventArgs e)
        {
            _newFrame.WaitOne();
            _newFrame.Reset();
            _Commander.Fire(1);
            _newFrame.Set();
        }
    }
}
