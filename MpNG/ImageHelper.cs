using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MpNG
{
    public static class ImageHelper
    {
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);

            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphics.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            }

            return resizedImage;
        }

        public static Image CreateResultImage(TargetTypes targetType, bool result)
        {
            Image baseImage = Properties.Resources.PngIcon;
            Image resultImage = Properties.Resources.Error;

            if (targetType == TargetTypes.Mp3)
            {
                baseImage = Properties.Resources.Mp3Icon;
            }

            if (result)
            {
                resultImage = Properties.Resources.Success;
            }

            resultImage = ResizeImage(resultImage, 60, 74);

            Bitmap convertImage = new Bitmap(baseImage.Width, baseImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(convertImage))
            {
                graphics.DrawImage(baseImage,
                    new Rectangle(new Point(), baseImage.Size),
                    new Rectangle(new Point(), baseImage.Size),
                    GraphicsUnit.Pixel);

                graphics.DrawImage(resultImage,
                    new Rectangle(new Point(baseImage.Width - resultImage.Width, baseImage.Height - resultImage.Height), resultImage.Size),
                    new Rectangle(new Point(), resultImage.Size),
                    GraphicsUnit.Pixel);
            }

            return convertImage;
        }

        public static Size ImageSize(int pixel)
        {
            List<int> factors = GetFactors(pixel);

            int width = factors[factors.Count - 2];
            int height = factors[factors.Count - 1];

            return new Size(width, height);
        }

        public static List<int> GetFactors(int number)
        {
            List<int> factors = new List<int>();
            int square = (int)Math.Ceiling(Math.Sqrt(number));

            for (int i = 1; i < square; i++)
            {
                if (number % i == 0)
                {
                    factors.Add(i);

                    if (i != square)
                    {
                        factors.Add(number / i);
                    }
                }
            }

            if ((square * square) == number)
            {
                // Add twice, since we have a square
                factors.Add(square);
                factors.Add(square);
            }
            return factors;
        }

        public static int GetBitmapDataStride(Bitmap bitmap)
        {
            int stride = 0;
            var bitmapData = bitmap.LockBits(
                                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                ImageLockMode.ReadOnly,
                                bitmap.PixelFormat
                            );

            stride = bitmapData.Stride;

            bitmap.UnlockBits(bitmapData);

            return stride;
        }

        public static BitmapSource GetBitmapSource(Bitmap bitmap)
        {
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap
            (
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions()
            );

            return bitmapSource;
        }
    }
}
