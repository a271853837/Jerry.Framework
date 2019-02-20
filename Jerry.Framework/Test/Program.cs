using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jerry.System.Log;
using Jerry.System.RabbitMq;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Test
{
    class Program
    {
        private static ILog log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            //for (int i = 0; i < 100; i++)
            //{
            //    RabbitMqClient.Instance.PublishMessage(i+"号消息", "jerry.direct", "queue1");
            //}

            for (int i = 0; i < 100; i++)
            {
                RabbitMqMessage message = new RabbitMqMessage()
                {
                    Message = i + "号消息",
                    IsOperationOk = false
                };
                RabbitMqContext.Instance.PublishMessage(message);
            }

            RabbitMqContext.Instance.ActionEventMessage += Instance_ActionEventMessage;
            RabbitMqContext.Instance.Receive();
            Console.Read();

        }

        private static void Instance_ActionEventMessage(RabbitMqMessage message)
        {
            message.IsOperationOk = true;
            message.ReceiveTime = DateTime.Now;
            Console.WriteLine(" [x] Received {0}", message.Message);
            log.Info("接受到消息：" + message.Message);
        }

    }
    public class Producer
    {
        public static void Send()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            //factory.Port = 5672;
            factory.UserName = "admin";
            factory.Password = "li83361658";

            //创建连接对象，基于 Socket
            using (var connection = factory.CreateConnection())
            {
                //创建新的渠道、会话
                using (var channel = connection.CreateModel())
                {
                    //声明队列
                    channel.QueueDeclare(queue: "queue1",    //队列名
                        durable: true,     //持久性
                        exclusive: false,   //排他性
                        autoDelete: false,  //自动删除
                        arguments: null);

                    const string message = "Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",  //交换机名
                        routingKey: "hello",    //路由键
                        basicProperties: null,
                        body: body);
                }
            }
        }
    }
}
