using API.V1.RPC.Server;
using System;
using MLP.Tools;

namespace MS.Task.Manager.RPCServerExample
{
    class TaskRPCServerExample
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
            var tree = aMessage.Json2Node<string>();
            tree.AddChild("Branch From MS.Task.Manager.RPCServerExample");
            Console.WriteLine(" [x] Branch has been added");

            return tree.ToJson();
        }
    }
}
