using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.System.Cache
{
    public interface ICache
    {

        object this[string key] { get;set; }

        object Get(string key);
        /// <summary>
        /// 向Cache中添加项
        /// </summary>
        /// <param name="key">被Cache对象的Key</param>
        /// <param name="value">被Cache的对象</param>
        void Add(object key, object value);

        /// <summary>
        /// 向Cache中添加项
        /// </summary>
        /// <param name="key">被Cache对象的Key</param>
        /// <param name="value">被Cache的对象</param>
        void Add(object key, object value, TimeSpan span);

        /// <summary>
        /// 从Cache中删除指定的对象
        /// </summary>
        /// <param name="key"></param>
        void Remove(object key);

        /// <summary>
        /// 刷新Cache以进行强制淘汰
        /// </summary>
        void Refresh();

        /// <summary>
        /// 清除所有的Cache内容
        /// </summary>
        void Flush();

    }
}
