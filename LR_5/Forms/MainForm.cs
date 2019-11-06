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
    public partial class MainForm : Form
    {
        int row;
        int col;
        int dgHeight;
        int dgWidth;
        double[] probabilities;
        int[,] matrix;
        DataGridView dataGridView;
        Button nextButton;
        TextBox probTextBox;
        Label probLabel;

        public MainForm(int row, int col)
        {
            InitializeComponent();
            this.row = row;
            this.col = col;
            matrix = new int[row, col];
            probabilities = new double[col];
            dataGridView = new DataGridView();
            nextButton = new Button();
            probTextBox = new TextBox();
            probLabel = new Label();
            CreateDataGridView();
            CreateLabel();
            CreateTextBox();
            CreateButton();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void CreateLabel()
        {
            probLabel.Text = "Введите вероятности (разделяя ';' ): ";
            probLabel.AutoSize = true;
            Point location = dataGridView.PointToScreen(Point.Empty);
            probLabel.Location = new Point(location.X - 10, location.Y + dgHeight);
            Controls.Add(probLabel);
        }

        private void CreateTextBox()
        {
            probTextBox.Width = 200;
            Point location = probLabel.PointToScreen(Point.Empty);
            probTextBox.Location = new Point(location.X - 10 + probLabel.Width + 10, location.Y - 2 * probLabel.Height - 7);
            Controls.Add(probTextBox);
        }

        public void CreateButton()
        {
            nextButton.Text = "Далее";
            nextButton.Width = 80;
            nextButton.Height = 25;
            Point location = probLabel.PointToScreen(Point.Empty);
            nextButton.Location = new Point(location.X - 10, location.Y + probLabel.Height - 20);
            nextButton.Click += NextButton_Click;
            Controls.Add(nextButton);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            bool res = ValidateDataGridValues() && ValidateProbabilities();
            if (res)
            {
                new Form1(matrix, row, col, probabilities).Show();
                Close();
            }
           
        }
        
        private bool ValidateDataGridValues()
        {
            try
            {
                for (int rows = 0; rows < dataGridView.Rows.Count; rows++)
                    for (int col = 0; col < dataGridView.Rows[rows].Cells.Count; col++)
                        matrix[rows,col] = Convert.ToInt32(dataGridView.Rows[rows].Cells[col].Value.ToString());
                return true;
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ошибка при считывании данных из таблицы. " + exp.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool ValidateProbabilities()
        {
            try
            {
                var tmp = probTextBox.Text.Split(new char[] { ';' });
                for (int i = 0; i < tmp.Length; i++)
                    probabilities[i] = Convert.ToDouble(tmp[i]);
                return true;
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ошибка при считывании данных из таблицы. " + exp.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void CreateDataGridView()
        {
            dataGridView.ColumnCount = col;
            for (int i = 0; i < col; i++)
            {
                dataGridView.Columns[i].Name = "s" + (i + 1);
            }
            for (int i = 0; i < row; i++)
            {
                string[] row = new string[col];
                dataGridView.Rows.Add(row);
                dataGridView.Rows[i].HeaderCell.Value = "a" + (i + 1);
            }
            
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.AllowUserToAddRows = false;
            
            dgHeight = 23;
            foreach (DataGridViewRow dr in dataGridView.Rows)
            {
                dgHeight += dr.Height;
            }

            dgWidth = 72;
            foreach (DataGridViewColumn dc in dataGridView.Columns)
            {
                dgWidth += dc.Width;
            }

            dataGridView.Width = dgWidth;
            dataGridView.Height = dgHeight;
            dataGridView.Location = new Point(25, 25);
            Controls.Add(dataGridView);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }
    }
}
