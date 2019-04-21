using PlantUml.Net;
using System;
using System.IO;

namespace MS.Step.PlantUML.Render
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MS.Step.PlantUML.Render");

            var bytes = RenderTest("Bob -> Alice : Hello");
            File.WriteAllBytes("out.png", bytes);
        }

        static byte[] RenderTest(string aPlantUML)
        {
            var factory = new RendererFactory();

            var renderer = factory.CreateRenderer(new PlantUmlSettings());

            var bytes = renderer.Render(aPlantUML, OutputFormat.Png);

            return bytes;
        }
    }
}
