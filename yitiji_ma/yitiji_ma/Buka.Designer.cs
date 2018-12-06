namespace yitiji_ma
{
    partial class Buka
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
            this.telone = new System.Windows.Forms.TextBox();
            this.teltwo = new System.Windows.Forms.TextBox();
            this.submitBtn = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // telone
            // 
            this.telone.BackColor = System.Drawing.SystemColors.InfoText;
            this.telone.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.telone.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.telone.Location = new System.Drawing.Point(511, 237);
            this.telone.Name = "telone";
            this.telone.Size = new System.Drawing.Size(387, 62);
            this.telone.TabIndex = 0;
            this.telone.Text = "亲情号1";
            this.telone.Enter += new System.EventHandler(this.telone_Enter);
            this.telone.Leave += new System.EventHandler(this.telone_Leave);
            // 
            // teltwo
            // 
            this.teltwo.BackColor = System.Drawing.SystemColors.InfoText;
            this.teltwo.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.teltwo.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.teltwo.Location = new System.Drawing.Point(511, 345);
            this.teltwo.Name = "teltwo";
            this.teltwo.Size = new System.Drawing.Size(387, 62);
            this.teltwo.TabIndex = 1;
            this.teltwo.Text = "亲情号2";
            this.teltwo.Enter += new System.EventHandler(this.teltwo_Enter);
            this.teltwo.Leave += new System.EventHandler(this.teltwo_Leave);
            // 
            // submitBtn
            // 
            this.submitBtn.BackgroundImage = global::yitiji_ma.Properties.Resources.btn_submit;
            this.submitBtn.Location = new System.Drawing.Point(417, 498);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(180, 60);
            this.submitBtn.TabIndex = 3;
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click);
            // 
            // backBtn
            // 
            this.backBtn.BackgroundImage = global::yitiji_ma.Properties.Resources.btn_return;
            this.backBtn.Location = new System.Drawing.Point(791, 498);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(180, 60);
            this.backBtn.TabIndex = 2;
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::yitiji_ma.Properties.Resources.main_guashi11;
            this.pictureBox1.Location = new System.Drawing.Point(456, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(465, 106);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Buka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.teltwo);
            this.Controls.Add(this.telone);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Buka";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox telone;
        private System.Windows.Forms.TextBox teltwo;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}