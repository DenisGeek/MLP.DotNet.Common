using API.V1.RPC;
using System;
using System.IO;
using System.Threading.Tasks;
using Tools.Environment;

namespace MS.Step.Convert.DiscordTree2PlantUml
{
    class Program
    {
        private static RpcServer _server4IncomingMessages;
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {

            InitServer4IncomingMessages();
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
        static int ifile = 0;
        private static string MessageHandler(string aMessage)
        {
            //ifile++;
            Console.WriteLine($"MS.Step.Convert.DiscordTree2PlantUml\n => [x] Recieved:  {aMessage}");
            File.WriteAllText($"{ifile}.MS.Step.Convert.DiscordTree2PlantUml.In.txt", aMessage);

            var res = new ConvertDiscordTree2PlantUml().Do(aMessage);

            Console.WriteLine($"MS.Step.Convert.DiscordTree2PlantUml\n <= [x] Reply with:  {res}");
            File.WriteAllText($"{ifile}.MS.Step.Convert.DiscordTree2PlantUml.Out.txt", res);

            return res;
        }

        private static void InitServer4IncomingMessages()
        {
            _server4IncomingMessages = new RpcServer(
                                    aHostName: EnvRabbitMQ.Host,
                                    aVirtualHost: EnvRqabbitMQStepConvertDiscordTree2PlantUml.VirtualHost,
                                    aPort: EnvRabbitMQ.Port,
                                    aQueueName: EnvRqabbitMQStepConvertDiscordTree2PlantUml.QueueName,
                                    aUser: EnvRabbitMQ.User,
                                    aPass: EnvRabbitMQ.Pass
                                    );
            _server4IncomingMessages.HandlerReceivedJson = MessageHandler;
            Console.WriteLine("MS.Step.Convert.DiscordTree2PlantUml\n [x] Awaiting RPC requests");
        }
    }
}
