using System;

namespace MS.Task.DiscordBot.Tree
{
    using API.V1.RPC;
    using System.Threading.Tasks;
    using Tools.Environment;

    class TaskDiscordBotTree
    {
        private static RpcServer _server;

        static void Main(string[] args) => new TaskDiscordBotTree().MainAsync().GetAwaiter().GetResult();


        public async Task MainAsync()
        {
            InitServer();
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private static void InitServer()
        {
            _server = new RpcServer(
                                    aHostName: EnvRabbitMQ.Host,
                                    aVirtualHost: EnvRabbitMQTaskDiscordTree.VirtualHost,
                                    aPort: EnvRabbitMQ.Port,
                                    aQueueName: EnvRabbitMQTaskDiscordTree.QueueName,
                                    aUser: EnvRabbitMQ.User,
                                    aPass: EnvRabbitMQ.Pass
                                    );
            _server.HandlerReceivedJson = MessageHandler;
            Console.WriteLine(" [x] Awaiting RPC requests");
        }

        private static string MessageHandler(string aMessage)
        {
            Console.WriteLine($"Resieved: \"{aMessage}\"");
            Console.WriteLine($"Reply with: \"{aMessage}\"");
            return aMessage;
        }
    }
}
