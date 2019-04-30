using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Tools
{
    public static class Ext_NodeIOJson
    {
        /// <summary>
        /// !!!!!!!!!!!!!!!!!!!!
        /// https://stackoverflow.com/questions/3142495/deserialize-json-into-c-sharp-dynamic-object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="the"></param>
        /// <returns></returns>
        public static Node<T> Json2NodeTEST<T>(this string the)
        {
            var o = JsonConvert.DeserializeObject(the);
            var node = (Node<T>)o;
            return node;
        }
        /// <summary>
        /// !!!!!!!!!!!!!!!!!!!!!
        /// https://stackoverflow.com/questions/3142495/deserialize-json-into-c-sharp-dynamic-object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="the"></param>
        /// <returns></returns>
        public static Node<T> Json2Node<T>(this string the)
            => (Node<T>)JsonConvert.DeserializeObject(the);

        public static string ToJson<T>(this Node<T> the)
            => JsonConvert.SerializeObject(the, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
    }
}
