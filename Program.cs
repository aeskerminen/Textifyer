using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using Textifyer.BitmapGeneration;
using Textifyer.ArgumentHandling;
using Textifyer.ASCIIArtGeneration;

namespace Textifyer
{
    class Program
    {
        static void Main(string[] args)
        {
            try 
            {
                // Handle the URI input by the user.
                Image rawImage = ArgumentHandler.HandleURI(args[0]);

                // Resize image so that the program won't take 10 years to run (only if image is huge)
                bool success = double.TryParse(args[2], out var resizeAmount);
                if(!success) 
                {
                    Console.WriteLine("Argument 3 is not a valid double (i.e 0,5)");
                    Environment.Exit(0);
                }
                Image image = Resizer.Resize(rawImage, resizeAmount);

                // Set complex or simple mode
                bool.TryParse(args[3], out var res);
                ASCIIArtGenerator.complex = res;

                // Generate ASCII art from the selected picture
                var generatedBitmap = BitmapGenerator.Generate8bitBitmap(image);
                var result  = ASCIIArtGenerator.GenerateASCIIArt(generatedBitmap);

                // Save the file with the name chosen by the user
                Font font = new Font("Courier", 14);
                var fileType = args[4] == "image" ? ".png" : ".txt"; 
                var fileName = args[1] + fileType;
                switch(fileType) 
                {
                    case ".txt":
                        File.WriteAllText(fileName, result);
                        break;
                    case ".png":
                        Console.WriteLine("Converting to an image is not supported yet..");
                        Environment.Exit(0);
                        break;
                }

                // Open the text file as it can look odd in the default texteditor
                var processes = Process.GetProcessesByName("Chrome");
                var path = processes.FirstOrDefault()?.MainModule?.FileName;
                Process.Start(path, fileName);
            } 
            catch(Exception e) {
                Console.WriteLine("Cannot run program without arguments\n" + e.ToString());
            }
        }
    }
}
