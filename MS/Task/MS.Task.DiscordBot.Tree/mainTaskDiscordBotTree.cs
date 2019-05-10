using System;

namespace MS.Task.DiscordBot.Tree
{
    using API.V1.RPC;
    using Newtonsoft.Json;
    using System.IO;
    using System.Threading.Tasks;
    using Tools.Environment;

    class Program
    {
        private static RpcServer _server4IncomingMessages;
        private static RpcClient _client4ConvertMsg2PlantUml;
        private static RpcClient _client4RenderPlantUml;

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();


        public async Task MainAsync()
        {
            var h = EnvRabbitMQ.Host;
            InitClient4RenderPlantUml();
            InitClient4ConvertMsg2PlantUml();
            InitServer4IncomingMessages();
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        static string ThisNamespace { get => System.Reflection.Assembly.GetExecutingAssembly().EntryPoint.DeclaringType.Namespace; }
        static int ifile = 0;
        private static string MessageHandler(string aMessage)
        {
            ifile++;

            Console.WriteLine($"{ThisNamespace}\n -=> [x] Recieved:  {aMessage.Length}");
            File.WriteAllText($"{ifile}.MS.Task.DiscordBot.Tree.0.In.txt", aMessage);

            var plantUML = _client4ConvertMsg2PlantUml.Call(aMessage);
            Console.WriteLine($"{ThisNamespace} =>_client4ConvertMsg2PlantUml =>" +
                $"\n >=< [x]  converted 2 plantUML:  {plantUML.Length}");
            File.WriteAllText($"{ifile}.{ThisNamespace}.1.plantUML.txt", plantUML);

            var renderedPlantUML = _client4RenderPlantUml.Call(plantUML);
            Console.WriteLine($"{ifile}.{ThisNamespace} =>_client4RenderPlantUml =>" +
                $"\n >=< [x] rendered 2 plantUML:  {renderedPlantUML.Length}");
            File.WriteAllText($"{ifile}.{ThisNamespace}.3.renderedPlantUML.txt", renderedPlantUML);
            var png = JsonConvert.DeserializeObject<byte[]>(renderedPlantUML);
            File.WriteAllBytes($"{ifile}.{ThisNamespace}.PlantUml.png", png);

            return renderedPlantUML;
        }

        private static void InitServer4IncomingMessages()
        {
            var h = EnvRabbitMQ.Host;
            _server4IncomingMessages = new RpcServer(
                                    aHostName: EnvRabbitMQ.Host,
                                    aVirtualHost: EnvRabbitMQTaskDiscordTree.VirtualHost,
                                    aPort: EnvRabbitMQ.Port,
                                    aQueueName: EnvRabbitMQTaskDiscordTree.QueueName,
                                    aUser: EnvRabbitMQ.User,
                                    aPass: EnvRabbitMQ.Pass
                                    );
            _server4IncomingMessages.HandlerReceivedJson = MessageHandler;
            Console.WriteLine("MS.Task.DiscordBot.Tree\n [x] Awaiting RPC requests");
        }
        private static void InitClient4ConvertMsg2PlantUml()
        {
            _client4ConvertMsg2PlantUml = new RpcClient(
                                    aHostName: EnvRabbitMQ.Host,
                                    aVirtualHost: EnvRqabbitMQStepConvertDiscordTree2PlantUml.VirtualHost,
                                    aPort: EnvRabbitMQ.Port,
                                    aQueueName: EnvRqabbitMQStepConvertDiscordTree2PlantUml.QueueName,
                                    aUser: EnvRabbitMQ.User,
                                    aPass: EnvRabbitMQ.Pass
                                    );
            Console.WriteLine("MS.Task.DiscordBot.Tree\n [x] Ready to reguest ConvertMessage2PlantUml");
        }
        private static void InitClient4RenderPlantUml()
        {
            _client4RenderPlantUml = new RpcClient(
                                    aHostName: EnvRabbitMQ.Host,
                                    aVirtualHost: EnvRqabbitMQStepRenderPlantUml.VirtualHost,
                                    aPort: EnvRabbitMQ.Port,
                                    aQueueName: EnvRqabbitMQStepRenderPlantUml.QueueName,
                                    aUser: EnvRabbitMQ.User,
                                    aPass: EnvRabbitMQ.Pass
                                    );
            Console.WriteLine("MS.Task.DiscordBot.Tree\n [x] Ready to reguest RenderPlantUml");
        }

    }
}
