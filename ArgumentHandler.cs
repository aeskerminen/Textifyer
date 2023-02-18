using System;
using System.Drawing;
using Textifyer.ImageLoading;

namespace Textifyer.ArgumentHandling 
{
    class ArgumentHandler 
    {
        public static Image HandleURI(string uri) 
        {
            if(Uri.TryCreate(uri, UriKind.Absolute, out var x)) 
            {
                return ImageLoader.LoadImage(x);
            } else 
            {
                return ImageLoader.LoadImage(uri);
            }
            
        }
    }
}