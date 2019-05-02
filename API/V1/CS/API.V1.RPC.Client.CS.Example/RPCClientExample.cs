using System;
using API.V1.RPC;

namespace API.V1.RPC.Client.CS.Example
{
    class Program
    {
        private static string _rmqUser;
        private static string _rmqPass;

        static void Main(string[] args)
        {
            GetConnectionParams();
            Console.WriteLine("Hello World!");
            var message = "1";
            using (var aReguest = new RpcClient(aUser: _rmqUser, aPass: _rmqPass))
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
        }
    }
}
