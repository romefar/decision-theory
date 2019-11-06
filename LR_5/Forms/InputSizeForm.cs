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
    public partial class InputSizeForm : Form
    {
        public InputSizeForm()
        {
            InitializeComponent();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            try
            {
                int row = Convert.ToInt32(rowTextBox.Text);
                int col = Convert.ToInt32(colTextBox.Text);
                new MainForm(row, col).Show();
                Hide();
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ожидается целочисленное значение. " + exp.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
