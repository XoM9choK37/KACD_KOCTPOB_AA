using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task29
{
    public partial class Form1 : Form
    {
        int p = 8;
        int q = 10;
        List<Tuple<int, int>> coords = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(100, 200),
            new Tuple<int, int>(200, 100),
            new Tuple<int, int>(200, 300),
            new Tuple<int, int>(300, 100),
            new Tuple<int, int>(300, 300),
            new Tuple<int, int>(400, 100),
            new Tuple<int, int>(400, 300),
            new Tuple<int, int>(500, 200),
        };
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label3.Hide();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var V = new List<int>();
            for (int i = 0; i < p; i++)
                V.Add(i + 1);
            var E = new List<Tuple<int, int, int>>()
            {
                new Tuple<int, int, int>(1, 2, 10),
                new Tuple<int, int, int>(1, 3, 11),
                new Tuple<int, int, int>(2, 4, 4),
                new Tuple<int, int, int>(2, 5, 7),
                new Tuple<int, int, int>(3, 4, 8),
                new Tuple<int, int, int>(3, 5, 9),
                new Tuple<int, int, int>(4, 6, 12),
                new Tuple<int, int, int>(5, 7, 6),
                new Tuple<int, int, int>(6, 8, 5),
                new Tuple<int, int, int>(7, 8, 15),
            };
            var graph = new Graph(V, E);
            var kryskal = Algorithms.Kruskal(graph);
            var g = e.Graphics;
            var pts = new List<Point>();
            foreach (var tuple in coords)
                pts.Add(new Point(tuple.Item1 + 10, tuple.Item2 + 10));
            var eFont = new Font(FontFamily.GenericSansSerif, 13);
            foreach (var tuple in E)
                if (!(tuple.Item1 == 3 && tuple.Item2 == 4) &&
                    !(tuple.Item1 == 2 && tuple.Item2 == 5))
                {
                    g.DrawLine(new Pen(Color.Black, 3),
                        pts[tuple.Item1 - 1], pts[tuple.Item2 - 1]);
                    var rect = new Rectangle(
                        (pts[tuple.Item1 - 1].X + pts[tuple.Item2 - 1].X) / 2,
                        (pts[tuple.Item1 - 1].Y + pts[tuple.Item2 - 1].Y) / 2,
                        25, 20);
                    g.FillRectangle(Brushes.White, rect);
                    g.DrawRectangle(new Pen(Color.Black, 3), rect);
                    g.DrawString(tuple.Item3.ToString(), eFont, Brushes.Black,
                        (pts[tuple.Item1 - 1].X + pts[tuple.Item2 - 1].X) / 2,
                        (pts[tuple.Item1 - 1].Y + pts[tuple.Item2 - 1].Y) / 2);
                }
            g.DrawLine(new Pen(Color.Black, 3),
                        pts[2], pts[3]);
            var newRect = new Rectangle(
                (pts[2].X + pts[3].X) / 2 + 5,
                (pts[2].Y + pts[3].Y) / 2 - 50,
                25, 20);
            g.FillRectangle(Brushes.White, newRect);
            g.DrawRectangle(new Pen(Color.Black, 3), newRect);
            g.DrawString("8", eFont, Brushes.Black,
                (pts[2].X + pts[3].X) / 2 + 5,
                (pts[2].Y + pts[3].Y) / 2 - 50);
            g.DrawLine(new Pen(Color.Black, 3),
                        pts[1], pts[4]);
            var newNewRect = new Rectangle(
                (pts[1].X + pts[4].X) / 2 + 5,
                (pts[1].Y + pts[4].Y) / 2 + 50,
                25, 20);
            g.FillRectangle(Brushes.White, newNewRect);
            g.DrawRectangle(new Pen(Color.Black, 3), newNewRect);
            g.DrawString("7", eFont, Brushes.Black,
                (pts[1].X + pts[4].X) / 2 + 5,
                (pts[1].Y + pts[4].Y) / 2 + 50);
            for (int i = 0; i < p; i++)
            {
                var tuple = coords[i];
                pts.Add(new Point(tuple.Item1 + 10, tuple.Item2 + 10));
                g.DrawEllipse(new Pen(Color.Black, 3),
                    tuple.Item1, tuple.Item2, 20, 20);
                g.FillEllipse(Brushes.Aqua,
                    tuple.Item1, tuple.Item2, 20, 20);
                var font = new Font(FontFamily.GenericSansSerif, 14);
                g.DrawString($"{i + 1}", font, Brushes.Black,
                    tuple.Item1 + 2, tuple.Item2 - 1);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var V = new List<int>();
            for (int i = 0; i < p; i++)
                V.Add(i + 1);
            var E = new List<Tuple<int, int, int>>()
            {
                new Tuple<int, int, int>(1, 2, 10),
                new Tuple<int, int, int>(1, 3, 11),
                new Tuple<int, int, int>(2, 4, 4),
                new Tuple<int, int, int>(2, 5, 7),
                new Tuple<int, int, int>(3, 4, 8),
                new Tuple<int, int, int>(3, 5, 9),
                new Tuple<int, int, int>(4, 6, 12),
                new Tuple<int, int, int>(5, 7, 6),
                new Tuple<int, int, int>(6, 8, 5),
                new Tuple<int, int, int>(7, 8, 15),
            };
            var graph = new Graph(V, E);
            var kryskal = Algorithms.Kruskal(graph);

            var c = new List<List<int>>();
            for (int i = 0; i < p; i++)
            {
                var temp = new List<int>();
                for (int j = 0; j < p; j++)
                    temp.Add(0);
                c.Add(temp);
            }
            c[0][1] = 10; c[0][2] = 11; c[1][3] = 4;
            c[1][4] = 7; c[2][3] = 8; c[2][4] = 9;
            c[3][5] = 12; c[4][6] = 6; c[5][7] = 5;
            c[6][7] = 15;
            int flow = Algorithms.MaxFlow(c, p);

            var list = new List<List<int>>();
            for (int i = 0; i < p; i++)
                list.Add(new List<int>());
            list[0] = new List<int>()
                { 1, 2 };
            list[1] = new List<int>()
                { 0, 3, 4 };
            list[2] = new List<int>()
                { 0, 3, 4 };
            list[3] = new List<int>()
                { 1, 2, 5 };
            list[4] = new List<int>()
                { 5, 1, 2 };
            list[5] = new List<int>()
                { 7, 3 };
            list[6] = new List<int>()
                { 7, 4 };
            var cliqs = new List<List<int>>();
            var potCliq = new List<int>();
            var remNodes = new List<int>();
            for (int i = 0; i < p; i++)
                remNodes.Add(i);
            var skipNodes = new List<int>();
            int depth = 0;
            Algorithms.FindCliques(potCliq, remNodes, skipNodes, depth, cliqs, list);
            if (comboBox1.SelectedIndex == 0)
            {
                string s = "";
                for (int i = 0; i < kryskal.E.Count; i++)
                    s += "(" + kryskal.E[i].Item1 + ", " + kryskal.E[i].Item2 + "); ";
                label3.Text = "Рёбра минимального остовного дерева:\n" + s;
                label3.Text = label3.Text.Substring(0, label3.Text.Length - 2);
                label3.Show();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                label3.Text = "Максимальный поток: " + flow;
                label3.Show();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                int mxi = 0;
                for (int i = 0; i < cliqs.Count; i++)
                    if (cliqs[i].Count > cliqs[mxi].Count)
                        mxi = i;
                string s = "";
                for (int i = 0; i < cliqs[mxi].Count; i++)
                    s += cliqs[mxi][i] + 1 + ", ";
                label3.Text = "Вершины максимальной клики:\n" + s;
                label3.Text = label3.Text.Substring(0, label3.Text.Length - 2);
                label3.Show();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void label3_Click(object sender, EventArgs e)
        {
        }
    }
}
