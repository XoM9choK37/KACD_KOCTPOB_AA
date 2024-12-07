using System;
using Task16;

namespace Task18
{
    class MyHashMap<K, V>
    {
        private MyLinkedList<Pair<K, V>>[] table;
        private int size;
        private double loadFactor;
        public MyHashMap()
        {
            table = new MyLinkedList<Pair<K, V>>[16];
            for (int i = 0; i < 16; i++)
                table[i] = new MyLinkedList<Pair<K, V>>();
            size = 0;
            loadFactor = 0.75;
        }
        public MyHashMap(int initialCapacity)
        {
            if (initialCapacity <= 0)
                throw new ArgumentException("Initial capacity");
            table = new MyLinkedList<Pair<K, V>>[initialCapacity];
            for (int i = 0; i < initialCapacity; i++)
                table[i] = new MyLinkedList<Pair<K, V>>();
            size = 0;
            loadFactor = 0.75;
        }
        public MyHashMap(int initialCapacity, double loadFactor)
        {
            if (initialCapacity <= 0)
                throw new ArgumentException("Initial capacity");
            if (loadFactor <= 0 || 1 <= loadFactor)
                throw new ArgumentException("Load factor");
            table = new MyLinkedList<Pair<K, V>>[initialCapacity];
            for (int i = 0; i < initialCapacity; i++)
                table[i] = new MyLinkedList<Pair<K, V>>();
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
                        Console.Write("(" + table[i].Get(j).key + ": " +
                            table[i].Get(j).value + ") ");
                    Console.Write("\n");
                }
        }
        public void Clear()
        {
            table = new MyLinkedList<Pair<K, V>>[16];
            for (int i = 0; i < 16; i++)
                table[i] = new MyLinkedList<Pair<K, V>>();
            size = 0;
        }
        public bool ContainsKey(object key)
        {
            int index = GetHashIndex(key);
            if (table[index].Size() == 0)
                return false;
            Node<Pair<K, V>> p = table[index].GetFirstNode();
            while (p != null)
            {
                if (Equals(p.value.key, key))
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
                Node<Pair<K, V>> p = table[i].GetFirstNode();
                while (p != null)
                {
                    if (Equals(p.value.value, value))
                        return true;
                    p = p.next;
                }
            }
            return false;
        }
        public MyHashMap<Pair<K, V>, byte> EntrySet()
        {
            MyHashMap<Pair<K, V>, byte> set = new MyHashMap<Pair<K, V>, byte>();
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
            Node<Pair<K, V>> p = table[index].GetFirstNode();
            while (p != null)
            {
                if (Equals(p.value.key, key))
                    return p.value.value;
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
                    set.Put(table[i].Get(j).key, 0);
            return set;
        }
        public void Resize()
        {
            MyLinkedList<Pair<K, V>>[] newTable =
                new MyLinkedList<Pair<K, V>>[table.Length * 2];
            for (int i = 0; i < table.Length * 2; i++)
                newTable[i] = new MyLinkedList<Pair<K, V>>();
            int index;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i].Size() == 0)
                    continue;
                index = GetNewHashIndex
                    (table[i].GetFirstNode().value.key, newTable.Length);
                newTable[index] = table[i];
                table[i] = new MyLinkedList<Pair<K, V>>();
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
                table[index] = new MyLinkedList<Pair<K, V>>();
                Pair<K, V> pair = new Pair<K, V>(key, value);
                table[index].Add(pair);
                size++;
                return;
            }
            Node<Pair<K, V>> p = table[index].GetFirstNode();
            while (p != null)
            {
                if (Equals(p.value.key, key))
                {
                    p.value.value = value;
                    size++;
                    return;
                }
                p = p.next;
            }
            Pair<K, V> newPair = new Pair<K, V>(key, value);
            table[index].Add(newPair);
            size++;
        }
        public void Remove(object key)
        {
            int index = GetHashIndex(key);
            if (table[index].Size() == 0)
                return;
            Node<Pair<K, V>> p = table[index].GetFirstNode();
            while (p != null)
            {
                if (Equals(p.value.key, key))
                {
                    Pair<K, V> pair =
                        new Pair<K, V>((K)key, p.value.value);
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
    }
    class Pair<T1, T2>
    {
        public T1 key;
        public T2 value;
        public Pair(T1 key, T2 value)
        {
            this.key = key;
            this.value = value;
        }
        public override string ToString()
        {
            return "(" + key.ToString() + "; " +
                value.ToString() + ")";
        }
    }
}
