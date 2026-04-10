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
            string outPutMMDir = Path.Combine(outputDir, "output_mm");
            string outPutPUMLDir = Path.Combine(outputDir, "output_puml");

            if (!Directory.Exists(inputDir))
            {
                Console.WriteLine($"Input directory not found: {inputDir}");
                return;
            }

            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            if(!Directory.Exists(outPutMMDir))
            {
                Directory.CreateDirectory(outPutMMDir);
            }

             if(!Directory.Exists(outPutPUMLDir))
            {
                Directory.CreateDirectory(outPutPUMLDir);
            }

            PlantUmlManager manager = new PlantUmlManager();

            foreach (var inputFile in Directory.GetFiles(inputDir, "*.puml"))
            {
                string fileName = Path.GetFileNameWithoutExtension(inputFile);
                string outputFile = Path.Combine(outPutPUMLDir, fileName + ".png");
                manager.RenderPumlFileToImage(inputFile, outputFile);
                Console.WriteLine($"Rendered: {inputFile} -> {outputFile}");
            }

            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            MindMapToPngConverter converter = new MindMapToPngConverter();
            foreach (var inputFile in Directory.GetFiles(inputDir, "*.mm"))
            {
                string fileName = Path.GetFileNameWithoutExtension(inputFile);
                string outputFile = Path.Combine(outPutMMDir, fileName + ".png");
                converter.Convert(inputFile, outputFile, fontPath);
                Console.WriteLine($"Converted: {inputFile} -> {outputFile}");
            }
        }
    }
}
