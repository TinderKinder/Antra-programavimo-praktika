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
    public partial class wishlist : Form
    {
        public string username;
        public wishlist(string Username)
        {
            InitializeComponent();
            username = Username;

            SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
            dbConnection.Open();

            string sql = "select item from wishlist where username='"+username+"'";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0]);
            }
            dbConnection.Close();
        }

        private void button2_Click(object sender, EventArgs e) // go back
        {
            new AccountLoggedIn(username).Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) // remove from wishlist
        {
            SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
            dbConnection.Open();

            for (int i = listBox1.Items.Count - 1; i >= 0; i--)
            {
                if (listBox1.GetSelected(i))
                {
                    string deleteItem = listBox1.GetItemText(listBox1.Items[i]);
                    listBox1.Items.Remove(listBox1.Items[i]);
                    
                    string deleteSql = "delete from wishlist where item='" + deleteItem + "' and username='" + username + "'";
                    SQLiteCommand deleteCommand = new SQLiteCommand(deleteSql, dbConnection);
                    deleteCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
