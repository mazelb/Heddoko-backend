using Jil;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RedisManager
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(Config.RedisConnectionString));

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return LazyConnection.Value;
            }
        }

        private static IDatabase Cache
        {
            get
            {
                return Connection.GetDatabase();
            }
        }


        public static void Set(string key, object item)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    if (item != null)
                    {
                        IDatabase db = Cache;

                        string value = JSON.Serialize(item, new Options(includeInherited: true));

                        db.StringSet(key, value);
                        db.KeyExpire(key, DateTime.Now.AddHours(Config.RedisCacheExpiration), flags: CommandFlags.FireAndForget);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("RedisManager.Set.Exception: key:{0} Error:{1}", key, ex.GetOriginalException());
            }
        }

        public static void Clear(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    IDatabase db = Cache;

                    if (db.KeyExists(key))
                    {
                        db.KeyDelete(key);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("RedisManager.Clear.Exception: key:{0} Error:{1}", key, ex.GetOriginalException());
            }
        }

        public static T Get<T>(string key)
        {
            T value = default(T);
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    IDatabase db = Cache;
                    if (db.KeyExists(key))
                    {
                        value = JSON.Deserialize<T>(db.StringGet(key), new Options(includeInherited: true));
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("RedisManager.Get.Exception: key:{0} Error:{1}", key, ex.GetOriginalException());
            }

            return value;
        }

        public static void Flush()
        {
            foreach (EndPoint endpoint in Connection.GetEndPoints())
            {
                IServer server = Connection.GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }
    }
}
