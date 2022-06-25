namespace Chess
{
    partial class MainMenu
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
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this._coolLogo = new System.Windows.Forms.PictureBox();
            this._playButton = new System.Windows.Forms.Button();
            this._exitButton = new System.Windows.Forms.Button();
            this._aboutButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._coolLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // _coolLogo
            // 
            this._coolLogo.Image = global::Chess2.Properties.Resources.Cool_Logo;
            this._coolLogo.Location = new System.Drawing.Point(104, 35);
            this._coolLogo.Name = "_coolLogo";
            this._coolLogo.Size = new System.Drawing.Size(324, 263);
            this._coolLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._coolLogo.TabIndex = 0;
            this._coolLogo.TabStop = false;
            // 
            // _playButton
            // 
            this._playButton.Location = new System.Drawing.Point(174, 340);
            this._playButton.Name = "_playButton";
            this._playButton.Size = new System.Drawing.Size(177, 39);
            this._playButton.TabIndex = 1;
            this._playButton.Text = "Play";
            this._playButton.UseVisualStyleBackColor = true;
            this._playButton.Click += new System.EventHandler(this.PlayClick);
            // 
            // _exitButton
            // 
            this._exitButton.Location = new System.Drawing.Point(174, 455);
            this._exitButton.Name = "_exitButton";
            this._exitButton.Size = new System.Drawing.Size(177, 34);
            this._exitButton.TabIndex = 3;
            this._exitButton.Text = "Exit";
            this._exitButton.UseVisualStyleBackColor = true;
            this._exitButton.Click += new System.EventHandler(this.ExitClick);
            // 
            // _aboutButton
            // 
            this._aboutButton.Location = new System.Drawing.Point(174, 398);
            this._aboutButton.Name = "_aboutButton";
            this._aboutButton.Size = new System.Drawing.Size(177, 39);
            this._aboutButton.TabIndex = 4;
            this._aboutButton.Text = "About";
            this._aboutButton.UseVisualStyleBackColor = true;
            this._aboutButton.Click += new System.EventHandler(this.AboutCkick);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(545, 526);
            this.Controls.Add(this._aboutButton);
            this.Controls.Add(this._exitButton);
            this.Controls.Add(this._playButton);
            this.Controls.Add(this._coolLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            ((System.ComponentModel.ISupportInitialize)(this._coolLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox _coolLogo;
        private Button _playButton;
        private Button _exitButton;
        private Button _aboutButton;
    }
}