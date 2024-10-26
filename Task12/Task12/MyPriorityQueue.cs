using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Task10;
using Task12;

namespace Task11
{
    class MyPriorityQueue<T> where T : IComparable<T>
    {
        private T[] queue;
        private int size;
        private Comparer<T> comparator;
        public MyPriorityQueue()
        {
            queue = new T[11];
            size = 0;
            comparator = Comparer<T>.Default;
        }
        public MyPriorityQueue(T[] A)
        {
            size = A.Length;
            queue = new T[size];
            for (int i = 0; i < size; i++)
                queue[i] = A[i];
            comparator = Comparer<T>.Default;
            Rebuild();
        }
        public MyPriorityQueue(int initialCapacity)
        {
            queue = new T[initialCapacity];
            size = 0;
            comparator = Comparer<T>.Default;
        }
        public MyPriorityQueue(int initialCapacity, Comparer<T> comparator)
        {
            queue = new T[initialCapacity];
            size = 0;
            this.comparator = comparator;
        }
        public MyPriorityQueue(MyPriorityQueue<T> Q)
        {
            queue = Q.ToArray();
            size = Q.Size();
            comparator = Q.ComparatorGet();
        }
        public void Print()
        {
            for (int i = 0; i < size; i++)
                Console.Write(queue[i] + " ");
            Console.Write('\n');
        }
        public Comparer<T> ComparatorGet()
        {
            return comparator;
        }
        public void ComparatorSet(Comparer<T> comparator)
        {
            this.comparator = comparator;
        }
        private void Rebuild()
        {
            MyHeap<T> myHeap = new MyHeap<T>(queue, size);
            myHeap.ComparatorSet(comparator);
            for (int i = 0; i < size; i++)
                queue[i] = myHeap.Get(i);
        }
        public void Add(T element)
        {
            if (size < queue.Length)
            {
                queue[size] = element;
                size++;
                Rebuild();
                return;
            }
            int newSize;
            if (queue.Length < 64)
                newSize = queue.Length + 2;
            else
                newSize = (int)(queue.Length * 1.5);
            T[] newQueue = new T[newSize];
            for (int i = 0; i < size; i++)
                newQueue[i] = queue[i];
            newQueue[size] = element;
            queue = newQueue;
            size++;
            Rebuild();
        }
        public void AddAll(T[] A)
        {
            for (int i = 0; i < A.Length; i++)
                Add(A[i]);
        }
        public void Clear()
        {
            size = 0;
        }
        public bool Contains(object obj)
        {
            for (int i = 0; i < size; i++)
                if (comparator.Compare((T)obj, queue[i]) == 0)
                    return true;
            return false;
        }
        public bool ContainsAll(T[] A)
        {
            bool flag;
            for (int i = 0; i < A.Length; i++)
            {
                flag = false;
                for (int j = 0; j < size; j++)
                    if (comparator.Compare(A[i], queue[j]) == 0)
                        flag = true;
                if (!flag)
                    return false;
            }
            return true;
        }
        public bool IsEmpty()
        {
            return size == 0;
        }
        public void Remove(object obj)
        {
            for (int i = 0; i < size; i++)
            {
                if (comparator.Compare((T)obj, queue[i]) == 0)
                {
                    for (int j = i; j < size - 1; j++)
                        queue[j] = queue[j + 1];
                    size--;
                    i--;
                }
            }
            Rebuild();
        }
        public void RemoveAll(T[] A)
        {
            for (int i = 0; i < A.Length; i++)
                Remove(A[i]);
        }
        public void RetainAll(T[] A)
        {
            bool flag;
            for (int i = 0; i < size; i++)
            {
                flag = false;
                for (int j = 0; j < A.Length; j++)
                    if (comparator.Compare(A[i], queue[j]) == 0)
                        flag = true;
                if (!flag)
                    Remove(A[i]);
            }
            Rebuild();
        }
        public int Size()
        {
            return size;
        }
        public T[] ToArray()
        {
            T[] A = new T[size];
            for (int i = 0; i < size; i++)
                A[i] = queue[i];
            return A;
        }
        public void ToArray(ref T[] A)
        {
            if (A == null)
            {
                A = ToArray();
                return;
            }
            if (A.Length == size)
            {
                for (int i = 0; i < size; i++)
                    A[i] = queue[i];
                return;
            }
            A = new T[size];
            for (int i = 0; i < size; i++)
                A[i] = queue[i];
        }
        public T Element()
        {
            if (size == 0)
                throw new ArgumentOutOfRangeException("Index");
            return queue[0];
        }
        private int Amount(T obj)
        {
            int amount = 0;
            for (int i = 0; i < size; i++)
                if (comparator.Compare(obj, queue[i]) == 0)
                    amount++;
            return amount;
        }
        public bool Offer(T obj)
        {
            int oldAmount = Amount(obj);
            Add(obj);
            int newAmount = Amount(obj);
            if (oldAmount != newAmount)
                return true;
            return false;
        }
        public T Peek()
        {
            if (size == 0)
                return default;
            return queue[0];
        }
        public T Poll()
        {
            if (size == 0)
                return default;
            T element = queue[0];
            for (int i = 0; i < size - 1; i++)
                queue[i] = queue[i + 1];
            size--;
            Rebuild();
            return element;
        }
    }
}
