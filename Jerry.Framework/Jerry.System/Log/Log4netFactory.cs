using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.System.Log
{
    public class Log4netFactory : AbstractFactory
    {
        internal static Dictionary<string, ILog> map = new Dictionary<string, ILog>(128);
        public override ILog GetLogger(string name)
        {
            ILog logger = null;
            if (!map.TryGetValue(name, out logger))
            {
                logger = new Logger4net(log4net.LogManager.GetLogger(name));
                map[name] = logger;
            }
            return logger;
        }

        public Log4netFactory(string configName)
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory+ "Log\\log4net.config");
            log4net.Config.XmlConfigurator.Configure(logCfg);
        }
    }
}
