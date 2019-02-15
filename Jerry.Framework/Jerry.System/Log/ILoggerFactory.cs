using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.System.Log
{
    public interface ILoggerFactory
    {
        /// <summary>
        /// 通过指定的名称取得logger对象。
        /// </summary>
        /// <param name="name">logger对象名称</param>
        /// <returns>logger对象</returns>
		ILog GetLogger(string name);
        /// <summary>
        /// 通过指定的类型取得logger对象。
        /// </summary>
        /// <param name="type">类型对象</param>
        /// <returns>logger对象</returns>
		ILog GetLogger(Type type);
    }
}
