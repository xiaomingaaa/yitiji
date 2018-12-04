namespace yitiji_ma
{
    partial class Guashi
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
            this.SuspendLayout();
            // 
            // telone
            // 
            this.telone.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.telone.Location = new System.Drawing.Point(450, 308);
            this.telone.Name = "telone";
            this.telone.Size = new System.Drawing.Size(387, 41);
            this.telone.TabIndex = 0;
            // 
            // teltwo
            // 
            this.teltwo.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.teltwo.Location = new System.Drawing.Point(450, 397);
            this.teltwo.Name = "teltwo";
            this.teltwo.Size = new System.Drawing.Size(387, 41);
            this.teltwo.TabIndex = 1;
            // 
            // submitBtn
            // 
            this.submitBtn.BackgroundImage = global::yitiji_ma.Properties.Resources.btn_submit;
            this.submitBtn.Location = new System.Drawing.Point(355, 579);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(180, 60);
            this.submitBtn.TabIndex = 2;
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click);
            // 
            // backBtn
            // 
            this.backBtn.BackgroundImage = global::yitiji_ma.Properties.Resources.btn_return;
            this.backBtn.Location = new System.Drawing.Point(738, 579);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(180, 60);
            this.backBtn.TabIndex = 3;
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // Guashi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::yitiji_ma.Properties.Resources.main_guashi;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1424, 862);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.teltwo);
            this.Controls.Add(this.telone);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Guashi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Guashi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Guashi_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox telone;
        private System.Windows.Forms.TextBox teltwo;
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.Button backBtn;
    }
}