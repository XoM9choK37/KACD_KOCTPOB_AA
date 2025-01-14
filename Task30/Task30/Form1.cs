using System;
using System.Drawing;
using System.Windows.Forms;

namespace Task30
{
    public partial class Form1 : Form
    {
        const string PATH = "words.txt";
        ArrayWord<Word> solution;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Крисс-кросс";
            WordArea wordArea = new WordArea(PATH);
            solution = wordArea.Solution();
            Width = (solution.Width() + 1) * 30;
            Height = (solution.Height() + 2) * 30;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Font font = new Font("Consolas", 16);
            int maxX = 0;
            int maxY = 0;
            foreach (Word word in solution)
            {
                if (word.Orient() == Orientation.HORIZ)
                {
                    if (word.ConstCoordination() < maxY)
                        maxY = word.ConstCoordination();
                    if (word.First() < maxX)
                        maxX = word.First();
                }
                if (word.Orient() == Orientation.VERTIC)
                {
                    if (word.ConstCoordination() < maxX)
                        maxX = word.ConstCoordination();
                    if (word.First() < maxY)
                        maxY = word.First();
                }
            }
            foreach (Word word in solution)
            {
                word.IncreaseCoordinate(maxX, maxY);
                word.ShowWord(g, font);
            }
        }
    }
}
