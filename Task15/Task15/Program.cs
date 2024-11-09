using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task14;

namespace Task15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /// Все текстовые файлы находятся в папке Debug
            try
            {
                Console.Write("Введите число n: ");
                int n = int.Parse(Console.ReadLine());
                const string FILENAME1 = "input.txt";
                const string FILENAME2 = "sorted.txt";
                StreamReader input = new StreamReader(FILENAME1);
                MyArrayDeque<string> deque = new MyArrayDeque<string>();
                MyArrayDeque<char> digits = new MyArrayDeque<char>();
                for (int i = (int)'0'; i <= (int)'9'; i++)
                    digits.Push((char)i);
                string line;
                int firstCount = 0;
                if (!input.EndOfStream)
                {
                    line = input.ReadLine();
                    deque.Push(line);
                    firstCount = Count(line, digits);
                }
                int digitsCount;
                while (!input.EndOfStream)
                {
                    line = input.ReadLine();
                    digitsCount = Count(line, digits);
                    if (digitsCount > firstCount)
                        deque.AddLast(line);
                    else
                    {
                        deque.AddFirst(line);
                        firstCount = digitsCount;
                    }
                }
                StreamWriter sorted = new StreamWriter(FILENAME2);
                string[] D = deque.ToArray();
                for (int i = 0; i < D.Length; i++)
                    sorted.Write(D[i] + "\n");
                for (int i = 0; i < D.Length; i++)
                    if (Count(D[i], ' ') > n)
                        deque.Remove(D[i]);
                deque.Print();
                input.Close();
                sorted.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static int Count(string line, MyArrayDeque<char> alphabet)
        {
            int digitsCount = 0;
            for (int i = 0; i < line.Length; i++)
                if (alphabet.Contains(line[i]))
                    digitsCount++;
            return digitsCount;
        }
        static int Count(string line, char symbol)
        {
            int digitsCount = 0;
            for (int i = 0; i < line.Length; i++)
                if (line[i] == symbol)
                    digitsCount++;
            return digitsCount;
        }
    }
}
