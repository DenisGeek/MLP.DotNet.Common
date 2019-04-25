using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Tools
{
    public static class NodeToJson_Ext
    {
        public static string ToJson<T>(this Node<T> the)
          => JsonConvert.SerializeObject(the, Formatting.Indented);
    }
}
