using System;
using RabbitMQ.Client;

namespace Jerry.System.RabbitMq
{
    public class RabbitMqClientFactory
    {
        public static IRabbitMqClient CreateRabbitMqClientInstance()
        {
            var rabbitMqClientContext = new RabbitMqClientContext();
            RabbitMqClient.Instance = new RabbitMqClient
            {
                Context = rabbitMqClientContext
            };
            return RabbitMqClient.Instance;
        }


    }
}
