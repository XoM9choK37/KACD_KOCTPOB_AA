using System;
using System.Collections.Generic;
using System.Linq;
using Task27;

namespace Task16
{
    class MyLinkedList<T> : MyIterator2<T>, MyList<T>
    {
        private Node<T> first;
        private Node<T> last;
        private int size;
        private IEnumerator<T> cursor;
        private int index = -1;
        public bool HasNext()
        {
            return -1 <= index && index <= Size() - 2;
        }
        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException
                    ("Cursor reached the end");
            cursor.MoveNext();
            index++;
            return cursor.Current;
        }
        public bool HasPrevious()
        {
            return 1 <= index && index <= Size();
        }
        public T Previous()
        {
            if (!HasPrevious())
                throw new InvalidOperationException
                    ("Cursor at the beginning");
            cursor = ListIterator();
            for (int i = 0; i < index; i++)
                cursor.MoveNext();
            return cursor.Current;
        }
        public int NextIndex()
        {
            if (index >= Size() - 1)
                throw new InvalidOperationException
                    ("Cursor reached the end");
            return index + 1;
        }
        public int PreviousIndex()
        {
            if (index <= 0)
                throw new InvalidOperationException
                    ("Cursor at the beginning");
            return index - 1;
        }
        public void Remove()
        {
            Remove(cursor.Current);
        }
        public void Set(T element)
        {
            if (index < 0 || index > Size() - 1)
                throw new IndexOutOfRangeException("Cursor out of range");
            Set(index, element);
        }
        public void ItAdd(T element)
        {
            Add(index + 1, element);
            Next();
        }
        public IEnumerator<T> ListIterator()
        {
            return ToArray().AsEnumerable().GetEnumerator();
        }
        public IEnumerator<T> ListIterator(int index)
        {
            IEnumerator<T> newCursor = ListIterator();
            for (int i = 0; i < index + 1; i++)
                newCursor.MoveNext();
            return newCursor;
        }
        private void IteratorReset()
        {
            cursor = ListIterator(index);
        }
        public MyLinkedList()
        {
            first = null;
            last = null;
            size = 0;
            cursor = ListIterator();
        }
        public MyLinkedList(MyCollection<T> collection)
        {
            T[] A = collection.ToArray();
            if (A.Length == 0)
            {
                first = null;
                last = null;
                size = 0;
                cursor = ListIterator();
                return;
            }
            if (A.Length == 1)
            {
                Node<T> node = new Node<T>(A[0], null, null);
                first = node;
                last = node;
                size = 1;
                cursor = ListIterator();
                return;
            }
            Node<T> begin = new Node<T>(A[0], null, null);
            Node<T> p = begin;
            Node<T> q = new Node<T>();
            Node<T> end;
            int i = 1;
            while (i < A.Length)
            {
                q = new Node<T>(A[i], null, p);
                p.next = q;
                p = q;
                i++;
            }
            end = q;
            first = begin;
            last = end;
            size = A.Length;
            cursor = ListIterator();
        }
        public void Print()
        {
            Node<T> p = first;
            while (p != null)
            {
                Console.Write(p.value + " ");
                p = p.next;
            }
            Console.Write('\n');
        }
        public void Add(T element)
        {
            Node<T> node = new Node<T>(element, null, last);
            if (size == 0)
            {
                first = last = node;
                size++;
                IteratorReset();
                return;
            }
            last.next = node;
            last = node;
            size++;
            IteratorReset();
        }
        public void AddAll(MyCollection<T> collection)
        {
            T[] array = collection.ToArray();
            for (int i = 0; i < array.Length; i++)
                Add(array[i]);
        }
        public void Clear()
        {
            first = null;
            last = null;
            size = 0;
        }
        public bool Contains(object obj)
        {
            Node<T> p = first;
            while (p != null)
            {
                if (p.value.Equals(obj))
                    return true;
                p = p.next;
            }
            return false;
        }
        public bool ContainsAll(MyCollection<T> collection)
        {
            T[] array = collection.ToArray();
            for (int i = 0; i < array.Length; i++)
                if (!Contains(array[i]))
                    return false;
            return true;
        }
        public bool IsEmpty()
        {
            return size == 0;
        }
        public void Remove(object obj)
        {
            if (first == null)
                return;
            Node<T> p = first.next;
            if (p != null)
                while (p.next != null)
                {
                    if (p.value.Equals(obj))
                    {
                        p.prev.next = p.next;
                        p.next.prev = p.prev;
                        size--;
                    }
                    p = p.next;
                }
            if (first.value.Equals(obj)) {
                first = first.next;
                if (first != null)
                    first.prev = null;
                size--;
            }
            if (last.value.Equals(obj))
            {
                last = last.prev;
                if (last != null)
                    last.next = null;
                size--;
            }
            if (first == null || last == null)
                first = last = null;
            IteratorReset();
        }
        public void RemoveAll(MyCollection<T> collection)
        {
            T[] array = collection.ToArray();
            for (int i = 0; i < array.Length; i++)
                Remove(array[i]);
        }
        public void RetainAll(MyCollection<T> collection)
        {
            T[] array = collection.ToArray();
            Node<T> p = first;
            bool flag;
            while (p != null)
            {
                flag = false;
                for (int i = 0; i < array.Length && !flag; i++)
                    if (array[i].Equals(p.value))
                        flag = true;
                if (!flag)
                    Remove(p.value);
                p = p.next;
            }
        }
        public int Size()
        {
            return size;
        }
        public T[] ToArray()
        {
            T[] array = new T[size];
            Node<T> p = first;
            int i = 0;
            while (p != null)
            {
                array[i] = p.value;
                i++;
                p = p.next;
            }
            return array;
        }
        public void ToArray(ref T[] array)
        {
            if (array == null)
            {
                array = ToArray();
                return;
            }
            Node<T> p = first;
            int i = 0;
            if (array.Length == size)
            {
                while (p != null)
                {
                    array[i] = p.value;
                    i++;
                    p = p.next;
                }
                return;
            }
            array = new T[size];
            while (p != null)
            {
                array[i] = p.value;
                i++;
                p = p.next;
            }
        }
        public void Add(int index, T element)
        {
            if (index < 0 || index > size)
                throw new ArgumentOutOfRangeException("Index");
            if (index == 0)
            {
                Node<T> node = new Node<T>(element, first, null);
                if (first != null)
                    first.prev = node;
                first = node;
                if (last == null)
                    last = first;
                size++;
                IteratorReset();
                return;
            }
            if (index == size)
            {
                Add(element);
                return;
            }
            Node<T> p = first;
            for (int i = 0; i < index; i++)
                p = p.next;
            Node<T> newNode = new Node<T>(element, p, p.prev);
            p.prev.next = newNode;
            p.prev = newNode;
            size++;
            IteratorReset();
        }
        public void AddAll(int index, MyCollection<T> collection)
        {
            T[] array = collection.ToArray();
            if (index < 0 || index > size)
                throw new ArgumentOutOfRangeException("Index");
            for (int i = array.Length - 1; i >= 0; i--)
                Add(index, array[i]);
        }
        public T Get(int index)
        {
            if (index < 0 || index > size - 1)
                throw new ArgumentOutOfRangeException("Index");
            Node<T> p = first;
            for (int i = 0; i < index; i++)
                p = p.next;
            return p.value;
        }
        public int IndexOf(object obj)
        {
            Node<T> p = first;
            int index = 0;
            while (p != null)
            {
                if (p.value.Equals(obj))
                    return index;
                index++;
                p = p.next;
            }
            return -1;
        }
        public int LastIndexOf(object obj)
        {
            Node<T> p = last;
            int index = size - 1;
            while (p != null)
            {
                if (p.value.Equals(obj))
                    return index;
                index--;
                p = p.prev;
            }
            return -1;
        }
        public T RemoveAt(int index)
        {
            if (index < 0 || index > size - 1)
                throw new ArgumentOutOfRangeException("Index");
            if (index == 0)
            {
                T element = first.value;
                first = first.next;
                if (first != null)
                    first.prev = null;
                if (first == null)
                    last = null;
                size--;
                IteratorReset();
                return element;
            }
            if (index == size - 1)
            {
                T element = last.value;
                last = last.prev;
                if (last != null)
                    last.next = null;
                if (last == null)
                    first = null;
                size--;
                IteratorReset();
                return element;
            }
            Node<T> p = first;
            for (int i = 0; i < index; i++)
                p = p.next;
            T value = p.value;
            p.prev.next = p.next;
            p.next.prev = p.prev;
            size--;
            IteratorReset();
            return value;
        }
        public void Set(int index, T element)
        {
            if (index < 0 || index > size - 1)
                throw new ArgumentOutOfRangeException("Index");
            Node<T> p = first;
            for (int i = 0; i < index; i++)
                p = p.next;
            p.value = element;
            IteratorReset();
        }
        public T[] SubList(int begin, int end)
        {
            if (begin < 0 || begin > size - 1)
                throw new ArgumentOutOfRangeException("BeginIndex");
            if (end < 0 || end > size)
                throw new ArgumentOutOfRangeException("EndIndex");
            MyLinkedList<T> list = new MyLinkedList<T>();
            Node<T> p = first;
            for (int i = 0; i < begin; i++)
                p = p.next;
            for (int i = 0; i < end - begin; i++)
            {
                list.Add(p.value);
                p = p.next;
            }
            return list.ToArray();
        }
        public T Element()
        {
            if (size == 0)
                throw new ArgumentOutOfRangeException("ListIsEmpty");
            return Get(0);
        }
        private int Amount(object obj)
        {
            int amount = 0;
            Node<T> p = first;
            while (p != null)
            {
                if (p.value.Equals(obj))
                    amount++;
                p = p.next;
            }
            return amount;
        }
        public bool Offer(T element)
        {
            int oldAmount = Amount(element);
            Add(element);
            int newAmount = Amount(element);
            return oldAmount != newAmount;
        }
        public T Peek()
        {
            if (size == 0)
                return default;
            return Get(0);
        }
        public T Poll()
        {
            if (size == 0)
                return default;
            T element = Get(0);
            RemoveAt(0);
            return element;
        }
        public void AddFirst(T element)
        {
            Add(0, element);
        }
        public void AddLast(T element)
        {
            Add(element);
        }
        public T GetFirst()
        {
            return Get(0);
        }
        public Node<T> GetFirstNode()
        {
            return first;
        }
        public T GetLast()
        {
            return Get(Size() - 1);
        }
        public bool OfferFirst(T element)
        {
            int oldAmount = Amount(element);
            Add(0, element);
            int newAmount = Amount(element);
            return oldAmount != newAmount;
        }
        public bool OfferLast(T element)
        {
            return Offer(element);
        }
        public T Pop()
        {
            if (size == 0)
                throw new ArgumentOutOfRangeException("ListIsEmpty");
            return Poll();
        }
        public void Push(T element)
        {
            AddFirst(element);
        }
        public T PeekFirst()
        {
            if (size == 0)
                return default;
            return GetFirst();
        }
        public T PeekLast()
        {
            if (size == 0)
                return default;
            return GetLast();
        }
        public T PollFirst()
        {
            return Poll();
        }
        public T PollLast()
        {
            if (size == 0)
                return default;
            T element = Get(Size() - 1);
            RemoveAt(Size() - 1);
            return element;
        }
        public T RemoveLast()
        {
            return RemoveAt(Size() - 1);
        }
        public T RemoveFirst()
        {
            return RemoveAt(0);
        }
        public bool RemoveFirstOccurrence(object obj)
        {
            int index = IndexOf(obj);
            if (index == -1)
                return false;
            RemoveAt(index);
            return true;
        }
        public bool RemoveLastOccurrence(object obj)
        {
            int index = LastIndexOf(obj);
            if (index == -1)
                return false;
            RemoveAt(index);
            return true;
        }
    }
    class Node<T>
    {
        public T value;
        public Node<T> next;
        public Node<T> prev;
        public Node()
        {
            value = default;
            next = null;
            prev = null;
        }
        public Node(T value, Node<T> next, Node<T> prev)
        {
            this.value = value;
            this.next = next;
            this.prev = prev;
        }
    }
}
