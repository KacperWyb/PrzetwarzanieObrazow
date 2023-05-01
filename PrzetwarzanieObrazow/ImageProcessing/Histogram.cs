using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace PrzetwarzanieObrazow.ImageProcessing
{
    class Histogram
    {
        private static Bitmap ConvertToNonIndexedBitmap(Bitmap image)
        {
            // Clone the image with a supported pixel format
            Bitmap newImage = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, new Rectangle(0, 0, newImage.Width, newImage.Height));
            }
            return newImage;
        }

        public static Bitmap CreateHistogram(Bitmap image)
        {
            Bitmap nonIndexedImage = ConvertToNonIndexedBitmap(image);

            int[] histogram = new int[256];

            BitmapData bitmapData = nonIndexedImage.LockBits(
                new Rectangle(0, 0, nonIndexedImage.Width, nonIndexedImage.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            try
            {
                byte[] pixels = new byte[bitmapData.Stride * bitmapData.Height];
                System.Runtime.InteropServices.Marshal.Copy(
                    bitmapData.Scan0, pixels, 0, pixels.Length);

                for (int i = 0; i < pixels.Length; i += 4)
                {
                    byte gray = (byte)(0.299 * pixels[i + 2] + 0.587 * pixels[i + 1] + 0.114 * pixels[i]);
                    histogram[gray]++;
                }
            }
            finally
            {
                nonIndexedImage.UnlockBits(bitmapData);
            }

            int maxCount = histogram.Max();

            Bitmap histogramImage = new Bitmap(256, 200);

            using (Graphics graphics = Graphics.FromImage(histogramImage))
            {
                for (int i = 0; i < histogram.Length; i++)
                {
                    double height = (double)histogram[i] / maxCount * 200;
                    graphics.DrawLine(
                        Pens.Black,
                        i, 200,
                        i, 200 - (int)height);
                }
            }

            return histogramImage;
        }

    }
}