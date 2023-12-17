using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Tyuiu.UleevRI.Sprint7.Project.V9.Lib;


namespace Tyuiu.UleevRI.Sprint7.Project.V9
{
    public partial class FormGraphyks : Form
    {
        public FormGraphyks()
        {
            InitializeComponent();
            openFileDialog_URI.Filter = "Значения, разделенные запятыми(*.csv)|*.csv|Всефайлы(*.*)|*.*";
        }

        private void toolStripMenuItemHelp_URI_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
        }

        private void открытьToolStripMenuItemGuied_URI_Click(object sender, EventArgs e)
        {
            FormGuied formGuied = new FormGuied();
            formGuied.Show();
        }

        private void toolStripMenuItemBack_URI_Click(object sender, EventArgs e)
        {
            this.Close();
            FormMain formMain = new FormMain();
            formMain.Show();
        }

        private void FormGraphyks_Load(object sender, EventArgs e)
        {
            this.Hide();
            FormMain formMain = new FormMain();
            formMain.Show();
        }

        static string openFile;
        static int rows;
        static int columns;
        DataService ds = new DataService();
        private void открытьToolStripMenuItemGraphyks_URI_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog_URI.ShowDialog();
                openFile = openFileDialog_URI.FileName;

                string[,] matrix = ds.LoadFromDataFile(openFile);
                rows = matrix.GetLength(0);
                columns = matrix.GetLength(1);
                this.chartFunction_URI.ChartAreas[0].AxisX.Title = "Ось X";
                this.chartFunction_URI.ChartAreas[0].AxisY.Title = "Ось Y";

                for (int i = 1; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (j == 2)
                        {
                            this.chartFunction_URI.Series[0].Points.AddXY(matrix[i, 0], matrix[i, j]);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void сохранитьToolStripMenuItemGraphyks_URI_Click(object sender, EventArgs e)
        {
            /*
            chartFunction_URI.FilName = "xlx";
            saveFileDialog_URI.InitialDirectory = @":C";
            saveFileDialog_URI.ShowDialog();
            string path = saveFileDialog_URI.FileName;
            FileInfo fileInfo = new FileInfo(path);
            bool fileExists = fileInfo.Exists;
            if (fileExists) File.Delete(path);
            */
        }

        private void buttonOpenFile_URI_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog_URI.ShowDialog();
                openFile = openFileDialog_URI.FileName;

                string[,] matrix = ds.LoadFromDataFile(openFile);
                rows = matrix.GetLength(0);
                columns = matrix.GetLength(1);
                dataGridViewGraphyks_URI.RowCount = 250;
                dataGridViewGraphyks_URI.ColumnCount = 50;

                for (int i = 0; i < rows; i++)
                {
                    dataGridViewGraphyks_URI.Columns[i].Width = 135;
                }
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        dataGridViewGraphyks_URI.Rows[i].Cells[j].Value = matrix[i, j];
                    }
                }
            }
            catch
            {
                MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSaveFile_URI_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog_URI.FileName = ".lsx";
                saveFileDialog_URI.InitialDirectory = @":C";
                saveFileDialog_URI.ShowDialog();
                string path = saveFileDialog_URI.FileName;
                FileInfo fileInfo = new FileInfo(path);
                bool fileExists = fileInfo.Exists;
                if (fileExists)
                {
                    File.Delete(path);
                }
            }
            catch
            {
                MessageBox.Show("Файл не сохранен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewOpenFile_URI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && dataGridViewGraphyks_URI.CurrentCell.ColumnIndex == 1)
            {
                e.Handled = true;
                DataGridViewCell cell = dataGridViewGraphyks_URI.Rows[0].Cells[0];
                dataGridViewGraphyks_URI.CurrentCell = cell;
                dataGridViewGraphyks_URI.BeginEdit(true);
            }
        }

    }
}
