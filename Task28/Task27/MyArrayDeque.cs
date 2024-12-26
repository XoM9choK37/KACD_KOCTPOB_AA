using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Task27;

namespace Task14
{
    class MyArrayDeque<T> : MyIterator1<T>, MyDeque<T>
    {
        private T[] elements;
        private int head;
        private int tail;
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
            return elements.AsEnumerable().GetEnumerator();
        }
        private void IteratorReset()
        {
            cursor = Iterator();
        }
        public MyArrayDeque()
        {
            elements = new T[16];
            head = 0;
            tail = -1;
            cursor = Iterator();
        }
        public MyArrayDeque(MyCollection<T> collection)
        {
            T[] A = collection.ToArray();
            elements = new T[A.Length];
            for (int i = 0; i < A.Length; i++)
                elements[i] = A[i];
            head = 0;
            tail = A.Length - 1;
            cursor = Iterator();
        }
        public MyArrayDeque(int numElements)
        {
            elements = new T[numElements];
            head = 0;
            tail = -1;
            cursor = Iterator();
        }
        public void Add(T element)
        {
            if (tail + 1 < elements.Length)
            {
                tail++;
                elements[tail] = element;
                IteratorReset();
                return;
            }
            if (Size() < elements.Length)
            {
                head--;
                for (int i = head; i < tail; i++)
                    elements[i] = elements[i + 1];
                elements[tail] = element;
                IteratorReset();
                return;
            }
            T[] newElements = new T[2 * (elements.Length + 1)];
            for (int i = head; i <= tail; i++)
                newElements[i] = elements[i];
            tail++;
            newElements[tail] = element;
            elements = newElements;
            IteratorReset();
        }
        public void AddAll(MyCollection<T> collection)
        {
            T[] A = collection.ToArray();
            for (int i = 0; i < A.Length; i++)
                Add(A[i]);
        }
        public void Clear()
        {
            head = 0;
            tail = -1;
        }
        public bool Contains(object obj)
        {
            for (int i = head; i <= tail; i++)
                if (Equals(obj, elements[i]))
                    return true;
            return false;
        }
        public bool ContainsAll(MyCollection<T> collection)
        {
            T[] A = collection.ToArray();
            bool flag;
            for (int i = 0; i < A.Length; i++)
            {
                flag = false;
                for (int j = head; j <= tail; j++)
                    if (Equals(A[i], elements[j]))
                        flag = true;
                if (!flag)
                    return false;
            }
            return true;
        }
        public bool IsEmpty()
        {
            return tail < head;
        }
        public void Remove(object obj)
        {
            for (int i = head; i <= tail; i++)
                if (Equals(obj, elements[i]))
                {
                    for (int j = i; j < tail; j++)
                        elements[j] = elements[j + 1];
                    tail--;
                    i--;
                }
            IteratorReset();
        }
        public void RemoveAll(MyCollection<T> collection)
        {
            T[] A = collection.ToArray();
            for (int i = 0; i < A.Length; i++)
                Remove(A[i]);
        }
        public void RetainAll(MyCollection<T> collection)
        {
            T[] A = collection.ToArray();
            bool flag;
            for (int i = head; i <= tail; i++)
            {
                flag = false;
                for (int j = 0; j < A.Length; j++)
                    if (Equals(elements[i], A[j]))
                        flag = true;
                if (!flag)
                    Remove(A[i]);
            }
        }
        public int Size()
        {
            return tail - head + 1;
        }
        public T[] ToArray()
        {
            T[] A = new T[Size()];
            int index = 0;
            for (int i = head; i <= tail; i++)
            {
                A[index] = elements[i];
                index++;
            }
            return A;
        }
        public void ToArray(ref T[] A)
        {
            if (A == null)
            {
                A = ToArray();
                return;
            }
            int index = 0;
            if (A.Length == Size())
            {
                for (int i = head; i <= tail; i++)
                {
                    A[index] = elements[i];
                    index++;
                }
                return;
            }
            A = new T[Size()];
            for (int i = head; i <= tail; i++)
            {
                A[index] = elements[i];
                index++;
            }
        }
        public T Element()
        {
            if (Size() == 0)
                throw new Exception("Deque is empty");
            return elements[head];
        }
        private int Amount(T element)
        {
            int amount = 0;
            for (int i = head; i <= tail; i++)
                if (Equals(element, elements[i]))
                    amount++;
            return amount;
        }
        public bool Offer(T element)
        {
            int oldAmount = Amount(element);
            Add(element);
            int newAmount = Amount(element);
            if (oldAmount != newAmount)
                return true;
            return false;
        }
        public T Peek()
        {
            if (Size() == 0)
                return default;
            return elements[head];
        }
        public T Poll()
        {
            if (Size() == 0)
                return default;
            head++;
            IteratorReset();
            return elements[head - 1];
        }
        public void AddFirst(T element)
        {
            if (head - 1 >= 0)
            {
                head--;
                elements[head] = element;
                IteratorReset();
                return;
            }
            if (Size() < elements.Length)
            {
                tail++;
                for (int i = tail; i > head; i--)
                    elements[i] = elements[i - 1];
                elements[head] = element;
                IteratorReset();
                return;
            }
            T[] newElements = new T[2 * (elements.Length + 1)];
            for (int i = head; i <= tail; i++)
                newElements[i + 1] = elements[i];
            newElements[head] = element;
            elements = newElements;
            IteratorReset();
        }
        public void AddLast(T element)
        {
            Add(element);
        }
        public T GetFirst()
        {
            return Element();
        }
        public T GetLast()
        {
            if (Size() == 0)
                throw new Exception("Deque is empty");
            return elements[tail];
        }
        public bool OfferFirst(T element)
        {
            if (Size() == elements.Length)
                return false;
            AddFirst(element);
            return true;
        }
        public bool OfferLast(T element)
        {
            if (Size() == elements.Length)
                return false;
            AddLast(element);
            return true;
        }
        public T Pop()
        {
            if (Size() == 0)
                throw new Exception("Deque is empty");
            return Poll();
        }
        public void Push(T element)
        {
            AddFirst(element);
        }
        public T PeekFirst()
        {
            return Peek();
        }
        public T PeekLast()
        {
            if (Size() == 0)
                return default;
            return elements[tail];
        }
        public T PollFirst()
        {
            return Poll();
        }
        public T PollLast()
        {
            if (Size() == 0)
                return default;
            tail--;
            IteratorReset();
            return elements[tail + 1];
        }
        public T RemoveFirst()
        {
            return Pop();
        }
        public T RemoveLast()
        {
            if (Size() == 0)
                throw new Exception("Deque is empty");
            tail--;
            IteratorReset();
            return elements[tail + 1];
        }
        public bool RemoveFirstOccurance(object obj)
        {
            for (int i = head; i <= tail; i++)
                if (Equals(obj, elements[i]))
                {
                    for (int j = i; j < tail; j++)
                        elements[j] = elements[j + 1];
                    tail--;
                    IteratorReset();
                    return true;
                }
            return false;
        }
        public bool RemoveLastOccurance(object obj)
        {
            for (int i = tail; i >= head; i--)
                if (Equals(obj, elements[i]))
                {
                    for (int j = i; j < tail; j++)
                        elements[j] = elements[j + 1];
                    tail--;
                    IteratorReset();
                    return true;
                }
            return false;
        }
        public void Print()
        {
            for (int i = head; i <= tail; i++)
                Console.WriteLine(elements[i]);
        }
    }
}
