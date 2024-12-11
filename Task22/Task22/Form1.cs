using System;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace Task22
{
    public partial class Form1 : Form
    {
        const int TIME1 = 10_000;
        const int TIME2 = 30_000;
        const int TIME3 = 50_000;
        const int TIME4 = 100_000;

        int state = 0;
        GraphPane pane;
        PointPairList listHM = new PointPairList();
        PointPairList listTM = new PointPairList();
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
                                times = Maps.GetTimes
                                (Maps.GetSpeedTest,
                                TIME1);
                                break;
                            case 1:
                                times = Maps.GetTimes
                                (Maps.GetSpeedTest,
                                TIME2);
                                break;
                            case 2:
                                times = Maps.GetTimes
                                (Maps.GetSpeedTest,
                                TIME3);
                                break;
                            case 3:
                                times = Maps.GetTimes
                                (Maps.GetSpeedTest,
                                TIME4);
                                break;
                        }
                        break;
                    case 1:
                        switch (comboBox2.SelectedIndex)
                        {
                            case 0:
                                times = Maps.GetTimes
                                (Maps.PutSpeedTest,
                                TIME1);
                                break;
                            case 1:
                                times = Maps.GetTimes
                                (Maps.PutSpeedTest,
                                TIME2);
                                break;
                            case 2:
                                times = Maps.GetTimes
                                (Maps.PutSpeedTest,
                                TIME3);
                                break;
                            case 3:
                                times = Maps.GetTimes
                                (Maps.PutSpeedTest,
                                TIME4);
                                break;
                        }
                        break;
                    case 2:
                        switch (comboBox2.SelectedIndex)
                        {
                            case 0:
                                times = Maps.GetTimes
                                (Maps.RemoveSpeedTest,
                                TIME1);
                                break;
                            case 1:
                                times = Maps.GetTimes
                                (Maps.RemoveSpeedTest,
                                TIME2);
                                break;
                            case 2:
                                times = Maps.GetTimes
                                (Maps.RemoveSpeedTest,
                                TIME3);
                                break;
                            case 3:
                                times = Maps.GetTimes
                                (Maps.RemoveSpeedTest,
                                TIME4);
                                break;
                        }
                        break;
                }
                if (0 <= comboBox1.SelectedIndex &&
                comboBox1.SelectedIndex <= 2 &&
                    0 <= comboBox2.SelectedIndex &&
                    comboBox2.SelectedIndex <= 3)
                {
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Invalidate();

                    zedGraphControl1.Show();
                    pane = zedGraphControl1.GraphPane;
                    pane.Legend.FontSpec.Size = 16;
                    pane.Title.Text = "Анализ эффективности хэш-таблицы и дерева поиска";
                    pane.XAxis.Title.Text = "Номер теста";
                    pane.YAxis.Title.Text = "Время выполнения операций (мс)";

                    pane.CurveList.Clear();
                    listHM.Clear();
                    listTM.Clear();

                    averages = Maps.GetMatrixAverage(times);
                    for (int i = 0; i < 20; i++)
                        listHM.Add(i + 1, times[i][0]);
                    LineItem curveHM = pane.AddCurve("MyHashMap\t", listHM,
                        Color.Red, SymbolType.None);
                    for (int i = 0; i < 20; i++)
                        listTM.Add(i + 1, times[i][1]);
                    LineItem curveTM = pane.AddCurve("MyTreeMap\t", listTM,
                        Color.Blue, SymbolType.None);
                    label1.Text = "Среднее время выполнения операций\n" +
                        "MyHashMap: " + averages[0] + " мс\n" +
                        "MyTreeMap: " + averages[1] + " мс";
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
