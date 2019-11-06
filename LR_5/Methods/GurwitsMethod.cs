using LR_5.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR_5
{
    class GurwitsMethod
    {
        // исходная матрица
        int[,] matrix;
        // вектор решений
        double[] optimal;
        // вектор минимумов
        int[] minVector;
        // вектор максимумов
        int[] maxVector;
        
        // показатель оптимизма (если необходимо, то менять его)
        double alpha = 0;
        
        // размер векторов
        int size;


        // Способ принятия решения
        Mode mode;

        /// <summary>
        /// Создает новый экземпляр класса GurwitsMethod
        /// </summary>
        /// <param name="matrix">Исходная матрица</param>
        /// <param name="size">Количество строк</param>
        /// <param name="a">Показатель оптмизма (по-умолчанию 0.25)</param>
        public GurwitsMethod(int[,] matrix, int size, Mode mode = Mode.Minimum, double a = 0.25)
        {
            this.matrix = matrix;
            this.size = size;
            optimal = new double[size];
            minVector = new int[size];
            maxVector = new int[size];
            alpha = a;
            this.mode = mode;
        }

        /// <summary>
        /// Выполняет поиск оптимального решения с помощью критерия Гурвица 
        /// </summary>
        public void Calculate()
        {
            for (int i = 0; i < size; i++)
            {
                minVector[i] = matrix[i, 0];
                maxVector[i] = matrix[i, 0];
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j] < minVector[i])
                        minVector[i] = matrix[i, j];
                    if (matrix[i, j] > maxVector[i])
                        maxVector[i] = matrix[i, j];
                }
                if(mode == Mode.Minimum)
                    optimal[i] = alpha * minVector[i] + (1 - alpha) * maxVector[i];
                else
                    optimal[i] = alpha * maxVector[i] + (1 - alpha) * minVector[i];
            }
        }

        /// <summary>
        /// Выполняет поиск минимального (оптимального) значения в векторе решений
        /// </summary>
        /// <returns>Минимальное (оптимальное) значение и его индекса</returns>
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
