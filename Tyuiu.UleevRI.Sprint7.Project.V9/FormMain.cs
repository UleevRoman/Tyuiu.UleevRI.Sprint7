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
            //this.WindowState = FormWindowState.Maximized;
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

                dataGridViewOpenFile_URI.Rows.Clear();
                dataGridViewOpenFile_URI.Columns.Clear();
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
                comboBoxSort_URI.Text = "";
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

                    int rows = dataGridViewOpenFile_URI.RowCount - 1;
                    int columns = dataGridViewOpenFile_URI.ColumnCount - 1;

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
            }
        }

        static string[,] mtrxSort;
        static int tralivali = 0;
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
                tralivali++;
                
                int vozmogno = 1; int k = -1; 
                for (int i = 1; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount; j++)
                    {
                        if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Value == null) vozmogno++;
                    }
                    if (vozmogno == dataGridViewOpenFile_URI.ColumnCount)
                    {
                        k = i;
                        break;
                    }
                    else vozmogno = 0;
                }
                if (k > -1) MessageBox.Show("Пожалуйста, удалите все пустые строки, кроме последней", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void comboBoxSort_URI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSort_URI.SelectedItem != null && dataGridViewOpenFile_URI.RowCount != 0)
            {
                int vozmogno = 1; int k = -1;
                for (int i = 1; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount; j++)
                    {
                        if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Value == null) vozmogno++;
                    }
                    if (vozmogno == dataGridViewOpenFile_URI.ColumnCount)
                    {
                        k = i;
                        break;
                    }
                    else vozmogno = 0;
                }
                if (k > -1) MessageBox.Show("Пожалуйста, удалите все пустые строки, кроме последней", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    int kakbuda = 0;
                    for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                    {
                        for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                        {
                            if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected == false) kakbuda++;
                        }
                    }
                    if (kakbuda != (dataGridViewOpenFile_URI.RowCount - 1) * (dataGridViewOpenFile_URI.ColumnCount - 1))
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
                            if (columnIndex > -1) break;
                        }

                        for (int i = 1; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                        {
                            string elmnt = dataGridViewOpenFile_URI.Rows[i].Cells[columnIndex].Value.ToString();
                            if (elmnt.Contains(","))
                            {
                                elmnt.Replace(",", ".");
                                dataGridViewOpenFile_URI.Rows[i].Cells[columnIndex].Value = Convert.ToDouble(dataGridViewOpenFile_URI.Rows[i].Cells[columnIndex].Value.ToString());
                            }
                            else
                            {
                                int cellValue;
                                if (int.TryParse(dataGridViewOpenFile_URI.Rows[i].Cells[columnIndex].Value.ToString(), out cellValue))
                                {
                                    dataGridViewOpenFile_URI.Rows[i].Cells[columnIndex].Value = Convert.ToDouble(dataGridViewOpenFile_URI.Rows[i].Cells[columnIndex].Value.ToString());
                                }
                            }
                        }

                        if (dataGridViewOpenFile_URI.RowCount != 0)
                        {
                            DataGridViewRow row = dataGridViewOpenFile_URI.Rows[0];
                            dataGridViewOpenFile_URI.Rows.Remove(dataGridViewOpenFile_URI.Rows[0]);

                            DataGridViewColumn column = dataGridViewOpenFile_URI.Columns[columnIndex];
                            string selectedItem = comboBoxSort_URI.SelectedItem.ToString();
                            if (selectedItem == "По возрастанию (от А до Я)") dataGridViewOpenFile_URI.Sort(column, ListSortDirection.Ascending);
                            if (selectedItem == "По убыванию (от Я до А)") dataGridViewOpenFile_URI.Sort(column, ListSortDirection.Descending);
                            dataGridViewOpenFile_URI.Rows.Insert(0, row);

                            for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                            {
                                for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                                {
                                    dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                                }
                            }

                            textBoxQuantity_URI.Text = "";
                            textBoxSum_URI.Text = "";
                            textBoxMiddleValue_URI.Text = "";
                            textBoxMinValue_URI.Text = "";
                            textBoxMaxValue_URI.Text = "";
                        }
                    }
                    else MessageBox.Show("Пожалуйста, выберите столбец", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonSort_URI_Click(object sender, EventArgs e)
        {
            //
            if (dataGridViewOpenFile_URI.RowCount != 0 && tralivali != 0)
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
            else if (dataGridViewOpenFile_URI.RowCount != 0 && tralivali == 0) MessageBox.Show("Пожалуйста, нажмите на пустое поле выбора сортировки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static string[,] mtrxFilter;
        static int bulo = 0;
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
                bulo++;
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBoxFilter_URI_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int nugno = -1;
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
                        if (nugno > -1) break;
                    }
                }
                for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                    }
                }

                if (nugno > -1)
                {
                    for (int i = 0; i < dataGridViewOpenFile_URI.RowCount; i++)
                    {
                        if (dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value != null)
                        {
                            string elmnt = dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString().ToLower();
                            if (elmnt.StartsWith(textBoxFilter_URI.Text.ToLower())) dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Selected = true;
                        }
                    }

                    for (int r = 1; r < dataGridViewOpenFile_URI.RowCount - 1; r++)
                    {
                        if (dataGridViewOpenFile_URI.Rows[r].Cells[nugno].Selected != true) dataGridViewOpenFile_URI.Rows[r].Visible = false;
                    }

                    for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                    {
                        for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                        {
                            dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                        }
                    }

                    textBoxQuantity_URI.Text = "";
                    textBoxSum_URI.Text = "";
                    textBoxMiddleValue_URI.Text = "";
                    textBoxMinValue_URI.Text = "";
                    textBoxMaxValue_URI.Text = "";
                }
                else MessageBox.Show("Не выбран столбец", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonFilter_URI_Click(object sender, EventArgs e)
        {
            if (dataGridViewOpenFile_URI.RowCount != 0 && bulo != 0)
            {
                dataGridViewOpenFile_URI.Rows.Clear();
                dataGridViewOpenFile_URI.Columns.Clear();
                dataGridViewOpenFile_URI.RowCount = mtrxFilter.GetUpperBound(0) + 1;
                dataGridViewOpenFile_URI.ColumnCount = mtrxFilter.GetUpperBound(1) + 1;
                textBoxMaxValue_URI.Text = Convert.ToString(mtrxFilter.GetUpperBound(0) + 1);
                textBoxMinValue_URI.Text = Convert.ToString(mtrxFilter.GetUpperBound(1) + 10);
                for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        dataGridViewOpenFile_URI.Rows[i].Cells[j].Value = mtrxFilter[i, j];
                    }
                }
                dataGridViewOpenFile_URI.AutoResizeColumns();
            }
            else if (dataGridViewOpenFile_URI.RowCount != 0 && bulo == 0) MessageBox.Show("Еще не были применены фильтры", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonDelete_URI_Click(object sender, EventArgs e)
        {
            if (dataGridViewOpenFile_URI.RowCount != 0)
            {
                int nugno = -1; int udal = 0;
                for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected == true)
                        {
                            nugno = j;
                            break;
                        }
                    }
                    if (nugno > -1) udal++;
                }
                if (nugno > -1)
                {
                    var result = MessageBox.Show($"{"Удалить данную строку?" + "\r"}{"Ее невозможно будет восстановить"}", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        int k = -1;
                        for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                        {
                            if (dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Selected == true)
                            {
                                k = i;
                                break;
                            }
                            if (k > -1) break;
                        }
                        for (int r = 0; r < udal; r++) dataGridViewOpenFile_URI.Rows.Remove(dataGridViewOpenFile_URI.Rows[k]);
                        for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                        {
                            for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                            {
                                dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                            }
                        }
                    }
                }
                else MessageBox.Show("Выберите строку, которую ходите удалить", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonAdd_URI_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewOpenFile_URI.Rows.Add();
                for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                {
                    for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                    {
                        dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected = false;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxQuantity_URI_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridViewOpenFile_URI.RowCount != 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int nugno = -1;
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
                            if (nugno > -1) break;
                        }
                    }

                    int counter = 0;
                    for (int r = 0; r < dataGridViewOpenFile_URI.RowCount - 1; r++)
                    {
                        if (dataGridViewOpenFile_URI.Rows[r].Cells[nugno].Selected == true) counter++;
                    }
                    textBoxQuantity_URI.Text = Convert.ToString(counter);
                }
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBoxSum_URI_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridViewOpenFile_URI.RowCount != 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int nugno = -1;
                    for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                    {
                        for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                        {
                            if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected == true)
                            {
                                nugno = j;
                                break;
                            }
                        }
                        if (nugno > -1) break;
                    }

                    if (nugno > -1)
                    {
                        if (dataGridViewOpenFile_URI.Rows[0].Cells[nugno].Selected != true)
                        {
                            double sum = 0;
                            for (int i = 1; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                            {
                                if (dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Selected == true)
                                {
                                    string elmnt = dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString();
                                    if (elmnt.Contains(","))
                                    {
                                        elmnt.Replace(",", ".");
                                        sum += Convert.ToDouble(dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString());
                                    }
                                    else
                                    {
                                        int cellValue;
                                        if (int.TryParse(dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString(), out cellValue))
                                        {
                                            sum += Convert.ToDouble(dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString());
                                        }
                                    }
                                }
                            }
                            if (sum != 0) textBoxSum_URI.Text = Convert.ToString(sum);
                            else
                            {
                                MessageBox.Show("Пожалуйста, выберите диапозон ячеек с числами", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                textBoxSum_URI.Text = "";

                                textBoxMiddleValue_URI.Text = "";
                                textBoxMinValue_URI.Text = "";
                                textBoxMaxValue_URI.Text = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пожалуйста, выберите диапозон ячеек с числами", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBoxSum_URI.Text = "";

                            textBoxMiddleValue_URI.Text = "";
                            textBoxMinValue_URI.Text = "";
                            textBoxMaxValue_URI.Text = "";
                        }
                    }
                    else MessageBox.Show("Не выбран столбец", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBoxMiddleValue_URI_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridViewOpenFile_URI.RowCount != 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int nugno = -1;
                    for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                    {
                        for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                        {
                            if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected == true)
                            {
                                nugno = j;
                                break;
                            }
                        }
                        if (nugno > -1) break;
                    }

                    if (nugno > -1)
                    {
                        if (dataGridViewOpenFile_URI.Rows[0].Cells[nugno].Selected != true)
                        {
                            double srznach = 0; int kol = 0;
                            for (int i = 1; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                            {
                                if (dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Selected == true)
                                {
                                    string elmnt = dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString();
                                    if (elmnt.Contains(","))
                                    {
                                        elmnt.Replace(",", ".");
                                        srznach += Convert.ToDouble(dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString());
                                        kol++;
                                    }
                                    else
                                    {
                                        int cellValue;
                                        if (int.TryParse(dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString(), out cellValue))
                                        {
                                            srznach += Convert.ToDouble(dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString());
                                            kol++;
                                        }
                                    }
                                }
                            }
                            if (srznach != 0) textBoxMiddleValue_URI.Text = Convert.ToString(srznach / kol);
                            else
                            {
                                MessageBox.Show("Пожалуйста, выберите диапозон ячеек с числами", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                textBoxMiddleValue_URI.Text = "";

                                textBoxSum_URI.Text = "";
                                textBoxMinValue_URI.Text = "";
                                textBoxMaxValue_URI.Text = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пожалуйста, выберите диапозон ячеек с числами", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBoxMiddleValue_URI.Text = "";

                            textBoxSum_URI.Text = "";
                            textBoxMinValue_URI.Text = "";
                            textBoxMaxValue_URI.Text = "";
                        }
                    }
                    else MessageBox.Show("Не выбран столбец", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBoxMinValue_URI_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridViewOpenFile_URI.RowCount != 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int nugno = -1;
                    for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                    {
                        for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                        {
                            if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected == true)
                            {
                                nugno = j;
                                break;
                            }
                        }
                        if (nugno > -1) break;
                    }

                    if (nugno > -1)
                    {
                        if (dataGridViewOpenFile_URI.Rows[0].Cells[nugno].Selected != true)
                        {
                            double min = 9999999;
                            for (int i = 1; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                            {
                                if (dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Selected == true)
                                {
                                    string elmnt = dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString();
                                    if (elmnt.Contains(","))
                                    {
                                        elmnt.Replace(",", ".");
                                        min = Math.Min(Convert.ToDouble(dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString()), min);
                                    }
                                    else
                                    {
                                        int cellValue;
                                        if (int.TryParse(dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString(), out cellValue))
                                        {
                                            min = Math.Min(Convert.ToDouble(dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString()), min);
                                        }
                                    }
                                }
                            }
                            if (min != 9999999) textBoxMinValue_URI.Text = Convert.ToString(min);
                            else
                            {
                                MessageBox.Show("Пожалуйста, выберите диапозон ячеек с числами", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                textBoxMinValue_URI.Text = "";

                                textBoxSum_URI.Text = "";
                                textBoxMiddleValue_URI.Text = "";
                                textBoxMaxValue_URI.Text = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пожалуйста, выберите диапозон ячеек с числами", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBoxMinValue_URI.Text = "";

                            textBoxSum_URI.Text = "";
                            textBoxMiddleValue_URI.Text = "";
                            textBoxMaxValue_URI.Text = "";
                        }
                    }
                    else MessageBox.Show("Не выбран столбец", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBoxMaxValue_URI_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridViewOpenFile_URI.RowCount != 0)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int nugno = -1;
                    for (int i = 0; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                    {
                        for (int j = 0; j < dataGridViewOpenFile_URI.ColumnCount - 1; j++)
                        {
                            if (dataGridViewOpenFile_URI.Rows[i].Cells[j].Selected == true)
                            {
                                nugno = j;
                                break;
                            }
                        }
                        if (nugno > -1) break;
                    }

                    if (nugno > -1)
                    {
                        if (dataGridViewOpenFile_URI.Rows[0].Cells[nugno].Selected != true)
                        {
                            double max = -9999999;
                            for (int i = 1; i < dataGridViewOpenFile_URI.RowCount - 1; i++)
                            {
                                if (dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Selected == true)
                                {
                                    string elmnt = dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString();
                                    if (elmnt.Contains(","))
                                    {
                                        elmnt.Replace(",", ".");
                                        max = Math.Max(Convert.ToDouble(dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString()), max);
                                    }
                                    else
                                    {
                                        int cellValue;
                                        if (int.TryParse(dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString(), out cellValue))
                                        {
                                            max = Math.Max(Convert.ToDouble(dataGridViewOpenFile_URI.Rows[i].Cells[nugno].Value.ToString()), max);
                                        }
                                    }
                                }
                            }
                            if (max != -9999999) textBoxMaxValue_URI.Text = Convert.ToString(max);
                            else
                            {
                                MessageBox.Show("Пожалуйста, выберите диапозон ячеек с числами", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                textBoxMaxValue_URI.Text = "";

                                textBoxSum_URI.Text = "";
                                textBoxMiddleValue_URI.Text = "";
                                textBoxMinValue_URI.Text = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пожалуйста, выберите диапозон ячеек с числами", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBoxMaxValue_URI.Text = "";

                            textBoxSum_URI.Text = "";
                            textBoxMiddleValue_URI.Text = "";
                            textBoxMinValue_URI.Text = "";
                        }
                    }
                    else MessageBox.Show("Не выбран столбец", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

