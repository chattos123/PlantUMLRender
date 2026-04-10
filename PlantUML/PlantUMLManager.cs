using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using PlantUml.Net;

namespace PlantUML
{
    public class PlantUmlManager
    {
        public static List<string> ExtractClassNames(string input)
        {
            List<string> classes = new List<string>();
            // Match the word 'class' followed by the name
            string pattern = @"class\s+(\w+)";

            MatchCollection matches = Regex.Matches(input, pattern);
            foreach (Match match in matches)
            {
                classes.Add(match.Groups[1].Value);
            }
            return classes;
        }

        public void RenderPumlFileToImage(string inputFilePath, string outputImagePath)
        {
            try
            {
                // 1. Read the raw string from the .puml file
                if (!File.Exists(inputFilePath))
                {
                    Console.WriteLine($"Error: The PlantUML file was not found: {inputFilePath}");
                    return;
                }

                string umlContent = File.ReadAllText(inputFilePath);

                // 2. Validate content (PlantUML usually starts with @startuml)
                if (!umlContent.Contains("@startuml"))
                {
                    // Optional: Automatically wrap it if the file is missing tags
                    umlContent = "@startuml\n" + umlContent + "\n@enduml";
                }

                // 3. Initialize the Renderer
                var factory = new RendererFactory();
                var renderer = factory.CreateRenderer(new PlantUmlSettings());

                // 4. Render to PNG (Bitmap)
                byte[] imageBytes = renderer.Render(umlContent, OutputFormat.Png);

                // 5. Save the file
                File.WriteAllBytes(outputImagePath, imageBytes);

                Console.WriteLine($"Successfully rendered {inputFilePath} to {outputImagePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}