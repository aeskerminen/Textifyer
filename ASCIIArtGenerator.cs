using System.Text;
using System.Drawing;

namespace Textifyer.ASCIIArtGeneration 
{
    class ASCIIArtGenerator 
    {
        static string[] asciiChars = { "@", "%", "#", "#", "*", "=", "+", "-", ":", ".", " " };
        static string[] asciiCharsComplex = { "$","@","B","%","8","&","W","M","#","*","o","a","h","k","b","d","p","q","w","m",
            "Z","O","0","Q","L","C","J","U","Y","X","z","c","v","u","n","x","r","j","f","t","/","\\","|","(",")","1","{","}","[","]","?","-","_","+","~","<",">","i","!","l","I",";",":",",",@"""","^","`","'","."," " };
        
        public static bool complex = true;

        public static string GenerateASCIIArt(Bitmap map) 
        {
            StringBuilder output = new StringBuilder();
            bool charToggle = false;

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if(!charToggle) {
                        string c = GetCharForBit(map.GetPixel(x,y).R);
                        output.Append(c);
                        
                    }
                }
                if(!charToggle) {
                    output.Append("\n");
                    charToggle = true;
                } else {
                    charToggle = false;
                }
                
            }
            
            return output.ToString();
        }

        private static string GetCharForBit(byte value) 
        {
            int colorIndex = complex ? (value * 69) / 255 : (value * 10) / 255;

            return complex ? asciiCharsComplex[colorIndex] : asciiChars[colorIndex];
        }
    }
}



            /*
            // .:-=+*#%@
            switch(value) 
            {
                case > 128:
                    return '@';
                case > 64:
                    return '%';
                case > 32:
                    return '#';
                case > 16:
                    return '*';
                case > 8:
                    return '+';
                case > 4:
                    return '=';
                case > 2:
                    return '-';
                case 1:
                    return ':';
                default:
                    return '.';
            }
            */