using PrzetwarzanieObrazow.ImageProcessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrzetwarzanieObrazow
{
    public partial class Form1 : Form
    {
        protected Bitmap OurImage = new Bitmap(@"j:\Desktop\kot1.jpg");
        public Form1()
        {
            InitializeComponent();

            pictureBox1.Image = OurImage;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = OurImage;
        }

        private void ConvertToGrayscale_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = PixelConverter.ConvertToGrayscale(OurImage);
        }

        private void AdjustBrightnessContrastGammaButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = ImageProcessor.AdjustBrightnessContrastGamma(OurImage, 50, 50, 0.5f);
        }

        private void HistogramButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Histogram.CreateHistogram(OurImage);
        }
    }
}
