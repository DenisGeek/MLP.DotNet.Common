using System;
using API.V1.RPC;

namespace API.V1.RPC.Client.CS.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var message = "1";
            using (var aReguest = new RpcClient())
            {
                Console.WriteLine($" [x] Requesting {message})");
                var response = aReguest.Call(message);
                Console.WriteLine($" [.] Got {response}");
            }
        }
    }
}
