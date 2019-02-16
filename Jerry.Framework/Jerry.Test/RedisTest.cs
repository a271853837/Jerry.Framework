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
        private RedisRepository redis;

        public RedisTest()
        {
            redis = RedisRepositoryFactory.CreateRedisRepository();
        }

        [Test]
        public void StringSet()
        {
            redis.StringSet("aaa", "1234");

            string str= redis.StringGet("aaa");
            Assert.IsTrue(str == "1234");
        }


        [Test]
        public void KeyTest()
        {
            //long count= redis.StringIncrement("count");

            redis.StringDecrement("count");
            //Assert.IsTrue(count == 1);

            Assert.IsTrue( redis.KeyExists("aaa"));
        }

        [Test]
        public void HashTest()
        {
            RedisModel p = new RedisModel()
            {
                name = "testName",
                age = 1,
                birthday = DateTime.Now
            };

            //redis.HashSet<RedisModel>("person", "a1", p);
            //redis.HashSet<RedisModel>("person", "a2", p);
            redis.HashSet<RedisModel>("person", "a3", p);
            redis.HashDelete("person",new List<string>() { "a1", "a2" });
            Assert.IsTrue(redis.KeyExists("aaa"));
        }
    }


    public class RedisModel
    {
        public string name { get; set; }
        public int age { get; set; }
        public DateTime birthday { get; set; }
    }
}
