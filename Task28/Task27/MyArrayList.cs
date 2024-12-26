using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Task27;

namespace Task4
{
    class MyArrayList<T> : MyIterator2<T>, MyList<T>
    {
        private T[] elementData;
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
            return elementData.AsEnumerable().GetEnumerator();
        }
        public IEnumerator<T> ListIterator(int index)
        {
            IEnumerator<T> newCursor =
                elementData.AsEnumerable().GetEnumerator();
            for (int i = 0; i < index + 1; i++)
                newCursor.MoveNext();
            return newCursor;
        }
        private void IteratorReset()
        {
            cursor = ListIterator(index);
        }
        public MyArrayList()
        {
            elementData = new T[0];
            size = 0;
            cursor = ListIterator();
        }
        public MyArrayList(MyCollection<T> collection)
        {
            T[] A = collection.ToArray();
            elementData = new T[A.Length];
            for (int i = 0; i < A.Length; i++)
                elementData[i] = A[i];
            size = A.Length;
            cursor = ListIterator();
        }
        public MyArrayList(int capacity)
        {
            elementData = new T[capacity];
            size = 0;
            cursor = ListIterator();
        }
        public void Add(T element)
        {
            if (size < elementData.Length)
            {
                elementData[size] = element;
                size++;
                IteratorReset();
                return;
            }
            T[] newElementData = new T[(int)(elementData.Length * 1.5) + 1];
            for (int i = 0; i < elementData.Length; i++)
                newElementData[i] = elementData[i];
            newElementData[elementData.Length] = element;
            elementData = newElementData;
            size++;
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
            elementData = new T[0];
            size = 0;
        }
        public bool Contains(object obj)
        {
            for (int i = 0; i < size; i++)
                if (object.Equals(obj, elementData[i]))
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
                for (int j = 0; j < size; j++)
                    if (object.Equals(A[i], elementData[j]))
                        flag = true;
                if (!flag)
                    return false;
            }
            return true;
        }
        public bool IsEmpty()
        {
            if (size == 0)
                return true;
            return false;
        }
        public void Remove(object obj)
        {
            for (int i = 0; i < size; i++)
            {
                if (object.Equals(obj, elementData[i]))
                {
                    for (int j = i; j < size - 1; j++)
                        elementData[j] = elementData[j + 1];
                    size--;
                    i--;
                }
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
            for (int i = 0; i < size; i++)
            {
                flag = false;
                for (int j = 0; j < A.Length; j++)
                    if (object.Equals(A[i], elementData[j]))
                        flag = true;
                if (!flag)
                    Remove(A[i]);
            }
        }
        public int Size()
        {
            return size;
        }
        public T[] ToArray()
        {
            T[] A = new T[size];
            for (int i = 0; i < size; i++)
                A[i] = elementData[i];
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
                    A[i] = elementData[i];
                return;
            }
            A = new T[size];
            for (int i = 0; i < size; i++)
                A[i] = elementData[i];
        }
        public void Add(int index, T element)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException("index");
            if (size < elementData.Length)
            {
                for (int i = size; i > index; i--)
                    elementData[i] = elementData[i - 1];
                elementData[index] = element;
                size++;
                IteratorReset();
                return;
            }
            T[] newElementData = new T[(int)(elementData.Length * 1.5) + 1];
            for (int i = 0; i < elementData.Length; i++)
                newElementData[i] = elementData[i];
            for (int i = elementData.Length; i > index; i--)
                newElementData[i] = newElementData[i - 1];
            newElementData[index] = element;
            elementData = newElementData;
            size++;
            IteratorReset();
        }
        public void AddAll(int index, MyCollection<T> collection)
        {
            T[] A = collection.ToArray();
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException("index");
            for (int i = A.Length - 1; i >= 0; i--)
                Add(index, A[i]);
        }
        public T Get(int index)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException("index");
            return elementData[index];
        }
        public int IndexOf(object obj)
        {
            for (int i = 0; i < size; i++)
                if (object.Equals(obj, elementData[i]))
                    return i;
            return -1;
        }
        public int LastIndexOf(object obj)
        {
            for (int i = size - 1; i >= 0; i--)
                if (object.Equals(obj, elementData[i]))
                    return i;
            return -1;
        }
        public T RemoveAt(int index)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            for (int i = index; i < size - 1; i++)
                elementData[i] = elementData[i + 1];
            size--;
            IteratorReset();
            return element;
        }
        public void Set(int index, T element)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException("index");
            elementData[index] = element;
            IteratorReset();
        }
        public T[] SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex >= size)
                throw new ArgumentOutOfRangeException("fromIndex");
            if (toIndex < 0 || toIndex >= size)
                throw new ArgumentOutOfRangeException("toIndex");
            T[] A = new T[toIndex - fromIndex];
            for (int i = toIndex; i < fromIndex; i++)
                A[i] = elementData[i];
            return A;
        }
    }
}
