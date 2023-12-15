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

        static string openFile;
        static int rows;
        static int columns;
        DataService ds = new DataService();
        public string[,] LoadFromData(string path)
        {
            string file = File.ReadAllText(path);
            file = file.Replace('\n', '\r');
            string[] lines = file.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

            rows = lines.Length;
            columns = lines[0].Split(';').Length;

            string[,] array = new string[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                string[] line_mas = lines[i].Split(';');
                for (int j = 0; j < columns; j++)
                {
                    array[i, j] = Convert.ToString(line_mas[j]);
                }
            }
            return array;

        }

        private void открытьToolStripMenuItemGraphyks_URI_Click(object sender, EventArgs e)
        {
            openFileDialog_URI.ShowDialog();
            openFile = openFileDialog_URI.FileName;

            string[,] matrix = new string[rows, columns];
            matrix = LoadFromData(openFile);

            dataGridViewGraphyks_URI.RowCount = rows;
            dataGridViewGraphyks_URI.ColumnCount = columns;

            for (int i = 0; i < columns; i++)
            {
                dataGridViewGraphyks_URI.Columns[i].Width = 50;
            }
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    dataGridViewGraphyks_URI.Rows[r].Cells[c].Value = matrix[r, c];
                }
            }
            matrix = ds.GetMatrix(LoadFromData(openFile));
            //
        }

        private void сохранитьToolStripMenuItemGraphyks_URI_Click(object sender, EventArgs e)
        {
            saveFileDialog_URI.FileName = "OutPutFileTask7.csv";
            saveFileDialog_URI.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog_URI.ShowDialog();
            string path = saveFileDialog_URI.FileName;
            FileInfo fileInfo = new FileInfo(path);
            bool fileExists = fileInfo.Exists;
            if (fileExists)
            {
                File.Delete(path);
            }
        }
    }
}
