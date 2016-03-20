using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MpNG
{
    // Author: Wladislaw B. 
    // Open Source Project - ipmix.de
    // Have Fun. :)
    public partial class MainForm : Form
    {
        private TargetTypes targetType = TargetTypes.None;
        private string filePath = string.Empty;

        public MainForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackgroundImageLayout = ImageLayout.None;
            this.ConvertItemPicture.BackColor = Color.Transparent;
            this.ConvertButton.BackColor = Color.Transparent;
            this.IpLogo.BackColor = Color.Transparent;

            this.AllowDrop = true;
            this.ConvertItemPicture.AllowDrop = true;
            this.ConvertItemPicture.DragEnter += this.ItemDragEnter;
            this.ConvertItemPicture.DragDrop += this.ItemDragDrop;
            
            this.ConvertButton.MouseEnter += this.SetConvertButtonMouseOver;
            this.ConvertButton.MouseLeave += this.SetConvertButtonDefault;
            this.ConvertButton.MouseDown += this.SetConvertButtonPressed;
            this.ConvertButton.MouseUp += this.SetConvertButtonMouseOver;
            this.IpLogo.Click += this.IpLogo_Click;
        }

        private void IpLogo_Click(object sender, EventArgs e)
        {
            Process.Start("http://ipmix.de/");
        }

        private void ItemDragDrop(object sender, DragEventArgs e)
        {
            filePath = ((string[])e.Data.GetData(DataFormats.FileDrop)).FirstOrDefault();
            string fileExtension = Path.GetExtension(filePath).ToLower();

            switch (fileExtension)
            {
                case ".mp3":
                    this.ConvertItemPicture.Image = Properties.Resources.Mp3Icon;
                    this.targetType = TargetTypes.Png;
                    break;
                case ".png":
                    this.ConvertItemPicture.Image = Properties.Resources.PngIcon;
                    this.targetType = TargetTypes.Mp3;
                    break;
                default:
                    this.ConvertItemPicture.Image = Properties.Resources.DropFile;
                    this.targetType = TargetTypes.None;
                    break;
            }
            
        }

        private void ItemDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        
        private void SetConvertButtonPressed(object sender, MouseEventArgs e)
        {
            this.ConvertButton.Image = Properties.Resources.Convert_Press;
        }

        private void SetConvertButtonMouseOver(object sender, EventArgs e)
        {
            this.ConvertButton.Image = Properties.Resources.Convert_Hover;
        }

        private void SetConvertButtonDefault(object sender, EventArgs e)
        {
            this.ConvertButton.Image = Properties.Resources.Convert;
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            bool result = false;
            string resultError = string.Empty;

            switch (this.targetType)
            {
                case TargetTypes.Png:
                    result = Converter.Mp3ToPng(filePath, out resultError);
                    break;
                case TargetTypes.Mp3:
                    result = Converter.PngToMp3(filePath, out resultError);
                    break;
                case TargetTypes.None:
                    return;
            }

            this.ConvertItemPicture.Image = ImageHelper.CreateResultImage(this.targetType, result);

            if (!result)
            {
                MessageBox.Show(resultError, "MpNG - Convert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
