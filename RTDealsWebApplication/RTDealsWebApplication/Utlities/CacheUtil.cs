using System;
using System.Web;
using System.Web.Caching;


namespace Utilities
{
    public class CacheUtil
    {

        // hide constructor
        private CacheUtil()
        {
        }

        /// <summary>
        /// Insert an item into cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="expireInSeconds"></param>
        public static void SetCacheItem(string key, object data, int expireInSeconds)
        {
            if (expireInSeconds > 0)
                HttpRuntime.Cache.Insert(key, data, null, DateTime.Now.AddYears(1), new TimeSpan(0, 0, expireInSeconds));
            else
                HttpRuntime.Cache.Insert(key, data);
        }


        /// <summary>
        /// get specified cache item based on key provided
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetCacheItem(string key)
        {
            object obj = HttpRuntime.Cache.Get(key);

            return obj;
        }

        /// <summary>
        /// remove specified cache item
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveCacheItem(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        /// <summary>
        /// used to determine if object in cache exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Boolean Exists(string key)
        {
            Boolean Exists = false;
            object obj = null;
            obj = HttpRuntime.Cache.Get(key);
            if (obj != null)
            {
                Exists = true;
            }
            return Exists;
        }

    }
}