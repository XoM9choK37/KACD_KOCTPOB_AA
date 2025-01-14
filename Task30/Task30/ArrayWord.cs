using System.Collections.Generic;

namespace Task30
{
    /// <summary>
    /// Определяет список слов
    /// </summary>
    /// <typeparam name="Word"></typeparam>
    public class ArrayWord<Word> : List<Word>
    {
        private int minCoordX = 0;
        private int minCoordY = 0;
        private int width = 0;
        private int height = 0;
        private int intersectCount = 0;

        /// <summary>
        /// Создаёт пустой список слов
        /// </summary>
        public ArrayWord() : base() {}

        /// <summary>
        /// Создаёт пустой список слов с указанной начальной ёмкостью
        /// </summary>
        /// <param name="initialCapacity"></param>
        public ArrayWord(int initialCapacity) : base(initialCapacity) {}

        /// <summary>
        /// Создаёт список всех слов, содержащихся в передаваемом
        /// </summary>
        /// <param name="arrayWord"></param>
        public ArrayWord(ArrayWord<Word> arrayWord) : base(arrayWord.Count)
        {
            foreach (var word in arrayWord)
                Add(word);
            minCoordX = arrayWord.MinX();
            minCoordY = arrayWord.MinY();
            width = arrayWord.Width();
            height = arrayWord.Height();
            intersectCount = arrayWord.IntersectCount();
        }

        /// <summary>
        /// Устанавливает количество пересечений слов
        /// </summary>
        /// <returns></returns>
        public void SetInterCount(int c) { intersectCount = c; }

        /// <summary>
        /// Устанавливает минимальную координату схемы по оси X
        /// </summary>
        /// <returns></returns>
        public void SetMinX(int x) { if (x < minCoordX) minCoordX = x; }

        /// <summary>
        /// Устанавливает минимальную координату схемы по оси Y
        /// </summary>
        /// <returns></returns>
        public void SetMinY(int y) { if (y < minCoordY) minCoordY = y; }

        /// <summary>
        /// Устанавливает ширину схемы
        /// </summary>
        /// <returns></returns>
        public void SetWidth(int w) { width = w; }

        /// <summary>
        /// Устанавливает высоту схемы
        /// </summary>
        /// <returns></returns>
        public void SetHeight(int h) { height = h; }

        /// <summary>
        /// Возвращает минимальную координату схемы по оси X
        /// </summary>
        /// <returns></returns>
        public int MinX() { return minCoordX; }

        /// <summary>
        /// Возвращает минимальную координату схемы по оси Y
        /// </summary>
        /// <returns></returns>
        public int MinY() { return minCoordY; }

        /// <summary>
        /// Возвращает ширину схемы
        /// </summary>
        /// <returns></returns>
        public int Width() { return width; }

        /// <summary>
        /// Возвращает высоту схемы
        /// </summary>
        /// <returns></returns>
        public int Height() { return height; }

        /// <summary>
        /// Возвращает количество пересечений слов
        /// </summary>
        /// <returns></returns>
        public int IntersectCount() { return intersectCount; }

        /// <summary>
        /// Сбрасывает минимальные координаты схемы
        /// </summary>
        public void Reset() { minCoordX = 0; minCoordY = 0; }
    }
}
