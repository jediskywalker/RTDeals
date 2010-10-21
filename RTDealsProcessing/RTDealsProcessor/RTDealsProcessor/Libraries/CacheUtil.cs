using System;
using System.Collections;
using System.Collections.Generic;

namespace Libraries
{
    class CacheItem
    {
        public string Key = "";
        public object Content = null;
        public DateTime InsertTime = DateTime.Now;
        public int LifeSpanSecs = 0;
    }

    public class CacheUtil
    {
        private static Hashtable _cacheTable = new Hashtable();

        private CacheUtil() { }

        private void InsertCache(string key, object content, int lifeSpanSecs)
        {
            if (content == null) return;

            lock (_cacheTable.SyncRoot)
            {
                CacheItem item = new CacheItem();
                item.Key = key;
                item.Content = content;
                item.LifeSpanSecs = lifeSpanSecs;
                _cacheTable[key] = content;
            }
        }


        public static void SetCache(string cacheKey, object cacheData, int lifeSpanSecs)
        {
            if (cacheData == null) return;

            lock (_cacheTable.SyncRoot)
            {
                CacheItem item = new CacheItem();
                item.Key = cacheKey;
                item.Content = cacheData;
                item.LifeSpanSecs = lifeSpanSecs;
                _cacheTable[cacheKey] = item;
            }
        }

        public static object GetCache(string cacheKey)
        {
            lock (_cacheTable.SyncRoot)
            {
                if (!_cacheTable.Contains(cacheKey)) return null;

                CacheItem item = (CacheItem)_cacheTable[cacheKey];
                
                if (item.LifeSpanSecs <= 0) return item.Content;

                if (item.InsertTime.AddSeconds(item.LifeSpanSecs) < DateTime.Now)
                {
                    _cacheTable.Remove(cacheKey);
                    return null;
                }
                else
                    return item.Content;

            }
        }

        public static void RemoveCacheItem(string cacheKey)
        {
            lock (_cacheTable.SyncRoot)
            {
                if (_cacheTable.Contains(cacheKey))
                    _cacheTable.Remove(cacheKey);
            }
        }

    }
}