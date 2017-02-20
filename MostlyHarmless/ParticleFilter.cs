using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MostlyHarmless
{
    class ParticleFilter
    {

        private Bitmap _template;
        private Bitmap _frame;
        private List<Particle> _Particles = new List<Particle>();
        public bool _Centered;
        private double Threshold = 0.15;
        private float _HoldPercentage = 0.8f;
        private float _Confidence = 0;
        private float _alpha = .05f;


        /// <summary>
        /// Instantiates a new Particle Filter object
        /// </summary>
        /// <param name="numParticles">Number of Particles</param>
        /// <param name="template">Image we're searching for</param>
        /// <param name="Frame">One frame of the main video feed</param>
        /// <param name="threshold">Percent of particle weights needed to agree in order to be sure we're locked on</param>
        /// <param name="holdPercent">Percent of particles that drift slowly</param>
        /// <param name="alpha">Weight decay for slow-drifting particles</param>
        public ParticleFilter(int numParticles, Bitmap template, Bitmap Frame, double threshold, float holdPercent, float alpha)
        {
            _frame = (Bitmap)Frame.Clone();
            _template = (Bitmap)template.Clone();
            _HoldPercentage = holdPercent;
            Threshold = threshold;
            _alpha = alpha;
            Random RN = new Random();
            int maxX = _frame.Size.Width;
            int maxY = _frame.Size.Height;
            for (int i = 0; i < numParticles; i++)
            {
                Particle P = new Particle();
                P.x = RN.Next(0, maxX);
                P.y = RN.Next(0, maxY);
                P.weight = 1 / numParticles;
                _Particles.Add(P);
            }
        }

        /// <summary>
        /// Take in a new frame, update particle weights and positions.
        /// </summary>
        /// <param name="newFrame"></param>
        public void processFrame(Bitmap newFrame)
        {
            try
            {
                int x;
                int y;
                int height = _template.Height;
                int width = _template.Width;

                Rectangle trect = new Rectangle(0, 0, _template.Width, _template.Height);

                System.Drawing.Imaging.BitmapData tbmp = _template.LockBits(trect, System.Drawing.Imaging.ImageLockMode.ReadWrite, _template.PixelFormat);
                int stayput = (int)(_Particles.Count * _HoldPercentage);
                int i = 0;
                foreach (Particle P in _Particles)
                {
                    //Check to see we're not passing the edge of the image.
                    x = Math.Max(P.x - (width / 2), 0);
                    y = Math.Max(P.y - (height / 2), 0);
                    x = Math.Min(x, newFrame.Width - width);
                    y = Math.Min(y, newFrame.Height - height);
                    //Update particle locations.
                    P.x = x + (width / 2);
                    P.y = y + (height / 2);

                    Rectangle rect = new Rectangle(x, y, width, height);
                    System.Drawing.Imaging.BitmapData bmp = newFrame.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, newFrame.PixelFormat);

                    if (i > stayput)
                    {
                        P.weight = P.weight * _alpha +  CalculateError(bmp, tbmp);
                    }
                    else
                    {
                        P.weight = CalculateError(bmp, tbmp);
                    }

                    newFrame.UnlockBits(bmp);

                }
                _template.UnlockBits(tbmp);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Calculates mean-square error between a slice of the image centered on a particle, and the template.
        /// Thanks to http://stackoverflow.com/questions/10771300/bitmap-lockbits-confusion for showing me how to do this.
        /// C# bitmap operations are weird.
        /// </summary>
        /// <param name="bmp">Frame slice</param>
        /// <param name="template">Template image</param>
        /// <returns></returns>
        private double CalculateError(System.Drawing.Imaging.BitmapData bmp, System.Drawing.Imaging.BitmapData template)
        {
            double result = 0.0;
            unsafe
            {

                List<double> res = new List<double>();

                var bpp = 3;//bmp.Stride / bmp.Width;
                var pt = (byte*)bmp.Scan0;
                var tpt = (byte*)template.Scan0;
                for (int y = 0; y < bmp.Height; y++)
                {
                    var row = pt + (y * bmp.Stride);
                    var trow = tpt + (y * template.Stride);

                    for(var x = 0; x < bmp.Width; x++)
                    {
                        var pixel = row + x * bpp;
                        var tpixel = trow + x * bpp;

                        for(var bit = 0; bit < bpp; bit++)
                        {
                            res.Add(Math.Pow((pixel[bit] - tpixel[bit]), 2));
                        }
                    }
                }
                result = res.Sum() * 1 / (bmp.Height * bmp.Width * bpp);
                result = 1 / result;
            }
            return result;
        }

        /// <summary>
        /// Normalize the weights of a list of particles.
        /// </summary>
        /// <param name="Original"></param>
        /// <returns></returns>
        private List<Particle> NormalizeWeights(List<Particle> Original)
        {
            double weightSum = 0;
            foreach(Particle P in Original)
            {
                weightSum += P.weight;
            }
            foreach(Particle P in Original)
            {
                P.weight /= weightSum;
            }
            return Original;
        }


        /// <summary>
        /// Gets the current lock confidence.
        /// </summary>
        /// <returns></returns>
        public float getConfidence()
        {
            return _Confidence;
        }

        /// <summary>
        /// Gets the X and Y coordinates of the current best guess center. 
        /// </summary>
        /// <returns>Array [x,y]</returns>
        public int[] findCenter()
        {
            int stayput = (int)(_Particles.Count * _HoldPercentage);
            List<Particle> temp = new List<Particle>();
            for (int i = stayput; i < _Particles.Count; i++)
            {
                temp.Add(_Particles[i]);
            }
            temp = NormalizeWeights(temp);

            int centerX = 0;
            int centerY = 0;
            
            foreach (Particle P in temp)
            { 
                centerX += (int)(P.x * P.weight);
                centerY += (int)(P.y * P.weight);
            }
            
            int[] result = new int[2];
            result[0] = centerX;
            result[1] = centerY;
            return result;

        }

        /// Scatter most of the particles to new locations.
        /// Most particles are redistributed at random.  The highest-weighted ones get to stay 
        /// close to where they were before.
        private void rescatterParticles()
        {
            Random RNG = new Random();
            int maxX = _frame.Size.Width;
            int maxY = _frame.Size.Height;
            int stayput = (int)(_Particles.Count * _HoldPercentage);
            int i = 0;
            foreach (Particle P in _Particles)
            {
                if (i < stayput)
                {
                    P.x = RNG.Next(0, maxX);
                    P.y = RNG.Next(0, maxY);
                }
                else
                {
                    P.x = RNG.Next(P.x - 5, P.x + 5);
                    P.y = RNG.Next(P.y - 5, P.y + 5);
                }
                i++;

            }
        }

        /// <summary>
        /// Displays graphical feedback of the particle filter on the webcam image
        /// </summary>
        /// <param name="gf"></param>
        /// <param name="canonCenter"></param>
        public void markupImage(Graphics gf, int[] canonCenter)
        {
            int x;
            int y;
            int height = _template.Height;
            int width = _template.Width;
            _Particles = _Particles.OrderBy(o => o.weight).ToList();
            _Particles = NormalizeWeights(_Particles);
            int[] center = findCenter();
            //Display all of the particles
            using (Pen brush = new Pen(Color.Red, 2))
            {
                foreach (Particle P in _Particles)
                {
                    int radius = Math.Max(_template.Width, _template.Height)*2;
                    x = P.x;
                    y = P.y;
                    radius = (int)(radius * P.weight);
                    Rectangle rect = new Rectangle(x, y, radius, radius);
                    gf.DrawEllipse(brush, rect);
                }
                

            }
            //Display the current best-fit for the template
            using (Pen templatePen = new Pen(Color.Green, 4))
            {
                Rectangle temp = new Rectangle(center[0]-_template.Width/2, center[1]-_template.Height/2, _template.Width, _template.Height);
                gf.DrawRectangle(templatePen,temp);
            }
            //Display the crosshairs of where the canon is aiming.
            using (Pen TargetPen = new Pen(Color.Black, 4))
            {
                Rectangle canonTarget = new Rectangle(canonCenter[0], canonCenter[1], 20, 20);
                gf.DrawEllipse(TargetPen, canonTarget);
            }

            _Centered = findIfCentered() > Threshold;
            rescatterParticles();

        }

        /// <summary>
        /// See if we're centered
        /// </summary>
        /// <returns>Percent confidence that we're centered</returns>
        public float findIfCentered()
        {
            double tolerance = Math.Max(_template.Width,_template.Height);
            int[] center = findCenter();
            int centerX = center[0];
            int centerY = center[1];
            float confidence = 0;
            foreach (Particle P in _Particles)
            {
                double test = distance(centerX, P.x, centerY, P.y);
                if (test < tolerance) 
                {
                confidence += (float)P.weight;
                }
            }
            _Confidence = confidence;
            return confidence;
        }

        //Utility function
        public double distance(int x1, int x2, int y1, int y2)
        {
            return Math.Sqrt(Math.Pow((x1- x2),2) + Math.Pow((y1- y2),2));
        }
    }

    
}
