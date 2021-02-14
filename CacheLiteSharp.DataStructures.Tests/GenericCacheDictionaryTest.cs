using NUnit.Framework;
using Shouldly;

namespace CacheLiteSharp.Core.DataStructures.Unit.Tests
{
    /// <summary>
    /// GenericCacheDictionary data structure basic operations test
    /// </summary>
    [TestFixture]
    public class GenericCacheDictionaryTest
    {
        private const string Key = "key";
        private const string Value = "Frequently used value from the database";

        private readonly GenericCacheDictionary _cache = new GenericCacheDictionary();

        [Test]
        public void GenericCacheDictionaryAddAndGetTest()
        {
            // Act
            _cache.Add(Key, Value);

            // Assert
            _cache.GetValue<string>(Key).ShouldBe("Frequently used value from the database");
        }

        [Test]
        public void GenericCacheDictionarySizeTest()
        {
            // Act
            _cache.Add(Key, Value);

            // Assert
            _cache.Size.ShouldBe(1);
        }

        [Test]
        public void GenericCacheDictionaryRemoveSingleTest()
        {
            _cache.Add(Key, Value);
            _cache.Size.ShouldBe(1);
            _cache.GetValue<string>(Key).ShouldBe("Frequently used value from the database");

            // Act
            _cache.Remove<string>(Key);

            // Assert
            _cache.Size.ShouldBe(0);
            _cache.GetValue<string>(Key).ShouldBe(null);
        }

        [Test]
        public void GenericCacheDictionaryClearAllTest()
        {
            // Arrange
            const string keyTwo = "keyTwo";
            const string valueTwo = "Another frequently used value from the database";

            _cache.Add(Key, Value);
            _cache.Add(keyTwo, valueTwo);

            _cache.Size.ShouldBe(2);

            // Act
            _cache.Clear();

            // Assert
            _cache.Size.ShouldBe(0);
        }

        [TearDown]
        public void TearDown()
        {
            _cache.Clear();
        }
    }
}