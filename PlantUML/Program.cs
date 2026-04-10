using System;
using System.IO;
using PlantUml.Net;

namespace PlantUML
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello: Plant uml render!");

            string inputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input");
            string outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output");

            if (!Directory.Exists(inputDir))
            {
                Console.WriteLine($"Input directory not found: {inputDir}");
                return;
            }

            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            PlantUmlManager manager = new PlantUmlManager();

            foreach (var inputFile in Directory.GetFiles(inputDir, "*.puml"))
            {
                string fileName = Path.GetFileNameWithoutExtension(inputFile);
                string outputFile = Path.Combine(outputDir, fileName + ".png");
                manager.RenderPumlFileToImage(inputFile, outputFile);
                Console.WriteLine($"Rendered: {inputFile} -> {outputFile}");
            }
        }
    }
}
