using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Tools
{
    public static class NodeFromJson_Ext
    {
        public static Node Json2Node(this string the)
            => (Node)JsonConvert.DeserializeObject(the);
    }
}
