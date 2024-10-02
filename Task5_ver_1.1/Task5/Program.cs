using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Task4;

namespace Task5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Текстовый файл находится в папке Debug
                const string FILENAME = "input.txt";
                StreamReader streamReader = new StreamReader(FILENAME);
                MyArrayList<string> A = new MyArrayList<string>();
                MyArrayList<char> digits = new MyArrayList<char>();
                MyArrayList<char> letters = new MyArrayList<char>();
                MyArrayList<char> stack = new MyArrayList<char>();
                for (int i = (int)'0'; i <= (int)'9'; i++)
                    digits.Add((char)i);
                for (int i = (int)'A'; i <= (int)'Z'; i++)
                    letters.Add((char)i);
                for (int i = (int)'a'; i <= (int)'z'; i++)
                    letters.Add((char)i);
                string line;
                string subline;
                char c;
                bool flag;
                while (!streamReader.EndOfStream)
                {
                    line = streamReader.ReadLine();
                    for (int i = 0; i < line.Length; i++)
                    {
                        c = line[i];
                        if (c == '<')
                        {
                            stack.Clear();
                            stack.Add(c);
                        }
                        else if (!stack.IsEmpty() &&
                            c == '>' &&
                            (stack.Size() > 1 &&
                            letters.Contains(stack.Get(1)) ||
                            stack.Size() > 2))
                        {
                            stack.Add(c);
                            subline = "";
                            for (int j = 0; j < stack.Size(); j++)
                                subline += stack.Get(j);
                            flag = false;
                            for (int j = 0; j < A.Size(); j++)
                                if (CheckForSame(A.Get(j), subline))
                                    flag = true;
                            if (!flag)
                                A.Add(subline);
                            stack.Clear();
                        }
                        else if (!stack.IsEmpty() &&
                            (digits.Contains(c) &&
                            stack.Get(stack.Size() - 1) != '/' &&
                            stack.Get(stack.Size() - 1) != '<' ||
                            letters.Contains(c) ||
                            c == '/' &&
                            stack.Get(stack.Size() - 1) == '<'))
                            stack.Add(c);
                        else
                            stack.Clear();
                    }
                }
                for (int i = 0; i < A.Size(); i++)
                    Console.Write(A.Get(i) + " ");

                streamReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        static bool CheckForSame(string s1, string s2)
        {
            int distance = (int)'a' - (int)'A';
            if (s1[1] == '/')
                s1 = s1.Remove(1, 1);
            if (s2[1] == '/')
                s2 = s2.Remove(1, 1);
            if (s1.Length != s2.Length)
                return false;
            bool flag = true;
            for (int i = 0; i < s1.Length; i++)
                if ((int)s1[i] != (int)s2[i] &&
                    (int)s1[i] != (int)s2[i] + distance &&
                    (int)s1[i] != (int)s2[i] - distance)
                    flag = false;
            return flag;
        }
    }
}
