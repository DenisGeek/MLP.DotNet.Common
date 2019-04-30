using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Tools
{
    public static class Ext_NodeIOJson
    {
        public static Node<T> Json2Node<T>(this string the)
            => (Node<T>)JsonConvert.DeserializeObject(the);

        public static string ToJson<T>(this Node<T> the)
            => JsonConvert.SerializeObject(the, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
    }
}
