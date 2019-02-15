using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.System.Redis
{
    public sealed class RedisConfig
    {
        public static readonly string config = "localhost:6379,password=1qaz2wsx";//ConfigurationManager.AppSettings["RedisConfig"];
        
        public static string Config()
        {
            return config;
        }

        public static string SERVER = "127.0.0.1";
        public static int PORT = 6379;
        public static string PWD = "Li83361658";
        
    }
}
