using System.Drawing;

namespace Task30
{
    /// <summary>
    /// Определяет ячейку с символом
    /// </summary>
    public class CharCell
    {
        private char value;
        private int variableCoord;
        public const int CELL_SIZE = 30;

        /// <summary>
        /// Создаёт пустую ячейку
        /// </summary>
        public CharCell()
        {
            value = ' ';
            variableCoord = 0;
        }

        /// <summary>
        /// Создаёт ячейку, копируя передаваемую
        /// </summary>
        /// <param name="cell"></param>
        public CharCell(CharCell cell)
        {
            value = cell.Value();
            variableCoord = cell.Coord();
        }

        /// <summary>
        /// Создаёт ячейку с заданными символом и координатой
        /// </summary>
        /// <param name="value"></param>
        /// <param name="variableCoord"></param>
        public CharCell(char value, int variableCoord)
        {
            this.value = value;
            this.variableCoord = variableCoord;
        }

        /// <summary>
        /// Отрисовывает ячейку и букву внутри неё
        /// </summary>
        /// <param name="g"></param>
        /// <param name="font"></param>
        /// <param name="orient"></param>
        /// <param name="constCoord"></param>
        public void ShowCharCell(Graphics g, Font font, int orient, int constCoord)
        {
            int coordX;
            int coordY;
            if (orient == Orientation.HORIZ)
            {
                coordX = variableCoord * CELL_SIZE;
                coordY = constCoord * CELL_SIZE;
            }
            else
            {
                coordX = constCoord * CELL_SIZE;
                coordY = variableCoord * CELL_SIZE;
            }
            string s = value.ToString();
            SizeF bounds = g.MeasureString(s, font);
            float x = coordX + (CELL_SIZE - bounds.Width) / 2;
            float y = coordY + (CELL_SIZE - bounds.Height) / 2;
            g.DrawRectangle(Pens.Black, coordX, coordY, CELL_SIZE, CELL_SIZE);
            g.DrawString(s, font, Brushes.Black, x, y);
        }

        /// <summary>
        /// Задаёт символ внутри ячейки
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(char value) { this.value = value; }

        /// <summary>
        /// Возвращает символ внутри ячейки
        /// </summary>
        /// <returns></returns>
        public char Value() { return value; }

        /// <summary>
        /// Задаёт координату ячейки
        /// </summary>
        /// <returns></returns>
        public void SetCoord(int x) { variableCoord = x; }

        /// <summary>
        /// Возвращает координату ячейки
        /// </summary>
        /// <returns></returns>
        public int Coord() { return variableCoord; }
    }
}
