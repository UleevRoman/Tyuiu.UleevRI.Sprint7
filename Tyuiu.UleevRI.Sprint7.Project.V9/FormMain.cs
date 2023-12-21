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
                dataGridViewOpenFile_URI.ColumnCount = columns + 10;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        dataGridViewOpenFile_URI.Rows[i].Cells[j].Value = matrix[i, j];
                        dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                    }
                }
                //this.dataGridViewOpenFile_URI.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
                //this.dataGridViewOpenFile_URI.DefaultCellStyle.Font = new Font("Segoe UI", 9);
                //this.dataGridViewOpenFile_URI.DefaultCellStyle.Font = new Font("Arial", 10.99F, GraphicsUnit.Pixel);
                this.dataGridViewOpenFile_URI.DefaultCellStyle.Font = new Font("Tahoma", 9);
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
                mtrxSearch = new string[dataGridViewOpenFile_URI.RowCount - 1, dataGridViewOpenFile_URI.ColumnCount - 1];
                for (int i = 0; i < mtrxSearch.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < mtrxSearch.GetUpperBound(1); j++)
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
            if (e.KeyCode == Keys.Enter)
            {
                for (int i = 0; i < mtrxSearch.GetUpperBound(0); i++)
                {
                    for (int j = 0; j < mtrxSearch.GetUpperBound(1); j++)
                    {
                        if (mtrxSearch[i, j] != null)
                        {
                            string elmnt = mtrxSearch[i, j].ToLower();
                            if (elmnt.Contains(textBoxSearch_URI.Text.ToLower())) dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = true;
                        }
                    }
                }

                int esti = 0;
                for (int r = 0; r < mtrxSearch.GetUpperBound(0); r++)
                {
                    for (int c = 0; c < mtrxSearch.GetUpperBound(1); c++)
                    {
                        if (dataGridViewOpenFile_URI.Rows[r].Cells[c].Selected == true) esti += 1;
                    }
                }
                if (esti == 0)
                {
                    for (int r = 0; r < mtrxSearch.GetUpperBound(0); r++)
                    {
                        for (int c = 0; c < mtrxSearch.GetUpperBound(1); c++)
                        {
                            dataGridViewOpenFile_URI.Rows[r].Visible = true;
                            dataGridViewOpenFile_URI.Rows[r].Selected = false;
                        }
                    }
                }
                else
                {
                    int clear = 0;
                    for (int r = 1; r < mtrxSearch.GetUpperBound(0); r++)
                    {
                        for (int c = 0; c < mtrxSearch.GetUpperBound(1); c++)
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
        private void comboBoxSort_URI_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridViewOpenFile_URI.RowCount != 0)
            {
                mtrxSort = new string[dataGridViewOpenFile_URI.RowCount, dataGridViewOpenFile_URI.ColumnCount];
                for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        mtrxSort[i, j] = Convert.ToString(dataGridViewOpenFile_URI.Rows[i].Cells[j].Value);
                    }
                }
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void comboBoxSort_URI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSort_URI.SelectedItem.ToString() != "Изначальное состояние")
            {
                if (comboBoxSort_URI.SelectedItem != null)
                {
                    int columnIndex = -1;
                    for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                    {
                        for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                        {
                            if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Value != null)
                            {
                                if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected == true)
                                {
                                    columnIndex = j;
                                    break;
                                }
                            }
                        }
                        if (columnIndex > 0) break;
                    }

                    DataGridViewRow row = dataGridViewOpenFile_URI.Rows[0];
                    dataGridViewOpenFile_URI.Rows.Remove(dataGridViewOpenFile_URI.Rows[0]);
                    DataGridViewColumn column = dataGridViewOpenFile_URI.Columns[columnIndex];
                    string selectedItem = comboBoxSort_URI.SelectedItem.ToString();
                    if (selectedItem == "По возрастанию")
                    {
                        dataGridViewOpenFile_URI.Sort(column, ListSortDirection.Ascending);
                        dataGridViewOpenFile_URI.Rows.Insert(0, row);
                        for (int i = 1; i < dataGridViewOpenFile_URI.RowCount - 2; i++)
                        {
                            if (dataGridViewOpenFile_URI.Rows[i].Cells[columnIndex].Value == null)
                            {
                                dataGridViewOpenFile_URI.Rows[i].Visible = false;
                            }
                        }
                    }
                    if (selectedItem == "По убыванию")
                    {
                        dataGridViewOpenFile_URI.Sort(column, ListSortDirection.Descending);
                        dataGridViewOpenFile_URI.Rows.Insert(0, row);
                    }
                }
            }
            else
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
                        if (dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value != null)
                        {
                            string elmnt = dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString().ToLower();
                            if (elmnt.StartsWith(textBoxFilter_URI.Text.ToLower())) dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Selected = true;
                        }
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

        private void textBoxQuantity_URI_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int nugno = -1;
                for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                { 
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        int cellCalue;
                        if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected == true && int.TryParse(dataGridViewOpenFile_URI.Rows[i].Cells[j].Value.ToString(), out cellCalue))
                        {
                            nugno = j;
                            break;
                        }
                    }
                    if (nugno > 0) break;
                }

                int counter = 0;
                if (nugno > 0)
                {
                    for (int r = 1; r < dataGridViewOpenFile_URI.RowCount - 1; r++) counter++;
                    textBoxQuantity_URI.Text = Convert.ToString(counter)
                }
            }
        }

        private void textBoxSum_URI_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int nugno = -1;
                for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        int cellCalue;
                        if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected == true && int.TryParse(dataGridViewOpenFile_URI.Rows[i].Cells[j].Value.ToString(), out cellCalue))
                        {
                            nugno = j;
                            break;
                        }
                    }
                    if (nugno > 0) break;
                }

                int sum = 0;
                if (nugno > 0)
                {
                    for (int r = 1; r < dataGridViewOpenFile_URI.RowCount - 1; r++)
                    {
                        string elmnt = dataGridViewOpenFile_URI.Rows[r].Cells[nugno].Value.ToString();
                        if (elmnt.Contains(",")) elmnt.Replace(",", ".");
                        sum += Convert.ToInt32(elmnt);
                    }
                    textBoxSum_URI.Text = Convert.ToString(sum);
                }
            }
        }
    }
}

