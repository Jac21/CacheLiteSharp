using NUnit.Framework;
using Shouldly;

namespace CacheLiteSharp.Core.Unit.Tests.LeastRecentlyUsedCacheTests
{
    /// <summary>
    /// LeastRecentlyUsedCache exposed methods test
    /// </summary>
    [TestFixture]
    public class LeastRecentlyUsedCacheTest
    {
        private const string Key = "key";
        private const string Value = "Frequently used value from the database";

        private readonly LeastRecentlyUsedCache<string> _leastRecentlyUsedCache = new(50);

        [Test]
        public void LeastRecentlyUsedCacheSetAndGetTest()
        {
            // Act
            _leastRecentlyUsedCache.Set(Key, Value);

            // Assert
            _leastRecentlyUsedCache.Get(Key).ShouldBe(Value);
        }

        [Test]
        public void LeastRecentlyUsedCacheSizeTest()
        {
            // Act
            _leastRecentlyUsedCache.Set(Key, Value);

            // Assert
            _leastRecentlyUsedCache.Size.ShouldBe(1);
        }

        [Test]
        public void LeastRecentlyUsedCacheRemoveSingleTest()
        {
            // Act
            _leastRecentlyUsedCache.Set(Key, Value);

            _leastRecentlyUsedCache.Remove(Key);

            // Assert
            _leastRecentlyUsedCache.Size.ShouldBe(0);
            _leastRecentlyUsedCache.Get(Key).ShouldBe(null);
        }

        [Test]
        public void LeastRecentlyUsedCacheRemovedEldestEntryTest()
        {
            // Act
            for (var i = 0; i <= 51; i++)
            {
                _leastRecentlyUsedCache.Set(Key + i, Value + i);
            }

            // Assert
            _leastRecentlyUsedCache.Size.ShouldBe(50);
        }

        [Test]
        public void LeastRecentlyUsedCacheClearAllTest()
        {
            const string keyTwo = "keyTwo";
            const string valueTwo = "Another frequently used value from the database";

            _leastRecentlyUsedCache.Set(Key, Value);
            _leastRecentlyUsedCache.Set(keyTwo, valueTwo);

            // Act
            _leastRecentlyUsedCache.Size.ShouldBe(2);
            _leastRecentlyUsedCache.Clear();

            // Assert
            _leastRecentlyUsedCache.Size.ShouldBe(0);
        }

        [TearDown]
        public void TearDown()
        {
            _leastRecentlyUsedCache.Clear();
        }
    }
}