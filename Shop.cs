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
    public partial class Shop : Form
    {
        public string username;
        public bool anonymous;
        public Shop(bool Anonymous, string Username)
        {
            InitializeComponent();
            label2.Hide();
            username = Username;
            anonymous = Anonymous;
            newItem.Visible = false;
            deleteItem.Visible = false;
            addCategory.Visible = false;
            
            if(username == "admin")
            {
                newItem.Visible = true;
                deleteItem.Visible = true;
                addCategory.Visible = true;
            }

            SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
            dbConnection.Open();

            string sql = "select name from category";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            categoryList.Items.Add("All");
            while (reader.Read())
                categoryList.Items.Add(reader[0]);
            reader.Close();

            string sql2 = "select name from item";
            SQLiteCommand command2 = new SQLiteCommand(sql2, dbConnection);
            SQLiteDataReader reader2 = command2.ExecuteReader();

            while (reader2.Read())
                itemList.Items.Add(reader2[0]);
            reader2.Close();
        }

        private void button1_Click(object sender, EventArgs e) //select category button
        {
            SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
            dbConnection.Open();
            itemList.Items.Clear();
            string sql;
            if(categoryList.SelectedItem == "All")
            {
                sql = "select name from item";
            }
            else
            {
                sql = "select name from item where category='" + categoryList.SelectedItem + "'";
            }
            
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
                itemList.Items.Add(reader[0]);
            reader.Close();
            dbConnection.Close();
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
            if(username != "")
            {
                new AccountLoggedIn(username).Show();
            }
            
        }

        private void openItem_Click(object sender, EventArgs e)
        {
            if(itemList.SelectedIndex != -1)
            {
                this.Close();
                string Item = itemList.SelectedItem.ToString();
                new itemView(Item, anonymous, username).Show();
            }
            else
            {
                label2.Show();
            }
        }

        private void newItem_Click(object sender, EventArgs e)
        {
            this.Close();
            new itemAdd(anonymous, username).Show();
        }

        private void deleteItem_Click(object sender, EventArgs e)
        {
            SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
            dbConnection.Open();

            for (int i = itemList.Items.Count - 1; i >= 0; i--)
            {
                if (itemList.GetSelected(i))
                {
                    string itemName = itemList.GetItemText(itemList.Items[i]);
                    itemList.Items.Remove(itemList.Items[i]);
                    string deleteSql = "delete from item where name='" + itemName + "'";
                    SQLiteCommand deleteCommand = new SQLiteCommand(deleteSql, dbConnection);
                    deleteCommand.ExecuteNonQuery();
                }
            }
            dbConnection.Close();
        }

        private void addCategory_Click(object sender, EventArgs e)
        {
            this.Close();
            new addCategoryAdmin(anonymous, username).Show();
        }

        private void itemList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
