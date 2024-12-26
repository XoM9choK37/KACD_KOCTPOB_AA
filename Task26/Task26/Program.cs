using System;
using System.IO;
using Task16;
using Task23;

namespace Task26
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /// Текстовый файл находится в папке Debug
                StreamReader streamReader = new StreamReader("input.txt");
                string line;
                MyHashSet<string> set = new MyHashSet<string>();
                string tempWords;
                string[] words;
                bool flag;
                MyLinkedList<char> alphabet = new MyLinkedList<char>();
                for (int i = 'A'; i < 'Z'; i++)
                    alphabet.Add((char)i);
                for (int i = 'a'; i < 'z'; i++)
                    alphabet.Add((char)i);
                while (!streamReader.EndOfStream)
                {
                    line = streamReader.ReadLine();
                    tempWords = line.Trim(' ', '\t');
                    while (tempWords.Contains("  "))
                        tempWords = tempWords.Replace("  ", " ");
                    words = tempWords.Split(' ', '\t');
                    for (int i = 0; i < words.Length; i++)
                    {
                        flag = true;
                        for (int j = 0; j < words[i].Length && flag; j++)
                            if (!alphabet.Contains(words[i][j]))
                                flag = false;
                        if (!flag)
                            continue;
                        if (!set.Contains(words[i].ToLower()))
                            set.Add(words[i].ToLower());
                    }
                }
                set.Print();
                streamReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
