using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoID
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private string SelectLoadFile(string initialDirectory)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter =
               "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            dialog.InitialDirectory = initialDirectory;
            dialog.Title = "Select an image file";
            return (dialog.ShowDialog() == DialogResult.OK)
               ? dialog.FileName : null;
        }

        private string SelectSaveFile(string initialDirectory)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter =
               "Png File(*.PNG)|*.PNG";
            dialog.InitialDirectory = initialDirectory;
            dialog.Title = "Select a file to save the image as";
            return (dialog.ShowDialog() == DialogResult.OK)
               ? dialog.FileName : null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fileNameBox.Text = SelectLoadFile("/");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image image;
            try
            {
                image = Image.FromFile(fileNameBox.Text);
            }
            catch (Exception)
            {
                return;
            }

            image = new Bitmap(image);
            Graphics graphics = Graphics.FromImage(image);

            // Draw image ids
            int count = 0;
            for (int i = 0; i < image.Size.Height / int.Parse(tileSizeBox.Text); i++)
            {
                for (int j = 0; j < image.Size.Width / int.Parse(tileSizeBox.Text); j++)
                {
                    graphics.DrawString("" + count, this.Font, Brushes.White, j * 32, i * 32);
                    count++;
                }
            }

            // display image to screen
            originalPictureBox.Width = image.Size.Width;
            originalPictureBox.Height = image.Size.Height;
            originalPictureBox.Image = image;
            this.Height = originalPictureBox.Height + optionsGroup.Height + 70;
            this.Width = originalPictureBox.Width + 50;

            // Save image to disk
            try
            {
                image.Save(saveLocationBox.Text, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveLocationBox.Text = SelectSaveFile("/");
        }
    }
}
