using LR_5.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR_5
{
    class SavageMethod
    {
        // исходная матрица
        int[,] matrix;
        // вектор минимумов при нахождении потерей
        int[] minVector;
        // вектор максимумов при нахождении потерей
        int[] maxVector;

        // вектор минимумов при нахождении доходов
        int[] _minVector;
        // вектор максимумов при нахождении доходов
        int[] _maxVector;

        // размер векторов
        readonly int row;
        readonly int col;

        // Способ принятия решения
        Mode mode;

        /// <summary>
        /// Создает новый экземпляр класса SavageMethod
        /// </summary>
        /// <param name="matrix">Исходная матрица</param>
        /// <param name="row">Количество строк</param>
        /// <param name="row">Количество столбцов</param>
        public SavageMethod(int[,] matrix, int row, int col, Mode mode = Mode.Minimum)
        {
            this.matrix = matrix;
            this.col = col;
            this.row = row;
            minVector = new int[col];
            maxVector = new int[row];
            _minVector = new int[row];
            _maxVector = new int[col];
            this.mode = mode;
        }

        /// <summary>
        /// Выполняет поиск оптимального решения с помощью критерия Сэвиджа
        /// </summary>
        public void Calculate()
        {
          if(mode == Mode.Minimum)
            {
                // 1. поиск минимума по столбцам матрицы
                for (int i = 0; i < col; i++)
                {
                    minVector[i] = matrix[i, i];
                    for (int j = 0; j < col; j++)
                    {
                        if (matrix[j, i] <= minVector[i])
                            minVector[i] = matrix[j, i];
                    }
                }

                // 2. Отнимаем минимум от каждого значения ячейки соответствующего столбца
                for (int i = 0; i < col; i++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        matrix[j, i] -= minVector[i];
                    }
                }

                // 3. Ищем максимум в каждой строке 
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        if (matrix[i, j] > maxVector[i])
                            maxVector[i] = matrix[i, j];
                    }
                }
            } else
            {
                // 1. поиск максимума по столбцам матрицы
                for (int i = 0; i < col; i++)
                {
                    _maxVector[i] = matrix[i, i];
                    for (int j = 0; j < col; j++)
                    {
                        if (matrix[j, i] >= _maxVector[i])
                            _maxVector[i] = matrix[j, i];
                    }
                }

                // 2. Отнимаем каждое значения максимум от ячейки соответствующего столбца
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        matrix[i, j] = _maxVector[j] - matrix[i,j];
                    }
                }

                // 3. Ищем максимум в каждой строке 
                for (int i = 0; i < row; i++)
                {
                    _maxVector[i] = matrix[i, 0];
                    for (int j = 0; j < col; j++)
                    {
                        if (matrix[i, j] > _maxVector[i])
                            _maxVector[i] = matrix[i, j];
                    }
                }
            }
        }

        /// <summary>
        /// Выполняет поиск минимального (оптимального) значения в векторе решений
        /// </summary>
        /// <returns>Минимальное (оптимальное) значение и его индекс в виде словаря</returns>
        Dictionary<int, double> TransformData()
        {
            if(mode == Mode.Minimum)
            {
                int index = 0;
                double min = maxVector[index];
                Dictionary<int, double> dictionary = new Dictionary<int, double>();

                // поиск минимального значения среди найденных максимумов
                for (int i = 0; i < maxVector.Length; i++)
                    if (maxVector[i] < min)
                    {
                        min = maxVector[i];
                        index = i;
                    }

                // формируем словарь на случай если минимальных значений > 1
                dictionary.Add(index, min);
                for (int i = 0; i < maxVector.Length; i++)
                    if (maxVector[i] == min && index != i)
                        dictionary.Add(i, maxVector[i]);
                return dictionary;
            } else
            {
                int index = 0;
                double min = _maxVector[index];
                Dictionary<int, double> dictionary = new Dictionary<int, double>();

                // поиск минимального значения среди найденных максимумов
                for (int i = 0; i < _maxVector.Length; i++)
                    if (maxVector[i] < min)
                    {
                        min = _maxVector[i];
                        index = i;
                    }

                // формируем словарь на случай если минимальных значений > 1
                dictionary.Add(index, min);
                for (int i = 0; i < _maxVector.Length; i++)
                    if (_maxVector[i] == min && index != i)
                        dictionary.Add(i, _maxVector[i]);
                return dictionary;
            }
        }

        public Dictionary<int, double> getResult { get { return TransformData(); } }
    }
}
