using System.Collections.Generic;

namespace CacheLiteSharp.Core.DataStructures
{
    /// <summary>
    /// Basic dictionary-based data-structure, used to back ICache implementing cache classes
    /// </summary>
    /// <remarks>
    /// https://stackoverflow.com/questions/654752/can-i-create-a-dictionary-of-generic-types
    /// </remarks>
    public class GenericCacheDictionary
    {
        public Dictionary<string, object> Dictionary = new Dictionary<string, object>();

        public int Size => Dictionary.Count;

        public void Add<T>(string key, T value) where T : class
        {
            Dictionary.Add(key, value);
        }

        public T Remove<T>(string key) where T : class
        {
            return Dictionary.Remove(key) as T;
        }

        public T GetValue<T>(string key) where T : class
        {
            if (Dictionary.TryGetValue(key, out _))
            {
                return Dictionary[key] as T;
            }

            return null;
        }

        public void Clear()
        {
            Dictionary.Clear();
        }
    }
}