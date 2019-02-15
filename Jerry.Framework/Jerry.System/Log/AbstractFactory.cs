using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.System.Log
{
    public abstract class AbstractFactory:ILoggerFactory
    {
        public AbstractFactory()
        {

        }

        public abstract ILog GetLogger(string name);

        public virtual ILog GetLogger(Type type)
        {
            return GetLogger(type.FullName);
        }
    }
}
