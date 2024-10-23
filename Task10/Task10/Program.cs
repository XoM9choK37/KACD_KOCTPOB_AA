using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] A = { 1, 9, 3, 6, 34, 6, 1, 0, 7, 23 };
            MyHeap<int> myHeap = new MyHeap<int>(A);
            myHeap.GetMaxValue();
            myHeap.Print();
            Console.WriteLine(myHeap.PopMaxValue());
            myHeap.Print();
            int[] B = { 1, 2, 4, 8, 16, 32, 64 };
            MyHeap<int> myHeap2 = new MyHeap<int>(B);
            myHeap.Merge(myHeap2);
            myHeap.Print();
            Console.WriteLine(myHeap.PopMaxValue());
            myHeap.Print();
        }
    }
    class MyHeap<T> where T : IComparable<T>
    {
        private T[] elementData;
        private int elementCount;
        private Comparer<T> comparator;
        public MyHeap()
        {
            elementData = new T[10];
            elementCount = 0;
            comparator = Comparer<T>.Default;
        }
        public MyHeap(T[] A)
        {
            elementCount = A.Length;
            elementData = new T[elementCount];
            for (int i = 0; i < elementCount; i++)
                elementData[i] = A[i];
            comparator = Comparer<T>.Default;
            Rebuild();
        }
        public void Print()
        {
            for (int i = 0; i < elementCount; i++)
                Console.Write(elementData[i] + " ");
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
            for (int i = elementCount - 1; i >= 0; i--)
            {
                int left;
                int right;
                int parent = i;
                T temp;
                bool flag;
                do
                {
                    flag = false;
                    left = 2 * i + 1;
                    right = 2 * i + 2;
                    if (left < elementCount &&
                        comparator.Compare(elementData[left], elementData[parent]) > 0)
                        parent = left;
                    if (right < elementCount &&
                        comparator.Compare(elementData[right], elementData[parent]) > 0)
                        parent = right;
                    if (parent != i)
                    {
                        temp = elementData[i];
                        elementData[i] = elementData[parent];
                        elementData[parent] = temp;
                        i = parent;
                        flag = true;
                    }
                }
                while (flag);
            }
        }
        public T GetMaxValue()
        {
            if (elementCount == 0)
                throw new ArgumentOutOfRangeException("Index");
            return elementData[0];
        }
        public T PopMaxValue()
        {
            if (elementCount == 0)
                throw new ArgumentOutOfRangeException("Index");
            T maxValue = elementData[0];
            for (int i = 0; i < elementCount - 1; i++)
                elementData[i] = elementData[i + 1];
            elementCount--;
            Rebuild();
            return maxValue;
        }
        public void Set(int index, T element)
        {
            if (index < 0 || index >= elementCount)
                throw new ArgumentOutOfRangeException("Index");
            elementData[index] = element;
            Rebuild();
        }
        public T Get(int index)
        {
            if (index < 0 || index >= elementCount)
                throw new ArgumentOutOfRangeException("Index");
            return elementData[index];
        }
        public void Add(T element)
        {
            if (elementCount < elementData.Length)
            {
                elementData[elementCount] = element;
                elementCount++;
                Rebuild();
                return;
            }
            T[] newElementData = new T[2 * elementData.Length + 1];
            for (int i = 0; i < elementCount; i++)
                newElementData[i] = elementData[i];
            newElementData[elementCount] = element;
            elementData = newElementData;
            elementCount++;
            Rebuild();
        }
        public int Size()
        {
            return elementCount;
        }
        public void Merge(MyHeap<T> myHeap)
        {
            T[] newElementData = new T[elementCount + myHeap.Size()];
            for (int i = 0; i < elementCount; i++)
                newElementData[i] = elementData[i];
            for (int i = 0; i < myHeap.Size(); i++)
                newElementData[elementCount + i] = myHeap.Get(i);
            elementData = newElementData;
            elementCount += myHeap.Size();
            Rebuild();
        }
    }
}
