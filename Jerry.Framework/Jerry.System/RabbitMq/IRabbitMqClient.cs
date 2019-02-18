using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.System.RabbitMq
{
    public interface IRabbitMqClient : IDisposable
    {
        RabbitMqClientContext Context { get; set; }

        event ActionEvent ActionEventMessage;

        void PublishMessage(string message, string exChange, string queue);

        void HandleMessage(string queue);
    }
}
