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
    public partial class HideLogin : Form
    {
        public HideLogin()
        {
            InitializeComponent();
        }

        private void HideLogin_Load(object sender, EventArgs e)
        {
            ILogin.Text = "Your Login: " + Database.UserName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new MainForm().Show();
            Hide();
        }
    }
}
