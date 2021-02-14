using System;
using CacheLiteSharp.Core.DataStructures;
using CacheLiteSharp.Core.Interfaces;

namespace CacheLiteSharp.Core
{
    /// <summary>
    /// Generic cache that flushes values based on given interval in minutes, cache is recycled every interval's millisecond representation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExpirableCache<T> : ICache<T> where T : class
    {
        private readonly GenericCacheDictionary _cache = new GenericCacheDictionary();

        private readonly double _flushInterval;

        private readonly long _lastFlushTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        /// <summary>
        /// /// Generic cache that flushes values based on given interval in minutes, cache is recycled every interval's millisecond representation
        /// </summary>
        /// <param name="cacheExpirationFlushIntervalInMinutes"></param>
        public ExpirableCache(double cacheExpirationFlushIntervalInMinutes)
        {
            _flushInterval = TimeSpan.FromMinutes(cacheExpirationFlushIntervalInMinutes).TotalMilliseconds;
        }

        /// <summary>
        /// Obtain the size of the Cache in integer form
        /// </summary>
        public int Size => _cache.Size;

        /// <summary>
        /// Set a generic cache value based on a string key 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, T value)
        {
            _cache.Dictionary[key] = value;
        }

        /// <summary>
        /// Get a particular cache value using a string key, recycle cache before obtaining value is flush interval given is reached
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get(string key)
        {
            Recycle();
            return _cache.GetValue<T>(key);
        }

        /// <summary>
        /// Remove a particular cache value using a string key
        /// </summary>
        /// <param name="key"></param>
        public T Remove(string key)
        {
            Recycle();
            return _cache.Remove<T>(key);
        }

        /// <summary>
        /// Clear the entire cache
        /// </summary>
        public void Clear()
        {
            _cache.Clear();
        }

        private void Recycle()
        {
            var shouldRecycle = DateTimeOffset.Now.ToUnixTimeMilliseconds() - _lastFlushTime >=
                                TimeSpan.FromMilliseconds(_flushInterval).TotalMilliseconds;

            if (!shouldRecycle)
            {
                return;
            }

            _cache.Clear();
        }
    }
}