using System;

namespace API.V1.RPC.Server.CS.Test
{
    class Program
    {
        private static RpcServer _server;

        static void Main(string[] args)
        {
            InitServer();
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static void InitServer()
        {
            _server = new RpcServer();
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
