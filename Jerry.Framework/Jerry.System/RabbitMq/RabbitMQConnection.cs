using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Jerry.System.RabbitMq
{
    public class RabbitMqConnection
    {
        private static IConnection _conn;
        private static object obj = new object();

        private RabbitMqConnection()
        {

        }

        

        public static IConnection Connection
        {
            get
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.HostName = RabbitMqContext._hostName;
                factory.Port = RabbitMqContext._port;
                factory.UserName = RabbitMqContext._userName;
                factory.Password = RabbitMqContext._password;
                factory.RequestedHeartbeat = 60;
                factory.AutomaticRecoveryEnabled = true;
                _conn = factory.CreateConnection();
                return _conn;
                //if (_conn == null)
                //{
                //    lock (obj)
                //    {
                //        if (_conn == null)
                //        {
                //            try
                //            {
                //                ConnectionFactory factory = new ConnectionFactory();
                //                factory.HostName = "localhost";
                //                factory.Port = 5672;
                //                factory.UserName = "admin";
                //                factory.Password = "li83361658";
                //                factory.RequestedHeartbeat = 60;
                //                factory.AutomaticRecoveryEnabled = true;
                //                _conn = factory.CreateConnection();
                //            }
                //            catch (Exception e)
                //            {
                //                throw e;
                //            }
                            
                //        }
                //    }
                //}
                //return _conn;
            }
        }

        public static IModel CreateModel(IConnection conn)
        {
            return conn.CreateModel();
        }
    }
}
