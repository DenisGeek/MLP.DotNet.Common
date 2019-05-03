using API.V1.RPC;
using System;
using System.Collections.Generic;
using System.Text;
using Tools.Environment;

namespace MS.Watcher.DiscordBot.MQ
{
    public class MQTaskStructureChannels:IDisposable
    {
        private readonly string _user;
        private readonly string _pass;
        private readonly string _host;
        private readonly int _port;
        private readonly string _queueName;
        private readonly string _virtualHost;
        public MQTaskStructureChannels()
        {
            _user = EnvRabbitMQ.User;
            _pass = EnvRabbitMQ.Pass;
            _host = EnvRabbitMQ.Host;
            _port = EnvRabbitMQ.Port;
            _queueName = "TaskDiscordTree";
            _virtualHost = "/";
        }

        public void Dispose(){}

        public string Call(string message)
        {
            var response = "";
            using (var aReguest = new RpcClient(
                                        aHostName: _host,
                                        aVirtualHost: _virtualHost,
                                        aPort: _port,
                                        aQueueName: _queueName,
                                        aUser: _user,
                                        aPass: _pass))
            {
                response = aReguest.Call(message);
            }
            return response;
        }
    }
}
