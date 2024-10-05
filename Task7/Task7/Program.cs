using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task6;

namespace Task7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Текстовые файлы находятся в папке Debug
                const string INPUTFILE = "input.txt";
                const string OUTPUTFILE = "output.txt";
                StreamReader streamReader = new StreamReader(INPUTFILE);
                StreamWriter streamWriter = new StreamWriter(OUTPUTFILE);
                MyVector<string> lines = new MyVector<string>();
                while (!streamReader.EndOfStream)
                    lines.Add(streamReader.ReadLine());
                MyVector<string> IPs = new MyVector<string>();
                MyVector<char> digits = new MyVector<char>();
                for (int i = (int)'0'; i <= (int)'9'; i++)
                    digits.Add((char)i);
                string line;
                string subline = "";
                int begin;
                int end;
                int count;
                for (int k = 0; k < lines.Size(); k++)
                {
                    line = lines.Get(k);
                    begin = -1;
                    end = -1;
                    count = 0;
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (!digits.Contains(line[i]) &&
                            line[i] != '.')
                        {
                            begin = -1;
                            end = -1;
                            count = 0;
                        }
                        else if (begin == -1 && digits.Contains(line[i]))
                            begin = i;
                        else if (begin != -1 && line[i] == '.')
                            count++;
                        if (count == 3)
                        {
                            if (i < line.Length - 1)
                                i++;
                            while (digits.Contains(line[i]) &&
                                i < line.Length)
                                i++;
                            if (line[i] != '.')
                            {
                                end = i - 1;
                                for (int j = begin; j <= end; j++)
                                    subline += line[j];
                                if (IPCheck(subline) &&
                                    !IPs.Contains(subline))
                                    IPs.Add(subline);
                            }
                            subline = "";
                            begin = -1;
                            end = -1;
                            count = 0;
                            i--;
                        }
                    }
                }
                for (int i = 0; i < IPs.Size(); i++)
                    streamWriter.Write(IPs.Get(i) + '\t');
                streamReader.Close();
                streamWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static bool IPCheck(string subline)
        {
            MyVector<string> stringNumbers = new MyVector<string>();
            stringNumbers.AddAll(subline.Split('.'));
            int element;
            for (int i = 0; i < stringNumbers.Size(); i++)
                if (stringNumbers.Get(i) != null)
                {
                    element = int.Parse(stringNumbers.Get(i));
                    if (element < 0 || element > 255)
                        return false;
                }
            return true;
        }
    }
}
