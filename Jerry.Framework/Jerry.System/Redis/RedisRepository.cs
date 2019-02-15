using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.System.Redis
{
    public class RedisRepository : BaseRedisRepository, IRedisCaching
    {

        public RedisRepository() : base()
        {

        }
        #region String
        #region 同步
        public void StringSet(string key, object obj, TimeSpan? exp = default(TimeSpan?))
        {
            Db.StringSet(key, ConvertJson(obj), exp);
        }


        public void StringSet(string key, string value, TimeSpan? exp = default(TimeSpan?))
        {
            Db.StringSet(key, value, exp);
        }

        public void StringSet<T>(string key, T obj, TimeSpan? exp = default(TimeSpan?))
        {
            Db.StringSet(key, ConvertJson(obj), exp);
        }

        public string StringGet(string key)
        {
            return Db.StringGet(key);
        }

        public T StringGet<T>(string key)
        {
            return ConvertObj<T>(Db.StringGet(key));
        }
        #endregion

        #region 异步
        public async Task<bool> StringSetAsync(string key, string value, TimeSpan? exp = default(TimeSpan?))
        {
            return await Db.StringSetAsync(key, value, exp);
        }

        public async Task<bool> StringSetAsync(string key, object obj, TimeSpan? exp = default(TimeSpan?))
        {
            return await Db.StringSetAsync(key, ConvertJson(obj), exp);
        }

        public async Task<bool> StringSetAsync<T>(string key, T val, TimeSpan? exp = default(TimeSpan?))
        {
            return await Db.StringSetAsync(key, ConvertJson(val), exp);
        }

        public async Task<string> StringGetAsync(string key)
        {
            return await Db.StringGetAsync(key);
        }

        public async Task<T> StringGetAsync<T>(string key)
        {
            var val = await Db.StringGetAsync(key);
            return ConvertObj<T>(val);
        }
        #endregion 
        #endregion


    }
}
