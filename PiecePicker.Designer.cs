namespace Chess
{
    partial class PiecePicker
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
            this._knight = new System.Windows.Forms.RadioButton();
            this._castle = new System.Windows.Forms.RadioButton();
            this._bishop = new System.Windows.Forms.RadioButton();
            this._queen = new System.Windows.Forms.RadioButton();
            this._groupBox = new System.Windows.Forms.GroupBox();
            this._picker = new System.Windows.Forms.Button();
            this._groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _knight
            // 
            this._knight.AutoSize = true;
            this._knight.Location = new System.Drawing.Point(82, 87);
            this._knight.Name = "_knight";
            this._knight.Size = new System.Drawing.Size(73, 24);
            this._knight.TabIndex = 0;
            this._knight.Text = "Knight";
            this._knight.UseVisualStyleBackColor = true;
            // 
            // _castle
            // 
            this._castle.AutoSize = true;
            this._castle.Location = new System.Drawing.Point(82, 140);
            this._castle.Name = "_castle";
            this._castle.Size = new System.Drawing.Size(70, 24);
            this._castle.TabIndex = 1;
            this._castle.Text = "Castle";
            this._castle.UseVisualStyleBackColor = true;
            // 
            // _bishop
            // 
            this._bishop.AutoSize = true;
            this._bishop.Location = new System.Drawing.Point(82, 189);
            this._bishop.Name = "_bishop";
            this._bishop.Size = new System.Drawing.Size(75, 24);
            this._bishop.TabIndex = 2;
            this._bishop.Text = "Bishop";
            this._bishop.UseVisualStyleBackColor = true;
            // 
            // _queen
            // 
            this._queen.AutoSize = true;
            this._queen.Checked = true;
            this._queen.Location = new System.Drawing.Point(82, 39);
            this._queen.Name = "_queen";
            this._queen.Size = new System.Drawing.Size(73, 24);
            this._queen.TabIndex = 3;
            this._queen.TabStop = true;
            this._queen.Text = "Queen";
            this._queen.UseVisualStyleBackColor = true;
            // 
            // _groupBox
            // 
            this._groupBox.BackColor = System.Drawing.Color.Transparent;
            this._groupBox.Controls.Add(this._picker);
            this._groupBox.Controls.Add(this._queen);
            this._groupBox.Controls.Add(this._bishop);
            this._groupBox.Controls.Add(this._knight);
            this._groupBox.Controls.Add(this._castle);
            this._groupBox.Location = new System.Drawing.Point(31, 28);
            this._groupBox.Name = "_groupBox";
            this._groupBox.Size = new System.Drawing.Size(286, 277);
            this._groupBox.TabIndex = 4;
            this._groupBox.TabStop = false;
            this._groupBox.Text = "Pick a piece";
            // 
            // _picker
            // 
            this._picker.Location = new System.Drawing.Point(6, 242);
            this._picker.Name = "_picker";
            this._picker.Size = new System.Drawing.Size(274, 29);
            this._picker.TabIndex = 5;
            this._picker.Text = "Pick";
            this._picker.UseVisualStyleBackColor = true;
            this._picker.Click += new System.EventHandler(this.Pick);
            // 
            // PiecePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(362, 329);
            this.Controls.Add(this._groupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PiecePicker";
            this.Text = "PiecePicker";
            this._groupBox.ResumeLayout(false);
            this._groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RadioButton _knight;
        private RadioButton _castle;
        private RadioButton _bishop;
        private RadioButton _queen;
        private GroupBox _groupBox;
        private Button _picker;
    }
}