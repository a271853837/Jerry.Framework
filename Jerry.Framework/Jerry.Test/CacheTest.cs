using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jerry.System.Cache;
using NUnit.Framework;

namespace Jerry.Test
{
    [TestFixture]
    class CacheTest
    {
        [Test]
        public void AddTest()
        {
            ICache cache = new Cache();
            cache["123"] = 123;
            int a=(int) cache.Get("123");

            Assert.IsTrue(a == 123);
            cache.Flush();

            Assert.IsNull(cache["123"]);
        }
    }
}
