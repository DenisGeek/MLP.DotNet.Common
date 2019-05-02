using API.V1.RPC;
using Discord;
using Discord.WebSocket;
using MLP.Tools;
using System;
using System.Threading.Tasks;

namespace MS.E.DiscordBot
{
    class mainDiscordBot
    {
        static void Main(string[] args) => new mainDiscordBot().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        public async Task MainAsync()
        {
            var tree = new Node<string>();
            tree.Data = "root";
            tree.AddChild("branch_1");
            tree.Children[0].AddChild("branch_1.1");
            tree.AddChild("branch_2");

            _client = new DiscordSocketClient();

            _client.Log += Log;
            // Remember to keep token private or to read it from an 
            // external source! In this case, we are reading the token 
            // from an environment variable. If you do not know how to set-up
            // environment variables, you may find more information on the 
            // Internet or by using other methods such as reading from 
            // a configuration.
            await _client.LoginAsync(TokenType.Bot,
                Environment.GetEnvironmentVariable("DiscordToken"));
            await _client.StartAsync();
            _client.MessageReceived += MessageReceived;
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private async Task MessageReceived(SocketMessage message)
        {
            switch (message.Content)
            {
                case "!Who":
                    await message.Channel.SendMessageAsync("MLP.net.BotTest");
                    break;
                case "Hi":
                    await message.Channel.SendMessageAsync("Hi my dear friend :grinning:");
                    break;
                case "Kill yourself":
                    await message.Channel.SendMessageAsync("Have a nice day :no_mouth:");
                    Environment.Exit(1);
                    break;
                case "RabbitMQ":
                    var tree = new Node<string>();
                    tree.Data = "root";
                    tree.AddChild("branch_1");
                    tree.Children[0].AddChild("branch_1.1");
                    tree.AddChild("branch_2");

                    var jsonMessage = tree.ToJson();

                    using (var aReguest = new RpcClient())
                    {
                        var aResJson = aReguest.Call(jsonMessage);
                        // do something with aResJson
                        await message.Channel.SendMessageAsync(aResJson);
                    }
                    break;
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
