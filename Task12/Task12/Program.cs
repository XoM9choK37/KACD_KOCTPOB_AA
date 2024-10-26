using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Task11;

namespace Task12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Текстовый файл находится в папке Debug
			const string FILENAME = "log.txt";
            try
            {
                StreamWriter streamWriter = new StreamWriter(FILENAME);
                int n = int.Parse(Console.ReadLine());
                MyPriorityQueue<Request> queue = new MyPriorityQueue<Request>();
                queue.ComparatorSet(Comparer<Request>.Create(Comparison));
                Random random = new Random();
                Stopwatch stopwatch = new Stopwatch();
                Tuple[] times =
                    new Tuple[n * 10 + 1];
                Request request;
                Request lastRequest;
                Tuple time;
                int m, k = 1;
                stopwatch.Start();
                for (int i = 1; i <= n; i++)
                {
                    m = random.Next(1, 11);
                    for (int j = 1; j <= m; j++)
                    {
                        request = new Request(random.Next(1, 6), k, i);
                        queue.Add(request);
                        time = new Tuple
                            (stopwatch.Elapsed.TotalMilliseconds,
                            request.priority,
                            request.number,
                            request.step);
                        times[k] = time;
                        streamWriter.Write($"ADD " +
                            $"{request.number} " +
                            $"{request.priority} " +
                            $"{request.step}\n");
                        k++;
                    }
                    lastRequest = queue.Poll();
                    double waitingTime = stopwatch.Elapsed.TotalMilliseconds -
                        times[lastRequest.number].time;
                    time = new Tuple
                        (waitingTime,
                        lastRequest.priority,
                        lastRequest.number,
                        lastRequest.step);
                    times[lastRequest.number] = time;
                    streamWriter.Write($"REMOVE " +
                        $"{lastRequest.number} " +
                        $"{lastRequest.priority} " +
                        $"{lastRequest.step}\n");
                }
                while (!queue.IsEmpty())
                {
                    lastRequest = queue.Poll();
                    double waitingTime = stopwatch.Elapsed.TotalMilliseconds -
                        times[lastRequest.number].time;
                    time = new Tuple
                        (waitingTime,
                        lastRequest.priority,
                        lastRequest.number,
                        lastRequest.step);
                    times[lastRequest.number] = time;
                    streamWriter.Write($"REMOVE " +
                        $"{lastRequest.number} " +
                        $"{lastRequest.priority} " +
                        $"{lastRequest.step}\n");
                }
                stopwatch.Stop();
                int maxIndex = 1;
                for (int i = 2; i < times.Length; i++)
                    if (times[i].time > times[maxIndex].time)
                        maxIndex = i;
                Console.Write("Максимальное время ожидания " +
                    "заявки: " + times[maxIndex].time + "\n");
                Console.Write("Номер заявки: " + times[maxIndex].number +
                    "\n");
                Console.Write("Приоритет заявки: " + times[maxIndex].priority +
                    "\n");
                Console.Write("Номер шага: " + times[maxIndex].step +
                    "\n");
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        static int Comparison(Request request1, Request request2)
        {
            return request1.priority - request2.priority;
        }
    }
	struct Request : IComparable<Request>
    {
		public int priority;
		public int number;
		public int step;
        public Request(int priority, int number, int step)
        {
            this.priority = priority;
            this.number = number;
            this.step = step;
        }
        public int CompareTo(Request request)
        {
            return priority - request.priority;
        }
    }
    struct Tuple
    {
        public double time;
        public int priority;
        public int number;
        public int step;
        public Tuple(double time, int priority, int number, int step)
        {
            this.time = time;
            this.priority = priority;
            this.number = number;
            this.step = step;
        }
    }
}
