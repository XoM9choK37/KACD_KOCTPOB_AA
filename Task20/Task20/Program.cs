using System;
using System.IO;
using System.Text.RegularExpressions;
using Task18;

namespace Task20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /// Текстовые файлы находятся в папке Debug
            const string INPUTFILE = "input.txt";
            const string OUTPUTFILE = "output.txt";
            StreamReader streamReader = new StreamReader(INPUTFILE);
            StreamWriter streamWriter = new StreamWriter(OUTPUTFILE);
            MyHashMap<string, Pair<Type, string>> defs =
                new MyHashMap<string, Pair<Type, string>>();
            MatchCollection matchCollection;
            Regex regex =
                new Regex(@"[A-Z_a-z][0-9A-Z_a-z]* +[A-Z_a-z][0-9A-Z_a-z]* *= *[0-9]+ *;");
            Pair<Type, string> pair;
            string line;
            string subline;
            string[] def;
            string type;
            string name;
            string value;
            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                matchCollection = regex.Matches(line);
                foreach (Match match in matchCollection)
                {
                    subline = match.Value;
                    subline = subline.Substring(0, subline.Length - 1);
                    def = subline.Split('=');
                    type = def[0].TrimEnd(' ').Split(' ')[0];
                    name = def[0].TrimEnd(' ').Split(' ')[1];
                    value = def[1].Trim(' ');
                    switch (type)
                    {
                        case "int":
                            pair = new Pair<Type, string>(Type.Int, value);
                            break;
                        case "float":
                            pair = new Pair<Type, string>(Type.Float, value);
                            break;
                        case "double":
                            pair = new Pair<Type, string>(Type.Double, value);
                            break;
                        default:
                            streamWriter.Write("Некорректный тип: " +
                                type + "\n");
                            continue;
                    }
                    if (defs.ContainsKey(name))
                    {
                        streamWriter.Write("Переопределение переменной: " +
                            type + " " +
                            name + " = " + value + "\n");
                        continue;
                    }
                    defs.Put(name, pair);
                    streamWriter.Write(type + " => " + name +
                        "(" + value + ")\n");
                }
            }
            streamReader.Close();
            streamWriter.Close();
        }
    }
    enum Type
    {
        Int,
        Float,
        Double
    }
}
