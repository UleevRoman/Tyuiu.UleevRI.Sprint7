﻿using System;
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
        private void открытьToolStripMenuItemGraphyks_URI_Click(object sender, EventArgs e)
        {
            /*
            openFileDialog_URI.ShowDialog();
            openFile = openFileDialog_URI.FileName;

            string[,] matrix = ds.LoadFromDataFile(openFile);
            rows = matrix.GetLength(0);
            columns = matrix.GetLength(1);
            dataGridViewGraphyks_URI.RowCount = 100;
            dataGridViewGraphyks_URI.ColumnCount = 100;

            for (int i = 0; i < rows; i++)
            {
                dataGridViewGraphyks_URI.Columns[i].Width = 100;
            }
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    dataGridViewGraphyks_URI.Rows[i].Cells[j].Value = matrix[i, j];
                }
            }
            */
        }

        private void сохранитьToolStripMenuItemGraphyks_URI_Click(object sender, EventArgs e)
        {
            /*
            saveFileDialog_URI.FileName = ".xlx";
            saveFileDialog_URI.InitialDirectory = @":C";
            saveFileDialog_URI.ShowDialog();
            string path = saveFileDialog_URI.FileName;
            FileInfo fileInfo = new FileInfo(path);
            bool fileExists = fileInfo.Exists;
            if (fileExists)
            {
                File.Delete(path);
            }
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
                dataGridViewGraphyks_URI.RowCount = 100;
                dataGridViewGraphyks_URI.ColumnCount = 100;

                for (int i = 0; i < rows; i++)
                {
                    dataGridViewGraphyks_URI.Columns[i].Width = 100;
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
    }
}
