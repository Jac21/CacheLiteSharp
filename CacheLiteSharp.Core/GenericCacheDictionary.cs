using CacheLiteSharp.Core.DataStructures;
using CacheLiteSharp.Core.Interfaces;

namespace CacheLiteSharp.Core
{
    /// <summary>
    /// Basic generic cache, caches items forever until removed manually
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PerpetualCache<T> : ICache<T> where T : class
    {
        private readonly GenericCacheDictionary _cache = new();

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
        /// Get a particular cache value using a string key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get(string key)
        {
            return _cache.GetValue<T>(key);
        }

        /// <summary>
        /// Remove a particular cache value using a string key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Remove(string key)
        {
            return _cache.Remove<T>(key);
        }

        /// <summary>
        /// Clear the entire cache
        /// </summary>
        public void Clear()
        {
            _cache.Clear();
        }
    }
}