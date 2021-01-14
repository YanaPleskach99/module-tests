using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Items
{
    public partial class FormGalery : Form
    {
        public FormGalery()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FormVegetables().Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FormFruits().Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new FormBerries().Show();
            Hide();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            new MainForm().Show();
            Hide();
        }
    }
}
