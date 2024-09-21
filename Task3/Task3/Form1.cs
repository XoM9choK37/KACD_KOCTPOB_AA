using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Task3
{
    public partial class Form1 : Form
    {
        int state = 0;
        GraphPane pane;
        PointPairList list1 = new PointPairList();
        PointPairList list2 = new PointPairList();
        PointPairList list3 = new PointPairList();
        PointPairList list4 = new PointPairList();
        PointPairList list5 = new PointPairList();
        PointPairList list6 = new PointPairList();
        double[][] T;
        double[] A;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            zedGraphControl1.Hide();
            button2.Hide();
            label1.Hide();
            label2.Hide();
            label3.Hide();
            
        }
        private void zedGraphControl1_Load(object sender, EventArgs e)
        {
            
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    if (comboBox2.SelectedIndex == 0)
                    {
                        T = SortsAndArrays.FirstArrayGroupGeneration(1);
                        A = SortsAndArrays.GetMatrixAverage(T);
                        state = 1;
                    }
                    if (comboBox2.SelectedIndex == 1)
                    {
                        T = SortsAndArrays.SecondArrayGroupGeneration(1);
                        A = SortsAndArrays.GetMatrixAverage(T);
                        state = 2;
                    }
                    if (comboBox2.SelectedIndex == 2)
                    {
                        T = SortsAndArrays.ThirdArrayGroupGeneration(1);
                        A = SortsAndArrays.GetMatrixAverage(T);
                        state = 3;
                    }
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    if (comboBox2.SelectedIndex == 0)
                    {
                        T = SortsAndArrays.FirstArrayGroupGeneration(2);
                        A = SortsAndArrays.GetMatrixAverage(T);
                        state = 1;
                    }
                    if (comboBox2.SelectedIndex == 1)
                    {
                        T = SortsAndArrays.SecondArrayGroupGeneration(2);
                        A = SortsAndArrays.GetMatrixAverage(T);
                        state = 2;
                    }
                    if (comboBox2.SelectedIndex == 2)
                    {
                        T = SortsAndArrays.ThirdArrayGroupGeneration(2);
                        A = SortsAndArrays.GetMatrixAverage(T);
                        state = 3;
                    }
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    if (comboBox2.SelectedIndex == 0)
                    {
                        T = SortsAndArrays.FirstArrayGroupGeneration(3);
                        A = SortsAndArrays.GetMatrixAverage(T);
                        state = 1;
                    }
                    if (comboBox2.SelectedIndex == 1)
                    {
                        T = SortsAndArrays.SecondArrayGroupGeneration(3);
                        A = SortsAndArrays.GetMatrixAverage(T);
                        state = 2;
                    }
                    if (comboBox2.SelectedIndex == 2)
                    {
                        T = SortsAndArrays.ThirdArrayGroupGeneration(3);
                        A = SortsAndArrays.GetMatrixAverage(T);
                        state = 3;
                    }
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    if (comboBox2.SelectedIndex == 0)
                    {
                        T = SortsAndArrays.FirstArrayGroupGeneration(4);
                        A = SortsAndArrays.GetMatrixAverage(T);
                        state = 1;
                    }
                    if (comboBox2.SelectedIndex == 1)
                    {
                        T = SortsAndArrays.SecondArrayGroupGeneration(4);
                        A = SortsAndArrays.GetMatrixAverage(T);
                        state = 2;
                    }
                    if (comboBox2.SelectedIndex == 2)
                    {
                        T = SortsAndArrays.ThirdArrayGroupGeneration(4);
                        A = SortsAndArrays.GetMatrixAverage(T);
                        state = 3;
                    }
                }
                if (0 <= comboBox1.SelectedIndex &&
                    comboBox1.SelectedIndex <= 3 &&
                    0 <= comboBox2.SelectedIndex &&
                    comboBox2.SelectedIndex <= 3)
                {
                    label1.Show();
                    label2.Hide();
                    button1.Hide();
                    button2.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                label1.Hide();
                if (state != 0)
                {
                    zedGraphControl1.Show();
                    pane = zedGraphControl1.GraphPane;
                    pane.Title.Text = "Анализ скорости сортировок";
                    pane.XAxis.Title.Text = "Номер теста";
                    pane.YAxis.Title.Text = "Время сортировки (мс)";

                    pane.CurveList.Clear();
                    list1.Clear();
                    list2.Clear();
                    list3.Clear();
                    list4.Clear();
                    list5.Clear();
                    list6.Clear();

                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Invalidate();

                    button1.Show();
                    button2.Hide();
                }
                if (state == 1)
                {
                    for (int i = 0; i < T.Length; i++)
                    {
                        list1.Add(i + 1, T[i][0]);
                        list2.Add(i + 1, T[i][1]);
                        list3.Add(i + 1, T[i][2]);
                        list4.Add(i + 1, T[i][3]);
                        list5.Add(i + 1, T[i][4]);
                    }
                    LineItem curve1 = pane.AddCurve("BubbleSort", list1,
                        Color.Blue, SymbolType.None);
                    LineItem curve2 = pane.AddCurve("InsertionSort", list2,
                        Color.Yellow, SymbolType.None);
                    LineItem curve3 = pane.AddCurve("SelectionSort", list3,
                        Color.Red, SymbolType.None);
                    LineItem curve4 = pane.AddCurve("ShakerSort", list4,
                        Color.Black, SymbolType.None);
                    LineItem curve5 = pane.AddCurve("GnomeSort", list5,
                        Color.LightBlue, SymbolType.None);
                    for (int i = 0; i < 5; i++)
                        ((LineItem)zedGraphControl1.GraphPane.CurveList[i]).
                            Line.Width = i + 1;
                    label2.Text = "Массивы автоматически сохранены в файл!";
                    label2.Show();
                    label3.Text = "Среднее время выполнения сортировок\n" +
                        "BubbleSort: " + A[0] + " мс\n" +
                        "InsertionSort: " + A[1] + " мс\n" +
                        "SelectionSort: " + A[2] + " мс\n" +
                        "ShakerSort: " + A[3] + " мс\n" +
                        "GnomeSort: " + A[4] + " мс";
                    label3.Show();
                }
                if (state == 2)
                {
                    for (int i = 0; i < T.Length; i++)
                    {
                        list1.Add(i + 1, T[i][0]);
                        list2.Add(i + 1, T[i][1]);
                        list3.Add(i + 1, T[i][2]);
                    }
                    LineItem curve1 = pane.AddCurve("BitonicSort", list1,
                        Color.Red, SymbolType.None);
                    LineItem curve2 = pane.AddCurve("ShellSort", list2,
                        Color.Yellow, SymbolType.None);
                    LineItem curve3 = pane.AddCurve("TreeSort", list3,
                        Color.Blue, SymbolType.None);
                    for (int i = 0; i < 3; i++)
                        ((LineItem)zedGraphControl1.GraphPane.CurveList[i]).
                            Line.Width = i + 1;
                    label2.Text = "Массивы автоматически сохранены в файл!";
                    label2.Show();
                    label3.Text = "Среднее время выполнения сортировок\n" +
                        "BitonicSort: " + A[0] + " мс\n" +
                        "ShellSort: " + A[1] + " мс\n" +
                        "TreeSort: " + A[2] + " мс";
                    label3.Show();
                }
                if (state == 3)
                {
                    for (int i = 0; i < T.Length; i++)
                    {
                        list1.Add(i + 1, T[i][0]);
                        list2.Add(i + 1, T[i][1]);
                        list3.Add(i + 1, T[i][2]);
                        list4.Add(i + 1, T[i][3]);
                        list5.Add(i + 1, T[i][4]);
                        list6.Add(i + 1, T[i][5]);
                    }
                    LineItem curve1 = pane.AddCurve("CombSort", list1,
                        Color.Black, SymbolType.None);
                    LineItem curve2 = pane.AddCurve("HeapSort", list2,
                        Color.Yellow, SymbolType.None);
                    LineItem curve3 = pane.AddCurve("QuickSort", list3,
                        Color.Blue, SymbolType.None);
                    LineItem curve4 = pane.AddCurve("MergeSort", list4,
                        Color.Red, SymbolType.None);
                    LineItem curve5 = pane.AddCurve("CountingSort", list5,
                        Color.LightBlue, SymbolType.None);
                    LineItem curve6 = pane.AddCurve("RadixSort", list6,
                        Color.Lime, SymbolType.None);
                    for (int i = 0; i < 6; i++)
                        ((LineItem)zedGraphControl1.GraphPane.CurveList[i]).
                            Line.Width = i + 1;
                    label2.Text = "Массивы автоматически сохранены в файл!";
                    label2.Show();
                    label3.Text = "Среднее время выполнения сортировок\n" +
                        "CombSort: " + A[0] + " мс\n" +
                        "HeapSort: " + A[1] + " мс\n" +
                        "QuickSort: " + A[2] + " мс\n" +
                        "MergeSort: " + A[3] + " мс\n" +
                        "CountingSort: " + A[4] + "  мс\n" +
                        "RadixSort: " + A[5] + " мс";
                    label3.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click_1(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
