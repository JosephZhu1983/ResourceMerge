using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace ResourceMerge.Core
{
    internal class MemcacheDictionary<Value>
    {
        private MemcachedClient mc = MemcachedClient.CacheClient;

        private string cacheKeyPrefix;

        internal MemcacheDictionary(string cacheKeyPrefix)
        {
            this.cacheKeyPrefix = cacheKeyPrefix;
        }

        public string CacheKeyPrefix
        {
            get
            {
                return ConfigProvider.Common.CacheKeyPrefix + cacheKeyPrefix;
            }
        }

        public bool Exists(string key)
        {
            return mc.KeyExists(CacheKeyPrefix + key);
        }

        public Value Get(string key)
        {
            if (key == null)
                return default(Value);
            return mc.Get<Value>(CacheKeyPrefix + key);
        }

        public List<Value> GetAll()
        {
            List<string> keys = mc.Get_Keys(CacheKeyPrefix);
            List<Value> data = new List<Value>();
            var fromcache = mc.Get_Multi(keys);
            foreach (string key in keys)
            {
                if (fromcache.ContainsKey(key))
                    data.Add((Value)fromcache[key]);
            }
            return data;
        }

        public void Set(string key, Value value)
        {
            mc.Store(StoreMode.Set, CacheKeyPrefix + key, value);
        }

        public void Set(string key, Value value, DateTime expiresAt)
        {
            mc.Store(StoreMode.Set, CacheKeyPrefix + key, value, expiresAt);
        }

        public void Set(string key, Value value, TimeSpan validFor)
        {
            mc.Store(StoreMode.Set, CacheKeyPrefix + key, value, validFor);
        }

        public void Remove(string key)
        {
            mc.Remove(CacheKeyPrefix + key);
        }

        public void RemoveAll()
        {
            List<string> keys = mc.Get_Keys(CacheKeyPrefix);
            keys.ForEach(key => mc.Remove(key));
        }
    }
}
