using API.V1.RPC;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using MLP.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tools.Environment;

namespace MS.Watcher.DiscordBot.StructureChannels
{
    public class DiscordBotStructureChannels
    {
        public int ScanInterval = 1000;
        private readonly DiscordSocketClient _client;
        private readonly SocketMessage _userMessage;
        private RestUserMessage _botMessage;

        public DiscordBotStructureChannels(
            DiscordSocketClient aClient
            , SocketMessage aMessage
            )
        {
            _client = aClient;
            _userMessage = aMessage;
        }

        public async Task Start()
        {
            _botMessage = await _userMessage.Channel.SendMessageAsync("MLP.net.BotTest started");

            var i = 0;
            while (true)
            {
                var structure = Scan(i).GetAwaiter().GetResult();
                var res = await Task.Run(() => Process(structure));
                await _botMessage.ModifyAsync(x => x.Content = $"{i}");
                Console.WriteLine($"{i++}");
                await Task.Delay(ScanInterval);
            }
        }

        public async Task<string> Scan(int i)
        {
            await Task.Delay(100);
            Console.WriteLine($"Scan {i++}");

            var tree = new Node<string>();
            tree.Data = "root";
            tree.AddChild("branch_1");
            tree.Children[0].AddChild("branch_1.1");
            tree.AddChild("branch_2");
            var jsonMessage = tree.ToJson();

            return jsonMessage;
        }

        public string Process(string message)
        {
            var response = "";
            using (var aReguest = new RpcClient(
                                        //aHostName: EnvRabbitMQ.Host,
                                        //aVirtualHost: EnvRabbitMQTaskDiscordTree.VirtualHost,
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
