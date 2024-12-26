using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Task29;

namespace Task29TestConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var p = int.Parse(Console.ReadLine());
            /// var q = int.Parse(Console.ReadLine());
            /*
            var V = new List<int>();
            for (int i = 1; i <= p; i++)
                V.Add(i);
            var E = new List<Tuple<int, int, int>>();
            for (int i = 0; i < q; i++)
            {
                var temp = Console.ReadLine().Trim().Split(' ').Select(x => int.Parse(x)).ToList();
                E.Add(new Tuple<int, int, int>(temp[0], temp[1], temp[2]));
            }

            Graph graph = new Graph(V, E);
            Graph graph2 = Algorithms.Kruskal(graph);
            Console.WriteLine();
            foreach (var edge in graph2.E)
                Console.WriteLine(edge.Item1 + " " + edge.Item2 + " " + edge.Item3);

            var c = new List<List<int>>();
            for (int i = 0; i < p; i++)
                c.Add(new List<int>());
            for (int i = 0; i < p; i++)
                for (int j = 0; j < p; j++)
                {
                    Console.Write((i + 1) + ", " + (j + 1) + ": ");
                    c[i].Add(int.Parse(Console.ReadLine()));
                }
            Console.WriteLine();
            Console.WriteLine(Algorithms.MaxFlow(c, p));  
            */
            var list = new List<List<int>>();
            for (int i = 0; i < p; i++)
                list.Add(new List<int>());
            for (int i = 0; i < p; i++)
            {
                Console.Write((i + 1) + ": ");
                list[i].AddRange(Console.ReadLine().Trim().Split(' ').Select(x => int.Parse(x) - 1).ToList());
            }
            var cliqs = new List<List<int>>();
            var potCliq = new List<int>();
            var remNodes = new List<int>();
            for (int i = 0; i < p; i++)
                remNodes.Add(i);
            var skipNodes = new List<int>();
            int depth = 0;
            Algorithms.FindCliques(potCliq, remNodes, skipNodes, depth, cliqs, list);
            for (int i = 0; i < cliqs.Count; i++)
            {
                for (int j = 0; j < cliqs[i].Count; j++)
                    Console.Write((cliqs[i][j] + 1) + " ");
                Console.WriteLine();
            }
        }
    }
}
