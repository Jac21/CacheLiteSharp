namespace CacheLiteSharp.ICache
{
    /// <summary>
    /// Cache interface, used to decorate implementing classes. There are some basic operations on a cache, 
    /// you may want to put a value into it, get a value by key from it, remove a value by key from it, 
    /// clear it and know what’s the size of it. This interface provides that as such.
    /// </summary>
    /// <remarks>
    /// References: 
    /// http://kezhenxu94.me/2018/05/27/Build-Your-Own-Cache/
    /// https://stackoverflow.com/questions/1344694/implement-an-interface-with-generic-methods
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public interface ICache<T> where T : class
    {
        /// <summary>
        /// Obtain the size of the Cache in integer form
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Set a generic cache value based on a string key 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set(string key, T value);

        /// <summary>
        /// Get a particular cache value using a string key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get(string key);

        /// <summary>
        /// Remove a particular cache value using a string key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T Remove(string key);

        /// <summary>
        /// Clear the entire cache
        /// </summary>
        void Clear();
    }
}