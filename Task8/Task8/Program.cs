using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Task6;

namespace Task8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyStack<int> myStack = new MyStack<int>();
            myStack.Push(1);
            myStack.Push(2);
            myStack.Push(3);
            myStack.Push(4);
            myStack.Push(5);
            myStack.Pop();
            Console.WriteLine(myStack.Peek());
            Console.WriteLine(myStack.Empty());
            Console.WriteLine(myStack.Search(2));
        }
    }
    class MyStack<T> : MyVector<T>
    {
        public MyStack()
        {
            elementData = new T[10];
            elementCount = 0;
            capacityIncrement = 0;
        }
        public void Push(T item)
        {
            Add(item);
        }
        public T Pop()
        {
            if (elementCount == 0)
                throw new ArgumentOutOfRangeException("stack is empty");
            elementCount--;
            return elementData[elementCount];
        }
        public T Peek()
        {
            if (elementCount == 0)
                throw new ArgumentOutOfRangeException("stack is empty");
            return elementData[elementCount - 1];
        }
        public bool Empty()
        {
            return elementCount == 0;
        }
        public int Search(T item)
        {
            int index = -1;
            for (int i = 0; i < elementCount && index == -1; i++)
                if (object.Equals(item, elementData[i]))
                    index = i;
            return elementCount - index;
        }
    }
}
