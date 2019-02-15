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
        public BaseRedisRepository() 
        {
            db = RedisManager.Instance;
        }

        public BaseRedisRepository(int dbnum)
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

        public string ConvertJson<T>(T val)
        {
            return val is string ? val.ToString() : JsonConvert.SerializeObject(val);
        }

        public T ConvertObj<T>(RedisValue val)
        {
            return JsonConvert.DeserializeObject<T>(val);
        }
    }
}
