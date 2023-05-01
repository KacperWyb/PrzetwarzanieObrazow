using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace PrzetwarzanieObrazow.ImageProcessing
{
    public class ImageProcessor
    {
        public static Bitmap AdjustBrightnessContrastGamma(Bitmap image, float brightness, float contrast, float gamma)
        {
            byte[] brightnessLUT = new byte[256];
            byte[] contrastLUT = new byte[256];
            byte[] gammaLUT = new byte[256];

            for (int i = 0; i < 256; i++)
            {
                float b = (brightness / 100.0f) * 255;
                brightnessLUT[i] = (byte)Math.Max(0, Math.Min(255, i + b));

                float c = (contrast / 100.0f) * 255;
                contrastLUT[i] = (byte)Math.Max(0, Math.Min(255, (c * (i - 128) / 255.0f) + 128));
            }
            gammaLUT = GetGammaLut(gamma);

            Bitmap result = new Bitmap(image.Width, image.Height);

            BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData resultData = result.LockBits(new Rectangle(0, 0, result.Width, result.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* imagePtr = (byte*)imageData.Scan0.ToPointer();
                byte* resultPtr = (byte*)resultData.Scan0.ToPointer();

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        int index = y * imageData.Stride + x * 3;

                        byte blue = imagePtr[index];
                        byte green = imagePtr[index + 1];
                        byte red = imagePtr[index + 2];

                        blue = brightnessLUT[blue];
                        green = brightnessLUT[green];
                        red = brightnessLUT[red];

                        blue = contrastLUT[blue];
                        green = contrastLUT[green];
                        red = contrastLUT[red];

                        blue = gammaLUT[blue];
                        green = gammaLUT[green];
                        red = gammaLUT[red];

                        resultPtr[index] = blue;
                        resultPtr[index + 1] = green;
                        resultPtr[index + 2] = red;
                    }
                }
            }

            image.UnlockBits(imageData);
            result.UnlockBits(resultData);

            return result;
        }
        private static byte[] GetGammaLut(float gamma)
        {
            byte[] gammaLUT = new byte[256];
            float invGamma = 1.0f / gamma;

            for (int i = 0; i < 256; i++)
            {
                float value = (float)i / 255.0f;
                value = (float)Math.Pow(value, invGamma) * 255.0f;
                gammaLUT[i] = (byte)value;
            }

            return gammaLUT;
        }
    }
}
