using System;
using System.Collections.Generic;
using Task16;
using Task18;
using Task27;

namespace Task21
{
    class MyTreeMap<K, V> : MyNavigableMap<K, V>
        where K : IComparable<K>
    {
        private Comparer<K> comparator;
        private Node<Tuple<K, V>> root;
        private int size;
        public MyTreeMap()
        {
            comparator = Comparer<K>.Default;
            root = null;
            size = 0;
        }
        public MyTreeMap(Comparer<K> comparator)
        {
            this.comparator = comparator;
            root = null;
            size = 0;
        }
        public MyTreeMap(MyMap<K, V> map)
        {
            PutAll(map);
        }
        public MyTreeMap(MySortedMap<K, V> map)
        {
            PutAll(map);
        }
        public void Print()
        {
            RecursionPrint(root);
            Console.Write("\n");
        }
        private void RecursionPrint(Node<Tuple<K, V>> p)
        {
            if (p != null)
            {
                RecursionPrint(p.left);
                Console.Write("(" + p.value.Item1 + ": " +
                    p.value.Item2 + ") ");
                RecursionPrint(p.right);
            }
        }
        public void Clear()
        {
            root = null;
            size = 0;
        }
        public bool ContainsKey(object key)
        {
            Node<Tuple<K, V>> p = root;
            while (p != null)
            {
                if (comparator.Compare((K)key, p.value.Item1) == 0)
                    return true;
                if (comparator.Compare((K)key, p.value.Item1) > 0)
                    p = p.right;
                else
                    p = p.left;
            }
            return false;
        }
        public bool ContainsValue(object value)
        {
            bool flag = false;
            RecursionContainsValue(value, ref flag, root);
            return flag;
        }
        private void RecursionContainsValue(object value,
            ref bool flag, Node<Tuple<K, V>> p)
        {
            if (p != null)
            {
                if (Equals(value, p.value.Item2))
                {
                    flag = true;
                    return;
                }
                RecursionContainsValue(value, ref flag, p.left);
                RecursionContainsValue(value, ref flag, p.right);
            }
        }
        public MyHashMap<Tuple<K, V>, object> EntrySet()
        {
            MyHashMap<Tuple<K, V>, object> set = new MyHashMap<Tuple<K, V>, object>();
            RecursionEntrySet(set, root);
            return set;
        }
        private void RecursionEntrySet(MyHashMap<Tuple<K, V>, object> set,
            Node<Tuple<K, V>> p)
        {
            if (p != null)
            {
                RecursionEntrySet(set, p.left);
                Tuple<K, V> pair = new Tuple<K, V>(p.value.Item1, p.value.Item2);
                set.Put(pair, 0);
                RecursionEntrySet(set, p.right);
            }
        }
        public V Get(object key)
        {
            Node<Tuple<K, V>> p = root;
            while (p != null)
            {
                if (comparator.Compare((K)key, p.value.Item1) == 0)
                    return p.value.Item2;
                if (comparator.Compare((K)key, p.value.Item1) > 0)
                    p = p.right;
                else
                    p = p.left;
            }
            return default;
        }
        public bool IsEmpty()
        {
            return size == 0;
        }
        public MyHashMap<K, object> KeySet()
        {
            MyHashMap<K, object> set = new MyHashMap<K, object>();
            RecursionKeySet(set, root);
            return set;
        }
        private void RecursionKeySet(MyHashMap<K, object> set,
            Node<Tuple<K, V>> p)
        {
            if (p != null)
            {
                RecursionKeySet(set, p.left);
                set.Put(p.value.Item1, 0);
                RecursionKeySet(set, p.right);
            }
        }
        public void Put(K key, V value)
        {
            Tuple<K, V> pair = new Tuple<K, V>(key, value);
            if (root == null)
            {
                root = new Node<Tuple<K, V>>(pair, null, null);
                size++;
                return;
            }
            Node<Tuple<K, V>> p = root;
            Node<Tuple<K, V>> q;
            do
            {
                q = p;
                if (comparator.Compare(pair.Item1, p.value.Item1) == 0)
                {
                    p.value = pair;
                    return;
                }
                if (comparator.Compare(pair.Item1, p.value.Item1) > 0)
                    p = p.right;
                else
                    p = p.left;
            }
            while (p != null);
            p = new Node<Tuple<K, V>>(pair, null, null);
            if (comparator.Compare(pair.Item1, q.value.Item1) > 0)
                q.right = p;
            else
                q.left = p;
            size++;
        }
        public void Remove(object key)
        {
            if (comparator.Compare((K)key, root.value.Item1) == 0)
            {
                if (root.left == null && root.right == null)
                {
                    root = null;
                    size--;
                    return;
                }
                if (root.right == null)
                {
                    root = root.left;
                    size--;
                    return;
                }
                if (root.left == null)
                {
                    root = root.right;
                    size--;
                    return;
                }
                Node<Tuple<K, V>> r = root.right;
                while (r.left != null)
                    r = r.left;
                r.left = root.left;
                root = root.right;
                size--;
                return;
            }
            Node<Tuple<K, V>> p = root;
            Node<Tuple<K, V>> q = root;
            bool isRight = false;
            if (comparator.Compare((K)key, p.value.Item1) > 0)
            {
                p = p.right;
                isRight = true;
            }
            else
            {
                p = p.left;
                isRight = false;
            }
            while (p != null)
            {                
                if (comparator.Compare((K)key, p.value.Item1) == 0)
                {
                    if (p.left == null && p.right == null)
                    {
                        if (isRight)
                        {
                            q.right = null;
                            size--;
                            return;
                        }
                        q.left = null;
                        size--;
                        return;
                    }
                    if (p.right == null)
                    {
                        if (isRight)
                        {
                            q.right = p.left;
                            size--;
                            return;
                        }
                        q.left = p.left;
                        size--;
                        return;
                    }
                    if (p.left == null)
                    {
                        if (isRight)
                        {
                            q.left = p.right;
                            size--;
                            return;
                        }
                        q.right = p.right;
                        size--;
                        return;
                    }
                    Node<Tuple<K, V>> r = p.right;
                    while (r.left != null)
                        r = r.left;
                    r.left = p.left;
                    if (isRight)
                        q.right = p.right;
                    else
                        q.left = p.right;
                    size--;
                    return;
                }
                if (comparator.Compare((K)key, p.value.Item1) > 0)
                {
                    p = p.right;
                    if (isRight)
                        q = q.right;
                    else
                        q = q.left;
                    isRight = true;
                }
                else
                {
                    p = p.left;
                    if (isRight)
                        q = q.right;
                    else
                        q = q.left;
                    isRight = false;
                }
            }
        }
        public int Size()
        {
            return size;
        }
        public MyTreeMap<K, V> HeadMap(K end)
        {
            MyTreeMap<K, V> treeMap = new MyTreeMap<K, V>();
            RecursionHeadMap(treeMap, end, root);
            return treeMap;
        }
        private void RecursionHeadMap(MyTreeMap<K, V> treeMap, K end,
            Node<Tuple<K, V>> p)
        {
            if (comparator.Compare(p.value.Item1, end) < 0 &&
                p != null)
            {
                RecursionHeadMap(treeMap, end, p.left);
                treeMap.Put(p.value.Item1, p.value.Item2);
                RecursionHeadMap(treeMap, end, p.right);
            }
        }
        public MyTreeMap<K, V> SubMap(K start, K end)
        {
            MyTreeMap<K, V> treeMap = new MyTreeMap<K, V>();
            RecursionSubMap(treeMap, start, end, root);
            return treeMap;
        }
        private void RecursionSubMap(MyTreeMap<K, V> treeMap, K start, K end,
            Node<Tuple<K, V>> p)
        {
            if (comparator.Compare(p.value.Item1, start) >= 0 &&
                comparator.Compare(p.value.Item1, end) < 0 &&
                p != null)
            {
                RecursionSubMap(treeMap, start, end, p.left);
                treeMap.Put(p.value.Item1, p.value.Item2);
                RecursionSubMap(treeMap, start, end, p.right);
            }
        }
        public MyTreeMap<K, V> TailMap(K start)
        {
            MyTreeMap<K, V> treeMap = new MyTreeMap<K, V>();
            RecursionTailMap(treeMap, start, root);
            return treeMap;
        }
        private void RecursionTailMap(MyTreeMap<K, V> treeMap, K start,
            Node<Tuple<K, V>> p)
        {
            if (comparator.Compare(p.value.Item1, start) >= 0 &&
                p != null)
            {
                RecursionTailMap(treeMap, start, p.left);
                treeMap.Put(p.value.Item1, p.value.Item2);
                RecursionTailMap(treeMap, start, p.right);
            }
        }
        public Tuple<K, V> LowerEntry(K key)
        {
            Node<Tuple<K, V>> p = root;
            while (p != null)
            {
                if (comparator.Compare(p.value.Item1, key) < 0)
                    return p.value;
                p = p.left;
            }
            throw new InvalidOperationException("Element has not found");
        }
        public Tuple<K, V> FloorEntry(K key)
        {
            Node<Tuple<K, V>> p = root;
            while (p != null)
            {
                if (comparator.Compare(p.value.Item1, key) <= 0)
                    return p.value;
                p = p.left;
            }
            throw new InvalidOperationException("Element has not found");
        }
        public Tuple<K, V> HigherEntry(K key)
        {
            Node<Tuple<K, V>> p = root;
            while (p != null)
            {
                if (comparator.Compare(p.value.Item1, key) > 0)
                    return p.value;
                p = p.right;
            }
            throw new InvalidOperationException("Element has not found");
        }
        public Tuple<K, V> CeilingEntry(K key)
        {
            Node<Tuple<K, V>> p = root;
            while (p != null)
            {
                if (comparator.Compare(p.value.Item1, key) >= 0)
                    return p.value;
                p = p.right;
            }
            throw new InvalidOperationException("Element has not found");
        }
        public K LowerKey(K key)
        {
            return LowerEntry(key).Item1;
        }
        public K FloorKey(K key)
        {
            return FloorEntry(key).Item1;
        }
        public K HigherKey(K key)
        {
            return HigherEntry(key).Item1;
        }
        public K CeilingKey(K key)
        {
            return CeilingEntry(key).Item1;            
        }
        public Tuple<K, V> PollFirstEntry()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Tree is empty");
            Node<Tuple<K, V>> p = root;
            Tuple<K, V> pair;
            while (p.left != null)
                p = p.left;
            pair = p.value;
            Remove(pair.Item1);
            return pair;
        }
        public Tuple<K, V> PollLastEntry()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Tree is empty");
            Node<Tuple<K, V>> p = root;
            Tuple<K, V> pair;
            while (p.right != null)
                p = p.right;
            pair = p.value;
            Remove(pair.Item1);
            return pair;
        }
        public Tuple<K, V> FirstEntry()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Tree is empty");
            Node<Tuple<K, V>> p = root;
            while (p.left != null)
                p = p.left;
            return p.value;
        }
        public Tuple<K, V> LastEntry()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Tree is empty");
            Node<Tuple<K, V>> p = root;
            while (p.right != null)
                p = p.right;
            return p.value;
        }
        public K FirstKey()
        {
            return FirstEntry().Item1;
        }
        public K LastKey()
        {
            return LastEntry().Item1;
        }
        private MyLinkedList<V> ValuesList()
        {
            var list = new MyLinkedList<V>();
            foreach (var tuple in EntrySet().EntryArray())
                list.Add(tuple.Item1.Item2);
            return list;
        }
        public void PutAll(MyMap<K, V> map)
        {
            var array = map.EntrySet().EntryArray();
            foreach (var tuple in array)
                Put(tuple.Item1.Item1, tuple.Item1.Item2);
        }
        public MyCollection<V> Values()
        {
            return ValuesList();
        }
    }
    class Node<T>
    {
        public T value;
        public Node<T> left;
        public Node<T> right;
        public Node()
        {
            value = default;
            left = null;
            right = null;
        }
        public Node(T value, Node<T> left, Node<T> right)
        {
            this.value = value;
            this.left = left;
            this.right = right;
        }
    }
}
