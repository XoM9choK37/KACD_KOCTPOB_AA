using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4;
using Task16;
using System.Runtime.InteropServices;
using System.Collections;
using System.Drawing;
using System.Data.SqlTypes;
using System.Diagnostics;

namespace Task17
{
    static class Lists
    {
        const int MODULE = 1_000;
        public static void ArrayListGet(MyArrayList<int> arrayList, int amount)
        {
            Random random = new Random();
            for (int i = 0; i < amount; i++)
                arrayList.Get(random.Next(arrayList.Size()));
        }
        public static void LinkedListGet(MyLinkedList<int> linkedList, int amount)
        {
            Random random = new Random();
            for (int i = 0; i < amount; i++)
                linkedList.Get(random.Next(linkedList.Size()));
        }
        public static void ArrayListSet(MyArrayList<int> arrayList, int amount)
        {
            Random random = new Random();
            for (int i = 0; i < amount; i++)
                arrayList.Set(random.Next(arrayList.Size()), random.Next(MODULE));
        }
        public static void LinkedListSet(MyLinkedList<int> linkedList, int amount)
        {
            Random random = new Random();
            for (int i = 0; i < amount; i++)
                linkedList.Set(random.Next(linkedList.Size()), random.Next(MODULE));
        }
        public static void ArrayListAdd(MyArrayList<int> arrayList, int amount)
        {
            Random random = new Random();
            for (int i = 0; i < amount; i++)
                arrayList.Add(random.Next(MODULE));
        }
        public static void LinkedListAdd(MyLinkedList<int> linkedList, int amount)
        {
            Random random = new Random();
            for (int i = 0; i < amount; i++)
                linkedList.Add(random.Next(MODULE));
        }
        public static void ArrayListAddAt(MyArrayList<int> arrayList, int amount)
        {
            Random random = new Random();
            for (int i = 0; i < amount; i++)
                arrayList.Add(random.Next(arrayList.Size()), random.Next(MODULE));
        }
        public static void LinkedListAddAt(MyLinkedList<int> linkedList, int amount)
        {
            Random random = new Random();
            for (int i = 0; i < amount; i++)
                linkedList.Add(random.Next(linkedList.Size()), random.Next(MODULE));
        }
        public static void ArrayListRemove(MyArrayList<int> arrayList)
        {
            Random random = new Random();
            for (int i = 0; i < arrayList.Size(); i++)
                arrayList.Remove(index: random.Next(arrayList.Size()));
        }
        public static void LinkedListRemove(MyLinkedList<int> linkedList)
        {
            Random random = new Random();
            for (int i = 0; i < linkedList.Size(); i++)
                linkedList.RemoveAt(random.Next(linkedList.Size()));
        }
        public static double[] AddSpeedTest(int amount)
        {
            double[] time = new double[2];
            Stopwatch stopwatch = new Stopwatch();
            MyArrayList<int> arrayList = new MyArrayList<int>();
            MyLinkedList<int> linkedList = new MyLinkedList<int>();
            int element;
            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                element = random.Next();
                arrayList.Add(element);
                linkedList.Add(element);
            }
            stopwatch.Start();
            ArrayListAdd(arrayList, amount);
            stopwatch.Stop();
            time[0] = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            LinkedListAdd(linkedList, amount);
            stopwatch.Stop();
            time[1] = stopwatch.ElapsedMilliseconds;
            return time;
        }
        public static double[] GetSpeedTest(int amount)
        {
            double[] time = new double[2];
            Stopwatch stopwatch = new Stopwatch();
            MyArrayList<int> arrayList = new MyArrayList<int>();
            MyLinkedList<int> linkedList = new MyLinkedList<int>();
            int element;
            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                element = random.Next();
                arrayList.Add(element);
                linkedList.Add(element);
            }
            stopwatch.Start();
            ArrayListGet(arrayList, amount);
            stopwatch.Stop();
            time[0] = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            LinkedListGet(linkedList, amount);
            stopwatch.Stop();
            time[1] = stopwatch.ElapsedMilliseconds;
            return time;
        }
        public static double[] SetSpeedTest(int amount)
        {
            double[] time = new double[2];
            Stopwatch stopwatch = new Stopwatch();
            MyArrayList<int> arrayList = new MyArrayList<int>();
            MyLinkedList<int> linkedList = new MyLinkedList<int>();
            int element;
            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                element = random.Next();
                arrayList.Add(element);
                linkedList.Add(element);
            }
            stopwatch.Start();
            ArrayListSet(arrayList, amount);
            stopwatch.Stop();
            time[0] = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            LinkedListSet(linkedList, amount);
            stopwatch.Stop();
            time[1] = stopwatch.ElapsedMilliseconds;
            return time;
        }
        public static double[] AddAtSpeedTest(int amount)
        {
            double[] time = new double[2];
            Stopwatch stopwatch = new Stopwatch();
            MyArrayList<int> arrayList = new MyArrayList<int>();
            MyLinkedList<int> linkedList = new MyLinkedList<int>();
            int element;
            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                element = random.Next();
                arrayList.Add(element);
                linkedList.Add(element);
            }
            stopwatch.Start();
            ArrayListAddAt(arrayList, amount);
            stopwatch.Stop();
            time[0] = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            LinkedListAddAt(linkedList, amount);
            stopwatch.Stop();
            time[1] = stopwatch.ElapsedMilliseconds;
            return time;
        }
        public static double[] RemoveSpeedTest(int amount)
        {
            double[] time = new double[2];
            Stopwatch stopwatch = new Stopwatch();
            MyArrayList<int> arrayList = new MyArrayList<int>();
            MyLinkedList<int> linkedList = new MyLinkedList<int>();
            int element;
            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                element = random.Next();
                arrayList.Add(element);
                linkedList.Add(element);
            }
            stopwatch.Start();
            ArrayListRemove(arrayList);
            stopwatch.Stop();
            time[0] = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            LinkedListRemove(linkedList);
            stopwatch.Stop();
            time[1] = stopwatch.ElapsedMilliseconds;
            return time;
        }
        public static double[] GetMatrixAverage(double[][] matrix)
        {
            double[] array = new double[matrix.Length];
            double sum;
            for (int j = 0; j < matrix[0].Length; j++)
            {
                sum = 0;
                for (int i = 0; i < matrix.Length; i++)
                    sum += matrix[i][j];
                array[j] = sum / matrix.Length;
            }
            return array;
        }
        public static double[][] GetTimes(Func<int, double[]> func, int amount)
        {
            double[][] times = new double[20][];
            for (int i = 0; i < 20; i++)
                times[i] = new double[2];
            for (int i = 0; i < 20; i++)
            {
                times[i][0] = func(amount)[0];
                times[i][1] = func(amount)[1];
            }
            return times;
        }
    }
}
