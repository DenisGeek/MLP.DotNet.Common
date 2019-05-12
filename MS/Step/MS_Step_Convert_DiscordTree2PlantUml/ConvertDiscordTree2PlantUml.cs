using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MLP.Tools;

namespace MS.Step.Convert.DiscordTree2PlantUml
{
    public class ConvertDiscordTree2PlantUml : IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public string Do(string aMessage)
        {
            var res = "";
            res += header();
            res += body(aMessage);
            res += footer();

            return res;
        }

        public string header()
            => "@startuml\nsalt\n{\n  {T\n";
                    //=> "@startuml\nskinparam backgroundColor transparent\nsalt\n{\n  {T\n";
        //\nskinparam backgroundColor black
        public string footer()
            => "  }\n}\n@enduml";

        private enum dscTree {
            _unknown,
            Category,
            ChannelText,
            ChannelVoice }
        private dscTree getDscNodeKind(DiscordNode aNode)
        {
            switch (aNode.NodePKind)
            {
                case "Category":
                    return dscTree.Category;
                case "TextChannel":
                    return dscTree.ChannelText;
                case "VoiceChannel":
                    return dscTree.ChannelVoice;
                default:
                    return dscTree._unknown;
            }
        }
        private string AddNode(int aLevel, string aNodeName, string aNodeDescr, dscTree aNodeKind)
        {
            string GetNodeColor(dscTree inner_aNodeKind)
            {
                switch (inner_aNodeKind)
                {
                    case dscTree.Category:
                        return "<color:blue>";
                    case dscTree.ChannelText:
                        return "<color:green>";
                    case dscTree.ChannelVoice:
                        return "<color:grey>";
                    default:
                        return "<color:black>";
                }
            }
            var nodeColor = GetNodeColor(aNodeKind);

            var res = "    ";
            for (var i = 0; i < aLevel; i++)
                res += "+";
            
            aNodeDescr = string.IsNullOrEmpty(aNodeDescr) ? "" : $" | {aNodeDescr}";
            res += $" {nodeColor}  {aNodeName} { aNodeDescr }\n";

            return res;
        }

        class DiscordNode
        {
            public string NodePName;
            public string NodePKind;
            public string NodePDescr;
        }
        //private void dummy;

        private string body(string aMessage)
        {
            var tree = aMessage.Json2Node<DiscordNode>();
            string Childs(int inner_Level, Node<DiscordNode> inner_Node)
            {
                if (inner_Node.Data.NodePName.ToLower().StartsWith("only"))
                    return "";

                var inner_res = AddNode(inner_Level, inner_Node.Data.NodePName, inner_Node.Data.NodePDescr, getDscNodeKind(inner_Node.Data));
                foreach (var inner_childNode in inner_Node.Children)
                    inner_res += Childs(inner_Level + 1, inner_childNode);
                return inner_res;
            }
            var res = Childs(1, tree);

            return res;
        }
    }
}
