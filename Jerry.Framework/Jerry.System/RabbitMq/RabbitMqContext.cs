using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Jerry.System.RabbitMq
{
    public class RabbitMqContext
    {
        public static string _hostName = "localhost";
        public static string _userName = "admin";
        public static int _port = 5672;
        public static string _password = "li83361658";
        public static string _exchangeName = "exchange";
        public static string _queueName = "queue";
        public static string _routingKey = "";
        public static string _direct = "direct";

        private bool _disposed;
        protected IConnection _connection;
        public RabbitMqContext()
        {
            _connection = RabbitMqConnection.Connection;
        }

        public void Dispose()
        {
            if (_disposed || !IsConnected) return;

            _disposed = true;

            _connection.Close();
            _connection.Dispose();
        }

        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;
            }
        }
        private static IRabbitMqClient _client;

        public static IRabbitMqClient Instance
        {
            get
            {
                if (_client == null)
                {
                    _client = RabbitMqClientFactory.CreateRabbitMqClientInstance();
                }
                return _client;
            }
            set { _client = value; }
        }

    }
}
