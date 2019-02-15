using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.System.Log
{
    public class LogManager
    {
        private static object key = new object();
        private LogManager()
        {

        }

        private static ILoggerFactory _factory;
        private static ILoggerFactory Factory
        {
            get
            {
                if (_factory==null)
                {
                    lock (key)
                    {
                        if (_factory==null)
                        {
                            _factory= new Log4netFactory(string.Empty);
                        }
                    }
                }
                return _factory;
            }
        }

        public static ILog GetLogger(string name)
        {
            return Factory.GetLogger(name);
        }


        public static ILog GetLogger(Type type)
        {
            return Factory.GetLogger(type);
        }

    }
}
