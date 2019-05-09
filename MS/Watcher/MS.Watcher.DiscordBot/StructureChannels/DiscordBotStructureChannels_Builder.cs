using Discord.WebSocket;
using MLP.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MS.Watcher.DiscordBot.StructureChannels
{
    class DiscordBotStructureChannels_Builder
    {
        private readonly DiscordSocketClient _client;
        public DiscordBotStructureChannels_Builder(DiscordSocketClient aClient)
        {
            _client = aClient;
        }

        public Node<DiscordNode> BuildTree()
        {
            var cnList = GetChannelsList(_client);
            var cnTree = ConvertChannelsList2Tree(cnList);
            var res = ConvertChannelsTree2DiscordTree(cnTree);

            return res;
        }

        private IEnumerable<SocketGuildChannel> getAllChannelsIter(DiscordSocketClient client)
        {
            foreach (var guild in client.Guilds)
                foreach (var channel in guild.Channels)
                    yield return channel;
        }

        private IEnumerable<SocketGuildChannel> GetChannelsList(DiscordSocketClient client)
        {
            var res = new List<SocketGuildChannel>();
            foreach (var guild in client.Guilds)
                foreach (var channel in guild.Channels)
                    res.Add(channel);
            return res;
        }

        private Node<SocketGuildChannel> ConvertChannelsList2Tree(IEnumerable<SocketGuildChannel> aChannelsList)
        {
            var res = new Node<SocketGuildChannel>();
            //var root = res.AddChild(null); // to find server node

            // add categories
            aChannelsList.Where(gch =>
                gch.GetType() == typeof(SocketCategoryChannel))
                .ToList()
                .ForEach(gch => res.AddChild(gch));

            // foreach category in root add channels
            res.Children.ForEach(gc =>
                ((SocketCategoryChannel)gc.Data).Channels
                .ToList().ForEach(gch =>
                    gc.AddChild(gch)));

            // add channels withot categories
            aChannelsList.Where(gch =>
                gch.GetType() == typeof(SocketTextChannel)
                && ((SocketTextChannel)gch).Category == null)
                .ToList()
                .ForEach(gch => res.AddChild(gch));
            aChannelsList.Where(gch =>
                gch.GetType() == typeof(SocketVoiceChannel)
                && ((SocketVoiceChannel)gch).Category == null)
                .ToList()
                .ForEach(gch => res.AddChild(gch));
            // sort
            res.Children = res.Children.OrderBy(c => c.Data.Position).ToList();
            //foreach (var category in aChannelsList/*.Where(c => c.GetType() == typeof(SocketCategoryChannel))*/)
            //{
            //    Console.WriteLine($"{category.Name} \t\t {category.GetType()} \t\t {category.Guild.Name}");
            //    addedChannels.Add(category);
            //}
            return res;
        }

        

        private Node<DiscordNode> ConvertChannelsTree2DiscordTree(Node<SocketGuildChannel> aChannelsTree)
        {
            string getChannelType(Type inner_type)
            {
                switch (inner_type.Name)
                {
                    case "SocketCategoryChannel":
                        return "Category";
                    case "SocketTextChannel":
                        return "TextChannel";
                    case "SocketVoiceChannel":
                        return "VoiceChannel";
                    default:
                        return "root";
                }
            }
            Node<DiscordNode> Childs(Node<SocketGuildChannel> inner_ChannelNode)
            {
                var inner_res = new Node<DiscordNode>();
                // Data
                if (inner_ChannelNode.Data != null)
                {
                    inner_res.Data = new DiscordNode();
                    inner_res.Data.NodePName = inner_ChannelNode.Data?.Name;
                    inner_res.Data.NodePKind = getChannelType(inner_ChannelNode.Data.GetType());
                    if (inner_ChannelNode.Data?.GetType() == typeof(SocketTextChannel))
                        inner_res.Data.NodePDescr = ((SocketTextChannel)inner_ChannelNode.Data).Topic;
                }
                else inner_res.Data = null;

                // Childrens
                foreach (var ichn in inner_ChannelNode.Children.Where(c => c != null))
                    inner_res.Children.Add(Childs(ichn));

                return inner_res;
            }

            var res = Childs(aChannelsTree);
            // name 4 Root node
            res.Data = new DiscordNode()
            {
                NodePName = _client.Guilds.First().Channels.First().Guild.Name,
                NodePKind = "root",
                NodePDescr = "Discord Server"
            };

            return res;
        }
    } // class
}// namespace
