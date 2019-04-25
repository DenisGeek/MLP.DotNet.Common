using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Tools
{
    public static class NodeToJson_Ext
    {
        public static string ToJson(this Node the)
          => JsonConvert.SerializeObject(the, Formatting.Indented);
    }
}
