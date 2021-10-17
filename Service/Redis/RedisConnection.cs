using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Redis
{
    public class RedisConnection
    {
        private static Lazy<ConnectionMultiplexer> lazyconnectionMultipelxer;
        static RedisConnection()
        {
            lazyconnectionMultipelxer = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect("localhost:6379");
            });
        }
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyconnectionMultipelxer.Value;
            }
        }      
    }
}
