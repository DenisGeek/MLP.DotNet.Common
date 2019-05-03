using System;

namespace Tools.Environment
{
    public static class EnvRabbitMQ
    {
        public static readonly string User;
        public static readonly string Pass;
        public static readonly string Host;
        public static readonly int Port;
        static EnvRabbitMQ()
        {
            User = EnvironmentExt.GetEnvironmentVariable("RabbitMQ_User", "guest");
            Pass = EnvironmentExt.GetEnvironmentVariable("RabbitMQ_Pass", "guest");
            Host = EnvironmentExt.GetEnvironmentVariable("RabbitMQ_Host", "localhost");
            Port = Convert.ToInt32(EnvironmentExt.GetEnvironmentVariable("RabbitMQ_Port", "5672"));
        }
    }
}
