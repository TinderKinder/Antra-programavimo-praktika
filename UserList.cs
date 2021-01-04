using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

namespace _3Pd_Sotkevicius_v1
{
    public partial class UserList : Form
    {
        public string name;
        public string surname;
        public DateTime birthdate;
        public string username;
        public string password;
        public UserList(string Username)
        {
            InitializeComponent();
            label1.Hide();
            
            username = Username;


            SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
            dbConnection.Open();

            string sql = "select username from user except select username from user where username='admin'";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                checkedListBox1.Items.Add(reader[0]);
            }

            if(checkedListBox1.Items.Count == 0)
            {
                deleteUser.Enabled = false;
                label1.Show();
            }

            

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void goBacktoAccountAdmin_Click(object sender, EventArgs e)
        {
            this.Close();
            new AccountLoggedIn(username).Show();
        }

        private void deleteUser_Click(object sender, EventArgs e)
        {
            SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
            dbConnection.Open();

            if (checkedListBox1.Items.Count == 0)
            {
                deleteUser.Enabled = false;
                label1.Show();
            }
            else
            {
                deleteUser.Enabled = true;
                label1.Hide();

                foreach (object Item in checkedListBox1.CheckedItems)
                {
                    for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                    {
                        string deleteSql = "delete from user where username='" + Item.ToString() + "'";
                        SQLiteCommand deleteCommand = new SQLiteCommand(deleteSql, dbConnection);
                        deleteCommand.ExecuteNonQuery();
                    }
                }
                //removes items from listbox
                for (int i = checkedListBox1.Items.Count - 1; i >= 0; i--)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        checkedListBox1.Items.Remove(checkedListBox1.Items[i]);
                    }
                }
            }
        }
    }
}
