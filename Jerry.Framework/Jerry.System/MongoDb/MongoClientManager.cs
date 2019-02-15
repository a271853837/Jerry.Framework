using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Jerry.System.MongoDb
{
    public class MongoClientManager
    {
        private static MongoClient _mongoClient;
        private static object obj = new object();
        private static string host = "localhost";
        private static int port = 27017;
        private static string MONGOROOTUSER = "admin";
        private static string MONGOADMINDB = "admin";
        private static string PWD = "Li83361658";


        private MongoClientManager()
        {
        }

        public static MongoClient GetClient
        {
            get
            {
                if (_mongoClient == null)
                {
                    lock (obj)
                    {
                        if (_mongoClient == null)
                        {
                            var setting = new MongoClientSettings()
                            {
                                Server = new MongoServerAddress(host, port)
                            };
                            if (!string.IsNullOrEmpty(PWD))
                            {
                                setting.Credential = MongoCredential.CreateCredential(MONGOADMINDB, MONGOROOTUSER, PWD);
                            }
                            _mongoClient = new MongoClient(setting);
                        }
                    }
                }
                return _mongoClient;
            }
        }

    }
}
