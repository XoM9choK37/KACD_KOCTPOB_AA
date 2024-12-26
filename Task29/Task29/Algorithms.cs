using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace Task29
{
    struct Graph
    {
        public List<int> V;
        public List<Tuple<int, int, int>> E;
        public Graph(List<int> V,
            List<Tuple<int, int, int>> E)
        {
            this.V = new List<int>(V);
            this.E = new List<Tuple<int, int, int>>(E);
        }
    }
    static class Algorithms
    {
        const int MAX = 999;
        public static Graph Kruskal(Graph graph)
        {
            var V = graph.V;
            var E = graph.E;
            var V2 = new List<int>(graph.V);
            var E2 = new List<Tuple<int, int, int>>();
            E.OrderBy(x => x.Item3);
            var comps = new int[V.Count + 1];
            for (var i = 1; i <= V.Count; i++)
                comps[i] = i;
            foreach (var edge in E)
            {
                var start = edge.Item1;
                var end = edge.Item2;
                var weight = edge.Item3;
                if (comps[start] != comps[end])
                {
                    E2.Add(new Tuple<int, int, int>
                        (start, end, weight));
                    int a = comps[start];
                    int b = comps[end];
                    for (var i = 1; i <= V.Count; i++)
                        if (comps[i] == b)
                            comps[i] = a;
                }
            }
            return new Graph(V2, E2);
        }
        public static int MaxFlow(List<List<int>> c, int n)
        {
            var f = new List<List<int>>();
            for (int i = 0; i < n; i++)
            {
                f.Add(new List<int>());
                for (int j = 0; j < n; j++)
                    f[i].Add(0);
            }
            for (int i = 1; i < n; i++)
            {
                f[0][i] = c[0][i];
                f[i][0] = -c[0][i];
            }
            var h = new List<int>() { n };
            for (int i = 1; i < n; i++)
                h.Add(0);
            var e1 = new List<int>();
            for (int i = 0; i < n; i++)
                e1.Add(f[0][i]);
            e1[0] = 0;
            while (true)
            {
                int i;
                for (i = 1; i < n - 1; i++)
                    if (e1[i] > 0)
                        break;
                if (i == n - 1)
                    break;
                int j;
                for (j = 0; j < n; j++)
                    if (c[i][j] - f[i][j] > 0 && h[i] == h[j] + 1)
                        break;
                if (j < n)
                    Push(i, j, f, e1, c);
                else
                    Lift(i, h, f, c);
            }
            int flow = 0;
            for (int i = 0; i < n; i++)
                if (c[0][i] > 0)
                    flow += f[0][i];
            return flow;
        }
        public static void Push(int u, int v, List<List<int>> f, List<int> e, List<List<int>> c)
        {
            int d = Math.Min(e[u], c[u][v] - f[u][v]);
            f[u][v] += d;
            f[v][u] = -f[u][v];
            e[u] -= d;
            e[v] += d;
        }
        public static void Lift(int u, List<int> h, List<List<int>> f, List<List<int>> c)
        {
            int d = MAX;
            for (int i = 0; i < f.Count; i++)
                if (c[u][i] - f[u][i] > 0)
                    d = Math.Min(d, h[i]);
            if (d == MAX)
                return;
            h[u] = d + 1;
        }
        public static int FindCliques(List<int> potCliq,
            List<int> remNodes, List<int> skipNodes, int depth,
            List<List<int>> cliqs, List<List<int>> list)
        {
            if (remNodes.Count == 0 && skipNodes.Count == 0)
            {
                cliqs.Add(potCliq);
                return 1;
            }
            int count = 0;
            for (int i = 0; i < remNodes.Count; i++)
            {
                var node = remNodes[i];
                var newPotCliq = new List<int>(potCliq) { node };
                var newRemNodes = new List<int>();
                foreach (var n in remNodes)
                    if (list[node].Contains(n))
                        newRemNodes.Add(n);
                var newSkipList = new List<int>();
                foreach (var n in skipNodes)
                    if (list[node].Contains(n))
                        newSkipList.Add(n);
                count += FindCliques(newPotCliq, newRemNodes,
                    newSkipList, depth + 1, cliqs, list);
                remNodes.Remove(node);
                skipNodes.Add(node);
            }
            return count;
        }
    }
}
