using System.Drawing;

namespace Task30
{
    /// <summary>
    /// Определяет слово
    /// </summary>
    public class Word
    {
        private CharCell[] cells;
        private int orientation;
        private int constWordCoord;
        private int length;
        private string word;

        /// <summary>
        /// Создаёт слово с заданными параметрами
        /// </summary>
        /// <param name="word"></param>
        /// <param name="orientation"></param>
        /// <param name="constWordCoord"></param>
        /// <param name="initialVariableCoord"></param>
        public Word(string word, int orientation, int constWordCoord, int initialVariableCoord)
        {
            this.orientation = orientation;
            this.constWordCoord = constWordCoord;
            length = word.Length;
            cells = new CharCell[length];
            for (int i = 0; i < length; i++)
                cells[i] = new CharCell(word[i], initialVariableCoord + i);
            this.word = word;
        }

        /// <summary>
        /// Создаёт слово, копируя передаваемое
        /// </summary>
        /// <param name="word"></param>
        public Word(Word word)
        {
            orientation = word.Orient();
            constWordCoord = word.ConstCoordination();
            length = word.Length();
            cells = new CharCell[length];
            this.word = word.GetWord();
            for (int i = 0; i < length; i++)
                cells[i] = new CharCell(word.Get(i));
        }

        /// <summary>
        /// Отрисовывает слово
        /// </summary>
        /// <param name="g"></param>
        /// <param name="font"></param>
        public void ShowWord(Graphics g, Font font)
        {
            for (int i = 0; i < length; i++)
                cells[i].ShowCharCell(g, font, orientation, constWordCoord);
        }

        /// <summary>
        /// Смещает слово, меняя координаты каждого символа
        /// </summary>
        /// <param name="minCoordX"></param>
        /// <param name="minCoordY"></param>
        public void IncreaseCoordinate(int minCoordX, int minCoordY)
        {
            if (orientation == Orientation.HORIZ)
            {
                constWordCoord -= minCoordY;
                for (int i = 0; i < length; i++)
                    cells[i].SetCoord(cells[i].Coord() - minCoordX);
            }
            else
            {
                constWordCoord -= minCoordX;
                for (int i = 0; i < length; i++)
                    cells[i].SetCoord(cells[i].Coord() - minCoordY);
            }
        }

        /// <summary>
        /// Возвращает ячейку по индексу
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public CharCell Get(int i) { return i >= 0 && i < length ? cells[i] : new CharCell(); }

        /// <summary>
        /// Возвращает ориентацию слова
        /// </summary>
        /// <returns></returns>
        public int Orient() { return orientation; }

        /// <summary>
        /// Возвращает координату слова
        /// </summary>
        /// <returns></returns>
        public int ConstCoordination() { return constWordCoord; }

        /// <summary>
        /// Возвращает длину слова
        /// </summary>
        /// <returns></returns>
        public int Length() { return length; }

        /// <summary>
        /// Возвращает координату первой ячейки слова
        /// </summary>
        /// <returns></returns>
        public int First() { return cells[0].Coord(); }

        /// <summary>
        /// Возвращает координату последней ячейки слова
        /// </summary>
        /// <returns></returns>
        public int Last() { return cells[length - 1].Coord(); }

        /// <summary>
        /// Возвращает слово в виде строки
        /// </summary>
        /// <returns></returns>
        public string GetWord() { return word; }
    }
}
