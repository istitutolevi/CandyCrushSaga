using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CandyCrushSaga.Utilities
{
    public static class ImageManager
    {
        public static byte[] ImageToBytes(Image image, ImageFormat format)
        {
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);
                // Convert Image to byte[]
                bytes = ms.ToArray();
            }
            return bytes;
        }

        public static string ImageToBase64(Image image, ImageFormat format)
        {
            string base64string = string.Empty;
            using (var ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                // Convert byte[] to Base64 string
                base64string = Convert.ToBase64String(ms.ToArray());
            }
            return base64string;
        }

        public static Image BytesToImage(byte[] imageBytes)
        {
            try
            {
                Image image;
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    // Convert byte[] to Image
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    image = Image.FromStream(ms, true);
                }
                return image;
            }
            catch
            {
                return null;
            }
        }

        public static Image Base64ToImage(string base64String)
        {
            try
            {
                // Convert Base64 string to byte[]
                var imageBytes = Convert.FromBase64String(base64String);
                Image image;
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    // Convert byte[] to Image
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    image = Image.FromStream(ms, true);
                }
                return image;
            }
            catch
            {
                return null;
            }
        }
    }
}
