using System;
using System.Diagnostics;
using System.Net;
using Jil;
using StackExchange.Redis;

namespace DAL
{
    public static class RedisManager
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(Config.RedisConnectionString));

        private static ConnectionMultiplexer Connection => LazyConnection.Value;

        private static IDatabase Cache => Connection.GetDatabase();


        public static void Set(string key, object item, int? hours = null)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    return;
                }

                if (item == null)
                {
                    return;
                }

                IDatabase db = Cache;

                string value = JSON.Serialize(item, new Options(includeInherited: true));

                db.StringSet(key, value);
                db.KeyExpire(key, DateTime.Now.AddHours(hours ?? Config.RedisCacheExpiration), CommandFlags.FireAndForget);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"RedisManager.Set.Exception: key:{key} Error:{ex.GetOriginalException()}");
            }
        }

        public static void Clear(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    return;
                }

                IDatabase db = Cache;

                if (db.KeyExists(key))
                {
                    db.KeyDelete(key);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"RedisManager.Clear.Exception: key:{key} Error:{ex.GetOriginalException()}");
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
                Trace.TraceError($"RedisManager.Get.Exception: key:{key} Error:{ex.GetOriginalException()}");
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