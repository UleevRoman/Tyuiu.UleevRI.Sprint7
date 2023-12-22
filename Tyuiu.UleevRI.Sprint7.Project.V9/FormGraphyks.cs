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

        private void сохранитьToolStripMenuItemGraphyks_URI_Click(object sender, EventArgs e)
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

        static string openFile;
        static int rows;
        static int columns;
        static string[,] matrix;
        DataService ds = new DataService();
        private void buttonOpenFile_URI_Click(object sender, EventArgs e)
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
            }
            catch
            {
                MessageBox.Show("Файл не выбран", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
