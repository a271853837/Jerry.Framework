using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Jerry.System.Log;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Jerry.System.RabbitMq
{
    public delegate void ActionEvent(BasicDeliverEventArgs result);

    public class RabbitMqClient : RabbitMqContext, IRabbitMqClient
    {
        private ILog log = LogManager.GetLogger(typeof(RabbitMqClient));
        
        private ActionEvent _actionMessage;

        public event ActionEvent ActionEventMessage
        {
            add
            {
                if (_actionMessage == null)
                    _actionMessage += value;
            }
            remove
            {
                if (_actionMessage != null)
                    _actionMessage -= value;
            }
        }


        public void PublishMessage(string message)
        {
            using (var model = _connection.CreateModel())
            {
                model.ExchangeDeclare(_exchangeName, _direct, true, false, null);
                model.QueueDeclare(_queueName, true, false, false, null);
                model.QueueBind(_queueName, _exchangeName, _routingKey);
                var properties = model.CreateBasicProperties();
                properties.Persistent = true;
                var body = Encoding.UTF8.GetBytes(message);
                model.BasicPublish(_exchangeName, _routingKey, properties, body);
            }
        }
        public void Receive()
        {
            var channel = _connection.CreateModel();
            {
                channel.ExchangeDeclare(_exchangeName, _direct, durable: true, autoDelete: false, arguments: null);
                channel.QueueDeclare(queue: _queueName,
                                      durable: true,
                                      exclusive: false,
                                      autoDelete: false,
                                      arguments: null);
                channel.QueueBind(_queueName, _exchangeName, _routingKey);
                Receive(channel);
            }

        }
        public void Receive(IModel channel)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                var handler = _actionMessage;
                if (handler != null)
                {
                    _actionMessage(ea);
                }

                channel.BasicAck(ea.DeliveryTag, false);
            };
            channel.BasicQos(0, 1, false);
            channel.BasicConsume(_queueName, false, consumer);
        }
    }
}
