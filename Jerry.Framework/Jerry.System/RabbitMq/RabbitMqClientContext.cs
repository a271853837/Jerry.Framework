using System;
using RabbitMQ.Client;

namespace Jerry.System.RabbitMq
{
    public class RabbitMqClientContext
    {
        public IConnection SendConnection { get; set; }

        public IModel SendChannel { get; set; }


        public IConnection ReceiveConnection { get; set; }
        public IModel ReceiveChannel { get; set; }
    }
}
