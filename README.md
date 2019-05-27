![logo](https://raw.githubusercontent.com/Jac21/CacheLiteSharp.Core/master/media/logo_transparent.png)

📂 A variety of cache implementations, all in .NET Core C#

Primarily influenced by the Kotlin-based [Cache-Lite](https://github.com/kezhenxu94/cache-lite) over at [Kid the Programmer](https://github.com/kezhenxu94)

Cache implementations include [*Perpetual*](https://github.com/Jac21/CacheLiteSharp.Core/blob/master/CacheLiteSharp.Core/PerpetualCache.cs), [*Least Recently Used*](https://github.com/Jac21/CacheLiteSharp.Core/blob/master/CacheLiteSharp.Core/LeastRecentlyUsedCache.cs), and [*Expirable*](https://github.com/Jac21/CacheLiteSharp.Core/blob/master/CacheLiteSharp.Core/ExpirableCache.cs).

This is mainly an academic exercise, however, feel entirely free to use this class library in your own projects, and please let me know the results of said attempt. This library comes in a handy [nuget package](https://www.nuget.org/packages/CacheLiteSharp.Core/) for the bold in question.

## Installation

```
PM> Install-Package CacheLiteSharp.Core -Version 1.0.0   
```

## Interface
All caches in this library implement the following simplistic interface, with slight variations on flush strategy inherent to said implementation:

```csharp
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
```
