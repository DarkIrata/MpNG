namespace MpNG
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ConvertItemPicture = new System.Windows.Forms.PictureBox();
            this.ConvertButton = new System.Windows.Forms.PictureBox();
            this.IpLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ConvertItemPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConvertButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IpLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // ConvertItemPicture
            // 
            this.ConvertItemPicture.Image = global::MpNG.Properties.Resources.DropFile;
            this.ConvertItemPicture.Location = new System.Drawing.Point(237, 39);
            this.ConvertItemPicture.Name = "ConvertItemPicture";
            this.ConvertItemPicture.Size = new System.Drawing.Size(128, 128);
            this.ConvertItemPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ConvertItemPicture.TabIndex = 0;
            this.ConvertItemPicture.TabStop = false;
            // 
            // ConvertButton
            // 
            this.ConvertButton.Image = global::MpNG.Properties.Resources.Convert;
            this.ConvertButton.Location = new System.Drawing.Point(248, 202);
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.Size = new System.Drawing.Size(110, 40);
            this.ConvertButton.TabIndex = 2;
            this.ConvertButton.TabStop = false;
            this.ConvertButton.Click += new System.EventHandler(this.ConvertButton_Click);
            // 
            // IpLogo
            // 
            this.IpLogo.Image = global::MpNG.Properties.Resources.logo;
            this.IpLogo.Location = new System.Drawing.Point(11, 283);
            this.IpLogo.Name = "IpLogo";
            this.IpLogo.Size = new System.Drawing.Size(129, 60);
            this.IpLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IpLogo.TabIndex = 3;
            this.IpLogo.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MpNG.Properties.Resources.Background;
            this.ClientSize = new System.Drawing.Size(600, 350);
            this.Controls.Add(this.IpLogo);
            this.Controls.Add(this.ConvertButton);
            this.Controls.Add(this.ConvertItemPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MpNG";
            ((System.ComponentModel.ISupportInitialize)(this.ConvertItemPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConvertButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IpLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ConvertItemPicture;
        private System.Windows.Forms.PictureBox ConvertButton;
        private System.Windows.Forms.PictureBox IpLogo;
    }
}

