using System;
using System.Web;

namespace Jerry.System.Cache
{
    public class Cache : ICache, IDisposable
    {
        public object this[string key] {
            get
            {
                return Get(key);
            }
            set
            {
                Add(key, value);
            }

        }

        public object Get(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }
        public void Add(object key, object value)
        {
            HttpRuntime.Cache.Insert(key.ToString(),value);
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
