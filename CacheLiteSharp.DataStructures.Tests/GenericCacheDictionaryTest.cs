using CacheLiteSharp.DataStructures;
using NUnit.Framework;
using Shouldly;

namespace CacheLiteSharp.Tests.DataStructureTests
{
    /// <summary>
    /// GenericCacheDictionary data structure basic operations test
    /// </summary>
    [TestFixture]
    public class GenericCacheDictionaryTest
    {
        private const string Key = "key";
        private const string Value = "Frequently used value from the database";

        private readonly GenericCacheDictionary cache = new GenericCacheDictionary();

        [Test]
        public void GenericCacheDictionaryAddAndGetTest()
        {
            // Act
            cache.Add(Key, Value);

            // Assert
            cache.GetValue<string>(Key).ShouldBe("Frequently used value from the database");
        }

        [Test]
        public void GenericCacheDictionarySizeTest()
        {
            // Act
            cache.Add(Key, Value);

            // Assert
            cache.Size.ShouldBe(1);
        }

        [Test]
        public void GenericCacheDictionaryRemoveSingleTest()
        {
            cache.Add(Key, Value);
            cache.Size.ShouldBe(1);
            cache.GetValue<string>(Key).ShouldBe("Frequently used value from the database");

            // Act
            cache.Remove<string>(Key);

            // Assert
            cache.Size.ShouldBe(0);
            cache.GetValue<string>(Key).ShouldBe(null);
        }

        [Test]
        public void GenericCacheDictionaryClearAllTest()
        {
            // Arrange
            const string keyTwo = "keyTwo";
            const string valueTwo = "Another frequently used value from the database";

            cache.Add(Key, Value);
            cache.Add(keyTwo, valueTwo);

            cache.Size.ShouldBe(2);

            // Act
            cache.Clear();

            // Assert
            cache.Size.ShouldBe(0);
        }

        [TearDown]
        public void TearDown()
        {
            cache.Clear();
        }
    }
}