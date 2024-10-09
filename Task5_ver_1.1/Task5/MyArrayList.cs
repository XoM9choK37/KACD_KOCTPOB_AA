using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class MyArrayList<T>
    {
        private T[] elementData;
        private int size;
        public MyArrayList()
        {
            elementData = new T[0];
            size = 0;
        }
        public MyArrayList(T[] A)
        {
            elementData = new T[A.Length];
            for (int i = 0; i < A.Length; i++)
                elementData[i] = A[i];
            size = A.Length;
        }
        public MyArrayList(int capacity)
        {
            elementData = new T[capacity];
            size = 0;
        }
        public void Add(T element)
        {
            if (size < elementData.Length)
            {
                elementData[size] = element;
                size++;
                return;
            }
            T[] newElementData = new T[(int)(elementData.Length * 1.5) + 1];
            for (int i = 0; i < elementData.Length; i++)
                newElementData[i] = elementData[i];
            newElementData[elementData.Length] = element;
            elementData = newElementData;
            size++;
        }
        public void AddAll(T[] A)
        {
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
        public bool ContainsAll(T[] A)
        {
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
                return;
            }
            T[] newElementData = new T[(int)(elementData.Length * 1.5) + 1];
            for (int i = 0; i < elementData.Length; i++)
                newElementData[i] = elementData[i];
            for (int i = elementData.Length; i > index; i--)
                elementData[i] = elementData[i - 1];
            elementData[index] = element;
            size++;
        }
        public void AddAll(int index, T[] A)
        {
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
        public T Remove(int index)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            for (int i = index; i < size - 1; i++)
                elementData[i] = elementData[i + 1];
            size--;
            return element;
        }
        public void Set(int index, T element)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException("index");
            elementData[index] = element;
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
