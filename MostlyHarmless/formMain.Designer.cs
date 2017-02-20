namespace MostlyHarmless
{
    partial class formMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.picBoxMain = new System.Windows.Forms.PictureBox();
            this.picBoxTrackingImage = new System.Windows.Forms.PictureBox();
            this.btnTrackingImage = new System.Windows.Forms.Button();
            this.btnProcessFrame = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tBoxAlpha = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tBoxHoldPercent = new System.Windows.Forms.TextBox();
            this.tboxThreshold = new System.Windows.Forms.TextBox();
            this.tBoxNumParticles = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tboxCenterCoordinates = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tBoxConfidence = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnZeroRocket = new System.Windows.Forms.Button();
            this.lblRocketStatus = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tBoxPixels = new System.Windows.Forms.TextBox();
            this.btnUpdateRocket = new System.Windows.Forms.Button();
            this.btnFireRocket = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTrackingImage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(276, 36);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 52);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start Webcam";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // picBoxMain
            // 
            this.picBoxMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.picBoxMain.Location = new System.Drawing.Point(7, 94);
            this.picBoxMain.Name = "picBoxMain";
            this.picBoxMain.Size = new System.Drawing.Size(640, 480);
            this.picBoxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxMain.TabIndex = 4;
            this.picBoxMain.TabStop = false;
            // 
            // picBoxTrackingImage
            // 
            this.picBoxTrackingImage.Location = new System.Drawing.Point(167, 19);
            this.picBoxTrackingImage.Name = "picBoxTrackingImage";
            this.picBoxTrackingImage.Size = new System.Drawing.Size(78, 88);
            this.picBoxTrackingImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxTrackingImage.TabIndex = 6;
            this.picBoxTrackingImage.TabStop = false;
            // 
            // btnTrackingImage
            // 
            this.btnTrackingImage.Location = new System.Drawing.Point(6, 36);
            this.btnTrackingImage.Name = "btnTrackingImage";
            this.btnTrackingImage.Size = new System.Drawing.Size(75, 51);
            this.btnTrackingImage.TabIndex = 7;
            this.btnTrackingImage.Text = "Set New Tracking Image";
            this.btnTrackingImage.UseVisualStyleBackColor = true;
            this.btnTrackingImage.Click += new System.EventHandler(this.btnTrackingImage_Click);
            // 
            // btnProcessFrame
            // 
            this.btnProcessFrame.Location = new System.Drawing.Point(10, 35);
            this.btnProcessFrame.Name = "btnProcessFrame";
            this.btnProcessFrame.Size = new System.Drawing.Size(75, 23);
            this.btnProcessFrame.TabIndex = 8;
            this.btnProcessFrame.Text = "Start Filter";
            this.btnProcessFrame.UseVisualStyleBackColor = true;
            this.btnProcessFrame.Click += new System.EventHandler(this.btnProcessFrame_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTrackingImage);
            this.groupBox1.Controls.Add(this.picBoxTrackingImage);
            this.groupBox1.Location = new System.Drawing.Point(658, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 134);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tracking Image";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tBoxAlpha);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.tBoxHoldPercent);
            this.groupBox2.Controls.Add(this.tboxThreshold);
            this.groupBox2.Controls.Add(this.tBoxNumParticles);
            this.groupBox2.Controls.Add(this.lblStatus);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tboxCenterCoordinates);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tBoxConfidence);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnProcessFrame);
            this.groupBox2.Location = new System.Drawing.Point(658, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(292, 250);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Particle Filter Controls";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // tBoxAlpha
            // 
            this.tBoxAlpha.Location = new System.Drawing.Point(9, 165);
            this.tBoxAlpha.Name = "tBoxAlpha";
            this.tBoxAlpha.Size = new System.Drawing.Size(63, 20);
            this.tBoxAlpha.TabIndex = 27;
            this.tBoxAlpha.Text = "0.05";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(75, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Alpha";
            // 
            // tBoxHoldPercent
            // 
            this.tBoxHoldPercent.Location = new System.Drawing.Point(9, 139);
            this.tBoxHoldPercent.Name = "tBoxHoldPercent";
            this.tBoxHoldPercent.Size = new System.Drawing.Size(63, 20);
            this.tBoxHoldPercent.TabIndex = 25;
            this.tBoxHoldPercent.Text = "0.80";
            // 
            // tboxThreshold
            // 
            this.tboxThreshold.Location = new System.Drawing.Point(10, 113);
            this.tboxThreshold.Name = "tboxThreshold";
            this.tboxThreshold.Size = new System.Drawing.Size(63, 20);
            this.tboxThreshold.TabIndex = 24;
            this.tboxThreshold.Text = ".9";
            // 
            // tBoxNumParticles
            // 
            this.tBoxNumParticles.Location = new System.Drawing.Point(9, 89);
            this.tBoxNumParticles.Name = "tBoxNumParticles";
            this.tBoxNumParticles.Size = new System.Drawing.Size(63, 20);
            this.tBoxNumParticles.TabIndex = 23;
            this.tBoxNumParticles.Text = "200";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(156, 40);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(21, 13);
            this.lblStatus.TabIndex = 22;
            this.lblStatus.Text = "Off";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(96, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Status:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(96, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Center Coordinates";
            // 
            // tboxCenterCoordinates
            // 
            this.tboxCenterCoordinates.Enabled = false;
            this.tboxCenterCoordinates.Location = new System.Drawing.Point(111, 217);
            this.tboxCenterCoordinates.Name = "tboxCenterCoordinates";
            this.tboxCenterCoordinates.Size = new System.Drawing.Size(63, 20);
            this.tboxCenterCoordinates.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Confidence";
            // 
            // tBoxConfidence
            // 
            this.tBoxConfidence.Enabled = false;
            this.tBoxConfidence.Location = new System.Drawing.Point(6, 217);
            this.tBoxConfidence.Name = "tBoxConfidence";
            this.tBoxConfidence.Size = new System.Drawing.Size(63, 20);
            this.tBoxConfidence.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Hold Percentage";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Threshold";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Number of Particles";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnStart);
            this.groupBox3.Controls.Add(this.picBoxMain);
            this.groupBox3.Location = new System.Drawing.Point(3, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(655, 602);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Webcam";
            // 
            // btnZeroRocket
            // 
            this.btnZeroRocket.Location = new System.Drawing.Point(207, 85);
            this.btnZeroRocket.Name = "btnZeroRocket";
            this.btnZeroRocket.Size = new System.Drawing.Size(75, 44);
            this.btnZeroRocket.TabIndex = 9;
            this.btnZeroRocket.Text = "Zero Rocket Launcher";
            this.btnZeroRocket.UseVisualStyleBackColor = true;
            this.btnZeroRocket.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblRocketStatus
            // 
            this.lblRocketStatus.AutoSize = true;
            this.lblRocketStatus.Location = new System.Drawing.Point(49, 16);
            this.lblRocketStatus.Name = "lblRocketStatus";
            this.lblRocketStatus.Size = new System.Drawing.Size(79, 13);
            this.lblRocketStatus.TabIndex = 26;
            this.lblRocketStatus.Text = "Not Connected";
            this.lblRocketStatus.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Status:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnFireRocket);
            this.groupBox4.Controls.Add(this.btnUpdateRocket);
            this.groupBox4.Controls.Add(this.lblRocketStatus);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.tBoxPixels);
            this.groupBox4.Controls.Add(this.btnZeroRocket);
            this.groupBox4.Location = new System.Drawing.Point(658, 408);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(292, 178);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Rocket Controls";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Pixels per Millisecond";
            // 
            // tBoxPixels
            // 
            this.tBoxPixels.Location = new System.Drawing.Point(116, 44);
            this.tBoxPixels.Name = "tBoxPixels";
            this.tBoxPixels.Size = new System.Drawing.Size(63, 20);
            this.tBoxPixels.TabIndex = 21;
            this.tBoxPixels.Text = "2";
            // 
            // btnUpdateRocket
            // 
            this.btnUpdateRocket.Location = new System.Drawing.Point(10, 85);
            this.btnUpdateRocket.Name = "btnUpdateRocket";
            this.btnUpdateRocket.Size = new System.Drawing.Size(75, 44);
            this.btnUpdateRocket.TabIndex = 27;
            this.btnUpdateRocket.Text = "Update Parameters";
            this.btnUpdateRocket.UseVisualStyleBackColor = true;
            this.btnUpdateRocket.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnFireRocket
            // 
            this.btnFireRocket.Location = new System.Drawing.Point(87, 85);
            this.btnFireRocket.Name = "btnFireRocket";
            this.btnFireRocket.Size = new System.Drawing.Size(75, 44);
            this.btnFireRocket.TabIndex = 28;
            this.btnFireRocket.Text = "Fire!";
            this.btnFireRocket.UseVisualStyleBackColor = true;
            this.btnFireRocket.Click += new System.EventHandler(this.btnFireRocket_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 595);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint_1);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTrackingImage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox picBoxMain;
        private System.Windows.Forms.PictureBox picBoxTrackingImage;
        private System.Windows.Forms.Button btnTrackingImage;
        private System.Windows.Forms.Button btnProcessFrame;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBoxConfidence;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tboxCenterCoordinates;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox tBoxHoldPercent;
        private System.Windows.Forms.TextBox tboxThreshold;
        private System.Windows.Forms.TextBox tBoxNumParticles;
        private System.Windows.Forms.TextBox tBoxAlpha;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblRocketStatus;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnZeroRocket;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tBoxPixels;
        private System.Windows.Forms.Button btnFireRocket;
        private System.Windows.Forms.Button btnUpdateRocket;
    }
}

