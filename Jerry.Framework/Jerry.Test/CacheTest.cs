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
        public void AddCache()
        {
            ICache cache = new Cache();
            cache.Add("TEST", 123);

            int a = (int)cache["TEST"];

            Assert.IsTrue(a == 123);

            cache.Flush();

            var test= cache["TEST"];
            Assert.IsNull(test);
        }
    }
}
