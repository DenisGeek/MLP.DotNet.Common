using System;
using API.V1.RPC;

namespace API.V1.RPC.Client.CS.Example
{
    class Program
    {
        private static string _rmqUser;
        private static string _rmqPass;
        private static string _rmqHostName;
        private static int _rmqPort;
        private static string _rmqQueueName = "RemoteTest";

        static void Main(string[] args)
        {
            GetConnectionParams();
            Console.WriteLine("Hello World!");
            var message = "1";
            using (var aReguest = new RpcClient(
                                    aHostName: _rmqHostName,
                                    //aVirtualHost: EnvRabbitMQTaskDiscordTree.VirtualHost,
                                    aPort: _rmqPort,
                                    aQueueName: _rmqQueueName,
                                    aUser: _rmqUser,
                                    aPass: _rmqPass
                ))
            {
                Console.WriteLine($" [x] Requesting {message})");
                var response = aReguest.Call(message);
                Console.WriteLine($" [.] Got {response}");
            }
        }
        private static void GetConnectionParams()
        {
            _rmqUser = Environment.GetEnvironmentVariable("RabbitMQ_User");
            _rmqPass = Environment.GetEnvironmentVariable("RabbitMQ_Pass");
            _rmqHostName = Environment.GetEnvironmentVariable("RabbitMQ_Host");
            _rmqPort = int.Parse(Environment.GetEnvironmentVariable("RabbitMQ_Port"));
        }
    }
}
