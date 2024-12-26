using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Task16;
using Task18;
using Task27;

namespace Task23
{
    class MyHashSet<T> : MyIterator1<T>
    {
        private MyHashMap<T, object> map;
        private IEnumerator<T> cursor;
        public bool HasNext()
        {
            IEnumerator<T> tempCursor = cursor;
            return tempCursor.MoveNext();
        }
        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException
                    ("Cursor reached the end");
            cursor.MoveNext();
            return cursor.Current;
        }
        public void Remove()
        {
            Remove(cursor.Current);
        }
        public IEnumerator<T> Iterator()
        {
            return map.KeysToList().ToArray()
                .AsEnumerable().GetEnumerator();
        }
        private void IteratorReset()
        {
            cursor = Iterator();
        }
        public MyHashSet()
        {
            map = new MyHashMap<T, object>();
            cursor = Iterator();
        }
        public MyHashSet(T[] array)
        {
            map = new MyHashMap<T, object>();
            foreach (T element in array)
                map.Put(element, 0);
            cursor = Iterator();
        }
        public MyHashSet(int initialCapacity, float loadFactor)
        {
            map = new MyHashMap<T, object>(initialCapacity, loadFactor);
            cursor = Iterator();
        }
        public MyHashSet(int initialCapacity)
        {
            map = new MyHashMap<T, object>(initialCapacity);
            cursor = Iterator();
        }
        public void Print()
        {
            MyLinkedList<T> list = map.KeysToList();
            list.Print();
        }
        public void Add(T element)
        {
            map.Put(element, 0);
            IteratorReset();
        }
        public void AddAll(T[] array)
        {
            foreach (T element in array)
                map.Put(element, 0);
        }
        public void Clear()
        {
            map.Clear();
        }
        public bool Contains(object element)
        {
            return map.ContainsKey(element);
        }
        public bool ContainsAll(T[] array)
        {
            foreach (T element in array)
                if (!map.ContainsKey(element))
                    return false;
            return true;
        }
        public bool IsEmpty()
        {
            return map.IsEmpty();
        }
        public void Remove(object element)
        {
            map.Remove(element);
            IteratorReset();
        }
        public void RemoveAll(T[] array)
        {
            foreach (T element in array)
                Remove(element);
        }
        public void RetainAll(T[] array)
        {
            MyLinkedList<T> list = map.KeysToList();
            bool flag;
            for (int i = 0; i < list.Size(); i++)
            {
                flag = false;
                for (int j = 0; j < array.Length && !flag; j++)
                    if (list.Get(i).Equals(array[j]))
                        flag = true;
                if (!flag)
                    map.Remove(list.Get(i));
            }
        }
        public int Size()
        {
            return map.Size();
        }
        public T[] ToArray()
        {
            return map.KeysToList().ToArray();
        }
        public void ToArray(ref T[] array)
        {
            if (array == null)
            {
                array = ToArray();
                return;
            }
            MyLinkedList<T> list = map.KeysToList();
            if (array.Length == Size())
            {
                for (int i = 0; i < Size(); i++)
                    array[i] = list.Get(i);
                return;
            }
            array = new T[Size()];
            for (int i = 0; i < Size(); i++)
                array[i] = list.Get(i);
        }
    }
}
