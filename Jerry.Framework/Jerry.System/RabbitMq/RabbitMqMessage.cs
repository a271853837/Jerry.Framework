using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.System.RabbitMq
{
    [Serializable]
    public class RabbitMqMessage
    {
        public RabbitMqMessage()
        {
            SendTime = DateTime.Now;
            IsOperationOk = false;
        }

        public RabbitMqMessage(string message)
        {
            Message = message;
        }

        public string Message { get; set; }

        private DateTime SendTime { get; set; }

        public DateTime ReceiveTime { get; set; }

        public bool IsOperationOk { get; set; }
    }
}
