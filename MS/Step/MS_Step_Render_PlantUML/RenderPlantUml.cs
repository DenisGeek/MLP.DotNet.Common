using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PlantUml.Net;
using Newtonsoft.Json;

namespace MS.Step.Render.PlantUml
{
    public class RenderPlantUML : IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public string Do(string message)
        {
            var res = Render(message);
            var json = JsonConvert.SerializeObject(res, Formatting.Indented);
            return json;
        }

        private byte[] Render(string aPlantUML)
        {
            var factory = new RendererFactory();

            var renderer = factory.CreateRenderer(new PlantUmlSettings());

            var bytes = renderer.Render(aPlantUML, OutputFormat.Png);

            return bytes;
        }
    }
}
