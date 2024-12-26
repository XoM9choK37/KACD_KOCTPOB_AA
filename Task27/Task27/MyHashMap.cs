using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Task16;
using Task27;

namespace Task18
{
    class MyHashMap<K, V>
    {
        private MyLinkedList<Tuple<K, V>>[] table;
        private int size;
        private double loadFactor;
        public MyHashMap()
        {
            table = new MyLinkedList<Tuple<K, V>>[16];
            for (int i = 0; i < 16; i++)
                table[i] = new MyLinkedList<Tuple<K, V>>();
            size = 0;
            loadFactor = 0.75;
        }
        public MyHashMap(int initialCapacity)
        {
            if (initialCapacity <= 0)
                throw new ArgumentException("Initial capacity");
            table = new MyLinkedList<Tuple<K, V>>[initialCapacity];
            for (int i = 0; i < initialCapacity; i++)
                table[i] = new MyLinkedList<Tuple<K, V>>();
            size = 0;
            loadFactor = 0.75;
        }
        public MyHashMap(int initialCapacity, double loadFactor)
        {
            if (initialCapacity <= 0)
                throw new ArgumentException("Initial capacity");
            if (loadFactor <= 0 || 1 <= loadFactor)
                throw new ArgumentException("Load factor");
            table = new MyLinkedList<Tuple<K, V>>[initialCapacity];
            for (int i = 0; i < initialCapacity; i++)
                table[i] = new MyLinkedList<Tuple<K, V>>();
            size = 0;
            this.loadFactor = 0.75;
        }
        private int GetHashIndex(object obj)
        {
            return Math.Abs(obj.GetHashCode()) % table.Length;
        }
        private int GetNewHashIndex(object obj, int module)
        {
            return Math.Abs(obj.GetHashCode()) % module;
        }
        public void Print()
        {
            for (int i = 0; i < table.Length; i++)
                if (table[i].Size() != 0)
                {
                    for (int j = 0; j < table[i].Size(); j++)
                        Console.Write("(" + table[i].Get(j).Item1 + ": " +
                            table[i].Get(j).Item2 + ") ");
                    Console.Write("\n");
                }
        }
        public void Clear()
        {
            table = new MyLinkedList<Tuple<K, V>>[16];
            for (int i = 0; i < 16; i++)
                table[i] = new MyLinkedList<Tuple<K, V>>();
            size = 0;
        }
        public bool ContainsKey(object key)
        {
            int index = GetHashIndex(key);
            if (table[index].Size() == 0)
                return false;
            Node<Tuple<K, V>> p = table[index].GetFirstNode();
            while (p != null)
            {
                if (p.value.Item1.Equals(key))
                    return true;
                p = p.next;
            }
            return false;
        }
        public bool ContainsValue(object value)
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i].Size() == 0)
                    continue;
                Node<Tuple<K, V>> p = table[i].GetFirstNode();
                while (p != null)
                {
                    if (p.value.Item2.Equals(value))
                        return true;
                    p = p.next;
                }
            }
            return false;
        }
        public MyHashMap<Tuple<K, V>, byte> EntrySet()
        {
            MyHashMap<Tuple<K, V>, byte> set = new MyHashMap<Tuple<K, V>, byte>();
            for (int i = 0; i < table.Length; i++)
                for (int j = 0; j < table[i].Size(); j++)
                    set.Put(table[i].Get(j), 0);
            return set;
        }
        public V Get(object key)
        {
            int index = GetHashIndex(key);
            if (table[index].Size() == 0)
                return default;
            Node<Tuple<K, V>> p = table[index].GetFirstNode();
            while (p != null)
            {
                if (p.value.Item1.Equals(key))
                    return p.value.Item2;
                p = p.next;
            }
            return default;
        }
        public bool IsEmpty()
        {
            return size == 0;
        }
        public MyHashMap<K, byte> KeySet()
        {
            MyHashMap<K, byte> set = new MyHashMap<K, byte>();
            for (int i = 0; i < table.Length; i++)
                for (int j = 0; j < table[i].Size(); j++)
                    set.Put(table[i].Get(j).Item1, 0);
            return set;
        }
        private void Resize()
        {
            MyLinkedList<Tuple<K, V>>[] newTable =
                new MyLinkedList<Tuple<K, V>>[table.Length * 2];
            for (int i = 0; i < table.Length * 2; i++)
                newTable[i] = new MyLinkedList<Tuple<K, V>>();
            int index;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i].Size() == 0)
                    continue;
                index = GetNewHashIndex
                    (table[i].GetFirstNode().value.Item1, newTable.Length);
                newTable[index] = table[i];
                table[i] = new MyLinkedList<Tuple<K, V>>();
            }
            table = newTable;
        }
        public void Put(K key, V value)
        {
            if ((double)size / table.Length > loadFactor)
                Resize();
            int index = GetHashIndex(key);
            if (table[index].Size() == 0)
            {
                table[index] = new MyLinkedList<Tuple<K, V>>();
                Tuple<K, V> pair = new Tuple<K, V>(key, value);
                table[index].Add(pair);
                size++;
                return;
            }
            Node<Tuple<K, V>> p = table[index].GetFirstNode();
            while (p != null)
            {
                if (p.value.Item1.Equals(key))
                {
                    p.value = new Tuple<K, V>(p.value.Item1, value);
                    size++;
                    return;
                }
                p = p.next;
            }
            Tuple<K, V> newTuple = new Tuple<K, V>(key, value);
            table[index].Add(newTuple);
            size++;
        }
        public void Remove(object key)
        {
            int index = GetHashIndex(key);
            if (table[index].Size() == 0)
                return;
            Node<Tuple<K, V>> p = table[index].GetFirstNode();
            while (p != null)
            {
                if (p.value.Item1.Equals(key))
                {
                    Tuple<K, V> pair =
                        new Tuple<K, V>((K)key, p.value.Item2);
                    table[index].Remove(pair);
                    size--;
                    return;
                }
                p = p.next;
            }
        }
        public int Size()
        {
            return size;
        }
        public MyLinkedList<K> KeysToList()
        {
            MyLinkedList<K> list = new MyLinkedList<K>();
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i].Size() == 0)
                    continue;
                Node<Tuple<K, V>> p = table[i].GetFirstNode();
                while (p != null)
                {
                    list.Add(p.value.Item1);
                    p = p.next;
                }
            }
            return list;
        }
    }
}
