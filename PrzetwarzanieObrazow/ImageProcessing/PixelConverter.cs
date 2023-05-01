using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzetwarzanieObrazow.ImageProcessing
{
    public class PixelConverter
    {
        public static Bitmap ConvertToGrayscale(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            Bitmap grayscaleBitmap = new Bitmap(width, height);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Color color = bitmap.GetPixel(j, i);

                    double grayscale = (color.R * 0.2126) + (color.G * 0.7152) + (color.B * 0.0722);
                    grayscaleBitmap.SetPixel(j, i, Color.FromArgb((int)grayscale, (int)grayscale, (int)grayscale));
                }
            }

            return grayscaleBitmap;
        }
    }
}
