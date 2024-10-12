using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Task6;
using Task8;

namespace Task9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MyVector<string> digits = new MyVector<string>();
                for (int k = (int)'0'; k <= (int)'9'; k++)
                    digits.Add(((char)k).ToString());
                string[] tempD = {"+", "-", "*", "/", "//", "%", "^",
                "min", "max"};
                MyVector<string> operations = new MyVector<string>();
                operations.AddAll(tempD);
                string[] tempF = {"sqrt", "abs", "sgn", "sin", "cos",
                "tg", "ln", "lg", "exp", "trunc"};
                MyVector<string> functions = new MyVector<string>();
                functions.AddAll(tempF);
                MyVector<string> letters = new MyVector<string>();
                for (int k = (int)'a'; k <= (int)'z'; k++)
                    letters.Add(((char)k).ToString());

                Console.Write("Введите математическое выражение: ");
                string line = Console.ReadLine();
                Console.Write("Введите переменные через запятую " +
                    "(если переменных нет, то просто нажмите \"Enter\"): ");
                string list = Console.ReadLine();

                MyVector<string> newList = new MyVector<string>();
                newList.AddAll(list.Split(','));
                line = ToNums(line, newList);
                MyVector<string> toPolishLine =
                    ToPolish(line, digits, operations, functions, letters);
                Console.Write("Выражение в обратной польской нотации: ");
                for (int i = 0; i < toPolishLine.Size(); i++)
                    Console.Write(toPolishLine.Get(i) + " ");
                Console.Write('\n');

                Console.Write("Результат: ");
                double toPolishResult =
                    PolishCalculate(toPolishLine, digits, operations, functions);
                Console.Write(toPolishResult + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        static int Priority(string operation)
        {
            switch (operation)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                case "//":
                case "%":
                    return 2;
                case "^":
                    return 3;
                case "sqrt":
                case "abs":
                case "sgn":
                case "sin":
                case "cos":
                case "tg":
                case "ln":
                case "lg":
                case "min":
                case "max":
                case "exp":
                case "trunc":
                    return 4;
                default:
                    return 0;
            }
        }
        static double Operation(string operation, double x, double y = 0)
        {
            switch (operation)
            {
                case "+":
                    return x + y;
                case "-":
                    return x - y;
                case "*":
                    return x * y;
                case "/":
                    return x / y;
                case "//":
                    return Math.Floor(x / y);
                case "%":
                    return x - Math.Floor(x / y);
                case "^":
                    return Math.Pow(x, y);
                case "sqrt":
                    return Math.Sqrt(x);
                case "abs":
                    return Math.Abs(x);
                case "sgn":
                    return x / Math.Abs(x);
                case "sin":
                    return Math.Sin(x);
                case "cos":
                    return Math.Cos(x);
                case "tg":
                    return Math.Tan(x);
                case "ln":
                    return Math.Log(x);
                case "lg":
                    return Math.Log10(x);
                case "min":
                    return Math.Min(x, y);
                case "max":
                    return Math.Max(x, y);
                case "exp":
                    return Math.Exp(x);
                case "trunc":
                    return Math.Truncate(x);
                default:
                    return 0;
            }
        }
        static string ToNums(string line, MyVector<string> list)
        {
            string word;
            string x;
            string y;
            for (int i = 0; i < list.Size(); i++)
            {
                word = list.Get(i);
                word = word.Replace(" ", "");
                if (word.Length != 0)
                {
                    x = word.Split('=')[0];
                    y = word.Split('=')[1];
                    line = line.Replace(x, y);
                }
            }
            return line;
        }
        static MyVector<string> ToPolish(string line, MyVector<string> digits,
            MyVector<string> operations, MyVector<string> functions,
            MyVector<string> letters)
        {
            MyVector<string> newLine = new MyVector<string>();
            MyStack<string> myStack = new MyStack<string>();
            MyStack<string> expression = new MyStack<string>();
            int i = 0;
            string number;
            string operation;
            while (i < line.Length)
            {
                if (digits.Contains(line[i].ToString()))
                {
                    number = "";
                    while (i < line.Length &&
                        (digits.Contains(line[i].ToString())))
                    {
                        number += line[i];
                        i++;
                    }
                    expression.Push(number);
                }
                else if (operations.Contains(line[i].ToString()) ||
                    letters.Contains(line[i].ToString()))
                {
                    operation = "";
                    while (i < line.Length &&
                        operations.Contains(line[i].ToString()) ||
                        letters.Contains(line[i].ToString()))
                    {
                        operation += line[i];
                        i++;
                    }
                    if (!operations.Contains(operation) &&
                        !functions.Contains(operation))
                        throw new Exception("Incorrect operation");
                    if (myStack.Empty() || myStack.Peek() == "(")
                        myStack.Push(operation);
                    else if (Priority(operation) > Priority(myStack.Peek()))
                        myStack.Push(operation);
                    else if (Priority(operation) <= Priority(myStack.Peek()))
                    {
                        while (!myStack.Empty() &&
                            Priority(operation) <= Priority(myStack.Peek()) &&
                            myStack.Peek() != "(")
                            expression.Push(myStack.Pop());
                        myStack.Push(operation);
                    }
                }
                else if (line[i] == '(')
                {
                    myStack.Push(line[i].ToString());
                    i++;
                }
                else if (line[i] == ')')
                {
                    while (myStack.Peek() != "(")
                        expression.Push(myStack.Pop());
                    myStack.Pop();
                    i++;
                }
                else
                    i++;
            }
            while (!myStack.Empty())
                expression.Push(myStack.Pop());
            for (int k = 0; k < expression.Size(); k++)
                newLine.Add(expression.Get(k));
            return newLine;
        }
        static double PolishCalculate(MyVector<string> line, MyVector<string> digits,
            MyVector<string> operations, MyVector<string> functions)
        {
            double result = 0;
            MyStack<string> operationsStack = new MyStack<string>();
            MyStack<double> numbersStack = new MyStack<double>();
            string word;
            bool digitFlag;
            double x = 0, y = 0;
            for (int i = 0; i < line.Size(); i++)
            {
                digitFlag = true;
                word = line.Get(i);
                for (int j = 0; j < word.Length && digitFlag; j++)
                    if (!digits.Contains(word[j].ToString()))
                        digitFlag = false;
                if (digitFlag)
                    numbersStack.Push(double.Parse(word));
                else
                    operationsStack.Push(word);
                if (!operationsStack.Empty())
                {
                    if (operations.Contains(word))
                    {
                        y = numbersStack.Pop();
                        x = numbersStack.Pop();
                        result = Operation(word, x, y);
                    }
                    else if (functions.Contains(word))
                    {
                        x = numbersStack.Pop();
                        result = Operation(word, x);
                    }
                    operationsStack.Pop();
                    numbersStack.Push(result);
                }
            }
            return result;
        }
    }
}
