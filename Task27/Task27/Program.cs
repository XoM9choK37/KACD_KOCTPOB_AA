using System;
using Task16;

namespace Task27
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyLinkedList<int> list = new MyLinkedList<int>();
            list.Add(10);
            Console.WriteLine(list.HasNext());
            Console.WriteLine(list.Next());
            Console.WriteLine(list.HasNext());
            list.Add(20);
            list.Add(25);
            list.Add(30);
            Console.WriteLine(list.Next());
            Console.WriteLine(list.Next());
            Console.WriteLine(list.NextIndex());
            Console.WriteLine(list.Next());
            Console.WriteLine(list.HasNext());
        }
    }
    interface MyIterator1<T>
    {
        bool HasNext();
        T Next();
        void Remove();
    }
    interface MyIterator2<T>
    {
        bool HasNext();
        T Next();
        bool HasPrevious();
        T Previous();
        int NextIndex();
        int PreviousIndex();
        void Remove();
        void Set(T element);
        void Add(T element);
    }
}
