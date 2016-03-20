using System;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MpNG
{
    public static class Converter
    {
        public static bool Mp3ToPng(string filePath, out string convertError)
        {
            convertError = string.Empty;

            if (!File.Exists(filePath))
            {
                convertError = $"\"{filePath}\" file not found.";
                return false;
            }

            byte[] pixel = null;

            try
            {
                using (var br = new BinaryReader(File.Open(filePath, FileMode.Open)))
                {
                    int binaryLength = (int)br.BaseStream.Length;
                    pixel = new byte[binaryLength];

                    pixel = br.ReadBytes(binaryLength);
                }
            }
            catch (IOException)
            {
                convertError = $"Couldn't access file \"{filePath}\".";
                return false;

            }

            if (pixel == null)
            {
                convertError = $"Could not read byte information of file \"{filePath}\".";
                return false;
            }

            Size imageSize = ImageHelper.ImageSize(pixel.Length);
            int stride = ((imageSize.Width * 8) + 7) / 8;
            BitmapSource resultSource = BitmapSource.Create(
                                            imageSize.Width, imageSize.Height,
                                            96, 96,
                                            PixelFormats.Gray8,
                                            null,
                                            pixel,
                                            stride
                                        );

            // Here could be a possible fix, by spliting the image
            // or do coding to reduce the size at the binary part. 
            if (resultSource.Height > 15000)
            {
                convertError = $"Result Size is to high for the encoder. File can't be converted.";
                return false;
            }

            string outputPath = Path.GetDirectoryName(filePath) + "\\" + Path.GetFileNameWithoutExtension(filePath) + ".png";

            try
            {
                using (var fs = new FileStream(outputPath, FileMode.Create))
                {
                    BitmapEncoder bitmapEncoder = new PngBitmapEncoder();
                    bitmapEncoder.Frames.Add(BitmapFrame.Create(resultSource));
                    bitmapEncoder.Save(fs);
                }
            }
            catch (ArgumentException)
            {
                // I only catch the exception here, since i didn't looked up the maximum size for the encoder.
                // Thats why we only catch ArgumentException.
                convertError = $"Result Size is to high for the encoder. File can't be converted.";
                return false;
            }

            return true;
        }

        public static bool PngToMp3(string filePath, out string convertError)
        {
            convertError = string.Empty;

            if (!File.Exists(filePath))
            {
                convertError = $"\"{filePath}\" file not found.";
                return false;
            }

            Bitmap img;
            using (Image image = Image.FromFile(filePath))
            {
                img = new Bitmap(image);
            }
            BitmapSource imageSource = ImageHelper.GetBitmapSource(img);

            int byteLength = (int)(imageSource.Width * imageSource.Height);
            int stride = ImageHelper.GetBitmapDataStride(img);

            byte[] pixel = new byte[stride * (int)imageSource.Height];
            byte[] fixedPixel = new byte[byteLength];
            imageSource.CopyPixels(pixel, stride, 0);

            fixedPixel = FixByteArray(pixel, byteLength);

            string outputPath = Path.GetDirectoryName(filePath) + "\\" + Path.GetFileNameWithoutExtension(filePath) + ".mp3";
            File.WriteAllBytes(outputPath, fixedPixel);

            return true;
        }

        private static byte[] FixByteArray(byte[] pixel, int byteLength)
        {
            byte[] fixedPixel = new byte[byteLength];

            int iPixel = 0;
            for (int i = 0; i < byteLength; i++)
            {
                fixedPixel[i] = pixel[iPixel];
                iPixel += 4;
            }

            return fixedPixel;
        }
    }
}
