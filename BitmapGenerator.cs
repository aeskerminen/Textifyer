using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Textifyer.BitmapGeneration
{
    class BitmapGenerator 
    {
        public static Bitmap Generate8bitBitmap(Image image) 
        {
            // Get image from file
            // Sample image to grayscale with 8-bit precision
            // Assign a character for each value. (8 bits = 8 characters)

            Bitmap imageToGrayscale = new Bitmap(image);

            BitmapData imageData = imageToGrayscale.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, imageToGrayscale.PixelFormat);

            byte bitsPerPixel = (byte)System.Drawing.Bitmap.GetPixelFormatSize(imageData.PixelFormat);

            int size = imageData.Stride * imageData.Height;

            byte[] data = new byte[size];
            Byte R, G, B, A;

            System.Runtime.InteropServices.Marshal.Copy(imageData.Scan0, data, 0, size);

            for (int i = 0; i < size; i += 4) // bitsPerPixel / 8
            {
                R = data[i+0];
                G = data[i+1];
                B = data[i+2];
                A = data[i +3];

              //  var grey = ConvertColorToGreyscale(R,G,B,A);
                var grey = (R+G+B) / 3;

                R = (byte)grey;
                G = (byte)grey;
                B = (byte)grey;
                A = A;
            }

            System.Runtime.InteropServices.Marshal.Copy(data, 0, imageData.Scan0, data.Length);

            imageToGrayscale.UnlockBits(imageData);
            
            return imageToGrayscale;       
        }

        private static Color ConvertColorToGreyscale(byte R, byte G, byte B, byte A)  
        {
            double Rlinear = R;
            double Glinear = G;
            double Blinear = B;

            if(R < 0.04045) {
                Rlinear = R / 12.92;
            } else {
                Rlinear = Math.Pow((R+0.055) / 1.055, 2.4);
            }

            if(G < 0.04045) {
                Glinear = G / 12.92;
            } else {
                Glinear = Math.Pow((G+0.055) / 1.055, 2.4);
            }
                    
            if(B < 0.04045) {
                Blinear = B / 12.92;
            } else {
                Blinear = Math.Pow((B+0.055) / 1.055, 2.4);
            }

            var YLinear = (0.2126 * Rlinear + 0.7125 * Glinear + 0.0722 * Blinear);
            double YSRGB = 0;

            if(YLinear <= 0.0031308) {
                YSRGB = 12.92 * YLinear;
            } else {
                YSRGB = 1.055 * (Math.Pow(YLinear, (1/2.4))) - 0.055;
            }

            Color newColor = Color.FromArgb(A, (int)YSRGB, (int)YSRGB, (int)YSRGB);
            return newColor;
        }
    }

}


/*
            for (int x = 0; x < og.Width; x++)
            {
                for (int y = 0; y < og.Height; y++)
                {
                    Color currentPixelColor = og.GetPixel(x,y);
                    
                    var R = currentPixelColor.R;
                    var G = currentPixelColor.G;
                    var B = currentPixelColor.B;
                    
                    double Rlinear = currentPixelColor.R;
                    double Glinear = currentPixelColor.G;
                    double Blinear = currentPixelColor.B;

                    if(R < 0.04045) {
                        Rlinear = R / 12.92;
                    } else {
                        Rlinear = Math.Pow((R+0.055) / 1.055, 2.4);
                    }

                    if(G < 0.04045) {
                        Glinear = G / 12.92;
                    } else {
                        Glinear = Math.Pow((G+0.055) / 1.055, 2.4);
                    }
                    
                    if(B < 0.04045) {
                        Blinear = B / 12.92;
                    } else {
                        Blinear = Math.Pow((B+0.055) / 1.055, 2.4);
                    }

                    var YLinear = (0.2126 * Rlinear + 0.7125 * Glinear + 0.0722 * Blinear);

                    double YSRGB = 0;

                    if(YLinear <= 0.0031308) {
                        YSRGB = 12.92 * YLinear;
                    } else {
                        YSRGB = 1.055 * (Math.Pow(YLinear, (1/2.4))) - 0.055;
                    }

                    Color newColor = Color.FromArgb(currentPixelColor.A, (int)YSRGB, (int)YSRGB, (int)YSRGB);

                    imageToGrayscale.SetPixel(x,y, newColor);
                }
            }
            */