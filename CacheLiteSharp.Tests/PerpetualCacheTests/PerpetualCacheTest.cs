using NUnit.Framework;
using Shouldly;

namespace CacheLiteSharp.Core.Unit.Tests.PerpetualCacheTests
{
    /// <summary>
    /// PerpetualCache exposed methods test
    /// </summary>
    [TestFixture]
    public class PerpetualCacheTest
    {
        private const string Key = "key";
        private const string Value = "Frequently used value from the database";

        private readonly PerpetualCache<string> _perpetualCache = new PerpetualCache<string>();

        [Test]
        public void PerpetualCacheSetAndGetTest()
        {
            // Act
            _perpetualCache.Set(Key, Value);

            // Assert
            _perpetualCache.Get(Key).ShouldBe(Value);
        }

        [Test]
        public void PerpetualCacheSizeTest()
        {
            // Act
            _perpetualCache.Set(Key, Value);

            // Assert
            _perpetualCache.Size.ShouldBe(1);
        }

        [Test]
        public void PerpetualCacheRemoveSingleTest()
        {
            // Act
            _perpetualCache.Set(Key, Value);

            _perpetualCache.Remove(Key);

            // Assert
            _perpetualCache.Size.ShouldBe(0);
            _perpetualCache.Get(Key).ShouldBe(null);
        }

        [Test]
        public void PerpetualCacheClearAllTest()
        {
            // Arrange
            const string keyTwo = "keyTwo";
            const string valueTwo = "Another frequently used value from the database";

            _perpetualCache.Set(Key, Value);
            _perpetualCache.Set(keyTwo, valueTwo);

            // Act
            _perpetualCache.Size.ShouldBe(2);
            _perpetualCache.Clear();

            // Assert
            _perpetualCache.Size.ShouldBe(0);
        }

        [TearDown]
        public void TearDown()
        {
            _perpetualCache.Clear();
        }
    }
}