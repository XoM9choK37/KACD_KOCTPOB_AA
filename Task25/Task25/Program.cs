using System;
using System.IO;
using Task16;
using Task23;

namespace Task25
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
                while (!streamReader.EndOfStream)
                {
                    line = streamReader.ReadLine();
                    set.Add(line);
                }
                MyLinkedList<string> lines = new MyLinkedList<string>(set.ToArray());
                MyLinkedList<string> words = new MyLinkedList<string>();
                for (int i = 0; i < lines.Size(); i++)
                    words.Add(MinWord(lines.Get(i)));
                int index;
                string tempWord;
                string tempLine;
                for (int i = 0; i < words.Size() - 1; i++)
                {
                    index = i;
                    for (int j = i + 1; j < words.Size(); j++)
                    {
                        if (words.Get(j).Length < words.Get(index).Length)
                        {
                            index = j;
                            continue;
                        }
                        if (words.Get(j).Length == words.Get(index).Length)
                        {
                            string tempLine1 = MinWordLenRemove(lines.Get(j));
                            string tempLine2 = MinWordLenRemove(lines.Get(index));
                            string minWord1 = MinWord(tempLine1);
                            string minWord2 = MinWord(tempLine2);
                            while (minWord1 != "" && minWord2 != "" &&
                                minWord1.Length == minWord2.Length)
                            {
                                tempLine1 = MinWordLenRemove(tempLine1);
                                tempLine2 = MinWordLenRemove(tempLine2);
                                minWord1 = MinWord(tempLine1);
                                minWord2 = MinWord(tempLine2);
                            }
                            if (minWord1 != "" && minWord2 != "" &&
                                minWord1.Length < minWord2.Length)
                            {
                                index = j;
                                continue;
                            }
                            if (minWord2 == "")
                                index = j;
                        }
                    }
                    tempWord = words.Get(i);
                    words.Set(i, words.Get(index));
                    words.Set(index, tempWord);
                    tempLine = lines.Get(i);
                    lines.Set(i, lines.Get(index));
                    lines.Set(index, tempLine);
                }
                for (int i = 0; i < lines.Size(); i++)
                    Console.WriteLine(lines.Get(i));
                streamReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static string MinWord(string line)
        {
            string tempLine = line.Trim(' ');
            while (tempLine.Contains("  "))
                tempLine = tempLine.Replace("  ", " ");
            MyLinkedList<string> words = new MyLinkedList<string>();
            words.AddAll(tempLine.Split(' ', '\t'));
            string word = "";
            if (words.Size() != 0)
                word = words.Get(0);
            for (int i = 1; i < words.Size(); i++)
                if (words.Get(i).Length < word.Length)
                    word = words.Get(i);
            return word;
        }
        static string MinWordLenRemove(string line)
        {
            string tempLine = line.Trim(' ');
            while (tempLine.Contains("  "))
                tempLine = tempLine.Replace("  ", " ");
            MyLinkedList<string> words = new MyLinkedList<string>();
            words.AddAll(tempLine.Split(' ', '\t'));
            string word = MinWord(line);
            string newLine = "";
            for (int i = 0; i < words.Size(); i++)
                if (words.Get(i).Length != word.Length)
                    newLine += words.Get(i) + " ";
            return newLine.TrimEnd(' ');
        }
    }
}
