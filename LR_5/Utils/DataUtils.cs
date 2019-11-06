using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR_5.Utils
{
    class DataUtils
    {
        /// <summary>
        /// Выделяет заданные строки таблицы
        /// </summary>
        /// <param name="dataGridView">Ссылка на DataGridView</param>
        /// <param name="index">Индекс строки</param>
        /// <param name="reSelect">Произвести новую выборку строк</param>
        public static void SelectRow(DataGridView dataGridView, int index, bool reSelect = false)
        {
            if (reSelect) dataGridView.CurrentCell.Selected = false;
            dataGridView.Rows[index].Selected = true;
        }

        /// <summary>
        /// Генерирует новую таблицу на основе заданной матрицы
        /// </summary>
        /// <param name="col">Количество столбцов</param>
        /// <param name="row">Количество строк</param>
        /// <param name="dataGridView">Ссылка на DataGridView</param>
        /// <param name="matrix">Матрицы для отрисовки</param>
        public static void FillDataGrid(int row, int col, DataGridView dataGridView, int[,] matrix)
        {
            dataGridView.ColumnCount = col;
            string[] rows = new string[row];
            for (int i = 0; i < row; i++)
            {
                dataGridView.Columns[i].Name = "s" + (i + 1);
                for (int j = 0; j < col; j++)
                {
                    rows[j] = matrix[i, j] + "";
                }
                dataGridView.Rows.Add(rows);
                dataGridView.Rows[i].HeaderCell.Value = "a" + (i + 1);
            }
            dataGridView.ClearSelection();
        }

        /// <summary>
        /// Отображает результат выполнения метода
        /// </summary>
        /// <param name="dictionary">Словарь оптимальных решений для отображения</param>
        /// <param name="dataGridView">Ссылка на DataGridView</param>
        /// <param name="label">Ссылка на Label</param>
        public static void PrintResult(Dictionary<int, double> dictionary, DataGridView dataGridView, Label label)
        {
            foreach (var item in dictionary)
            {
                SelectRow(dataGridView, item.Key);
                label.Text += item.Value + "; ";
            }
        }
    }
}
