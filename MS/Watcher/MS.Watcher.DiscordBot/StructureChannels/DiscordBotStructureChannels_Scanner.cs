using API.V1.RPC;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using MLP.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tools.Environment;

namespace MS.Watcher.DiscordBot.StructureChannels
{
    public class DiscordBotStructureChannels_Scanner
    {
        public int ScanInterval = 1000;
        private readonly DiscordSocketClient _client;
        private readonly SocketMessage _userMessage;
        private RestUserMessage _botMessage;

        public DiscordBotStructureChannels_Scanner(
            DiscordSocketClient aClient
            , SocketMessage aMessage
            )
        {
            _client = aClient;
            _userMessage = aMessage;
        }

        public async Task<string> Scan(int i)
        {
            await Task.Delay(100);
            Console.WriteLine($"Scan {i++}");

            //var tree = new Node<DiscordNode>();
            //tree.Data = new DiscordNode() { NodePName = "MLP Discord", NodePKind = "root" };
            //tree.AddChild(new DiscordNode() { NodePName = "cat 1", NodePKind = "category" });
            //tree.Children[0].AddChild(new DiscordNode() { NodePName = "chan 1", NodePKind = "chanhel" });
            //tree.AddChild(new DiscordNode() { NodePName = "cat 2", NodePKind = "category" });
            //tree.AddChild(new DiscordNode() { NodePName = "chan in root", NodePKind = "chanhel" });
            //var jsonMessage = tree.ToJson();

            var channelsBuilder = new DiscordBotStructureChannels_Builder(_client);
            var tree = channelsBuilder.BuildTree();

            var jsonMessage = tree.ToJson();

            return jsonMessage;
        }

        static string ThisNamespace { get => System.Reflection.Assembly.GetExecutingAssembly().EntryPoint.DeclaringType.Namespace; }
        public async Task Start()
        {
            _botMessage = await _userMessage.Channel.SendMessageAsync($"MLP.net.BotTest started ({ThisNamespace})");

            var i = 0;
            while (true)
            {
                var structure = Scan(i).GetAwaiter().GetResult();
                var res = await Task.Run(() => Process(structure));


                //// not working
                //var png = JsonConvert.DeserializeObject<byte[]>(res);
                //var stream = new MemoryStream(png);
                //await _userMessage.Channel.SendFileAsync(stream, $"{i}.png");

                //working
                var png = JsonConvert.DeserializeObject<byte[]>(res);
                var fileName = $"{ThisNamespace}.PlantUml.png";
                File.WriteAllBytes(fileName, png);
                await _userMessage.Channel.SendFileAsync(fileName);

                Console.WriteLine($"{i++}");
                await Task.Delay(ScanInterval);
            }
        }


        public string Process(string message)
        {
            var h = EnvRabbitMQ.Host;
            var response = "";
            using (var aReguest = new RpcClient(
                                        aHostName: EnvRabbitMQ.Host,
                                        aVirtualHost: EnvRabbitMQTaskDiscordTree.VirtualHost,
                                        aPort: EnvRabbitMQ.Port,
                                        aQueueName: EnvRabbitMQTaskDiscordTree.QueueName,
                                        aUser: EnvRabbitMQ.User,
                                        aPass: EnvRabbitMQ.Pass
                                        ))
            {
                response = aReguest.Call(message);
            }
            return response;
        }
    }
}
