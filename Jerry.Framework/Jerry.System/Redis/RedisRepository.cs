using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jerry.System.Log;
using StackExchange.Redis;

namespace Jerry.System.Redis
{
    public class RedisRepository : BaseRedisRepository
    {

        ILog log = LogManager.GetLogger(typeof(RedisRepository));
        public RedisRepository(int dbnum = -1) : base(dbnum)
        {

        }
        #region Key
        public bool KeyDelete(string key)
        {
            return Db.KeyDelete(key);
        }

        public long KeyDelete(List<string> keys)
        {
            return Db.KeyDelete(ConvertRedisKeys(keys));
        }

        public bool KeyExists(string key)
        {
            return Db.KeyExists(key);
        }

        public bool KeyRename(string key, string newKey)
        {
            return Db.KeyRename(key, newKey);
        }

        public bool KeyExpire(string key, TimeSpan? expiry = default(TimeSpan?))
        {
            return Db.KeyExpire(key, expiry);
        }

        public bool KeyPersist(string key)
        {
            return Db.KeyPersist(key);
        }

        public RedisType KeyType(string key)
        {
            return Db.KeyType(key);
        }
        #endregion

        #region String
        #region 同步
        /// <summary>
        /// 返回 key 所储存的字符串值的长度。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long String(string key)
        {

            return Db.StringLength(key);
        }
        /// <summary>
        /// 将 key 所储存的值加上给定的增量值+1
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long StringIncrement(string key)
        {
            return Db.StringIncrement(key);
        }

        /// <summary>
        /// 将 key 所储存的值加上给定的增量值-1
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long StringDecrement(string key)
        {
            return Db.StringDecrement(key);
        }

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

        #region List
        #region 同步
        public void ListRemove<T>(string key, T value)
        {
            Db.ListRemove(key, ConvertJson(value));
        }

        public IEnumerable<RedisValue> ListRange(string key)
        {
            return Db.ListRange(key);
        }

        public List<T> ListRange<T>(string key)
        {
            return ConvetList<T>(Db.ListRange(key));
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void ListRightPush<T>(string key, T value)
        {
            Db.ListRightPush(key, ConvertJson(value));
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void ListLeftPush<T>(string key, T value)
        {
            Db.ListLeftPush(key, ConvertJson(value));
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListLeftPop<T>(string key)
        {
            return ConvertObj<T>(Db.ListLeftPop(key));
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListRightPop<T>(string key)
        {
            return ConvertObj<T>(Db.ListRightPop(key));
        }

        #endregion
        #endregion

        #region SortedSet
        #region 同步
        public bool SortedSetAdd<T>(string key, T value, double score)
        {
            return Db.SortedSetAdd(key, ConvertJson(value), score);
        }

        public bool SortedSetRemove<T>(string key, T value)
        {
            return Db.SortedSetRemove(key, ConvertJson(value));
        }

        public List<T> SortedSetRangeByRank<T>(string key)
        {
            var values = Db.SortedSetRangeByRank(key);
            return ConvetList<T>(values);
        }

        public long SortedSetLength(string key)
        {
            return Db.SortedSetLength(key);
        }


        #endregion
        #endregion

        #region Set
        #region 同步
        public bool SetAdd<T>(string key, T t)
        {
            return Db.SetAdd(key, ConvertJson(t));
        }

        public long SetAdd(string key, List<string> list)
        {
            return Db.SetAdd(key, list.Select(c => (RedisValue)c).ToArray());
        }
        public bool SetRemove<T>(string key,T t)
        {
            return Db.SetRemove(key, ConvertJson(t));
        }
        public long SetRemove(string key,List<string> list)
        {
            return Db.SetRemove(key, list.Select(c => (RedisValue)c).ToArray());
        }

        public long SetLength(string key)
        {
            return Db.SetLength(key);
        }

        public List<T> SetScan<T>(string key)
        {
            var values= Db.SetScan(key);
            return ConvetList<T>(values.ToArray());
        }

        public List<T> SetCombine<T>(string key1,string key2, SetOperation opt)
        {
            var values = Db.SetCombine(opt, key1, key2);
            return ConvetList<T>(values.ToArray());
        }

        public List<T> SetMembers<T>(string key)
        {
            var values= Db.SetMembers(key);
            return ConvetList<T>(values.ToArray());
        }

        #endregion
        #endregion

        #region Hash
        #region 同步
        public bool HashExists(string key, string dataKey)
        {
            return Db.HashExists(key, dataKey);
        }

        public bool HashSet<T>(string key, string dataKey, T t)
        {
            return Db.HashSet(key, dataKey, ConvertJson(t));
        }

        public bool HashDelete(string key, string dataKey)
        {
            return Db.HashDelete(key, dataKey);
        }

        public long HashDelete(string key, List<string> dataKeys)
        {
            return Db.HashDelete(key, ConvertRedisValues(dataKeys));
        }

        public T HashGet<T>(string key, string dataKey)
        {
            string value = Db.HashGet(key, dataKey);
            return ConvertObj<T>(value);
        }

        public double HashIncrement(string key, string dataKey, double val = 1)
        {
            return Db.HashIncrement(key, dataKey, val);
        }

        public double HashDecrement(string key, string dataKey, double val = 1)
        {
            return Db.HashDecrement(key, dataKey, val);
        }

        public List<T> HashKeys<T>(string key)
        {
            RedisValue[] values = Db.HashKeys(key);
            return ConvetList<T>(values);
        }
        #endregion
        #endregion

        #region 发布订阅
        /// <summary>
        /// 发布
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public long Publish<T>(string channel, T msg)
        {
            ISubscriber subscriber = Db.Multiplexer.GetSubscriber();
            return subscriber.Publish(channel, ConvertJson(msg));
        }

        /// <summary>
        /// 取消发布
        /// </summary>
        /// <param name="channel"></param>
        public void Unsubscribe(string channel)
        {
            ISubscriber sub = Db.Multiplexer.GetSubscriber();
            sub.Unsubscribe(channel);
        }

        /// <summary>
        /// 订阅 发布订阅模式 首先要有订阅方，再发布
        /// </summary>
        /// <param name="subChannel"></param>
        /// <param name="handler"></param>
        public void Subscribe(string subChannel, Action<RedisChannel, RedisValue> handler = null)
        {
            try
            {
                ISubscriber sub = Db.Multiplexer.GetSubscriber();
                sub.Subscribe(subChannel, (channel, message) =>
                {
                    if (handler == null)
                    {
                        log.Info(subChannel + " 订阅收到消息：" + message);
                    }
                    else
                    {
                        handler(channel, message);
                    }
                });
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }

        }

        #endregion
    }
}
