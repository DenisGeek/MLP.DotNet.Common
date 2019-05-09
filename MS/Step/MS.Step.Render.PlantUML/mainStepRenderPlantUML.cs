using API.V1.RPC;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Tools.Environment;

namespace MS.Step.Render.PlantUml
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
            Console.WriteLine($"MS.Step.Render.PlantUml\n => [x] Recieved:  {aMessage.Length}");
            File.WriteAllText($"{ifile}.MS.Step.Render.PlantUml.In.txt", aMessage);

            var res = new RenderPlantUML().Do(aMessage);

            var png = JsonConvert.DeserializeObject<byte[]>(res);
            File.WriteAllBytes("MS.Step.Render.PlantUml.png", png);

            Console.WriteLine($"MS.Step.Render.PlantUml\n <= [x] Reply with:  {res.Length}");
            File.WriteAllText($"{ifile}.MS.Step.Render.PlantUml.Out.txt", res);

            return res;
        }

        private static void InitServer4IncomingMessages()
        {
            _server4IncomingMessages = new RpcServer(
                                    aHostName: EnvRabbitMQ.Host,
                                    aVirtualHost: EnvRqabbitMQStepRenderPlantUml.VirtualHost,
                                    aPort: EnvRabbitMQ.Port,
                                    aQueueName: EnvRqabbitMQStepRenderPlantUml.QueueName,
                                    aUser: EnvRabbitMQ.User,
                                    aPass: EnvRabbitMQ.Pass
                                    );
            _server4IncomingMessages.HandlerReceivedJson = MessageHandler;
            Console.WriteLine("MS.Step.Render.PlantUml\n [x] Awaiting RPC requests");
        }
    }
}
