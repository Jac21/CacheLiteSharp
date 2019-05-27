using System;
using System.Collections.Generic;

namespace CacheLiteSharp.DataStructures
{
    /// <summary>
    /// Basic Java/Kotlin-like LinkedHashMap data-structure, used to back particular facets
    /// of the LeastRecentlyUsedCache class
    /// </summary>
    /// <remarks>
    /// https://stackoverflow.com/questions/29205934/c-sharp-equivalent-of-linkedhashmap
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TU"></typeparam>
    public class LinkedHashMap<T, TU>
    {
        public readonly Dictionary<T, LinkedListNode<Tuple<TU, T>>> LinkedHashMapDictionary =
            new Dictionary<T, LinkedListNode<Tuple<TU, T>>>();

        public readonly LinkedList<Tuple<TU, T>> LinkedHashMapLinkedList = new LinkedList<Tuple<TU, T>>();

        public int Count => LinkedHashMapDictionary.Count;

        public TU this[T c]
        {
            get => LinkedHashMapDictionary[c].Value.Item1;

            set
            {
                if (LinkedHashMapDictionary.ContainsKey(c))
                {
                    LinkedHashMapLinkedList.Remove(LinkedHashMapDictionary[c]);
                }

                LinkedHashMapDictionary[c] = new LinkedListNode<Tuple<TU, T>>(Tuple.Create(value, c));
                LinkedHashMapLinkedList.AddLast(LinkedHashMapDictionary[c]);
            }
        }

        public bool ContainsKey(T k)
        {
            return LinkedHashMapDictionary.ContainsKey(k);
        }

        public TU PopFirst()
        {
            if (LinkedHashMapLinkedList.Count != 0)
            {
                var node = LinkedHashMapLinkedList.First;
                LinkedHashMapLinkedList.Remove(node);
                LinkedHashMapDictionary.Remove(node.Value.Item2);
                return node.Value.Item1;
            }

            return default(TU);
        }

        public void Clear()
        {
            LinkedHashMapDictionary.Clear();
            LinkedHashMapLinkedList.Clear();
        }
    }
}