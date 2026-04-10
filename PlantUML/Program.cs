using System;
using PlantUml.Net;

namespace PlantUML
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var factory = new RendererFactory();
            var renderer = factory.CreateRenderer(new PlantUmlSettings());

            string plantUmlCode = @"
                                    @startuml
                                    class Car
                                    class Engine
                                    Car *-- Engine
                                    @enduml";

           // Render to bytes (PNG)
           // byte[] bytes = renderer.Render(plantUmlCode, OutputFormat.Png);
            //File.WriteAllBytes(@"C:\Users\soumy\diagram.png", bytes);

            PlantUmlManager manager = new PlantUmlManager();
            manager.RenderPumlFileToImage(@"C:\Users\soumy\TestArch\input\RequirementV.puml", @"C:\Users\soumy\TestArch\output\RequirementV.png");
            manager.RenderPumlFileToImage(@"C:\Users\soumy\TestArch\input\ConceptualV.puml", @"C:\Users\soumy\TestArch\output\ConceptualV.png");
            manager.RenderPumlFileToImage(@"C:\Users\soumy\TestArch\input\ProcessV.puml", @"C:\Users\soumy\TestArch\output\ProcessV.png"); 
            manager.RenderPumlFileToImage(@"C:\Users\soumy\TestArch\input\SecurityV.puml", @"C:\Users\soumy\TestArch\output\SecurityV.png"); 
            manager.RenderPumlFileToImage(@"C:\Users\soumy\TestArch\input\DeploymentV.puml", @"C:\Users\soumy\TestArch\output\DeploymentV.png"); 
        }
    }
}
