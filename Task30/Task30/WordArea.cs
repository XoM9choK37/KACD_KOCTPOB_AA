using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Task30
{
    /// <summary>
    /// Определяет схему крисс-кросса
    /// </summary>
    public class WordArea
    {
        private List<string> rawWords;
        private int numberOfWords;
        private List<ArrayWord<Word>> allWordArea = new List<ArrayWord<Word>>();
        private double alpha;
        private double density = 0;
        private int intersectWordArea = 0;
        private int sizeWordArea = 1000;
        private ArrayWord<Word> mainWordArea = new ArrayWord<Word>();
        private ArrayWord<Word> solution;

        /// <summary>
        /// Создаёт корректную и связную схему
        /// крисс-кросса из слов, находящихся в текстовом файле
        /// </summary>
        /// <param name="path">
        /// Путь до текстового файла со словами
        /// для крисс-кросса
        /// </param>
        /// <exception cref="Exception"></exception>
        public WordArea(string path)
        {
            ReadWords(path);
            if (rawWords.Count == 0)
                throw new Exception("Список слов пуст!");
            alpha = 1.0 / Math.Pow(1.4, numberOfWords);
            string first = rawWords[0];
            rawWords.RemoveAt(0);
            Word firstWord = new Word(first, Orientation.HORIZ, 0, 0);
            ArrayWord<Word> firstWordArea = new ArrayWord<Word> { firstWord };
            WordsBacktracking(firstWordArea, rawWords);
            if (allWordArea.Count == 0)
                throw new Exception("Невозможно построить связную схему!");
            solution = new ArrayWord<Word>(allWordArea[0]);
            foreach (var wordArea in allWordArea)
                if (SizeWordArea(wordArea) < SizeWordArea(solution))
                    solution = new ArrayWord<Word>(wordArea);
                else if (SizeWordArea(wordArea) == SizeWordArea(solution) &&
                    wordArea.IntersectCount() > solution.IntersectCount())
                    solution = new ArrayWord<Word>(wordArea);
        }

        /// <summary>
        /// Перебирает слова с возвратом
        /// </summary>
        /// <param name="wordArea"></param>
        /// <param name="words"></param>
        private void WordsBacktracking(ArrayWord<Word> wordArea, List<string> words)
        {
            if (Accept(wordArea))
            {
                ArrayWord<Word> tempWordArea = new ArrayWord<Word>(wordArea);
                allWordArea.Add(tempWordArea);
                mainWordArea = tempWordArea;
                return;
            }
            if (Reject(wordArea, words))
                return;
            for (int i = 0; i < words.Count; i++)
            {
                List<string> tempWords = new List<string>(words);
                string newWord = tempWords[i];
                tempWords.RemoveAt(i);
                AddNewWord(wordArea, tempWords, newWord);
            }
        }

        /// <summary>
        /// Отклоняет неоптимальные решения
        /// </summary>
        /// <param name="wordArea"></param>
        /// <param name="words"></param>
        /// <returns></returns>
        private bool Reject(ArrayWord<Word> wordArea, List<string> words)
        {
            int currentSize = SizeWordArea(wordArea);
			/// Площадь текущей схемы не меньше площади ранее зафиксированной
            if (currentSize >= sizeWordArea)
                return true;
            double currentDensity = (double)wordArea.IntersectCount() / currentSize;
			/// Плотность текущей схемы меньше плотности ранее зафиксированной
            if (currentDensity < density - alpha)
                return true;
            double averageLengthWA = (double)SumWordLength(wordArea) / wordArea.Count;
            int sumLengthWL = 0;
            foreach (var word in words)
                sumLengthWL += word.Length;
            double averageLengthWL = (double)sumLengthWL / words.Count;
			/// Средняя длина не меньше ранее зафиксированной
            if (averageLengthWA >= averageLengthWL)
                return true;
            return false;
        }

        /// <summary>
        /// Определяет суммарную длину всех слов в схеме
        /// </summary>
        /// <param name="wordArea"></param>
        /// <returns></returns>
        private int SumWordLength(ArrayWord<Word> wordArea)
        {
            int result = 0;
            var wordAreaIter = wordArea.GetEnumerator();
            while (wordAreaIter.MoveNext())
                result += wordAreaIter.Current.Length();
            return result;
        }

        /// <summary>
        /// Добавляет новое слово в схему, если это возможно
        /// </summary>
        /// <param name="wordArea"></param>
        /// <param name="words"></param>
        /// <param name="newWord"></param>
        private void AddNewWord(ArrayWord<Word> wordArea, List<string> words, string newWord)
        {
            Word existentWord;
            for (int k = 0; k < wordArea.Count; k++)
            {
                existentWord = wordArea[k];
				/// Сравниваем символы в новом слове и уже имеющимся в схеме
                for (int i = 0; i < existentWord.Length(); i++)
                    for (int j = 0; j < newWord.Length; j++)
                        if (existentWord.Get(i).Value() == newWord[j])
                        {
                            int newOrient = Invert(existentWord.Orient());
                            int newWordCoord = existentWord.Get(i).Coord();
                            int initialVariableCoord = existentWord.ConstCoordination() - j;
                            Word word = new Word
                                (newWord, newOrient, newWordCoord, initialVariableCoord);
                            int interCount = wordArea.IntersectCount();
							/// Добавляем слово, если оно проходит проверку
                            if (Check(wordArea, word, existentWord.ConstCoordination()))
                            {
                                wordArea.Add(word);
                                int minX = wordArea.MinX();
                                int minY = wordArea.MinY();
                                if (existentWord.Orient() == Orientation.HORIZ)
                                    wordArea.SetMinY(initialVariableCoord);
                                else
                                    wordArea.SetMinX(initialVariableCoord);
                                /// Запускаем поиск с возвратом
                                WordsBacktracking(wordArea, words);
                                wordArea.RemoveAt(wordArea.Count() - 1);
                                wordArea.Reset();
                                wordArea.SetMinX(minX);
                                wordArea.SetMinY(minY);
                                wordArea.SetInterCount(interCount);
                            }
                        }
            }
        }

        /// <summary>
        /// Меняет ориентацию слова на противоположную
        /// </summary>
        /// <param name="orient"></param>
        /// <returns></returns>
        private int Invert(int orient)
        {
            return orient == Orientation.HORIZ ? Orientation.VERTIC : Orientation.HORIZ;
        }

        /// <summary>
        /// Проверяет оптимальность решения
        /// </summary>
        /// <param name="wordArea"></param>
        /// <returns></returns>
        private bool Accept(ArrayWord<Word> wordArea)
        {
            if (wordArea.Count == numberOfWords)
            {
                int currentSize = SizeWordArea(wordArea);
                double currentDensity = (double)wordArea.IntersectCount() / currentSize;
                if (currentDensity > density)
                {
                    intersectWordArea = wordArea.IntersectCount();
                    sizeWordArea = currentSize;
                    density = currentDensity;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Рассчитывает площадь схемы крисс-кросса
        /// </summary>
        /// <param name="wordArea"></param>
        /// <returns></returns>
        private int SizeWordArea(ArrayWord<Word> wordArea)
        {
            if (wordArea.Count == 0)
                return 0;
            var wordAreaIter = wordArea.GetEnumerator();
            Word word;
            int minX = 0;
            int minY = 0;
            int maxX = 0;
            int maxY = 0;
            bool firstUse = true;
            while (wordAreaIter.MoveNext())
            {
                word = wordAreaIter.Current;
                int constWordCoord = word.ConstCoordination();
                int first = word.First();
                int last = word.Last();
                if (firstUse)
                {
                    firstUse = false;
                    if (word.Orient() == Orientation.HORIZ)
                    {
                        minY = constWordCoord;
                        maxY = constWordCoord;
                        minX = first;
                        maxX = last;
                    }
                    else
                    {
                        minX = constWordCoord;
                        maxX = constWordCoord;
                        minY = first;
                        maxY = last;
                    }
                }
                if (word.Orient() == Orientation.HORIZ && firstUse == false)
                {
                    if (constWordCoord < minY)
                        minY = constWordCoord;
                    if (constWordCoord > maxY)
                        maxY = constWordCoord;
                    if (first < minX)
                        minX = first;
                    if (last > maxX)
                        maxX = last;
                }
                if (word.Orient() == Orientation.VERTIC && firstUse == false)
                {
                    if (constWordCoord < minX)
                        minX = constWordCoord;
                    if (constWordCoord > maxX)
                        maxX = constWordCoord;
                    if (first < minY)
                        minY = first;
                    if (last > maxY)
                        maxY = last;
                }
            }
            int width = maxX - minX + 1;
            int height = maxY - minY + 1;
            wordArea.SetWidth(width);
            wordArea.SetHeight(height);
            return width * height;
        }

        /// <summary>
        /// Проверяет возможность добавления нового слова
        /// </summary>
        /// <param name="wordArea"></param>
        /// <param name="newWord"></param>
        /// <param name="intersect"></param>
        /// <returns></returns>
        private bool Check(ArrayWord<Word> wordArea, Word newWord, int intersect)
        {
            var wordAreaIter = wordArea.GetEnumerator();
            Word word;
            int intersectCount = wordArea.IntersectCount();
            int orient = newWord.Orient();
            int newWordCoord = newWord.ConstCoordination();
            int newFirst = newWord.First();
            int newLast = newWord.Last();
            while (wordAreaIter.MoveNext())
            {
                word = wordAreaIter.Current;
                int existFirst = word.First();
                int existLast = word.Last();
                /// Проверяем слова с той же ориентацией
                if (word.Orient() == orient)
                {
                    if (word.ConstCoordination() == newWordCoord - 1 ||
                        word.ConstCoordination() == newWordCoord + 1)
                    {
                        if ((!((newFirst == existLast && newFirst == intersect) ||
                            (newLast == existFirst && newLast == intersect))) &&
                            Intersect(newFirst, newLast, existFirst, existLast))
                            return false;
                    }
                    else if (word.ConstCoordination() == newWordCoord &&
                        Intersect(newFirst - 1, newLast + 1, existFirst, existLast))
                        return false;
                }
                /// Проверяем слова с противоположной ориентацией
                else
                {
                    /// Слова, лежащие в диапазоне координат добавляемого слова
                    if (Range(newFirst, newLast, word.ConstCoordination()))
                    {
                        for (int i = 0; i < word.Length(); i++)
                            for (int j = 0; j < newWord.Length(); j++)
                                if (word.Get(i).Coord() == newWordCoord &&
                                    newWord.Get(j).Coord() == word.ConstCoordination())
                                    if (word.Get(i).Value() != newWord.Get(j).Value())
                                        return false;
                                    else
                                        intersectCount++;
                        if ((existFirst == newWordCoord + 1) || (existLast == newWordCoord - 1))
                            return false;
                    }
                    /// Слова, лежащие по бокам от добавленного слова
                    if ((word.ConstCoordination() == newFirst - 1 ||
                        word.ConstCoordination() == newLast + 1) &&
                        Range(existFirst, existLast, newWordCoord))
                            return false;
                }
            }
            if (wordArea.IntersectCount() == intersectCount)
                wordArea.SetInterCount(++intersectCount);
            else
                wordArea.SetInterCount(intersectCount);
            return true;
        }

        /// <summary>
        /// Проверяет, пересекаются ли отрезки [a; b] и [c; d]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        private bool Intersect(int a, int b, int c, int d)
        {
            return Range(a, b, c) || Range(a, b, d) || Range(c, d, a) || Range(c, d, b);
        }

        /// <summary>
        /// Проверяет, принадлежит ли число x отрезку [a; b]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private bool Range(int a, int b, int x)
        {
            return a <= x && x <= b;
        }

        /// <summary>
        /// Считывает слова из текстового файла
        /// </summary>
        private void ReadWords(string path)
        {
            rawWords = new List<string>();
            var reader = new StreamReader(path);
            /// Формируем отсортированный список слов
            while (!reader.EndOfStream)
            {
                string next = reader.ReadLine();
                if (!rawWords.Contains(next))
                {
                    int index = rawWords.FindIndex(word => word.Length >= next.Length);
                    if (index == -1)
                        rawWords.Add(next);
                    else
                        rawWords.Insert(index, next);
                }
                else
                    MessageBox.Show($"В списке есть повторяющееся слово: {next}\n" +
                        "Оно не будет задействовано в схеме", "Внимание!");
            }
            numberOfWords = rawWords.Count;
        }

        /// <summary>
        /// Возвращает решение
        /// </summary>
        /// <returns></returns>
        public ArrayWord<Word> Solution() { return solution; }
    }
}
