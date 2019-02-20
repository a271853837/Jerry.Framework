using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Jerry.System.Log;
using Jerry.System.RabbitMq;
using NUnit.Framework;
using RabbitMQ.Client.Events;

namespace Jerry.Test
{
    [TestFixture]
    public class RabbitMqTest
    {

        private ILog log =LogManager.GetLogger(typeof(RabbitMqTest));

       
        [Test]
        public void SendMessage()
        {

            try
            {
                for (int i = 0; i < 100; i++)
                {
                    RabbitMqMessage message = new RabbitMqMessage()
                    {
                        Message = i + "号消息",
                        IsOperationOk = false
                    };
                    RabbitMqContext.Instance.PublishMessage(message);
                }
                //RabbitMqContext.Instance.ActionEventMessage += this.Instance_ActionEventMessage;
                //RabbitMqContext.Instance.Receive();
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        [Test]
        public void HandleMessage()
        {
            RabbitMqClient.Instance.ActionEventMessage += this.Instance_ActionEventMessage;
            RabbitMqClient.Instance.Receive();

        }

        private void Instance_ActionEventMessage(BasicDeliverEventArgs result)
        {
            var message = Encoding.UTF8.GetString(result.Body);
            log.Info("接受到消息："+message);
        }
    }
}
