using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jerry.System.Log;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Jerry.System.RabbitMq
{
    public delegate void ActionEvent(BasicDeliverEventArgs result);

    public class RabbitMqClient : IRabbitMqClient
    {
        private ILog log = LogManager.GetLogger(typeof(RabbitMqClient));
        public RabbitMqClientContext Context { get; set; }


        private static IRabbitMqClient _client;

        public static IRabbitMqClient Instance
        {
            get
            {
                if (_client==null)
                {
                    RabbitMqClientFactory.CreateRabbitMqClientInstance();
                }

                return _client;
            }
             set { _client = value; }
        }

        private ActionEvent _actionMessage;

        public event ActionEvent ActionEventMessage
        {
            add
            {
                if (_actionMessage==null)
                    _actionMessage += value;
            }
            remove
            {
                if (_actionMessage!=null)
                    _actionMessage -= value;
            }
        }


        public void PublishMessage(string message, string exChange, string queue)
        {
            Context.SendConnection = RabbitMQConnection.Connection;
            using (Context.SendConnection)
            {
                Context.SendChannel = RabbitMQConnection.CreateModel(Context.SendConnection);
                using (Context.SendChannel)
                {
                    Context.SendChannel.QueueDeclare(queue, true, false, false, null);
                    var properties = Context.SendChannel.CreateBasicProperties();
                    //properties.DeliveryMode = 2; //表示持久化消息
                    properties.Persistent = true;
                    var body = Encoding.UTF8.GetBytes(message);
                    Context.SendChannel.BasicPublish(exChange, queue, properties, body);
                }
            }
        }

        public void HandleMessage(string queue)
        {
            Context.ReceiveConnection = RabbitMQConnection.Connection;
            //using (Context.ReceiveConnection)
            {
                Context.ReceiveChannel = RabbitMQConnection.CreateModel(Context.ReceiveConnection);
                //using (Context.ReceiveChannel)
                {
                    bool durable = true;
                    Context.ReceiveChannel.QueueDeclare(queue, durable, false, false, null);
                    var consumer = new EventingBasicConsumer(Context.ReceiveChannel); //创建事件驱动的消费者类型
                    consumer.Received += Consumer_Received; ;
                    Context.ReceiveChannel.BasicQos(0, 1, false);
                    Context.ReceiveChannel.BasicConsume(queue, false, consumer);
                }
            }
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                if (_actionMessage != null)
                { 
                    _actionMessage(e);
                }
                var result= Encoding.UTF8.GetString(e.Body);
                Context.ReceiveChannel.BasicAck(e.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw (ex);
            }
        }

        public void Dispose()
        {
            if (Context.SendConnection == null) return;

            if (Context.SendConnection.IsOpen)
                Context.SendConnection.Close();

            Context.SendConnection.Dispose();
        }
    }
}
