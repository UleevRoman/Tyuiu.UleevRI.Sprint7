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
using System.Data;
using Tyuiu.UleevRI.Sprint7.Project.V9.Lib;

namespace Tyuiu.UleevRI.Sprint7.Project.V9
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            openFileDialog_URI.Filter = "Значения, разделенные запятыми(*.csv)|*.csv|Всефайлы(*.*)|*.*";
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
        DataService ds = new DataService();
        private void открытьToolStripMenuItemFile_URI_Click(object sender, EventArgs e)
        {
            openFileDialog_URI.ShowDialog();
            openFile = openFileDialog_URI.FileName;

            string[,] matrix = ds.LoadFromDataFile(openFile);
            rows = matrix.GetLength(0);
            columns = matrix.GetLength(1);
            dataGridViewOpenFile_URI.RowCount = 20;
            dataGridViewOpenFile_URI.ColumnCount = 20;

            for (int i = 0; i < rows; i++)
            {
                dataGridViewOpenFile_URI.Columns[i].Width = 135;
            }
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    dataGridViewOpenFile_URI.Rows[i].Cells[j].Value = matrix[i, j];
                }
            }
        }

        private void сохранитьToolStripMenuItemFile_URI_Click(object sender, EventArgs e)
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
    }
}
