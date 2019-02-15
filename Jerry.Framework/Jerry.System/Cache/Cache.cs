using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Jerry.System.Cache
{
    public class Cache : ICache, IDisposable
    {
        public object this[string key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Add(key, value);
            }
        }

        public object Get(object key)
        {
            return HttpRuntime.Cache.Get(key.ToString());
        }

        public void Add(string key, object value)
        {
            HttpRuntime.Cache.Insert(key, value);
        }

        public void Add(object key, object value, TimeSpan span)
        {
            throw new NotImplementedException();
        }


        public void Flush()
        {
            var cache = HttpRuntime.Cache.GetEnumerator();
            while (cache.MoveNext())
            {
                HttpRuntime.Cache.Remove(cache.Key.ToString());
            }
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            HttpRuntime.Cache.Remove(key.ToString());
        }

        public void Dispose()
        {
            this.Flush();
        }
    }
}
