using System;
using Task18;
using Task21;
using System.Diagnostics;

namespace Task22
{
    static class Maps
    {
        const int MODULE = 1_000;
        public static void HashMapGet(MyHashMap<char, int> hashMap, int amount)
        {
            Random random = new Random();
            for (int i = 0; i < amount; i++)
                hashMap.Get((char)random.Next(MODULE));
        }
        public static void TreeMapGet(MyTreeMap<char, int> treeMap, int amount)
        {
            Random random = new Random();
            for (int i = 0; i < amount; i++)
                treeMap.Get((char)random.Next(MODULE));
        }
        public static void HashMapPut(MyHashMap<char, int> hashMap, int amount)
        {
            Random random = new Random();
            for (int i = 0; i < amount; i++)
                hashMap.Put((char)random.Next(MODULE), random.Next(MODULE));
        }
        public static void TreeMapPut(MyTreeMap<char, int> treeMap, int amount)
        {
            Random random = new Random();
            for (int i = 0; i < amount; i++)
                treeMap.Put((char)random.Next(MODULE), random.Next(MODULE));
        }
        public static void HashMapRemove(MyHashMap<char, int> hashMap)
        {
            Random random = new Random();
            for (int i = 0; i < MODULE; i++)
                hashMap.Remove((char)i);
        }
        public static void TreeMapRemove(MyTreeMap<char, int> treeMap)
        {
            Random random = new Random();
            for (int i = 0; i < MODULE; i++)
                treeMap.Remove((char)i);
        }
        public static double[] PutSpeedTest(int amount)
        {
            double[] time = new double[2];
            Stopwatch stopwatch = new Stopwatch();
            MyHashMap<char, int> hashMap = new MyHashMap<char, int>();
            MyTreeMap<char, int> treeMap = new MyTreeMap<char, int>();
            char key;
            int value;
            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                key = (char)random.Next(MODULE);
                value = random.Next(MODULE);
                hashMap.Put(key, value);
                treeMap.Put(key, value);
            }
            stopwatch.Start();
            HashMapPut(hashMap, amount);
            stopwatch.Stop();
            time[0] = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            TreeMapPut(treeMap, amount);
            stopwatch.Stop();
            time[1] = stopwatch.ElapsedMilliseconds;
            return time;
        }
        public static double[] GetSpeedTest(int amount)
        {
            double[] time = new double[2];
            Stopwatch stopwatch = new Stopwatch();
            MyHashMap<char, int> hashMap = new MyHashMap<char, int>();
            MyTreeMap<char, int> treeMap = new MyTreeMap<char, int>();
            char key;
            int value;
            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                key = (char)random.Next(MODULE);
                value = random.Next(MODULE);
                hashMap.Put(key, value);
                treeMap.Put(key, value);
            }
            stopwatch.Start();
            HashMapGet(hashMap, amount);
            stopwatch.Stop();
            time[0] = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            TreeMapGet(treeMap, amount);
            stopwatch.Stop();
            time[1] = stopwatch.ElapsedMilliseconds;
            return time;
        }
        public static double[] RemoveSpeedTest(int amount)
        {
            double[] time = new double[2];
            Stopwatch stopwatch = new Stopwatch();
            MyHashMap<char, int> hashMap = new MyHashMap<char, int>();
            MyTreeMap<char, int> treeMap = new MyTreeMap<char, int>();
            char key;
            int value;
            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                key = (char)random.Next(MODULE);
                value = random.Next(MODULE);
                hashMap.Put(key, value);
                treeMap.Put(key, value);
            }
            stopwatch.Start();
            HashMapRemove(hashMap);
            stopwatch.Stop();
            time[0] = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            stopwatch.Start();
            TreeMapRemove(treeMap);
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
