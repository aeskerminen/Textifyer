using System;
using System.IO;
using System.Net;
using System.Drawing;

namespace Textifyer.ImageLoading
{
    class ImageLoader
    {
        public static Bitmap LoadImage(Uri uri) 
        {
            try 
            {
                Bitmap fetchedImage;
                using(var wb = new WebClient()) 
                {
                    Stream data = wb.OpenRead(uri);
                    fetchedImage = new Bitmap(data);

                    data.Flush();
                    data.Close();

                    wb.Dispose();
                }
                return fetchedImage;
            } 
            catch(WebException e)
            {
                Console.WriteLine("Error in downloading image from specified uri\n" + e.ToString());
                Environment.Exit(0);
            } 
            
            return null;
        }

        public static Bitmap LoadImage(string uri) 
        {
            try 
            {
                return (Bitmap)Image.FromFile(uri);
            } 
            catch(FileNotFoundException e) 
            {
                Console.WriteLine("The file from the specified path could not be found\n" + e.ToString());
                Environment.Exit(0);
            }

            return null;
        } 
    }
}