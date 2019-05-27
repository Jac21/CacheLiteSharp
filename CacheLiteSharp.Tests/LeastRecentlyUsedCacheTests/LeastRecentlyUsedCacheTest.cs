using NUnit.Framework;
using Shouldly;

namespace CacheLiteSharp.Tests.LeastRecentlyUsedCacheTests
{
    /// <summary>
    /// LeastRecentlyUsedCache exposed methods test
    /// </summary>
    [TestFixture]
    public class LeastRecentlyUsedCacheTest
    {
        private const string Key = "key";
        private const string Value = "Frequently used value from the database";

        private readonly LeastRecentlyUsedCache<string> leastRecentlyUsedCache =
            new LeastRecentlyUsedCache<string>(50);

        [Test]
        public void LeastRecentlyUsedCacheSetAndGetTest()
        {
            // Act
            leastRecentlyUsedCache.Set(Key, Value);

            // Assert
            leastRecentlyUsedCache.Get(Key).ShouldBe(Value);
        }

        [Test]
        public void LeastRecentlyUsedCacheSizeTest()
        {
            // Act
            leastRecentlyUsedCache.Set(Key, Value);

            // Assert
            leastRecentlyUsedCache.Size.ShouldBe(1);
        }

        [Test]
        public void LeastRecentlyUsedCacheRemoveSingleTest()
        {
            // Act
            leastRecentlyUsedCache.Set(Key, Value);

            leastRecentlyUsedCache.Remove(Key);

            // Assert
            leastRecentlyUsedCache.Size.ShouldBe(0);
            leastRecentlyUsedCache.Get(Key).ShouldBe(null);
        }

        [Test]
        public void LeastRecentlyUsedCacheRemovedEldestEntryTest()
        {
            // Act
            for (int i = 0; i <= 51; i++)
            {
                leastRecentlyUsedCache.Set(Key + i, Value + i);
            }

            // Assert
            leastRecentlyUsedCache.Size.ShouldBe(50);
        }

        [Test]
        public void LeastRecentlyUsedCacheClearAllTest()
        {
            const string keyTwo = "keyTwo";
            const string valueTwo = "Another frequently used value from the database";

            leastRecentlyUsedCache.Set(Key, Value);
            leastRecentlyUsedCache.Set(keyTwo, valueTwo);

            // Act
            leastRecentlyUsedCache.Size.ShouldBe(2);
            leastRecentlyUsedCache.Clear();

            // Assert
            leastRecentlyUsedCache.Size.ShouldBe(0);
        }

        [TearDown]
        public void TearDown()
        {
            leastRecentlyUsedCache.Clear();
        }
    }
}