using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jerry.System.RabbitMq;
using NUnit.Framework;
using RabbitMQ.Client.Events;

namespace Jerry.Test
{
    [TestFixture]
    public class RabbitMqTest
    {
        [Test]
        public void SendMessage()
        {

            try
            {
                RabbitMqClient.Instance.PublishMessage("123", "change", "queue2");
                RabbitMqClient.Instance.ActionEventMessage += this.Instance_ActionEventMessage;
                RabbitMqClient.Instance.HandleMessage("queue2");
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        [Test]
        public void HandleMessage()
        {
            RabbitMqClient.Instance.HandleMessage("queue1");
            RabbitMqClient.Instance.ActionEventMessage += this.Instance_ActionEventMessage;
        }

        private void Instance_ActionEventMessage(BasicDeliverEventArgs result)
        {
            var message = Encoding.UTF8.GetString(result.Body);
        }
    }
}
