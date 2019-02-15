using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.System.Cache
{
    public class Cache : ICache, IDisposable
    {
        public void Add(object key, object value)
        {

            throw new NotImplementedException();
        }

        public void Add(object key, object value, TimeSpan span)
        {
            throw new NotImplementedException();
        }


        public void Flush()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this.Flush();
        }
    }
}
