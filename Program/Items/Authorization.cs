using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Items
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void btnAuth_Click(object sender, EventArgs e)
        {
            string login = tbLogin.Text;
            string password = tbPassword.Text;
            string query = "SELECT Id, Login, Password FROM Users WHERE Login = '" + login + "' AND Password = '" + password + "'";

            Database.OpenDatabase();
            SqlDataReader reader = Database.BasicSelectQuery(query);
            if (reader.HasRows)
            {
                reader.Read();
                Database.UserId = reader.GetInt32(0);
                Database.UserName = reader[1].ToString();

                new MainForm().Show();
                Hide();
            }
            else
                MessageBox.Show("Логин или пароль введены не верно! Попробуйте еще раз...");

            Database.CloseDatabase();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
