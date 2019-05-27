using System;
using System.Collections.Generic;
using CacheLiteSharp.DataStructures;
using NUnit.Framework;
using Shouldly;

namespace CacheLiteSharp.Tests.DataStructureTests
{
    /// <summary>
    /// LinkedHashMapTest data structure basic operations test
    /// </summary>
    [TestFixture]
    public class LinkedHashMapTest
    {
        private const string Key = "key";
        private const string Value = "Frequently used value from the database";

        [Test]
        public void LinkedHashMapSetAndGetTest()
        {
            // Arrange
            LinkedHashMap<string, string> keyMap = new LinkedHashMap<string, string>();

            LinkedListNode<Tuple<string, string>> value = keyMap.LinkedHashMapLinkedList.First;

            // Act
            keyMap.LinkedHashMapDictionary[Key] = value;

            // Assert
            keyMap.Count.ShouldBe(1);
            keyMap.LinkedHashMapDictionary.TryGetValue(Key, out value)
                .ShouldBeOfType(typeof(bool));
        }

        [Test]
        public void LinkedHashMapSetAndGetRepeatedKeyTest()
        {
            // Arrange
            LinkedHashMap<string, string> keyMap = new LinkedHashMap<string, string> { [Key] = Value, [Key] = Value };

            // Act

            // Assert
            keyMap.Count.ShouldBe(1);
            keyMap[Key].ShouldBe(Value);
        }

        [Test]
        public void LinkedHashMapCountTest()
        {
            // Arrange
            LinkedHashMap<string, string> keyMap = new LinkedHashMap<string, string>();

            LinkedListNode<Tuple<string, string>> value = keyMap.LinkedHashMapLinkedList.First;

            // Act
            keyMap.LinkedHashMapDictionary[Key] = value;

            // Assert
            keyMap.Count.ShouldBe(1);
        }

        [Test]
        public void LinkedHashMapContainsKeyTest()
        {
            // Arrange
            LinkedHashMap<string, string> keyMap = new LinkedHashMap<string, string>();

            LinkedListNode<Tuple<string, string>> value = keyMap.LinkedHashMapLinkedList.First;

            // Act
            keyMap.LinkedHashMapDictionary[Key] = value;

            // Assert
            keyMap.ContainsKey(Key).ShouldBe(true);
        }

        [Test]
        public void LinkedHashMapPopFirstTest()
        {
            // Arrange
            LinkedHashMap<string, string> keyMap = new LinkedHashMap<string, string> { [Key] = Value };

            LinkedListNode<Tuple<string, string>> value = keyMap.LinkedHashMapLinkedList.First;

            // Act
            keyMap.LinkedHashMapDictionary[Key] = value;

            // Assert
            keyMap.PopFirst().ShouldBe(Value);
        }

        [Test]
        public void LinkedHashMapPopFirstDefaultValueTest()
        {
            // Arrange
            LinkedHashMap<string, string> keyMap = new LinkedHashMap<string, string>();

            LinkedListNode<Tuple<string, string>> value = keyMap.LinkedHashMapLinkedList.First;

            // Act
            keyMap.LinkedHashMapDictionary[Key] = value;

            // Assert
            keyMap.PopFirst().ShouldBe(null);
        }

        [Test]
        public void LinkedHashMapClearTest()
        {
            // Arrange
            LinkedHashMap<string, string> keyMap = new LinkedHashMap<string, string>();

            LinkedListNode<Tuple<string, string>> value = keyMap.LinkedHashMapLinkedList.First;

            keyMap.LinkedHashMapDictionary[Key] = value;

            // Act
            keyMap.Clear();

            // Assert
            keyMap.Count.ShouldBe(0);
        }
    }
}