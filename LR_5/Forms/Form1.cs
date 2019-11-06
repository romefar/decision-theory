using LR_5.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR_5
{
    public partial class Form1 : Form
    {
        // количество строк
        int row;
        // количество столбцов
        int col;
        // исходная матрица
        int[,] matrix;
        // копии исходной матрицы для отображения в DataGridView
        int[,] matrix1;
        int[,] matrix2;
        int[,] matrix3;
        // вектор вероятностей
        double[] probabilities;

        /// <summary>
        /// Создаёт экземпляр основной формы
        /// </summary>
        /// <param name="matrix">Исходная матрицы</param>
        /// <param name="row">Количество строк</param>
        /// <param name="col">Количестов столбцов</param>
        /// <param name="probabilities">Вектор вероятностей</param>
        public Form1(int[,] matrix, int row, int col, double[] probabilities)
        {
            this.col = col;
            this.row = row;
            this.matrix = matrix;
            this.probabilities = probabilities;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /* Ручной ввод матрицы */

           // matrix = new int[,] { { 5, 7, 12 }, { 6, 8, 10 }, { 12, 14, 9 } };
           // matrix = new int[,] { { 100, 120, 160, 185 }, { 120, 110, 145, 170 }, { 140, 145, 140, 175 }, { 170, 165, 150, 190 } };
            matrix1 = new int[row, col];
            matrix2 = new int[row, col];
            matrix3 = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int  j= 0; j < col; j++)
                {
                    matrix1[i, j] = matrix[i, j];
                    matrix2[i, j] = matrix[i, j];
                    matrix3[i, j] = matrix[i, j];
                }
            }
            /* Ручной ввод вектора вероятностей */
            //probabilities = new double[] { 0.4, 0.5, 0.1 };
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            #region Метод Лапласа
            DataUtils.FillDataGrid(row, col, dataGridView1, matrix1);

            // Для нахождения дохода изменить Mode.Minimum на Mode.Maximum
            LaplaceMethod laplace = new LaplaceMethod(matrix1, probabilities, row, Mode.Minimum);

            laplace.Calculate();
            DataUtils.PrintResult(laplace.getResult, dataGridView1, LaplaceLabel);
            #endregion

            #region Метод Сэвиджа
            DataUtils.FillDataGrid(row, col, dataGridView2, matrix2);

            // Для нахождения дохода изменить Mode.Minimum на Mode.Maximum
            SavageMethod savage = new SavageMethod(matrix2, row, col, Mode.Minimum);

            savage.Calculate();
            DataUtils.PrintResult(savage.getResult, dataGridView2, SavageLabel);
            #endregion

            #region Метод Гурвица
            DataUtils.FillDataGrid(row, col, dataGridView3, matrix3);

            // Для нахождения дохода изменить Mode.Minimum на Mode.Maximum
            GurwitsMethod gurwits = new GurwitsMethod(matrix3, row, Mode.Minimum);

            gurwits.Calculate();
            DataUtils.PrintResult(gurwits.getResult, dataGridView3, GurwitsLabel);
            #endregion

            button1.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
