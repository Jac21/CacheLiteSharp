using NUnit.Framework;
using Shouldly;

namespace CacheLiteSharp.Tests.PerpetualCacheTests
{
    /// <summary>
    /// PerpetualCache exposed methods test
    /// </summary>
    [TestFixture]
    public class PerpetualCacheTest
    {
        private const string Key = "key";
        private const string Value = "Frequently used value from the database";

        private readonly PerpetualCache<string> perpetualCache = new PerpetualCache<string>();

        [Test]
        public void PerpetualCacheSetAndGetTest()
        {
            // Act
            perpetualCache.Set(Key, Value);

            // Assert
            perpetualCache.Get(Key).ShouldBe(Value);
        }

        [Test]
        public void PerpetualCacheSizeTest()
        {
            // Act
            perpetualCache.Set(Key, Value);

            // Assert
            perpetualCache.Size.ShouldBe(1);
        }

        [Test]
        public void PerpetualCacheRemoveSingleTest()
        {
            // Act
            perpetualCache.Set(Key, Value);

            perpetualCache.Remove(Key);

            // Assert
            perpetualCache.Size.ShouldBe(0);
            perpetualCache.Get(Key).ShouldBe(null);
        }

        [Test]
        public void PerpetualCacheClearAllTest()
        {
            // Arrange
            const string keyTwo = "keyTwo";
            const string valueTwo = "Another frequently used value from the database";

            perpetualCache.Set(Key, Value);
            perpetualCache.Set(keyTwo, valueTwo);

            // Act
            perpetualCache.Size.ShouldBe(2);
            perpetualCache.Clear();

            // Assert
            perpetualCache.Size.ShouldBe(0);
        }

        [TearDown]
        public void TearDown()
        {
            perpetualCache.Clear();
        }
    }
}