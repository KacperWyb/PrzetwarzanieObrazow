using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace PrzetwarzanieObrazow.ImageProcessing
{
    class Histogram
    {
        public static Bitmap EqualizeHistogram(Bitmap image)
        {
            // Oblicz wartości histogramu obrazu
            int[] histogram = new int[256];
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color color = image.GetPixel(i, j);
                    int gray = (int)(color.R * 0.299 + color.G * 0.587 + color.B * 0.114);
                    histogram[gray]++;
                }
            }

            // Oblicz funkcję dystrybuanty (CDF)
            int[] cdf = new int[256];
            cdf[0] = histogram[0];
            for (int i = 1; i < 256; i++)
            {
                cdf[i] = cdf[i - 1] + histogram[i];
            }

            // Przeskaluj wartości pikseli w obrazie
            int min = cdf.Min();
            int max = cdf.Max();
            int[] newValues = new int[256];
            for (int i = 0; i < 256; i++)
            {
                newValues[i] = (int)(((double)(cdf[i] - min) / (image.Width * image.Height - min)) * 255);
            }

            Bitmap result = new Bitmap(image.Width, image.Height);

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color color = image.GetPixel(i, j);
                    int gray = (int)(color.R * 0.299 + color.G * 0.587 + color.B * 0.114);
                    int newValue = newValues[gray];
                    result.SetPixel(i, j, Color.FromArgb(newValue, newValue, newValue));
                }
            }

            return result;
        }

        public static Bitmap StretchHistogram(Bitmap image)
        {
            int[] histogram = new int[256];
            double[] cdf = new double[256];

            BitmapData bitmapData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
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
                image.UnlockBits(bitmapData);
            }

            cdf[0] = histogram[0];
            for (int i = 1; i < 256; i++)
            {
                cdf[i] = cdf[i - 1] + histogram[i];
            }

            double min = cdf.FirstOrDefault(x => x > 0);
            double max = cdf[255];
            double factor = 255.0 / (max - min);

            Bitmap stretchedImage = new Bitmap(image.Width, image.Height);

            bitmapData = stretchedImage.LockBits(
                new Rectangle(0, 0, stretchedImage.Width, stretchedImage.Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            try
            {
                byte[] pixels = new byte[bitmapData.Stride * bitmapData.Height];

                for (int i = 0; i < pixels.Length; i += 4)
                {
                    byte gray = (byte)(0.299 * image.GetPixel(i / 4 % image.Width, i / 4 / image.Width).R
                        + 0.587 * image.GetPixel(i / 4 % image.Width, i / 4 / image.Width).G
                        + 0.114 * image.GetPixel(i / 4 % image.Width, i / 4 / image.Width).B);

                    double newValue = (cdf[gray] - min) * factor;
                    byte newGray = (byte)Math.Round(newValue);

                    pixels[i + 3] = 255;
                    pixels[i + 2] = newGray;
                    pixels[i + 1] = newGray;
                    pixels[i] = newGray;
                }

                System.Runtime.InteropServices.Marshal.Copy(
                    pixels, 0, bitmapData.Scan0, pixels.Length);
            }
            finally
            {
                stretchedImage.UnlockBits(bitmapData);
            }

            return stretchedImage;
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
    }
}