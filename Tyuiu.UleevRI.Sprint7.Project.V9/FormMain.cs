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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            //openFileDialog_URI.Filter = "Значения, разделенные запятыми(*.csv)|*.csv|Всефайлы(*.*)|*.*";
        }

        private void toolStripMenuItemHelp_URI_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
        }

        private void перейтиКРазделуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormGraphyks formGraphyks = new FormGraphyks();
            formGraphyks.Show();

        }

        private void открытьToolStripMenuItemGuied_URI_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormGuied formGuied = new FormGuied();
            formGuied.Show();
        }

        static string openFile;
        static int rows;
        static int columns;
        static string[,] matrix;
        DataService ds = new DataService();
        private void открытьToolStripMenuItemFile_URI_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog_URI.ShowDialog();
                openFile = openFileDialog_URI.FileName;

                matrix = ds.LoadFromDataFile(openFile);
                rows = matrix.GetLength(0);
                columns = matrix.GetLength(1);
                dataGridViewOpenFile_URI.RowCount = 250;
                dataGridViewOpenFile_URI.ColumnCount = 50;

                for (int i = 0; i < rows; i++)
                {
                    //dataGridViewOpenFile_URI.Columns[i].Width = 50;
                }
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        dataGridViewOpenFile_URI.Rows[i].Cells[j].Value = matrix[i, j];
                        dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                    }
                }
                dataGridViewOpenFile_URI.AutoResizeColumns();
            }
            catch
            {
                MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void сохранитьToolStripMenuItemFile_URI_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog_URI.FileName = ".csv";
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

        static string[,] mtrxSearch;
        private void textBoxSearch_URI_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridViewOpenFile_URI.RowCount != 0)
            {
                mtrxSearch = new string[dataGridViewOpenFile_URI.RowCount, dataGridViewOpenFile_URI.ColumnCount];
                for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        mtrxSearch[i, j] = Convert.ToString(dataGridViewOpenFile_URI.Rows[i].Cells[j].Value);
                        dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                    }
                }
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBoxSearch_URI_KeyUp(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                {
                    dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Value != null)
                        {
                            string elmnt = dataGridViewOpenFile_URI.Rows[i].Cells[j].Value.ToString().ToLower();
                            if (elmnt.Contains(textBoxSearch_URI.Text.ToLower())) dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = true;
                        }
                    }
                }

                int clear = 0;
                for (int r = 1; r < dataGridViewOpenFile_URI.RowCount - 1; r++)
                {
                    for (int c = 0; c < dataGridViewOpenFile_URI.ColumnCount - 1; c++)
                    {
                        if (dataGridViewOpenFile_URI.Rows[r].Cells[c].Selected == true) clear += 1;
                    }
                    if (clear == 0) dataGridViewOpenFile_URI.Rows[r].Visible = false;
                    else
                    {
                        dataGridViewOpenFile_URI.Rows[r].Visible = true;
                        clear = 0;
                    }
                }
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBoxSearch_URI_KeyPress(object sender, KeyPressEventArgs e)
        {
            for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                {
                    dataGridViewOpenFile_URI.Rows[i].Cells[j].Value = mtrxSearch[i, j];
                    dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                }
            }
        }

        static string[,] mtrxSort;
        private void textBoxSort_URI_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridViewOpenFile_URI.RowCount != 0)
            {
                mtrxSort = new string[dataGridViewOpenFile_URI.RowCount, dataGridViewOpenFile_URI.ColumnCount];
                for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        mtrxSort[i, j] = Convert.ToString(dataGridViewOpenFile_URI.Rows[i].Cells[j].Value);
                        dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                    }
                }
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBoxSort_URI_KeyUp(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                {
                    dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                //int seichas;
                if (comboBoxSort_URI.Text == "По возрастанию")
                {
                    for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                    {
                        for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                        {
                            if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected == true)
                            {
                                //DataGridViewColumn.SortedRows[i];
                                //seichas = 1;
                                //break;
                            }
                        }
                    }
                    /*
                    if (seichas == 1)
                    {
                        for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                        {
                            for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                            {

                            }
                        }
                    }
                    */
                }
                if (comboBoxSort_URI.Text == "По убыванию")
                {
                    //
                }
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBoxSort_URI_KeyPress(object sender, KeyPressEventArgs e)
        {
            for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                {
                    dataGridViewOpenFile_URI.Rows[i].Cells[j].Value = mtrxSort[i, j];
                    dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                }
            }
        }

        static string[,] mtrxFilter;
        private void textBoxFilter_URI_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridViewOpenFile_URI.RowCount != 0)
            {
                mtrxFilter = new string[dataGridViewOpenFile_URI.RowCount, dataGridViewOpenFile_URI.ColumnCount];
                for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        mtrxFilter[i, j] = Convert.ToString(dataGridViewOpenFile_URI.Rows[i].Cells[j].Value);
                        dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                    }
                }
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBoxFilter_URI_KeyUp(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                {
                    dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Value != null)
                        {
                            string elmnt = dataGridViewOpenFile_URI.Rows[i].Cells[j].Value.ToString().ToLower();
                            if (elmnt.Contains(comboBoxFilter_URI.Text.ToLower())) dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = true;
                        }
                    }
                }

                int clear = 0;
                for (int r = 1; r < dataGridViewOpenFile_URI.RowCount - 1; r++)
                {
                    for (int c = 0; c < dataGridViewOpenFile_URI.ColumnCount - 1; c++)
                    {
                        if (dataGridViewOpenFile_URI.Rows[r].Cells[c].Selected == true) clear += 1;
                    }
                    if (clear == 0) dataGridViewOpenFile_URI.Rows[r].Visible = false;
                    else
                    {
                        dataGridViewOpenFile_URI.Rows[r].Visible = true;
                        clear = 0;
                    }
                }
                for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                    }
                }
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBoxFilter_URI_KeyPress(object sender, KeyPressEventArgs e)
        {
            for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                {
                    dataGridViewOpenFile_URI.Rows[i].Cells[j].Value = mtrxFilter[i, j];
                    dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                }
            }
        }

        private void buttonDelete_URI_Click(object sender, EventArgs e)
        {
            if (dataGridViewOpenFile_URI.RowCount != 0)
            {
                int konechno = 0;
                var result = MessageBox.Show($"{"Удалить данную строку?" + "\r"}{"Ее невозможно будет восстановить"}", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) konechno = 1;
                if (konechno == 1)
                {
                    int a = dataGridViewOpenFile_URI.CurrentCell.RowIndex;
                    dataGridViewOpenFile_URI.Rows.Remove(dataGridViewOpenFile_URI.Rows[a]);
                }
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonAdd_URI_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewOpenFile_URI.Rows.Add();
            }
            catch
            {
                MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
