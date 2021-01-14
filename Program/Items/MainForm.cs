using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Items
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void TextBoxesClear()
        {
            btnItemAdd.Text = "Добавить";

            cbCategory.Text = "";
            cbPodcategory.Text = "";
            tbItemName.Text = "";
            tbPriceRoznica.Text = "";
        }

        private void ClearData()
        {
            dgvItems.AutoGenerateColumns = true;
            

            dgvItems.Columns.Clear();
            

            dgvItems.DataSource = null;
            
        }

        private void ItemsColumnsFormat()
        {
            dgvItems.Columns[0].HeaderText = "Код предмета";
            dgvItems.Columns[1].HeaderText = "Код категории";
            dgvItems.Columns[2].HeaderText = "Код подкатегории";
            dgvItems.Columns[3].HeaderText = "Название";
            dgvItems.Columns[4].HeaderText = "Розничная цена";
            dgvItems.Columns[5].HeaderText = "Безнал. розничная";
            dgvItems.Columns[6].HeaderText = "Мелкий опт";
            dgvItems.Columns[7].HeaderText = "Опт";

            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            btnEdit.HeaderText = "Редактирование";
            btnEdit.Text = "Изменить";
            btnEdit.Name = "btnEdit";
            btnEdit.UseColumnTextForButtonValue = true;
            dgvItems.Columns.Add(btnEdit);

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.HeaderText = "Удаление";
            btnDelete.Text = "Удалить";
            btnDelete.Name = "btnDelete";
            btnDelete.UseColumnTextForButtonValue = true;
            dgvItems.Columns.Add(btnDelete);
        }

       

        /// <summary>
        /// Загрузка данных
        /// </summary>
        private void LoadData()
        {
            ClearData();

            if (tabs.SelectedIndex == 0) //Items
            {
                Database.OpenDatabase();
                DataTable dt = Database.SelectQuery("SELECT " +
                    "i.ItemId, " +
                    "c.Name, " +
                    "p.NamePodcat, " +
                    "i.ItemName, " +
                    "i.PriceRoznica, " +
                    "i.PriceBezNalRoznica, " +
                    "i.PriceLittleOpt, " +
                    "i.PriceOpt FROM Items i " +
                    "JOIN Category c ON i.CategoryId = c.CategoryId " +
                    "JOIN Podcategory p ON p.PodcategoryId = i.PodcategoryId");
                dgvItems.DataSource = dt;
                Database.CloseDatabase();

                ItemsColumnsFormat();

                Database.OpenDatabase();
                cbCategory.Items.Clear();
                SqlDataReader reader = Database.BasicSelectQuery("SELECT * FROM Category");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cbCategory.Items.Add(reader[0].ToString() + ". " + reader[1].ToString());
                    }
                }
                Database.CloseDatabase(); //Transactions
            }
            else
            {
                Database.OpenDatabase();
                DataTable dt = Database.SelectQuery("SELECT " +
                    "t.Id, " +
                    "i.ItemName, " +
                    "t.PriceTrans, " +
                    "t.DateTrans, " +
                    "u.Name + ' ' + u.Surname FROM Transactions t " +
                    "JOIN Users u ON u.Id = t.UserId " +
                    "JOIN Items i ON i.ItemId = t.ItemId");
               // dgvTransactions.DataSource = dt;
                Database.CloseDatabase();

             
            }
        }

        private void завершитьСессиюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            new Authorization().Show();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lDatetime.Text = System.DateTime.Now.ToString();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Выбор категории из списка. Загрузка списка подкатегорий
        /// </summary>
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbPodcategory.Enabled = true;

            Database.OpenDatabase();
            int CategoryId = int.Parse(cbCategory.Items[cbCategory.SelectedIndex].ToString().Split('.')[0]);
            cbPodcategory.Items.Clear();
            SqlDataReader reader = Database.BasicSelectQuery("SELECT * FROM Podcategory WHERE CategoryId = " + CategoryId);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cbPodcategory.Items.Add(reader[0].ToString() + ". " + reader[2].ToString());
                }
            }
            Database.CloseDatabase();
        }

        /// <summary>
        /// Выбор подкатегории из списка
        /// </summary>
        private void cbPodcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbItemName.Enabled = true;
            tbPriceRoznica.Enabled = true;
        }

        private void tbPriceRoznica_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void tbPriceRoznica_TextChanged(object sender, EventArgs e)
        {
            if (tbItemName.Text.Length > 0 && tbPriceRoznica.Text.Length > 0)
            {
                btnItemAdd.Enabled = true;
                btnReset.Enabled = true;
            }
            else
            {
                btnItemAdd.Enabled = false;
                btnReset.Enabled = false;
            }
        }

        private void tbItemName_TextChanged(object sender, EventArgs e)
        {
            if (tbItemName.Text.Length > 0 && tbPriceRoznica.Text.Length > 0)
            {
                btnItemAdd.Enabled = true;
                btnReset.Enabled = true;
            }
            else
            {
                btnItemAdd.Enabled = false;
                btnReset.Enabled = false;
            }
        }
        
        /// <summary>
        /// Добавление предмета
        /// </summary>
        private void btnItemAdd_Click(object sender, EventArgs e)
        {
            int CategoryId = int.Parse(cbCategory.Text.Split('.')[0]);
            int PodcategoryId = int.Parse(cbPodcategory.Text.Split('.')[0]);

            double priceRoznica = Math.Round(double.Parse(tbPriceRoznica.Text), 2);
            double beznalRoznica = Math.Round(priceRoznica * 96.5 / 100, 2);
            double littleOpt = Math.Round(priceRoznica * 92 / 100, 2);
            double opt = Math.Round(priceRoznica * 87 / 100, 2);

            if (btnItemAdd.Text == "Добавить")
            {
                Database.OpenDatabase();
                int ItemId = 0;
                SqlDataReader reader = Database.BasicSelectQuery("SELECT TOP 1 ItemId FROM Items ORDER BY ItemId DESC");
                if (reader.HasRows)
                {
                    reader.Read();
                    ItemId = reader.GetInt32(0);
                }
                Database.CloseDatabase();
                ItemId++;

                Database.InsertInto("INSERT INTO Items VALUES (" +
                    ItemId + ", " +
                    CategoryId + ", " +
                    PodcategoryId + ", '" +
                    tbItemName.Text + "', " +
                    "CONVERT(DECIMAL(18,2)," + priceRoznica + "), " +
                    "CONVERT(DECIMAL(18,2), " + beznalRoznica + "), " +
                    "CONVERT(DECIMAL(18,2), " + littleOpt + "), " +
                    "CONVERT(DECIMAL(18,2), " + opt + "))");
            }
            else //изменить
            {
                if (MessageBox.Show("Вы уверены, что хотите изменить запись?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    int ItemId = int.Parse(dgvItems[0, dgvItems.SelectedRows[0].Index].Value.ToString());

                    Database.UpdateQuery("UPDATE Items Set " +
                        "CategoryId = " + CategoryId + ", " +
                        "PodcategoryId = " + PodcategoryId + ", " +
                        "ItemName = '" + tbItemName.Text + "', " +
                        "PriceRoznica = CONVERT(DECIMAL(18,2)," + priceRoznica + "), " +
                        "PriceBezNalRoznica = CONVERT(DECIMAL(18,2), " + beznalRoznica + "), " +
                        "PriceLittleOpt = CONVERT(DECIMAL(18,2), " + littleOpt + "), " +
                        "PriceOpt = CONVERT(DECIMAL(18,2), " + opt + ") " +
                        "WHERE ItemId =  " + ItemId);
                }
            }

            LoadData();
            TextBoxesClear();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvItems.Columns["btnEdit"].Index) //изменение записи
            {
                btnItemAdd.Text = "Изменить";
                int ItemId = int.Parse(dgvItems[0, dgvItems.SelectedRows[0].Index].Value.ToString());

                Database.OpenDatabase();
                SqlDataReader reader = Database.BasicSelectQuery("SELECT i.*, c.Name, p.NamePodcat FROM Items i " +
                    "JOIN Category c ON c.CategoryId = i.CategoryId " +
                    "JOIN Podcategory p ON p.PodcategoryId = i.PodcategoryId " +
                    "WHERE ItemId = " + ItemId);
                if (reader.HasRows)
                {
                    reader.Read();

                    cbCategory.Text = reader[1].ToString() + ". " + reader[8].ToString();
                    cbPodcategory.Text = reader[2].ToString() + ". " + reader[9].ToString();
                    tbItemName.Text = reader[3].ToString();
                    tbPriceRoznica.Text = reader[4].ToString();
                }
                Database.CloseDatabase();   
            }
            else if (e.ColumnIndex == dgvItems.Columns["btnDelete"].Index) //удаление записи
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить выделенную запись?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    int ItemId = int.Parse(dgvItems[0, dgvItems.SelectedRows[0].Index].Value.ToString());

                    Database.DeleteQuery("DELETE FROM Items WHERE ItemId =  " + ItemId);

                    LoadData();
                }
            }
        }

        private void tbSearchName_TextChanged(object sender, EventArgs e)
        {
            if (tbSearchName.Text.Length > 0)
            {
                ClearData();

                Database.OpenDatabase();
                DataTable dt = Database.SelectQuery("SELECT * FROM Items WHERE ItemName Like '%" + tbSearchName.Text + "%'");
                dgvItems.DataSource = dt;
                ItemsColumnsFormat();
                Database.CloseDatabase();
            }
            else
                LoadData();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            TextBoxesClear();
        }

        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FormTransactions().Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new HideLogin().Show();
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new FormGalery().Show();
            Hide();
        }
    }
}
