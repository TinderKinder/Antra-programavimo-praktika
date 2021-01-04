using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace _3Pd_Sotkevicius_v1
{
    public partial class itemAdd : Form
    {
        public string username;
        public bool anonymous;
        public string category = "";
        public string imageLocation;
        public itemAdd(bool Anonymous, string Username)
        {
            InitializeComponent();
            anonymous = Anonymous;
            username = Username;
            label6.Hide();
            SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
            dbConnection.Open();

            string sql = "select name from category";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                listBox1.Items.Add(reader[0]);
            reader.Close();
            dbConnection.Close();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CancelItem_Click(object sender, EventArgs e)
        {
            new Shop(anonymous, username).Show();
            this.Close();
        }

        private void SelectCategoryItem_Click(object sender, EventArgs e)
        {
            category = listBox1.SelectedItem.ToString();
            label6.Hide();
        }

        private void AddPictureItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files (*.png)|*.png| All Files(*.*)|*.*";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imageLocation = dialog.FileName;
                pictureBox1.ImageLocation = imageLocation;
                label6.Hide();
            }
        }

        private void AddItemItem_Click(object sender, EventArgs e)
        {
            if(imageLocation == "" || textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0 || category == "")
            {
                label6.Show();
            }
            else
            {
                SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
                dbConnection.Open();

                string insertSql = "insert into item values('" + textBox2.Text + "', '" + textBox1.Text + "','" + textBox3.Text + "eur','" + imageLocation + "', '" + category + "')";
                SQLiteCommand insertCommand = new SQLiteCommand(insertSql, dbConnection);
                insertCommand.ExecuteNonQuery();
                dbConnection.Close();

                new Shop(anonymous, username).Show();
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
