﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tyuiu.UleevRI.Sprint7.Project.V9
{
    public partial class FormGraphyks : Form
    {
        public FormGraphyks()
        {
            InitializeComponent();
        }

        private void toolStripMenuItemHelp_URI_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
        }

        private void открытьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FormGuied formGuied = new FormGuied();
            formGuied.Show();
        }
    }
}
