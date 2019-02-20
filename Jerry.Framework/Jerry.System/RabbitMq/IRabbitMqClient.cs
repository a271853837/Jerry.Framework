using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.System.RabbitMq
{
    public interface IRabbitMqClient : IDisposable
    {

        event ActionEvent ActionEventMessage;

        void PublishMessage(string message);

        void Receive();
    }
}
