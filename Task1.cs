using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task1
{
    internal class Task1
    {
        static void Main(string[] args)
        {
            const string FILENAME = "file.txt";
            try
            {
                StreamReader streamReader = new StreamReader(FILENAME);
                uint dimension = uint.Parse(streamReader.ReadLine());
                double[][] tensorMatrix = new double[dimension][];
                for (int i = 0; i < dimension; i++)
                    tensorMatrix[i] = streamReader.ReadLine()
                        .Split(' ').Select(x => double.Parse(x)).ToArray();
                if (SymmetryCheck(tensorMatrix))
                {
                    double[][] vector = new double[1][];
                    vector[0] = streamReader.ReadLine()
                        .Split(' ').Select(x => double.Parse(x)).ToArray();
                    Console.Write($"{VectorLength(vector, tensorMatrix)}\n");
                }
                else
                    Console.Write("Матрица тензора не является симметричной!\n");
                streamReader.Close();
            }
            catch (Exception ex)
            {
                Console.Write($"{ex}\n");
            }
        }
        static bool SymmetryCheck(double[][] G)
        {
            for (int i = 1; i < G.Length; i++)
                for (int j = i; j < G[0].Length; j++)
                    if (G[i][j] != G[j][i])
                        return false;
            return true;
        }
        static double[][] MatrixMultiply(double[][] A, double[][] B)
        {
            double[][] C = new double[A.Length][];
            for (int i = 0; i < A.Length; i++)
                C[i] = new double[B[0].Length];
            for (int i = 0; i < A.Length; i++)
                for (int j = 0; j < B[0].Length; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < A[0].Length; k++)
                        sum += A[i][k] * B[k][j];
                    C[i][j] = sum;
                }
            return C;
        }
        static double[][] MatrixTranspose(double[][] A)
        {
            double[][] T = new double[A[0].Length][];
            for (int i = 0; i < A[0].Length; i++)
                T[i] = new double[A.Length];
            for (int i = 0; i < A.Length; i++)
                for (int j = 0; j < A[0].Length; j++)
                    T[j][i] = A[i][j];
            return T;
        }
        static double VectorLength(double[][] X, double[][] G)
        {
            double[][] A1 = MatrixMultiply(X, G);
            double[][] A2 = MatrixMultiply(A1, MatrixTranspose(X));
            return Math.Sqrt(A2[0][0]);
        }
    }
}
