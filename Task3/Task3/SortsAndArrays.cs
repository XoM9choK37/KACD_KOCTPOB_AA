using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task3
{
    static class SortsAndArrays
    {
        public static void Print(int[] A)
        {
            string s = "";
            for (int i = 0; i < A.Length; i++)
                s += $"{A[i]} ";
            MessageBox.Show(s);
        }
        public static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        public static void BubbleSort(int[] A)
        {
            for (int i = 0; i < A.Length; i++)
                for (int j = 0; j < A.Length - 1 - i; j++)
                    if (A[j] > A[j + 1])
                        Swap(ref A[j], ref A[j + 1]);
        }
        public static void ShakerSort(int[] A)
        {
            int begin = 1;
            int end = A.Length - 1;
            while (begin <= end)
            {
                for (int i = end; i >= begin; i--)
                    if (A[i - 1] > A[i])
                        Swap(ref A[i - 1], ref A[i]);
                begin++;
                for (int i = begin; i <= end; i++)
                    if (A[i - 1] > A[i])
                        Swap(ref A[i - 1], ref A[i]);
                end--;
            }
        }
        public static void CombSort(int[] A)
        {
            int step = (int)(A.Length / 1.25);
            while (step != 0)
            {
                for (int i = 0; i + step < A.Length; i++)
                    if (A[i] > A[i + step])
                        Swap(ref A[i], ref A[i + step]);
                step = (int)(step / 1.25);
            }
        }
        public static void InsertionSort(int[] A)
        {
            for (int i = 0; i < A.Length - 1; i++)
            {
                int j = i + 1;
                while (j != 0 && A[j - 1] > A[j])
                {
                    Swap(ref A[j - 1], ref A[j]);
                    j--;
                }
            }
        }
        public static void ShellSort(int[] A)
        {
            for (int i = A.Length / 2; i > 0; i /= 2)
                for (int j = i; j < A.Length; j++)
                {
                    int temp = A[j];
                    int k;
                    for (k = j; k >= i && A[k - i] > temp; k -= i)
                        A[k] = A[k - i];
                    A[k] = temp;
                }
        }
        public static void TreeSort(int[] A)
        {
            Tree tree = new Tree();
            if (A.Length == 0)
                return;
            tree.Info = A[0];
            tree.Left = null;
            tree.Right = null;
            for (int k = 1; k < A.Length; k++)
                TreeAdd(tree, A[k]);
            int i = 0;
            TreeSortDFS(tree, A, ref i);

        }
        public static void TreeAdd(Tree tree, int x)
        {
            if (x <= tree.Info)
            {
                if (tree.Left == null)
                {
                    Tree branch = new Tree();
                    branch.Info = x;
                    branch.Left = null;
                    branch.Right = null;
                    tree.Left = branch;
                }
                else
                    TreeAdd(tree.Left, x);
            }
            else
            {
                if (tree.Right == null)
                {
                    Tree branch = new Tree();
                    branch.Info = x;
                    branch.Left = null;
                    branch.Right = null;
                    tree.Right = branch;
                }
                else
                    TreeAdd(tree.Right, x);
            }
        }
        public static void TreeSortDFS(Tree tree, int[] A, ref int i)
        {
            if (tree != null && i < A.Length)
            {
                TreeSortDFS(tree.Left, A, ref i);
                A[i] = tree.Info;
                i++;
                TreeSortDFS(tree.Right, A, ref i);
            }

        }
        public static void GnomeSort(int[] A)
        {
            int i = 1;
            int j = 2;
            while (i < A.Length)
                if (A[i] > A[i - 1])
                {
                    i = j;
                    j++;
                }
                else
                {
                    Swap(ref A[i], ref A[i - 1]);
                    i--;
                    if (i == 0)
                    {
                        i = j;
                        j++;
                    }
                }
        }
        public static void SelectionSort(int[] A)
        {
            for (int i = 0; i < A.Length - 1; i++)
            {
                int minElementIndex = i;
                for (int j = i + 1; j < A.Length; j++)
                    if (A[j] < A[minElementIndex])
                        minElementIndex = j;
                Swap(ref A[i], ref A[minElementIndex]);
            }
        }
        public static void HeapSort(int[] A)
        {
            if (A.Length <= 1)
                return;
            for (int k = 1; k < A.Length; k++)
            {
                int i = k;
                int parentIndex = (i - 1) / 2;
                while (i != 0 && A[i] > A[parentIndex])
                {
                    Swap(ref A[i], ref A[parentIndex]);
                    i = parentIndex;
                    parentIndex = (i - 1) / 2;
                }
            }
            for (int top = A.Length - 1; top != 0; top--)
            {
                Swap(ref A[0], ref A[top]);
                int i = 0;
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                while ((left < top) && (A[left] > A[i]) ||
                    (right < top) && (A[right] > A[i]))
                {
                    if ((right >= top) || (A[left] > A[right]))
                    {
                        Swap(ref A[left], ref A[i]);
                        i = left;
                    }
                    else
                    {
                        Swap(ref A[right], ref A[i]);
                        i = right;
                    }
                    left = 2 * i + 1;
                    right = 2 * i + 2;
                }
            }
        }
        public static void QuickSort(int[] A)
        {
            if (A.Length == 0)
                return;
            QuickSortRecursion(A, 0, A.Length - 1);
        }
        public static void QuickSortRecursion(int[] A, int begin, int end)
        {
            int i = begin;
            int j = end;
            int element = A[(i + j) / 2];
            do
            {
                while (A[i] < element)
                    i++;
                while (A[j] > element)
                    j--;
                if (i <= j)
                {
                    if (i < j)
                        Swap(ref A[i], ref A[j]);
                    i++;
                    j--;
                }
            } while (i <= j);
            if (i < end)
                QuickSortRecursion(A, i, end);
            if (begin < j)
                QuickSortRecursion(A, begin, j);
        }
        public static void MergeSort(int[] A)
        {
            MergeSortRecursion(A, 0, A.Length - 1);
        }
        public static void MergeSortMerging(int[] A, int left, int middle, int right)
        {
            int leftSize = middle - left + 1;
            int rightSize = right - middle;
            int[] L = new int[leftSize];
            int[] R = new int[rightSize];
            for (int k = 0; k < leftSize; k++)
                L[k] = A[left + k];
            for (int k = 0; k < rightSize; k++)
                R[k] = A[middle + 1 + k];
            int i = 0;
            int j = 0;
            int t = left;
            while (i < leftSize && j < rightSize)
            {
                if (L[i] <= R[j])
                {
                    A[t] = L[i];
                    i++;
                }
                else
                {
                    A[t] = R[j];
                    j++;
                }
                t++;
            }
            while (i < leftSize)
            {
                A[t] = L[i];
                i++;
                t++;
            }
            while (j < rightSize)
            {
                A[t] = R[j];
                j++;
                t++;
            }
        }
        public static void MergeSortRecursion(int[] A, int left, int right)
        {
            if (left >= right)
                return;
            int middle = (left + right) / 2;
            MergeSortRecursion(A, left, middle);
            MergeSortRecursion(A, middle + 1, right);
            MergeSortMerging(A, left, middle, right);
        }
        public static void CountingSort(int[] A)
        {
            if (A.Length == 0)
                return;
            int maxElement = A[0];
            for (int i = 1; i < A.Length; i++)
                if (A[i] > maxElement)
                    maxElement = A[i];
            int[] C = new int[maxElement + 1];
            for (int i = 0; i < A.Length; i++)
                C[A[i]] += 1;
            int k = 0;
            for (int i = 0; i <= maxElement; i++)
                for (int j = 0; j < C[i]; j++)
                {
                    A[k] = i;
                    k++;
                }
        }
        public static void BucketSort(int[] A)
        {
            if (A.Length == 0)
                return;
            int[][] B = new int[A.Length][];
            for (int i = 0; i < A.Length; i++)
                B[i] = new int[0];
            int maxElement = A[0];
            for (int i = 1; i < A.Length; i++)
                if (A[i] > maxElement)
                    maxElement = A[i];
            for (int i = 0; i < A.Length; i++)
            {
                int j = A.Length * A[i] / (maxElement + 1);
                PushBack(ref B[j], A[i]);
            }
            for (int i = 0; i < A.Length; i++)
                BucketSortInsertion(B[i]);
            int k = 0;
            for (int i = 0; i < A.Length; i++)
                for (int j = 0; j < B[i].Length; j++)
                {
                    A[k] = B[i][j];
                    k++;
                }
        }
        public static void PushBack(ref int[] A, int x)
        {
            int[] B = new int[A.Length + 1];
            for (int i = 0; i < A.Length; i++)
                B[i] = A[i];
            B[A.Length] = x;
            A = B;
        }
        public static void BucketSortInsertion(int[] A)
        {
            for (int i = 1; i < A.Length; i++)
            {
                int element = A[i];
                int j = i - 1;
                while (j >= 0 && A[j] > element)
                {
                    A[j + 1] = A[j];
                    j--;
                }
                A[j + 1] = element;
            }
        }
        public static void RadixSort(int[] A)
        {
            if (A.Length == 0)
                return;
            int maxElement = A[0];
            for (int i = 1; i < A.Length; i++)
                if (A[i] > maxElement)
                    maxElement = A[i];
            for (int k = 1; maxElement / k > 0; k *= 10)
                RadixSortCounting(A, k);
        }
        public static void RadixSortCounting(int[] A, int k)
        {
            int[] B = new int[A.Length];
            int[] C = new int[10];
            for (int i = 0; i < A.Length; i++)
                C[(A[i] / k) % 10]++;
            for (int i = 1; i < 10; i++)
                C[i] += C[i - 1];
            for (int i = A.Length - 1; i >= 0; i--)
            {
                B[C[(A[i] / k) % 10] - 1] = A[i];
                C[(A[i] / k) % 10]--;
            }
            for (int i = 0; i < A.Length; i++)
                A[i] = B[i];
        }
        public static void BitonicSort(int[] A)
        {
            BitonicSortRecursion(A, 0, A.Length, true);
        }
        public static void BitonicCompareSwap(int[] A, int i, int j, bool ascending)
        {
            if (ascending)
            {
                if (A[i] > A[j])
                    Swap(ref A[i], ref A[j]);
            }
            else
            {
                if (A[i] < A[j])
                    Swap(ref A[i], ref A[j]);
            }
        }
        public static void BitonicSortMerging(int[] A, int begin, int count, bool ascending)
        {
            if (count > 1)
            {
                int k = count / 2;
                for (int i = begin; i < begin + k; i++)
                    BitonicCompareSwap(A, i, i + k, ascending);
                BitonicSortMerging(A, begin, k, ascending);
                BitonicSortMerging(A, begin + k, k, ascending);
            }
        }
        public static void BitonicSortRecursion(int[] A, int begin, int count, bool ascending)
        {
            if (count > 1)
            {
                int k = count / 2;
                BitonicSortRecursion(A, begin, k, true);
                BitonicSortRecursion(A, begin + k, k, false);
                BitonicSortMerging(A, begin, count, ascending);
            }
        }
        public static int[] RandomArrayCreate(int size, int module)
        {
            int[] A = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
                A[i] = random.Next(0, module);
            return A;
        }
        public static int[] WavyArrayCreate(int size, int module, int sizeModule)
        {
            int[] A = new int[size];
            Random random = new Random();
            int i = 0;
            while (i < size)
            {
                int subSize = random.Next(1, size) * sizeModule;
                int[] B = SortedArrayCreate(subSize, module, true);
                foreach (int b in B)
                    if (i < size)
                    {
                        A[i] = b;
                        i++;
                    }
            }
            return A;
        }
        public static int[] SortedArrayCreate(int size, int module, bool ascending)
        {
            int[] A = RandomArrayCreate(size, module);
            CountingSort(A);
            if (!ascending)
                for (int i = 0; i < size / 2; i++)
                    A[i] = A[size - 1 - i];
            return A;
        }
        public static int[] PermutationArrayCreate(int size, int module, int permutation)
        {
            int[] A = RandomArrayCreate(size, module);
            Random random = new Random();
            for (int i = 0; i < permutation; i++)
            {
                int a = random.Next(0, size);
                int b = random.Next(0, size);
                Swap(ref A[a], ref A[b]);
            }
            return A;
        }
        public static int[] ReplaceArrayCreate(int size, int module, int count)
        {
            int[] A = RandomArrayCreate(size, module);
            Random random = new Random();
            for (int i = 0; i < count; i++)
                A[random.Next(0, size)] = random.Next(0, module);
            return A;
        }
        public static int[] RepeatArrayCreate(int size, int module, double percentage)
        {
            int[] A = RandomArrayCreate(size, module);
            Random random = new Random();
            int element = random.Next(0, module);
            for (int i = 0; i < size * percentage; i++)
                A[random.Next(0, size)] = element;
            return A;
        }
        public static double[][] FirstArrayGroupGeneration(int arrayGroup)
        {
            int[] unsortedArray;
            double[][] T = new double[20][];
            for (int i = 0; i < 20; i++)
                T[i] = new double[5];
            if (arrayGroup < 1 || arrayGroup > 4)
                return null;
            if (arrayGroup == 1)
                for (int i = 0, j = 10; i < 20; i++)
                {
                    unsortedArray = RandomArrayCreate(j, 1000);
                    T[i] = FirstSortGroupSpeedTest(unsortedArray);
                    if (i == 4)
                        j = 100;
                    else if (i == 9)
                        j = 1000;
                    else if (i == 14)
                        j = 10000;
                }
            if (arrayGroup == 2)
                for (int i = 0, j = 10; i < 20; i++)
                {
                    unsortedArray = WavyArrayCreate(j, 1000, j / 10);
                    T[i] = FirstSortGroupSpeedTest(unsortedArray);
                    if (i == 4)
                        j = 100;
                    else if (i == 9)
                        j = 1000;
                    else if (i == 14)
                        j = 10000;
                }
            if (arrayGroup == 3)
                for (int i = 0, j = 10; i < 20; i++)
                {
                    unsortedArray = PermutationArrayCreate(j, 1000, j / 10);
                    T[i] = FirstSortGroupSpeedTest(unsortedArray);
                    if (i == 4)
                        j = 100;
                    else if (i == 9)
                        j = 1000;
                    else if (i == 14)
                        j = 10000;
                }
            if (arrayGroup == 4)
                for (int i = 0, j = 10; i < 20; i++)
                {
                    if (i % 5 == 0)
                        unsortedArray =
                            SortedArrayCreate(j, 1000, true);
                    else if (i % 5 == 1)
                        unsortedArray =
                            SortedArrayCreate(j, 1000, false);
                    else if (i % 5 == 2)
                        unsortedArray =
                            ReplaceArrayCreate(j, 1000, j / 10);
                    else if (i % 5 == 3)
                        unsortedArray =
                            RepeatArrayCreate(j, 1000,
                            (i + 1) % 2 * 0.1 + (i % 2) * 0.9);
                    else
                        unsortedArray =
                            RepeatArrayCreate(j, 1000,
                            0.25 * ((i % 3) + 1));
                    T[i] = FirstSortGroupSpeedTest(unsortedArray);
                    if (i == 4)
                        j = 100;
                    else if (i == 9)
                        j = 1000;
                    else if (i == 14)
                        j = 10000;
                }
            return T;
        }
        public static double[][] SecondArrayGroupGeneration(int arrayGroup)
        {
            // Количество элементов в массивах второй группы
            // уменьшено для предотвращения переполнения памяти
            // (которое может происходить, например, из-за рекурсивных
            // вызовов метода BitonicSort)
            int[] unsortedArray;
            double[][] T = new double[20][];
            for (int i = 0; i < 20; i++)
                T[i] = new double[3];
            if (arrayGroup < 1 || arrayGroup > 4)
                return null;
            if (arrayGroup == 1)
                for (int i = 0, j = 10; i < 20; i++)
                {
                    unsortedArray = RandomArrayCreate(j, 1000);
                    T[i] = SecondSortGroupSpeedTest(unsortedArray);
                    if (i == 3)
                        j = 10;
                    else if (i == 7)
                        j = 100;
                    else if (i == 11)
                        j = 1000;
                    else if (i == 15)
                        j = 10000;
                }
            if (arrayGroup == 2)
                for (int i = 0, j = 10; i < 20; i++)
                {
                    unsortedArray = WavyArrayCreate(j, 1000, j / 10);
                    T[i] = SecondSortGroupSpeedTest(unsortedArray);
                    if (i == 3)
                        j = 10;
                    else if (i == 7)
                        j = 100;
                    else if (i == 11)
                        j = 1000;
                    else if (i == 15)
                        j = 10000;
                }
            if (arrayGroup == 3)
                for (int i = 0, j = 10; i < 20; i++)
                {
                    unsortedArray = PermutationArrayCreate(j, 1000, j / 10);
                    T[i] = SecondSortGroupSpeedTest(unsortedArray);
                    if (i == 3)
                        j = 10;
                    else if (i == 7)
                        j = 100;
                    else if (i == 11)
                        j = 1000;
                    else if (i == 15)
                        j = 10000;
                }
            if (arrayGroup == 4)
                for (int i = 0, j = 10; i < 20; i++)
                {
                    if (i % 5 == 0)
                        unsortedArray =
                            SortedArrayCreate(j, 1000, true);
                    else if (i % 5 == 1)
                        unsortedArray =
                            SortedArrayCreate(j, 1000, false);
                    else if (i % 5 == 2)
                        unsortedArray =
                            ReplaceArrayCreate(j, 1000, j / 10);
                    else if (i % 5 == 3)
                        unsortedArray =
                            RepeatArrayCreate(j, 1000,
                            (i + 1) % 2 * 0.1 + (i % 2) * 0.9);
                    else
                        unsortedArray =
                            RepeatArrayCreate(j, 1000,
                            0.25 * ((i % 3) + 1));
                    T[i] = SecondSortGroupSpeedTest(unsortedArray);
                    if (i == 3)
                        j = 10;
                    else if (i == 7)
                        j = 100;
                    else if (i == 11)
                        j = 1000;
                    else if (i == 15)
                        j = 10000;
                }
            return T;
        }
        public static double[][] ThirdArrayGroupGeneration(int arrayGroup)
        {
            // Количество элементов в массивах третьей группы
            // уменьшено для предотвращения переполнения памяти
            // (которое может происходить, например, из-за рекурсивных
            // вызовов метода MergeSortRecursion)
            int[] unsortedArray;
            double[][] T = new double[20][];
            for (int i = 0; i < 20; i++)
                T[i] = new double[6];
            if (arrayGroup < 1 || arrayGroup > 4)
                return null;
            if (arrayGroup == 1)
                for (int i = 0, j = 10; i < 20; i++)
                {
                    unsortedArray = RandomArrayCreate(j, 1000);
                    T[i] = ThirdSortGroupSpeedTest(unsortedArray);
                    if (i == 2)
                        j = 10;
                    else if (i == 5)
                        j = 100;
                    else if (i == 8)
                        j = 1000;
                    else if (i == 11)
                        j = 10000;
                    else if (i == 14)
                        j = 10000;
                }
            if (arrayGroup == 2)
                for (int i = 0, j = 10; i < 20; i++)
                {
                    unsortedArray = WavyArrayCreate(j, 1000, j / 10);
                    T[i] = ThirdSortGroupSpeedTest(unsortedArray);
                    if (i == 2)
                        j = 10;
                    else if (i == 5)
                        j = 100;
                    else if (i == 8)
                        j = 1000;
                    else if (i == 11)
                        j = 10000;
                    else if (i == 14)
                        j = 10000;
                }
            if (arrayGroup == 3)
                for (int i = 0, j = 10; i < 20; i++)
                {
                    unsortedArray = PermutationArrayCreate(j, 1000, j / 10);
                    T[i] = ThirdSortGroupSpeedTest(unsortedArray);
                    if (i == 2)
                        j = 10;
                    else if (i == 5)
                        j = 100;
                    else if (i == 8)
                        j = 1000;
                    else if (i == 11)
                        j = 10000;
                    else if (i == 14)
                        j = 10000;
                }
            if (arrayGroup == 4)
                for (int i = 0, j = 10; i < 20; i++)
                {
                    if (i % 5 == 0)
                        unsortedArray =
                            SortedArrayCreate(j, 1000, true);
                    else if (i % 5 == 1)
                        unsortedArray =
                            SortedArrayCreate(j, 1000, false);
                    else if (i % 5 == 2)
                        unsortedArray =
                            ReplaceArrayCreate(j, 1000, j / 10);
                    else if (i % 5 == 3)
                        unsortedArray =
                            RepeatArrayCreate(j, 1000,
                            (i + 1) % 2 * 0.1 + (i % 2) * 0.9);
                    else
                        unsortedArray =
                            RepeatArrayCreate(j, 1000,
                            0.25 * ((i % 3) + 1));
                    T[i] = ThirdSortGroupSpeedTest(unsortedArray);
                    if (i == 2)
                        j = 10;
                    else if (i == 5)
                        j = 100;
                    else if (i == 8)
                        j = 1000;
                    else if (i == 11)
                        j = 10000;
                    else if (i == 14)
                        j = 10000;
                }
            return T;
        }
        public static double[] FirstSortGroupSpeedTest(int[] unsortedArray)
        {
            // Тестовые и отсортированные массивы
            // автоматически сохраняются в файл
            // для экономии памяти
            int[] sortingArray = new int[unsortedArray.Length];
            double[] A = new double[5];
            Stopwatch stopwatch = new Stopwatch();

            CopyArrayFromTo(unsortedArray, sortingArray);
            stopwatch.Start();
            BubbleSort(sortingArray);
            stopwatch.Stop();
            A[0] = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            CopyArrayFromTo(unsortedArray, sortingArray);
            stopwatch.Start();
            InsertionSort(sortingArray);
            stopwatch.Stop();
            A[1] = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            CopyArrayFromTo(unsortedArray, sortingArray);
            stopwatch.Start();
            SelectionSort(sortingArray);
            stopwatch.Stop();
            A[2] = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            CopyArrayFromTo(unsortedArray, sortingArray);
            stopwatch.Start();
            ShakerSort(sortingArray);
            stopwatch.Stop();
            A[3] = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            CopyArrayFromTo(unsortedArray, sortingArray);
            stopwatch.Start();
            GnomeSort(sortingArray);
            stopwatch.Stop();
            A[4] = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();
            
            // Файл находится в папке Debug
            StreamWriter streamWriter = new StreamWriter("Arrays.txt");
            for (int i = 0; i < unsortedArray.Length; i++)
                streamWriter.Write($"{unsortedArray[i]} ");
            streamWriter.Write("\n");
            for (int i = 0; i < sortingArray.Length; i++)
                streamWriter.Write($"{sortingArray[i]} ");
            streamWriter.Close();

            return A;
        }
        public static double[] SecondSortGroupSpeedTest(int[] unsortedArray)
        {
            // Тестовые и отсортированные массивы
            // автоматически сохраняются в файл
            // для экономии памяти
            int[] sortingArray = new int[unsortedArray.Length];
            double[] A = new double[3];
            Stopwatch stopwatch = new Stopwatch();

            CopyArrayFromTo(unsortedArray, sortingArray);
            stopwatch.Start();
            BitonicSort(sortingArray);
            stopwatch.Stop();
            A[0] = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            CopyArrayFromTo(unsortedArray, sortingArray);
            stopwatch.Start();
            ShellSort(sortingArray);
            stopwatch.Stop();
            A[1] = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            CopyArrayFromTo(unsortedArray, sortingArray);
            stopwatch.Start();
            TreeSort(sortingArray);
            stopwatch.Stop();
            A[2] = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            // Файл находится в папке Debug
            StreamWriter streamWriter = new StreamWriter("Arrays.txt");
            for (int i = 0; i < unsortedArray.Length; i++)
                streamWriter.Write($"{unsortedArray[i]} ");
            streamWriter.Write("\n");
            for (int i = 0; i < sortingArray.Length; i++)
                streamWriter.Write($"{sortingArray[i]} ");
            streamWriter.Close();

            return A;
        }
        public static double[] ThirdSortGroupSpeedTest(int[] unsortedArray)
        {
            // Тестовые и отсортированные массивы
            // автоматически сохраняются в файл
            // для экономии памяти
            int[] sortingArray = new int[unsortedArray.Length];
            double[] A = new double[6];
            Stopwatch stopwatch = new Stopwatch();

            CopyArrayFromTo(unsortedArray, sortingArray);
            stopwatch.Start();
            CombSort(sortingArray);
            stopwatch.Stop();
            A[0] = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            CopyArrayFromTo(unsortedArray, sortingArray);
            stopwatch.Start();
            HeapSort(sortingArray);
            stopwatch.Stop();
            A[1] = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            CopyArrayFromTo(unsortedArray, sortingArray);
            stopwatch.Start();
            QuickSort(sortingArray);
            stopwatch.Stop();
            A[2] = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            CopyArrayFromTo(unsortedArray, sortingArray);
            stopwatch.Start();
            MergeSort(sortingArray);
            stopwatch.Stop();
            A[3] = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            CopyArrayFromTo(unsortedArray, sortingArray);
            stopwatch.Start();
            CountingSort(sortingArray);
            stopwatch.Stop();
            A[4] = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            CopyArrayFromTo(unsortedArray, sortingArray);
            stopwatch.Start();
            RadixSort(sortingArray);
            stopwatch.Stop();
            A[5] = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            // Файл находится в папке Debug
            StreamWriter streamWriter = new StreamWriter("Arrays.txt");
            for (int i = 0; i < unsortedArray.Length; i++)
                streamWriter.Write($"{unsortedArray[i]} ");
            streamWriter.Write("\n");
            for (int i = 0; i < sortingArray.Length; i++)
                streamWriter.Write($"{sortingArray[i]} ");
            streamWriter.Close();

            return A;
        }
        public static void CopyArrayFromTo(int[] A, int[] B)
        {
            for (int i = 0; i < A.Length && i < B.Length; i++)
                B[i] = A[i];
        }
        public static double[] GetMatrixAverage(double[][] T)
        {
            double[] A = new double[T.Length];
            double sum;
            for (int j = 0; j < T[0].Length; j++)
            {
                sum = 0;
                for (int i = 0; i < T.Length; i++)
                    sum += T[i][j];
                A[j] = sum / T.Length;
            }
            return A;
        }
    }
    class Tree
    {
        public int Info;
        public Tree Left;
        public Tree Right;
    }
}
