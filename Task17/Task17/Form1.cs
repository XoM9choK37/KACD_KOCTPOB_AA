using System;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace Task17
{
    public partial class Form1 : Form
    {
        const int TIME1 = 3_000;
        const int TIME2 = 5_000;
        const int TIME3 = 7_000;
        const int TIME4 = 10_000;

        int state = 0;
        GraphPane pane;
        PointPairList listAL = new PointPairList();
        PointPairList listLL = new PointPairList();
        double[][] times;
        double[] averages;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            zedGraphControl1.Hide();
            label1.Hide();
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
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        switch (comboBox2.SelectedIndex)
                        {
                            case 0:
                                times = Lists.GetTimes
                                (Lists.GetSpeedTest,
                                TIME1);
                                break;
                            case 1:
                                times = Lists.GetTimes
                                (Lists.GetSpeedTest,
                                TIME2);
                                break;
                            case 2:
                                times = Lists.GetTimes
                                (Lists.GetSpeedTest,
                                TIME3);
                                break;
                            case 3:
                                times = Lists.GetTimes
                                (Lists.GetSpeedTest,
                                TIME4);
                                break;
                        }
                        break;
                    case 1:
                        switch (comboBox2.SelectedIndex)
                        {
                            case 0:
                                times = Lists.GetTimes
                                (Lists.SetSpeedTest,
                                TIME1);
                                break;
                            case 1:
                                times = Lists.GetTimes
                                (Lists.SetSpeedTest,
                                TIME2);
                                break;
                            case 2:
                                times = Lists.GetTimes
                                (Lists.SetSpeedTest,
                                TIME3);
                                break;
                            case 3:
                                times = Lists.GetTimes
                                (Lists.SetSpeedTest,
                                TIME4);
                                break;
                        }
                        break;
                    case 2:
                        switch (comboBox2.SelectedIndex)
                        {
                            case 0:
                                times = Lists.GetTimes
                                (Lists.AddSpeedTest,
                                TIME1);
                                break;
                            case 1:
                                times = Lists.GetTimes
                                (Lists.AddSpeedTest,
                                TIME2);
                                break;
                            case 2:
                                times = Lists.GetTimes
                                (Lists.AddSpeedTest,
                                TIME3);
                                break;
                            case 3:
                                times = Lists.GetTimes
                                (Lists.AddSpeedTest,
                                TIME4);
                                break;
                        }
                        break;
                    case 3:
                        switch (comboBox2.SelectedIndex)
                        {
                            case 0:
                                times = Lists.GetTimes
                                (Lists.AddAtSpeedTest,
                                TIME1);
                                break;
                            case 1:
                                times = Lists.GetTimes
                                (Lists.AddAtSpeedTest,
                                TIME2);
                                break;
                            case 2:
                                times = Lists.GetTimes
                                (Lists.AddAtSpeedTest,
                                TIME3);
                                break;
                            case 3:
                                times = Lists.GetTimes
                                (Lists.AddAtSpeedTest,
                                TIME4);
                                break;
                        }
                        break;
                    case 4:
                        switch (comboBox2.SelectedIndex)
                        {
                            case 0:
                                times = Lists.GetTimes
                                (Lists.RemoveSpeedTest,
                                TIME1);
                                break;
                            case 1:
                                times = Lists.GetTimes
                                (Lists.RemoveSpeedTest,
                                TIME2);
                                break;
                            case 2:
                                times = Lists.GetTimes
                                (Lists.RemoveSpeedTest,
                                TIME3);
                                break;
                            case 3:
                                times = Lists.GetTimes
                                (Lists.RemoveSpeedTest,
                                TIME4);
                                break;
                        }
                        break;
                }
                if (0 <= comboBox1.SelectedIndex &&
                    comboBox1.SelectedIndex <= 4 &&
                    0 <= comboBox2.SelectedIndex &&
                    comboBox2.SelectedIndex <= 3)
                {
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Invalidate();

                    zedGraphControl1.Show();
                    pane = zedGraphControl1.GraphPane;
                    pane.Legend.FontSpec.Size = 16;
                    pane.Title.Text = "Анализ эффективности массива и списка";
                    pane.XAxis.Title.Text = "Номер теста";
                    pane.YAxis.Title.Text = "Время выполнения операций (мс)";

                    pane.CurveList.Clear();
                    listAL.Clear();
                    listLL.Clear();

                    averages = Lists.GetMatrixAverage(times);
                    for (int i = 0; i < 20; i++)
                        listAL.Add(i + 1, times[i][0]);
                    LineItem curveAL = pane.AddCurve("MyArrayList\t", listAL,
                        Color.Red, SymbolType.None);
                    for (int i = 0; i < 20; i++)
                        listLL.Add(i + 1, times[i][1]);
                    LineItem curveLL = pane.AddCurve("MyLinkedList\t", listLL,
                        Color.Blue, SymbolType.None);
                    label1.Text = "Среднее время выполнения операций\n" +
                        "MyArrayList: " + averages[0] + " мс\n" +
                        "MyLinkedList: " + averages[1] + " мс";
                    label1.Show();
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
    }
}
