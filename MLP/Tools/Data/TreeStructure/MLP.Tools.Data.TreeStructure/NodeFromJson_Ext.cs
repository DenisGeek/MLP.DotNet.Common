using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Tools
{
    public static class NodeFromJson_Ext
    {
        public static Node<T> Json2Node<T>(this string the)
            => (Node<T>)JsonConvert.DeserializeObject(the);
    }
}
