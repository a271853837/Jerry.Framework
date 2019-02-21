using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Jerry.System.Log;
using Jerry.System.RabbitMq;
using NUnit.Framework;

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
                        Message = i + "号消息"
                    };
                    RabbitMqContext.Instance.PublishMessage(message);
                }
                //RabbitMqContext.Instance.Dispose();
                //RabbitMqContext.Instance.Connect();
                RabbitMqContext.Instance.ActionEventMessage += Instance_ActionEventMessage;
                RabbitMqContext.Instance.Receive();
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        private void Instance_ActionEventMessage(RabbitMqMessage message)
        {
            message.IsOperationOk = true;
            message.ReceiveTime = DateTime.Now;
            log.Info(message);
        }
    }
}
