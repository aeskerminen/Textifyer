using System.Drawing;

namespace Textifyer.BitmapGeneration 
{
    class Resizer 
    {
        public static Bitmap Resize(Image toResize, double scaleAmount) 
        {
            int sourceX = toResize.Width;
            int sourceY = toResize.Height;

            int newX = (int)(scaleAmount * sourceX);
            int newY = (int)(scaleAmount * sourceY);


            Bitmap resizedMap = new Bitmap(newX, newY);
            Graphics g = Graphics.FromImage(resizedMap);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(toResize, 0,0,newX,newY);
            g.Dispose();

            return resizedMap;
        }
    }
}