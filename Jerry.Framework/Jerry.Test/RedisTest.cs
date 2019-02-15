using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jerry.System.Redis;
using NUnit.Framework;

namespace Jerry.Test
{
    [TestFixture]
    public class RedisTest
    {
        private RedisRepository repository;

        public RedisTest()
        {
            repository = RedisRepositoryFactory.CreateRedisRepository();
        }

        [Test]
        public void StringSet()
        {
            repository.StringSet("aaa", "1234");

            string str= repository.StringGet("aaa");
            Assert.IsTrue(str == "1234");
        }
    }
}
