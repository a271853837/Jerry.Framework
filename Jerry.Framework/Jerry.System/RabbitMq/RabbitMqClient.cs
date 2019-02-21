using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Jerry.System.Log;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Jerry.System.RabbitMq
{
    public delegate void ActionEvent(RabbitMqMessage message);

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


        public void PublishMessage(RabbitMqMessage message)
        {
            if (message == null)
            {
                throw new Exception("消息为空");
            }
            using (var model = _connection.CreateModel())
            {
                model.ExchangeDeclare(_exchangeName, _direct, true, false, null);
                model.QueueDeclare(_queueName, true, false, false, null);
                model.QueueBind(_queueName, _exchangeName, _routingKey);
                var properties = model.CreateBasicProperties();
                properties.Persistent = true;
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
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
                try
                {
                    var obj = JsonConvert.DeserializeObject<RabbitMqMessage>(Encoding.UTF8.GetString(ea.Body));
                    if (_actionMessage != null)
                    {
                        _actionMessage(obj);
                    }
                    if (obj.IsOperationOk)
                    {
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    else
                    {
                        //未能消费此消息，重新放入队列头
                        channel.BasicReject(ea.DeliveryTag, true);
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    throw ex;
                }
            };
            channel.BasicQos(0, 1, false);
            channel.BasicConsume(_queueName, false, consumer);


        }
    }
}
