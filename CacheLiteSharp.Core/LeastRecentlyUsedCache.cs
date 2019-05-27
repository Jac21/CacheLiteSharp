using System;
using System.Collections.Generic;
using CacheLiteSharp.DataStructures;
using CacheLiteSharp.ICache;

namespace CacheLiteSharp
{
    /// <summary>
    /// Generic cache that employs the Least Recently Used flush strategy, 
    /// we can keep only a certain number of entries that are recently used and remove others
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LeastRecentlyUsedCache<T> : ICache<T> where T : class
    {
        private readonly GenericCacheDictionary cache = new GenericCacheDictionary();

        private readonly int minimalSize;

        private readonly LinkedHashMap<string, T> keyMap = new LinkedHashMap<string, T>();

        private LinkedListNode<Tuple<T, string>> eldestKeyToRemove;

        private const int DefaultSize = 100;

        /// <summary>
        /// Generic cache that employs the Least Recently Used flush strategy,
        /// minimalSize parameter determines the amount of values the cache can contain before being cycled, removing the eldest entry.
        /// Default size is set to 100 values.
        /// </summary>
        /// <param name="minimalSize"></param>
        /// 
        public LeastRecentlyUsedCache(int minimalSize = DefaultSize)
        {
            this.minimalSize = minimalSize;
        }

        /// <summary>
        /// Obtain the size of the Cache in integer form
        /// </summary>
        public int Size => cache.Size;

        /// <summary>
        /// Set a generic cache value based on a string key. Cycle values if size exceeds minimal size argument, remove eldest entry as such
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, T value)
        {
            cache.Dictionary[key] = keyMap[key] = value;

            if (RemovedEldestEntry(keyMap))
            {
                CycleKeyMap();
            }
        }

        /// <summary>
        /// Get a particular cache value using a string key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get(string key)
        {
            return cache.GetValue<T>(key);
        }

        /// <summary>
        /// Remove a particular cache value using a string key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Remove(string key)
        {
            return cache.Remove<T>(key);
        }

        /// <summary>
        /// Clear the entire cache
        /// </summary>
        public void Clear()
        {
            keyMap.Clear();
            cache.Clear();
        }

        private void CycleKeyMap()
        {
            if (eldestKeyToRemove != null)
            {
                cache.Dictionary.Remove(eldestKeyToRemove.List.Last.Value.Item2);
            }

            eldestKeyToRemove = null;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/20772869/what-is-the-use-of-linkedhashmap-removeeldestentry
        /// </summary>
        /// <param name="eldest"></param>
        /// <returns></returns>
        private bool RemovedEldestEntry(LinkedHashMap<string, T> eldest)
        {
            var tooManyCachedItems = Size > minimalSize;

            if (tooManyCachedItems)
            {
                eldestKeyToRemove = eldest.LinkedHashMapLinkedList.Last;
            }

            return tooManyCachedItems;
        }
    }
}