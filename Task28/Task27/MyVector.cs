using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task27;

namespace Task6
{
    class MyVector<T> : MyIterator2<T>, MyList<T>
    {
        protected T[] elementData;
        protected int elementCount;
        protected int capacityIncrement;
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
            IEnumerator<T> newCursor = ListIterator();
            for (int i = 0; i < index + 1; i++)
                newCursor.MoveNext();
            return newCursor;
        }
        private void IteratorReset()
        {
            cursor = ListIterator(index);
        }
        public MyVector(int initialCapacity, int capacityIncrement)
        {
            elementData = new T[initialCapacity];
            elementCount = 0;
            this.capacityIncrement = capacityIncrement;
            cursor = ListIterator();
        }
        public MyVector(int initialCapacity)
        {
            elementData = new T[initialCapacity];
            elementCount = 0;
            capacityIncrement = 0;
            cursor = ListIterator();
        }
        public MyVector()
        {
            elementData = new T[10];
            elementCount = 0;
            capacityIncrement = 0;
            cursor = ListIterator();
        }
        public MyVector(MyCollection<T> collection)
        {
            T[] A = collection.ToArray();
            elementData = new T[A.Length];
            for (int i = 0; i < A.Length; i++)
                elementData[i] = A[i];
            elementCount = A.Length;
            cursor = ListIterator();
        }
        public void Add(T element)
        {
            if (elementCount < elementData.Length)
            {
                elementData[elementCount] = element;
                elementCount++;
                IteratorReset();
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
            elementCount = 0;
        }
        public bool Contains(object obj)
        {
            for (int i = 0; i < elementCount; i++)
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
                IteratorReset();
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
                elementData[i] = elementData[i - 1];
            elementData[index] = element;
            elementCount++;
            IteratorReset();
        }
        public void AddAll(int index, MyCollection<T> collection)
        {
            T[] A = collection.ToArray();
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
        public T RemoveAt(int index)
        {
            if (index < 0 || index >= elementCount)
                throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            for (int i = index; i < elementCount - 1; i++)
                elementData[i] = elementData[i + 1];
            elementCount--;
            IteratorReset();
            return element;
        }
        public void Set(int index, T element)
        {
            if (index < 0 || index >= elementCount)
                throw new ArgumentOutOfRangeException("index");
            elementData[index] = element;
            IteratorReset();
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
            IteratorReset();
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
