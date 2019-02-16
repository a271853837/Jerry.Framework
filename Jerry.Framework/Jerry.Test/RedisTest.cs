using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jerry.System.Redis;
using NUnit.Framework;
using StackExchange.Redis;

namespace Jerry.Test
{
    [TestFixture]
    public class RedisTest
    {
        private RedisRepository redis;

        public RedisTest()
        {
            redis = RedisRepositoryFactory.CreateRedisRepository(1);
        }

        [Test]
        public void StringSet()
        {
            redis.StringSet("aaa", "1234");

            string str = redis.StringGet("aaa");
            Assert.IsTrue(str == "1234");
        }


        [Test]
        public void KeyTest()
        {
            //long count= redis.StringIncrement("count");

            redis.StringDecrement("count");
            //Assert.IsTrue(count == 1);

            Assert.IsTrue(redis.KeyExists("aaa"));
        }

        [Test]
        public void HashTest()
        {
            RedisModel p = CreateModel("a1");

            //redis.HashSet<RedisModel>("person", "a1", p);
            //redis.HashSet<RedisModel>("person", "a2", p);
            redis.HashSet<RedisModel>("person", "a3", p);
            redis.HashDelete("person", new List<string>() { "a1", "a2" });
            Assert.IsTrue(redis.KeyExists("aaa"));
        }

        private RedisModel CreateModel(string name)
        {
            RedisModel p = new RedisModel(name)
            {
                name = "testName",
                age = 1,
                birthday = DateTime.Now
            };
            return p;
        }

        /// <summary>
        /// 发布订阅模式 首先要有订阅方，再发布
        /// </summary>
        [Test]
        public void Publish()
        {
            RedisModel p = CreateModel("a2");
            redis.Subscribe("channel1", (channel, message) =>
            {
                Console.WriteLine(channel.ToString() + " 订阅收到消息：" + message);
            });
            redis.Publish<RedisModel>("channel1", p);
        }

        [Test]
        public void SetCombine()
        {
            //RedisModel p3 = CreateModel("a3");
            //redis.SetAdd("setkey", p3);
            //RedisModel p4 = CreateModel("a4");
            //redis.SetAdd("setkey", p4);
            redis.SetAdd("setkey1", "1");
            redis.SetAdd("setkey1", "2");
            redis.SetAdd("setkey1", "3");
            redis.SetAdd("setKey1", CreateModel("123"));
            var strings = redis.SetMembers<string>("setkey1");

            redis.SetAdd("setkey2", "1");
            var list = redis.SetCombine<string>("setkey1", "setkey2", SetOperation.Intersect);


        }

    }


    public class RedisModel
    {
        public RedisModel(string name)
        {
            this.name = name;
        }
        public string name { get; set; }
        public int age { get; set; }
        public DateTime birthday { get; set; }
    }
}
