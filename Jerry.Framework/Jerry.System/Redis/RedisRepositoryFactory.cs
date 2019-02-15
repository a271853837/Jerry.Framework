using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jerry.System.Redis
{
    public class RedisRepositoryFactory
    {
        public static RedisRepository CreateRedisRepository()
        {
            return new RedisRepository();
        }
    }
}
