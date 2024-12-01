using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }
    class MyVector<T>
    {
        private T[] elementData;
        private int elementCount;
        private int capacityIncrement;
        public MyVector(int initialCapacity, int capacityIncrement)
        {
            elementData = new T[initialCapacity];
            elementCount = 0;
            this.capacityIncrement = capacityIncrement;
        }
        public MyVector(int initialCapacity)
        {
            elementData = new T[initialCapacity];
            elementCount = 0;
            capacityIncrement = 0;
        }
        public MyVector()
        {
            elementData = new T[10];
            elementCount = 0;
            capacityIncrement = 0;
        }
        public MyVector(T[] A)
        {
            elementData = new T[A.Length];
            for (int i = 0; i < A.Length; i++)
                elementData[i] = A[i];
            elementCount = A.Length;
        }
        public void Add(T element)
        {
            if (elementCount < elementData.Length)
            {
                elementData[elementCount] = element;
                elementCount++;
                return;
            }
            T[] newElementData;
            if (capacityIncrement != 0)
                newElementData = new T[elementData.Length + capacityIncrement];
            else
                newElementData = new T[2 * elementData.Length + 1];
            for (int i = 0; i < elementData.Length; i++)
                newElementData[i] = elementData[i];
            newElementData[elementData.Length] = element;
            elementData = newElementData;
            elementCount++;
        }
        public void AddAll(T[] A)
        {
            for (int i = 0; i < A.Length; i++)
                Add(A[i]);
        }
        public void Clear()
        {
            elementData = new T[0];
            elementCount = 0;
        }
        public bool Contains(object obj)
        {
            for (int i = 0; i < elementCount; i++)
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
                for (int j = 0; j < elementCount; j++)
                    if (object.Equals(A[i], elementData[j]))
                        flag = true;
                if (!flag)
                    return false;
            }
            return true;
        }
        public bool IsEmpty()
        {
            if (elementCount == 0)
                return true;
            return false;
        }
        public void Remove(object obj)
        {
            for (int i = 0; i < elementCount; i++)
            {
                if (object.Equals(obj, elementData[i]))
                {
                    for (int j = i; j < elementCount - 1; j++)
                        elementData[j] = elementData[j + 1];
                    elementCount--;
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
            for (int i = 0; i < elementCount; i++)
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
            return elementCount;
        }
        public T[] ToArray()
        {
            T[] A = new T[elementCount];
            for (int i = 0; i < elementCount; i++)
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
            if (A.Length == elementCount)
            {
                for (int i = 0; i < elementCount; i++)
                    A[i] = elementData[i];
                return;
            }
            A = new T[elementCount];
            for (int i = 0; i < elementCount; i++)
                A[i] = elementData[i];
        }
        public void Add(int index, T element)
        {
            if (index < 0 || index >= elementCount)
                throw new ArgumentOutOfRangeException("index");
            if (elementCount < elementData.Length)
            {
                for (int i = elementCount; i > index; i--)
                    elementData[i] = elementData[i - 1];
                elementData[index] = element;
                elementCount++;
                return;
            }
            T[] newElementData;
            if (capacityIncrement != 0)
                newElementData = new T[elementData.Length + capacityIncrement];
            else
                newElementData = new T[2 * elementData.Length + 1];
            for (int i = 0; i < elementData.Length; i++)
                newElementData[i] = elementData[i];
            for (int i = elementData.Length; i > index; i--)
                newElementData[i] = newElementData[i - 1];
            newElementData[index] = element;
            elementData = newElementData;
            elementCount++;
        }
        public void AddAll(int index, T[] A)
        {
            if (index < 0 || index >= elementCount)
                throw new ArgumentOutOfRangeException("index");
            for (int i = A.Length - 1; i >= 0; i--)
                Add(index, A[i]);
        }
        public T Get(int index)
        {
            if (index < 0 || index >= elementCount)
                throw new ArgumentOutOfRangeException("index");
            return elementData[index];
        }
        public int IndexOf(object obj)
        {
            for (int i = 0; i < elementCount; i++)
                if (object.Equals(obj, elementData[i]))
                    return i;
            return -1;
        }
        public int LastIndexOf(object obj)
        {
            for (int i = elementCount - 1; i >= 0; i--)
                if (object.Equals(obj, elementData[i]))
                    return i;
            return -1;
        }
        public T Remove(int index)
        {
            if (index < 0 || index >= elementCount)
                throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            for (int i = index; i < elementCount - 1; i++)
                elementData[i] = elementData[i + 1];
            elementCount--;
            return element;
        }
        public void Set(int index, T element)
        {
            if (index < 0 || index >= elementCount)
                throw new ArgumentOutOfRangeException("index");
            elementData[index] = element;
        }
        public T[] SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex >= elementCount)
                throw new ArgumentOutOfRangeException("fromIndex");
            if (toIndex < 0 || toIndex >= elementCount)
                throw new ArgumentOutOfRangeException("toIndex");
            T[] A = new T[toIndex - fromIndex];
            for (int i = toIndex; i < fromIndex; i++)
                A[i] = elementData[i];
            return A;
        }
        public T FirstElement()
        {
            if (elementCount == 0)
                throw new ArgumentOutOfRangeException("index");
            return elementData[0];
        }
        public T LastElement()
        {
            if (elementCount == 0)
                throw new ArgumentOutOfRangeException("index");
            return elementData[elementCount - 1];
        }
        public void RemoveElementAt(int pos)
        {
            if (pos < 0 || pos >= elementCount)
                throw new ArgumentOutOfRangeException("pos");
            T element = elementData[pos];
            for (int i = pos; i < elementCount - 1; i++)
                elementData[i] = elementData[i + 1];
            elementCount--;
        }
        public void RemoveRange(int begin, int end)
        {
            if (begin < 0 || begin >= elementCount)
                throw new ArgumentOutOfRangeException("begin");
            if (end < 0 || end >= elementCount)
                throw new ArgumentOutOfRangeException("end");
            for (int i = 0; i < end - begin; i++)
                RemoveElementAt(begin);
        }
    }
}
