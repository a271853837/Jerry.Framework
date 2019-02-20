using System;
using RabbitMQ.Client;

namespace Jerry.System.RabbitMq
{
    public class RabbitMqClientFactory
    {
        public static IRabbitMqClient CreateRabbitMqClientInstance()
        {
            return new RabbitMqClient();
        }


    }
}
