using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jerry.System.Log;
using StackExchange.Redis;

namespace Jerry.System.Redis
{
    public class RedisManager
    {
        private static ILog log = LogManager.GetLogger(typeof(RedisManager));
        //private static string Constring = RedisConfig.Config();
        private static readonly object locker = new object();
        private static ConnectionMultiplexer instance;
        private static readonly Dictionary<string, ConnectionMultiplexer> Concache = new Dictionary<string, ConnectionMultiplexer>();


        private RedisManager()
        {
        }

        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null || instance.IsConnected == false)
                            instance = GetManager();
                    }
                }
                return instance;
            }
        }

        private static ConnectionMultiplexer GetManager(string constr = null)
        {
            try
            {
                var server = RedisConfig.SERVER;
                var port = RedisConfig.PORT;
                var configString = $"{server}:{port}";
                var config = ConfigurationOptions.Parse(configString);
                config.Password = RedisConfig.PWD;
                var connect = ConnectionMultiplexer.Connect(config);
                //var connect = ConnectionMultiplexer.Connect("127.0.0.1:6379,password=Li83361658");
                connect.ConnectionFailed += Connect_ConnectionFailed;
                connect.ConnectionRestored += Connect_ConnectionRestored;
                return connect;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }

        }

        private static void Connect_ConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            log.Error("重连错误" + e.EndPoint);
        }

        private static void Connect_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            log.Error("连接异常" + e.EndPoint + "，类型为" + e.FailureType + (e.Exception == null ? "" : ("，异常信息是" + e.Exception.Message)));
        }
    }
}
