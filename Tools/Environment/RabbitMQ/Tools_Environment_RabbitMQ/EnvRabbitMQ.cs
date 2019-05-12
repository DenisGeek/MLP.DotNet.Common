using System;

namespace Tools.Environment
{
    public class EnvRabbitMQ
    {
        public static readonly string User;
        public static readonly string Pass;
        public static readonly string Host;
        public static readonly int Port;
        static EnvRabbitMQ()
        {
            User = EnvironmentExt.GetEnvironmentVariable("MLP_RabbitMQ_User", "guest");
            Pass = EnvironmentExt.GetEnvironmentVariable("MLP_RabbitMQ_Pass", "guest");
            Host = EnvironmentExt.GetEnvironmentVariable("MLP_RabbitMQ_Host", "localhost");
            Port = Convert.ToInt32(EnvironmentExt.GetEnvironmentVariable("MLP_RabbitMQ_Port", "5672"));
        }
    }
}
