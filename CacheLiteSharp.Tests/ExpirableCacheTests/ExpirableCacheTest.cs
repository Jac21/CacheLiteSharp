using System;
using System.Threading;
using NUnit.Framework;
using Shouldly;

namespace CacheLiteSharp.Core.Unit.Tests.ExpirableCacheTests
{
    /// <summary>
    /// ExpirableCache exposed methods test
    /// </summary>
    [TestFixture]
    public class ExpirableCacheTest
    {
        private const string Key = "key";
        private const string Value = "Frequently used value from the database";

        private ExpirableCache<string> _expirableCache;

        [Test]
        public void ExpirableCacheSetAndGetTest()
        {
            // Arrange
            _expirableCache = new ExpirableCache<string>(1);

            // Act
            _expirableCache.Set(Key, Value);

            // Assert
            _expirableCache.Get(Key).ShouldBe(Value);
        }

        [Test]
        public void ExpirableCacheSetAndGetPastFlushIntervalTest()
        {
            // Arrange
            _expirableCache = new ExpirableCache<string>(0.001);

            // Act
            _expirableCache.Set(Key, Value);

            Thread.Sleep((int) TimeSpan.FromSeconds(0.06).TotalMilliseconds);

            // Assert
            _expirableCache.Get(Key).ShouldBe(null);
        }

        [Test]
        public void ExpirableCacheSizeTest()
        {
            // Arrange
            _expirableCache = new ExpirableCache<string>(1);

            // Act
            _expirableCache.Set(Key, Value);

            // Assert
            _expirableCache.Size.ShouldBe(1);
        }

        [Test]
        public void ExpirableCacheRemoveSingleTest()
        {
            // Arrange
            _expirableCache = new ExpirableCache<string>(1);

            // Act
            _expirableCache.Set(Key, Value);

            _expirableCache.Remove(Key);

            // Assert
            _expirableCache.Size.ShouldBe(0);
            _expirableCache.Get(Key).ShouldBe(null);
        }

        [Test]
        public void ExpirableCacheClearAllTest()
        {
            // Arrange
            _expirableCache = new ExpirableCache<string>(1);

            const string keyTwo = "keyTwo";
            const string valueTwo = "Another frequently used value from the database";

            _expirableCache.Set(Key, Value);
            _expirableCache.Set(keyTwo, valueTwo);

            // Act
            _expirableCache.Size.ShouldBe(2);
            _expirableCache.Clear();

            // Assert
            _expirableCache.Size.ShouldBe(0);
        }

        [TearDown]
        public void TearDown()
        {
            _expirableCache.Clear();
        }
    }
}