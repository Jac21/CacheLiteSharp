using System;
using System.Threading;
using NUnit.Framework;
using Shouldly;

namespace CacheLiteSharp.Tests.ExpirableCacheTests
{
    /// <summary>
    /// ExpirableCache exposed methods test
    /// </summary>
    [TestFixture]
    public class ExpirableCacheTest
    {
        private const string Key = "key";
        private const string Value = "Frequently used value from the database";

        private ExpirableCache<string> expirableCache;

        [Test]
        public void ExpirableCacheSetAndGetTest()
        {
            // Arrange
            expirableCache = new ExpirableCache<string>(1);

            // Act
            expirableCache.Set(Key, Value);

            // Assert
            expirableCache.Get(Key).ShouldBe(Value);
        }

        [Test]
        public void ExpirableCacheSetAndGetPastFlushIntervalTest()
        {
            // Arrange
            expirableCache = new ExpirableCache<string>(0.001);

            // Act
            expirableCache.Set(Key, Value);

            Thread.Sleep((int)TimeSpan.FromSeconds(0.06).TotalMilliseconds);

            // Assert
            expirableCache.Get(Key).ShouldBe(null);
        }

        [Test]
        public void ExpirableCacheSizeTest()
        {
            // Arrange
            expirableCache = new ExpirableCache<string>(1);

            // Act
            expirableCache.Set(Key, Value);

            // Assert
            expirableCache.Size.ShouldBe(1);
        }

        [Test]
        public void ExpirableCacheRemoveSingleTest()
        {
            // Arrange
            expirableCache = new ExpirableCache<string>(1);

            // Act
            expirableCache.Set(Key, Value);

            expirableCache.Remove(Key);

            // Assert
            expirableCache.Size.ShouldBe(0);
            expirableCache.Get(Key).ShouldBe(null);
        }

        [Test]
        public void ExpirableCacheClearAllTest()
        {
            // Arrange
            expirableCache = new ExpirableCache<string>(1);

            const string keyTwo = "keyTwo";
            const string valueTwo = "Another frequently used value from the database";

            expirableCache.Set(Key, Value);
            expirableCache.Set(keyTwo, valueTwo);

            // Act
            expirableCache.Size.ShouldBe(2);
            expirableCache.Clear();

            // Assert
            expirableCache.Size.ShouldBe(0);
        }

        [TearDown]
        public void TearDown()
        {
            expirableCache.Clear();
        }
    }
}