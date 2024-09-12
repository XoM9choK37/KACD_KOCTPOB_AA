using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Task2
{
    internal class Task2
    {
        static void Main(string[] args)
        {
            Complex z = new Complex(0, 0);
            Complex w = new Complex(0, 0);
            double rez;
            double imz;
            double rew;
            double imw;
            PrintMenu();
            while (true)
            {
                Console.Write("Выберите действие: ");
                string command = Console.ReadLine();
                try
                {
                    switch (command)
                    {
                        case "a":
                            {
                                Console.Write("Введите вещественную часть комплексного " +
                                    "числа: ");
                                rez = double.Parse(Console.ReadLine());
                                Console.Write("Введите мнимую часть комплексного " +
                                    "числа: ");
                                imz = double.Parse(Console.ReadLine());
                                z.Re = rez;
                                z.Im = imz;
                                Console.Write("Результат записан в текущее " +
                                    "комплексное число\n");
                                break;
                            }
                        case "b":
                            {
                                Console.Write("Введите вещественную часть второго " +
                                    "комплексного числа: ");
                                rew = double.Parse(Console.ReadLine());
                                Console.Write("Введите мнимую часть второго " +
                                    "комплексного числа: ");
                                imw = double.Parse(Console.ReadLine());
                                w.Re = rew;
                                w.Im = imw;
                                z = z + w;
                                Console.Write("Результат записан в текущее " +
                                    "комплексное число\n");
                                break;
                            }
                        case "c":
                            {
                                Console.Write("Введите вещественную часть второго " +
                                    "комплексного числа: ");
                                rew = double.Parse(Console.ReadLine());
                                Console.Write("Введите мнимую часть второго " +
                                    "комплексного числа: ");
                                imw = double.Parse(Console.ReadLine());
                                w.Re = rew;
                                w.Im = imw;
                                z = z - w;
                                Console.Write("Результат записан в текущее " +
                                    "комплексное число\n");
                                break;
                            }
                        case "d":
                            {
                                Console.Write("Введите вещественную часть второго " +
                                    "комплексного числа: ");
                                rew = double.Parse(Console.ReadLine());
                                Console.Write("Введите мнимую часть второго " +
                                    "комплексного числа: ");
                                imw = double.Parse(Console.ReadLine());
                                w.Re = rew;
                                w.Im = imw;
                                z = z * w;
                                Console.Write("Результат записан в текущее " +
                                    "комплексное число\n");
                                break;
                            }
                        case "e":
                            {
                                Console.Write("Введите вещественную часть второго " +
                                    "комплексного числа: ");
                                rew = double.Parse(Console.ReadLine());
                                Console.Write("Введите мнимую часть второго " +
                                    "комплексного числа: ");
                                imw = double.Parse(Console.ReadLine());
                                w.Re = rew;
                                w.Im = imw;
                                z = z / w;
                                Console.Write("Результат записан в текущее " +
                                    "комплексное число\n");
                                break;
                            }
                        case "f":
                            {
                                Console.Write($"|z| = {Complex.Abs(z)}\n");
                                break;
                            }
                        case "g":
                            {
                                Console.Write($"arg(z) = {Complex.Arg(z)}\n");
                                break;
                            }
                        case "h":
                            {
                                Console.Write($"Re(z) = {z.Re}\n");
                                break;
                            }
                        case "i":
                            {
                                Console.Write($"Im(z) = {z.Im}\n");
                                break;
                            }
                        case "j":
                            {
                                Console.Write("z = ");
                                Complex.Print(z);
                                break;
                            }
                        case "q":
                            {
                                Environment.Exit(0);
                                break;
                            }
                        case "/help":
                            {
                                PrintMenu();
                                break;
                            }
                        default:
                            {
                                Console.Write("Неизвестная команда!\n");
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write($"{ex}\n");
                }
            }
        }
        public struct Complex
        {
            public double Re;
            public double Im;
            public Complex(double a, double b)
            {
                Re = a;
                Im = b;
            }
            public static Complex Add(Complex z1, Complex z2)
            {
                Complex z = new Complex(z1.Re + z2.Re, z1.Im + z2.Im);
                return z;
            }
            public static Complex operator +(Complex z1, Complex z2)
            {
                return Add(z1, z2);
            }
            public static Complex Sub(Complex z1, Complex z2)
            {
                Complex z = new Complex(z1.Re - z2.Re, z1.Im - z2.Im);
                return z;
            }
            public static Complex operator -(Complex z1, Complex z2)
            {
                return Sub(z1, z2);
            }
            public static Complex Mul(Complex z1, Complex z2)
            {
                double rez = z1.Re * z2.Re - z1.Im * z2.Im;
                double imz = z1.Re * z2.Im + z2.Re * z1.Im;
                Complex z = new Complex(rez, imz);
                return z;
            }
            public static Complex operator *(Complex z1, Complex z2)
            {
                return Mul(z1, z2);
            }
            public static Complex Div(Complex z1, Complex z2)
            {
                double rez = (z1.Re * z2.Re + z1.Im * z2.Im) /
                    (z2.Re * z2.Re + z2.Im * z2.Im);
                double imz = (z1.Im * z2.Re - z1.Re * z2.Im) /
                    (z2.Re * z2.Re + z2.Im * z2.Im);
                Complex z = new Complex(rez, imz);
                return z;
            }
            public static Complex operator /(Complex z1, Complex z2)
            {
                return Div(z1, z2);
            }
            public static double Abs(Complex z)
            {
                return Math.Sqrt(z.Re * z.Re + z.Im * z.Im);
            }
            public static double Arg(Complex z)
            {
                if (z.Re == 0 && z.Im > 0)
                    return Math.PI / 2;
                if (z.Re == 0 && z.Im < 0)
                    return -Math.PI / 2;
                if (z.Re < 0 && z.Im > 0)
                    return Math.Atan(z.Im / z.Re) + Math.PI;
                if (z.Re < 0 && z.Im < 0)
                    return Math.Atan(z.Im / z.Re) - Math.PI;
                if (z.Re < 0 && z.Im == 0)
                    return Math.PI;
                return Math.Atan(z.Im / z.Re);
            }
            public static void Print(Complex z)
            {
                Console.Write($"({z.Re}; {z.Im})\n");
            }
        }
        public static void PrintMenu()
        {
            Console.Write("Доступные команды:\n" +
                "a - Создать комплексное число\n" +
                "b - Сложить комплексные числа\n" +
                "c - Вычесть комплексные числа\n" +
                "d - Перемножить комплексные числа\n" +
                "e - Поделить комплексные числа\n" +
                "f - Найти модуль комплексного числа\n" +
                "g - Найти аргумент комплексного числа\n" +
                "h - Вывести вещественную часть комплексного числа\n" +
                "i - Вывести мнимую часть комплексного числа\n" +
                "j - Вывести комплексное число\n" +
                "q - Выйти из программы\n" +
                "/help - Показать список доступных команд\n");
        }
    }
}
