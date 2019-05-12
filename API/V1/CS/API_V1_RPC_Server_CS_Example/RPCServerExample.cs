using System;

namespace API.V1.RPC.Server.CS.Test
{
    class Program
    {
        private static RpcServer _server;
        private static string _rmqUser;
        private static string _rmqPass;
        private static string _rmqHostName;
        private static int _rmqPort;
        private static string _rmqQueueName = "RemoteTest";

        static void Main(string[] args)
        {
            GetConnectionParams();
            InitServer();
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static void GetConnectionParams()
        {
            _rmqUser = Environment.GetEnvironmentVariable("RabbitMQ_User");
            _rmqPass = Environment.GetEnvironmentVariable("RabbitMQ_Pass");
            _rmqHostName = Environment.GetEnvironmentVariable("RabbitMQ_Host");
            _rmqPort = int.Parse(Environment.GetEnvironmentVariable("RabbitMQ_Port"));
        }
        private static void InitServer()
        {
            _server = new RpcServer(
                                        aHostName: _rmqHostName,
                                        //aVirtualHost: EnvRabbitMQTaskDiscordTree.VirtualHost,
                                        aPort: _rmqPort,
                                        aQueueName: _rmqQueueName,
                                        aUser: _rmqUser,
                                        aPass: _rmqPass
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
