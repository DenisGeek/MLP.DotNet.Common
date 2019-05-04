using System;

namespace API.V1.RPC.Server.CS.Test
{
    class Program
    {
        private static RpcServer _server;
        private static string _rmqUser;
        private static string _rmqPass;

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
        }
        private static void InitServer()
        {
            _server = new RpcServer(/*QueueName: "TaskDiscordTree"*/, aUser: _rmqUser,aPass:_rmqPass);
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
