using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Items
{
    public partial class FormTransactions : Form
    {
        public FormTransactions()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(@"Data Source=(local);Initial Catalog=F:\DB.MDF;Integrated Security=True");

        private void shdata()
        {
            listView1.Items.Clear();

            cn.Open();
            SqlCommand comd = new SqlCommand("Select * From Transactions", cn);
            SqlDataReader readata = comd.ExecuteReader();

            while (readata.Read())
            {
                ListViewItem add = new ListViewItem();
                add.Text = readata["id"].ToString();
                add.SubItems.Add(readata["ItemId"].ToString());
                add.SubItems.Add(readata["PriceTrans"].ToString());
                add.SubItems.Add(readata["DateTrans"].ToString());
                add.SubItems.Add(readata["UserId"].ToString());
           

                listView1.Items.Add(add);
            }
            cn.Close();
        }

       
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            shdata();
        }

        private void btnReturn_Click_1(object sender, EventArgs e)
        {
            new MainForm().Show();
            Hide();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
//Data Source=(local);Initial Catalog=E:\BASE\DB.MDF;Integrated Security=True