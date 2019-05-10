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

        public string Scan()
        {

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
            try
            {
                byte[] pngStart = JsonConvert.DeserializeObject<byte[]>("iVBORw0KGgoAAAANSUhEUgAAAJsAAAAYCAIAAABiJxitAAADZUlEQVR42u2YQUSsURTHhzFGRhJJRpJIWiSzSTLSIjKLZIxIiyRtklnMIpEksxpmmSTSqtUwq1nMIpKR0S4tWiWSJGnzjCRjvPczV/dd33wzffN6M3m981/k3vPdc8+953/Ouadx/RR8L7jEBcKoQBgVCKMCYVQgjAqjgt9ec7n+C0a558HBgSk5OTnRl6/0gusdLS0tk5OTNzc3Dq0Aj8fT1tY2Pz9/cXHRfEfbGnp8fJybm/P5fJxtfHw8k8l8B0ZHRkZMycTERG1G1eDt7W17ezsQCNTlzZeXl729vc7OTpPUL2QUFjc3NzlVqVQ6Pz+fmZn5DoxypaOjIzU9Pj6OxWJOGFUgtP/Am/v7++Fw2PIpm80ODQ15vd7e3t7Dw0O9eH19ncxubW3d2dlRktfX15WVFV8ZDJjqrVjT1dXldrtVzC0uLrIhAZRMJm0ZrXH+jY2N9vZ21CkqhUKh0sTAwIC5vr+//+rqyoliwxm9u7vDlWpKvl5fXzthtFgsxuPxenNUZyoMWT51dHQQTwyenp6i0agSbm1thUKh5+dn6IFaJVxbWyOZfpRBZDDVWy0sLGgnsp6vepkto8FgEHU8YJFTfjCBXa65vLysz2OaGB0dPTs7U/JcLsfUoWLDGeXv7OxsOp0mS6ampkwv13hHicG63tFqyaE/9fT0EMUPDw/mMr/ff3t7a9El0rWQASmot8KVpq6mimW2jBI9ZDkbqpTS1ru7u7Uu8UeRqDRBpaEGqDEDpg4Vm8EoIRYo4/T09ENGP/+GEaqVOXp5eUlg4QKqGbFVw1y1ym+R155awJF4fcbGxvRi7zsY61JpbgJhtIeFMtTAoWIzGAUU3uHhYYuwQYwSzpFIpNqGhBdJUztHzeQzc9SS3x/maLXKQaqRvh9eZGlpaXd3l9Kik9WhYjMYtRX+dUYre139Caco8mCUzsJ8R/GR+Y7qB5K04NEy31HTqFqmcohltidnQT6fp9G1tAWJRGJ6evr+/p4xjYUmzLIJ76jKBM5cl+JXMmriM4wqQBXPFQW20lYqlRocHCRL8JGuuqoPoqe19Lp0HKqyra6uQrbtXZDTm7AhRqv1uhjiHxhqI2UT1s0WCW7ouvkEYZytmrv6yjAlDhXlNyP5FVAgjAqEUYEwKhBGhVHBv4dfpj1L58pECMoAAAAASUVORK5CYII=");
                var fileNameStart = $"Start.PlantUml.png";
                File.WriteAllBytes(fileNameStart, pngStart);
                _botMessage = await _userMessage.Channel.SendFileAsync(fileNameStart, "0");
            }
            catch
            {
                _botMessage = await _userMessage.Channel.SendMessageAsync($"MLP.net.BotTest started ({ThisNamespace})");
            }

            //var i = 0;
            //while (true)
            //{
            //    var structure = Scan(i).GetAwaiter().GetResult();
            //    var res = await Task.Run(() => Process(structure));
            //    await _botMessage.ModifyAsync(x => x.Content = $"{i}");
            //    Console.WriteLine($"{i++}");
            //    await Task.Delay(ScanInterval);
            //}

            var i = 0;
            //while (true)
            {
                Console.WriteLine($" => {i++} Scan");
                var structure = Scan();
                var res = await Task.Run(() => Process(structure));


                //// not working
                //var png = JsonConvert.DeserializeObject<byte[]>(res);
                //var stream = new MemoryStream(png);
                //await _userMessage.Channel.SendFileAsync(stream, $"{i}.png");

                //working
                var png = JsonConvert.DeserializeObject<byte[]>(res);
                var fileName = $"{ThisNamespace}.PlantUml.png";
                File.WriteAllBytes(fileName, png);
                await _userMessage.Channel.SendFileAsync(fileName,$"{DateTime.UtcNow}");

                //var png = JsonConvert.DeserializeObject<byte[]>(res);
                //var fileName = $"{ThisNamespace}.PlantUml.png";
                //File.WriteAllBytes(fileName, png);
                //await _botMessage.ModifyAsync(x =>
                //{
                //    x.Content = $"{i} the Content of the Message";
                //    //x.Embed = new EmbedBuilder()
                //    //{
                //    //    ImageUrl = $"attachment://{fileName}"
                //    //}.Build();
                //});


                Console.WriteLine($" <= {i++} Done");
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
