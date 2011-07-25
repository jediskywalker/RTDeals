using System;
using System.Collections;
using System.Collections.Generic;


namespace Util
{
    public class Cache
    {
        class CacheItem
        {
            public string Key;
            public object Content;
            public DateTime LastUpdate;
            public int LifeSpan; // in seconds

            private CacheItem() { }

            public CacheItem(string key, object content, int lifeSpan) // 38409
            {
                Key = key;
                Content = content;
                LifeSpan = lifeSpan;
                LastUpdate = DateTime.Now;
            }
        }

        private static Hashtable AllItems = new Hashtable();
        private static object LockObj = new object();
        private static DateTime LastExpireCheck = DateTime.Now;

        // hide constructor
        private Cache() { }

        private static void ExpireCheck()
        {
            if (DateTime.Now.Subtract(LastExpireCheck).TotalSeconds > 60) // check every 60 seconds
            {
                lock (LockObj)
                {
                    ICollection keys = AllItems.Keys;
                    foreach (string key in keys)
                    {
                        CacheItem item = (CacheItem)AllItems[key];
                        
                        if (item.LifeSpan == -1) continue; // forever

                        if (DateTime.Now.Subtract(item.LastUpdate).TotalSeconds >= item.LifeSpan)
                            AllItems.Remove(key);
                    }

                }
                LastExpireCheck = DateTime.Now;
            }
        }

        /// <summary>
        /// Insert an item into cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="expireInSeconds"></param>
        public static void SetCacheItem(string key, object data, int expireInSeconds)
        {
            lock (LockObj)
            {
                ExpireCheck();

                CacheItem item = new CacheItem(key, data, expireInSeconds);

                if (AllItems.Contains(key))
                    AllItems.Remove(key);

                AllItems.Add(key, item);
            }
        }


        /// <summary>
        /// get specified cache item based on key provided
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetCacheItem(string key)
        {
            object obj = null;
            lock (LockObj)
            {
                if (AllItems.Contains(key))
                {
                    CacheItem item = (CacheItem)AllItems[key];
                    obj = item.Content;
                }
            }
            return obj;
        }

        /// <summary>
        /// remove specified cache item
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveCacheItem(string key)
        {
            lock (LockObj)
            {
                if (AllItems.Contains(key))
                {
                    AllItems.Remove(key);
                }
            }
        }

    }
}