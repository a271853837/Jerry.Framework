using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Jerry.System.Redis
{
    public class BaseRedisRepository
    {
        private static ConnectionMultiplexer db = null;
        private int DbNumber { get; }

        public BaseRedisRepository(int dbnum = -1)
        {
            DbNumber = dbnum;
            db = RedisManager.Instance;

        }


        public IDatabase Db
        {
            get
            {
                return db.GetDatabase(DbNumber);
            }
        }

        protected string ConvertJson<T>(T val)
        {
            return val is string ? val.ToString() : JsonConvert.SerializeObject(val);
        }

        protected T ConvertObj<T>(RedisValue val)
        {
            return JsonConvert.DeserializeObject<T>(val);
        }

        protected List<T> ConvetList<T>(RedisValue[] values)
        {
            List<T> result = new List<T>();
            foreach (var item in values)
            {
                var model = ConvertObj<T>(item);
                result.Add(model);
            }
            return result;
        }

        protected RedisKey[] ConvertRedisKeys(List<string> redisKeys)
        {
            return redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray();
        }

        protected RedisValue[] ConvertRedisValues(List<string> redisValues)
        {
            return redisValues.Select(redisKey => (RedisValue)redisKey).ToArray();
        }
    }
}
