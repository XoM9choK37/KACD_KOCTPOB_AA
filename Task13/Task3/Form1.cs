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

namespace Task13
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
        PointPairList list7 = new PointPairList();
        PointPairList list8 = new PointPairList();
        PointPairList list9 = new PointPairList();
        PointPairList list10 = new PointPairList();
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
                        T = TSortsAndArrays.FirstArrayGroupGeneration(1);
                        A = TSortsAndArrays.GetMatrixAverage(T);
                        state = 1;
                    }
                    if (comboBox2.SelectedIndex == 1)
                    {
                        T = TSortsAndArrays.SecondArrayGroupGeneration(1);
                        A = TSortsAndArrays.GetMatrixAverage(T);
                        state = 2;
                    }
                    if (comboBox2.SelectedIndex == 2)
                    {
                        T = TSortsAndArrays.ThirdArrayGroupGeneration(1);
                        A = TSortsAndArrays.GetMatrixAverage(T);
                        state = 3;
                    }
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    if (comboBox2.SelectedIndex == 0)
                    {
                        T = TSortsAndArrays.FirstArrayGroupGeneration(2);
                        A = TSortsAndArrays.GetMatrixAverage(T);
                        state = 1;
                    }
                    if (comboBox2.SelectedIndex == 1)
                    {
                        T = TSortsAndArrays.SecondArrayGroupGeneration(2);
                        A = TSortsAndArrays.GetMatrixAverage(T);
                        state = 2;
                    }
                    if (comboBox2.SelectedIndex == 2)
                    {
                        T = TSortsAndArrays.ThirdArrayGroupGeneration(2);
                        A = TSortsAndArrays.GetMatrixAverage(T);
                        state = 3;
                    }
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    if (comboBox2.SelectedIndex == 0)
                    {
                        T = TSortsAndArrays.FirstArrayGroupGeneration(3);
                        A = TSortsAndArrays.GetMatrixAverage(T);
                        state = 1;
                    }
                    if (comboBox2.SelectedIndex == 1)
                    {
                        T = TSortsAndArrays.SecondArrayGroupGeneration(3);
                        A = TSortsAndArrays.GetMatrixAverage(T);
                        state = 2;
                    }
                    if (comboBox2.SelectedIndex == 2)
                    {
                        T = TSortsAndArrays.ThirdArrayGroupGeneration(3);
                        A = TSortsAndArrays.GetMatrixAverage(T);
                        state = 3;
                    }
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    if (comboBox2.SelectedIndex == 0)
                    {
                        T = TSortsAndArrays.FirstArrayGroupGeneration(4);
                        A = TSortsAndArrays.GetMatrixAverage(T);
                        state = 1;
                    }
                    if (comboBox2.SelectedIndex == 1)
                    {
                        T = TSortsAndArrays.SecondArrayGroupGeneration(4);
                        A = TSortsAndArrays.GetMatrixAverage(T);
                        state = 2;
                    }
                    if (comboBox2.SelectedIndex == 2)
                    {
                        T = TSortsAndArrays.ThirdArrayGroupGeneration(4);
                        A = TSortsAndArrays.GetMatrixAverage(T);
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
                    pane.Legend.FontSpec.Size = 10;
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
                    list7.Clear();
                    list8.Clear();
                    list9.Clear();
                    list10.Clear();

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
                        list6.Add(i + 1, T[i][5]);
                        list7.Add(i + 1, T[i][6]);
                        list8.Add(i + 1, T[i][7]);
                        list9.Add(i + 1, T[i][8]);
                        list10.Add(i + 1, T[i][9]);
                    }
                    LineItem curve1 = pane.AddCurve("Пузырьком\t", list1,
                        Color.Blue, SymbolType.None);
                    LineItem curve2 = pane.AddCurve("Вставками\t", list2,
                        Color.Yellow, SymbolType.None);
                    LineItem curve3 = pane.AddCurve("Выбором", list3,
                        Color.Red, SymbolType.None);
                    LineItem curve4 = pane.AddCurve("Шейкерная\t", list4,
                        Color.Black, SymbolType.None);
                    LineItem curve5 = pane.AddCurve("Гномья", list5,
                        Color.LightBlue, SymbolType.None);
                    LineItem curve6 = pane.AddCurve("Пузырьком (строки)\t", list6,
                        Color.Blue, SymbolType.None);
                    LineItem curve7 = pane.AddCurve("Вставками (строки)\t", list7,
                        Color.Yellow, SymbolType.None);
                    LineItem curve8 = pane.AddCurve("Выбором (строки)", list8,
                        Color.Red, SymbolType.None);
                    LineItem curve9 = pane.AddCurve("Шейкерная (строки)\t", list9,
                        Color.Black, SymbolType.None);
                    LineItem curve10 = pane.AddCurve("Гномья (строки)", list10,
                        Color.LightBlue, SymbolType.None);
                    for (int i = 0; i < 5; i++)
                        ((LineItem)zedGraphControl1.GraphPane.CurveList[i]).
                            Line.Width = 1;
                    for (int i = 5; i < 10; i++)
                        ((LineItem)zedGraphControl1.GraphPane.CurveList[i]).
                            Line.Width = 5;
                    label2.Text = "Массивы автоматически сохранены в файл!";
                    label2.Show();
                    label3.Text = "Среднее время выполнения сортировок\n" +
                        "Пузырьком: " + A[0] + " мс\n" +
                        "Вставками: " + A[1] + " мс\n" +
                        "Выбором: " + A[2] + " мс\n" +
                        "Шейкерная: " + A[3] + " мс\n" +
                        "Гномья: " + A[4] + " мс\n" +
                        "Пузырьком (строки): " + A[5] + " мс\n" +
                        "Вставками (строки): " + A[6] + " мс\n" +
                        "Выбором (строки): " + A[7] + " мс\n" +
                        "Шейкерная (строки): " + A[8] + " мс\n" +
                        "Гномья (строки): " + A[9] + " мс";
                    label3.Show();
                    pane.XAxis.Scale.Min = 1;
                    pane.XAxis.Scale.Max = 20;
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Invalidate();
                }
                if (state == 2)
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
                    LineItem curve1 = pane.AddCurve("Битонная", list1,
                        Color.Red, SymbolType.None);
                    LineItem curve2 = pane.AddCurve("Шелла", list2,
                        Color.Yellow, SymbolType.None);
                    LineItem curve3 = pane.AddCurve("Деревом", list3,
                        Color.Blue, SymbolType.None);
                    LineItem curve4 = pane.AddCurve("Битонная (строки)", list4,
                        Color.Red, SymbolType.None);
                    LineItem curve5 = pane.AddCurve("Шелла (строки)", list5,
                        Color.Yellow, SymbolType.None);
                    LineItem curve6 = pane.AddCurve("Деревом (строки)", list6,
                        Color.Blue, SymbolType.None);
                    for (int i = 0; i < 3; i++)
                        ((LineItem)zedGraphControl1.GraphPane.CurveList[i]).
                            Line.Width = 1;
                    for (int i = 3; i < 6; i++)
                        ((LineItem)zedGraphControl1.GraphPane.CurveList[i]).
                            Line.Width = 5;
                    label2.Text = "Массивы автоматически сохранены в файл!";
                    label2.Show();
                    label3.Text = "Среднее время выполнения сортировок\n" +
                        "Битонная: " + A[0] + " мс\n" +
                        "Шелла: " + A[1] + " мс\n" +
                        "Деревом: " + A[2] + " мс\n" +
                        "Битонная (строки): " + A[3] + " мс\n" +
                        "Шелла (строки): " + A[4] + " мс\n" +
                        "Деревом (строки): " + A[5] + " мс";
                    label3.Show();
                    pane.XAxis.Scale.Min = 1;
                    pane.XAxis.Scale.Max = 20;
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Invalidate();
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
                        list7.Add(i + 1, T[i][6]);
                        list8.Add(i + 1, T[i][7]);
                    }
                    LineItem curve1 = pane.AddCurve("Расчёской", list1,
                        Color.Black, SymbolType.None);
                    LineItem curve2 = pane.AddCurve("Пирамидальная", list2,
                        Color.Yellow, SymbolType.None);
                    LineItem curve3 = pane.AddCurve("Быстрая", list3,
                        Color.Blue, SymbolType.None);
                    LineItem curve4 = pane.AddCurve("Слиянием", list4,
                        Color.Red, SymbolType.None);
                    LineItem curve5 = pane.AddCurve("Расчёской (строки)", list5,
                        Color.Black, SymbolType.None);
                    LineItem curve6 = pane.AddCurve("Пирамидальная (строки)", list6,
                        Color.Yellow, SymbolType.None);
                    LineItem curve7 = pane.AddCurve("Быстрая (строки)", list7,
                        Color.Blue, SymbolType.None);
                    LineItem curve8 = pane.AddCurve("Слиянием (строки)", list8,
                        Color.Red, SymbolType.None);
                    for (int i = 0; i < 4; i++)
                        ((LineItem)zedGraphControl1.GraphPane.CurveList[i]).
                            Line.Width = 1;
                    for (int i = 4; i < 8; i++)
                        ((LineItem)zedGraphControl1.GraphPane.CurveList[i]).
                            Line.Width = 5;
                    label2.Text = "Массивы автоматически сохранены в файл!";
                    label2.Show();
                    label3.Text = "Среднее время выполнения сортировок\n" +
                        "Расчёской: " + A[0] + " мс\n" +
                        "Пирамидальная: " + A[1] + " мс\n" +
                        "Быстрая: " + A[2] + " мс\n" +
                        "Слиянием: " + A[3] + " мс\n" +
                        "Расчёской (строки): " + A[4] + " мс\n" +
                        "Пирамидальная (строки): " + A[5] + " мс\n" +
                        "Быстрая (строки): " + A[6] + " мс\n" +
                        "Слиянием (строки): " + A[7] + " мс";
                    label3.Show();
                    pane.XAxis.Scale.Min = 1;
                    pane.XAxis.Scale.Max = 20;
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Invalidate();
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
