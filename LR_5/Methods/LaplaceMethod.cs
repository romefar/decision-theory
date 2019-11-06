using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LR_5.Utils;

namespace LR_5
{
    class LaplaceMethod
    {
        // исходная матрица
        int[,] matrix;
        // вектор вероятностей
        double[] probabilities;
        // вектор решений
        double[] optimal;
        // количество строк
        readonly int size;

        // Способ принятия решения
        Mode mode;

        /// <summary>
        /// Создает новый экземпляр класса LaplaceMethod
        /// </summary>
        /// <param name="matrix">Исходная матрица</param>
        /// <param name="probabilities">>Массив вероятностей</param>
        /// <param name="size">Количество строк</param>
        /// <param name="mode">Способ принятия решения (доход, расход)</param>
        public LaplaceMethod(int[,] matrix, double[] probabilities, int size, Mode mode = Mode.Minimum)
        {
            this.matrix = matrix;
            this.probabilities = probabilities;
            this.size = size;
            optimal = new double[size];
            this.mode = mode;
        }

        /// <summary>
        /// Выполняет поиск оптимального решения с помощью критерия Лапласа 
        /// </summary>
        public void Calculate()
        {
            // 1. Ищем сумму по строкам
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    optimal[i] += matrix[i, j];
                }

                // 2. умножаем на соответствующую вероятность
                optimal[i] *= probabilities[i];
            }
        }

        /// <summary>
        /// Выполняет поиск минимального (оптимального) значения в векторе решений
        /// </summary>
        /// <returns>Минимальное (оптимальное) значение и его индекс в виде словаря</returns>
        private Dictionary<int, double> TransformData()
        {
           if(mode == Mode.Minimum)
            {
                int index = 0;
                double min = optimal[index];
                Dictionary<int, double> dictionary = new Dictionary<int, double>();

                // поиск минимального значения среди найденных решений
                for (int i = 0; i < optimal.Length; i++)
                    if (optimal[i] < min)
                    {
                        min = optimal[i];
                        index = i;
                    }

                // формируем словарь на случай если минимальных значений > 1
                dictionary.Add(index, min);
                for (int i = 0; i < optimal.Length; i++)
                    if (optimal[i] == min && index != i)
                        dictionary.Add(i, optimal[i]);
                return dictionary;
            } else
            {
                int index = 0;
                double max = optimal[index];
                Dictionary<int, double> dictionary = new Dictionary<int, double>();

                // поиск максимального значения среди найденных решений
                for (int i = 0; i < optimal.Length; i++)
                    if (optimal[i] > max)
                    {
                        max = optimal[i];
                        index = i;
                    }

                // формируем словарь на случай если максимальных значений > 1
                dictionary.Add(index, max);
                for (int i = 0; i < optimal.Length; i++)
                    if (optimal[i] == max && index != i)
                        dictionary.Add(i, optimal[i]);
                return dictionary;
            }
        }

        public Dictionary<int, double> getResult { get { return TransformData(); } } 

    }
}
