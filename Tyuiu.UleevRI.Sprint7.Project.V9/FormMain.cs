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
                dataGridViewOpenFile_URI.RowCount = rows + 1;
                dataGridViewOpenFile_URI.ColumnCount = columns + 1;

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
                if (saveFileDialog_URI.ShowDialog() == DialogResult.OK)
                {
                    string savepath = saveFileDialog_URI.FileName;

                    if (File.Exists(savepath)) File.Delete(savepath);

                    int rows = dataGridViewOpenFile_URI.RowCount;
                    int columns = dataGridViewOpenFile_URI.ColumnCount;

                    StringBuilder strBuilder = new StringBuilder();

                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            strBuilder.Append(dataGridViewOpenFile_URI.Rows[i].Cells[j].Value);

                            if (j != columns - 1) strBuilder.Append(";");
                        }
                        strBuilder.AppendLine();
                    }
                    File.WriteAllText(savepath, strBuilder.ToString(), Encoding.GetEncoding(1251));
                    MessageBox.Show("Файл успешно сохранен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                int esti = 0;
                for (int r = 0; r < dataGridViewOpenFile_URI.RowCount - 1; r++)
                {
                    for (int c = 0; c < dataGridViewOpenFile_URI.ColumnCount - 1; c++)
                    {
                        if (dataGridViewOpenFile_URI.Rows[r].Cells[c].Selected == true) esti += 1;
                    }
                }
                if (esti == 0)
                {
                    for (int r = 0; r < dataGridViewOpenFile_URI.RowCount - 1; r++)
                    {
                        for (int c = 0; c < dataGridViewOpenFile_URI.ColumnCount - 1; c++)
                        {
                            dataGridViewOpenFile_URI.Rows[r].Visible = true;
                        }
                    }
                }
                else
                {
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

        private void comboBoxSort_URI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSort_URI.SelectedItem != null)
            {
                int columnIndex = 0;
                int stolbets = 0;
                for (int i = 0; i < dataGridViewOpenFile_URI.ColumnCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Value != null)
                        {
                            if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected == true)
                            {
                                columnIndex = j;
                                stolbets = j;
                                break;
                            }
                        }
                        if (columnIndex > 0) break;
                    }
                    bool Num = true;

                    foreach (DataGridViewRow row in dataGridViewOpenFile_URI.Rows)
                    {
                        int cellValue;
                        if (row.Cells[columnIndex].Value != null && int.TryParse(row.Cells[columnIndex].Value.ToString(), out cellValue))
                        {
                            row.Cells[columnIndex].Value = cellValue;
                        }
                        else
                        {
                            row.Cells[columnIndex].Value = 0; 
                            Num = false;
                        }
                    }
                    if (Num)
                    {
                        DataGridViewColumn column = dataGridViewOpenFile_URI.Columns[stolbets];
                        string selectedItem = comboBoxSort_URI.SelectedItem.ToString();

                        if (selectedItem == "По возрастанию") dataGridViewOpenFile_URI.Sort(column, ListSortDirection.Ascending);
                        if (selectedItem == "По убыванию") dataGridViewOpenFile_URI.Sort(column, ListSortDirection.Descending);
                    }
                    //else MessageBox.Show("Невозможно выполнить сортировку");
                }
            }
        }

        static string[,] mtrxFilter;
        private void textBoxFilter_URI_KeyDoWn(object sender, KeyEventArgs e)
        {
            if (dataGridViewOpenFile_URI.RowCount != 0)
            {
                mtrxFilter = new string[dataGridViewOpenFile_URI.RowCount, dataGridViewOpenFile_URI.ColumnCount];
                for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        mtrxFilter[i, j] = Convert.ToString(dataGridViewOpenFile_URI.Rows[i].Cells[j].Value);
                    }
                }
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBoxFilter_URI_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int nugno = 0;
                for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Value != null)
                        {
                            if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected == true)
                            {
                                nugno = j;
                                break;
                            }
                        }
                        if (nugno > 0) break;
                    }
                }
                for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                    }
                }

                if (nugno > 0)
                {
                    for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                    {
                        string elmnt = dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString().ToLower();
                        if (elmnt.StartsWith(textBoxFilter_URI.Text.ToLower())) dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Selected = true;
                    }

                    for (int r = 1; r < dataGridViewOpenFile_URI.RowCount - 1; r++)
                    {
                        if (dataGridViewOpenFile_URI.Rows[r].Cells[nugno].Selected == true) dataGridViewOpenFile_URI.Rows[r].Visible = true;
                        else dataGridViewOpenFile_URI.Rows[r].Visible = false;
                    }
                    for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                    {
                        for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                        {
                            dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                        }
                    }
                }
                else MessageBox.Show("Не выбран столбец", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

