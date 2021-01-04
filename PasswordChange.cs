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
    public partial class PasswordChange : Form
    {
        public string name;
        public string surname;
        public DateTime birthdate;
        public string username;
        public string password;
        public string imagelocation;
        public string[] lines;
        public PasswordChange(string Username)
        {
            InitializeComponent();
            
            username = Username;
            
            label3.Hide();
            label4.Hide();
        }

        private void goBacktoAccount_Click(object sender, EventArgs e)
        {
            this.Close();
            new AccountLoggedIn(username).Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ChangePassword_Click(object sender, EventArgs e)
        {
            SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
            dbConnection.Open();

            string getUser = "select password from user where username='" + username + "'";
            SQLiteCommand commandGetAccount = new SQLiteCommand(getUser, dbConnection);
            SQLiteDataReader readerAccount = commandGetAccount.ExecuteReader();
            while (readerAccount.Read())
            {
                password = readerAccount[0].ToString();
            }

            if (textBox1.Text == password)
            {
                if(textBox2.Text == null || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrEmpty(textBox2.Text))
                {
                    label3.Hide();
                    label4.Show();
                }
                else
                {
                    label3.Hide();
                    label4.Hide();

                    string updateSql = "update user set password='"+textBox2.Text+"' where username='" + username + "'";
                    SQLiteCommand updateCommand = new SQLiteCommand(updateSql, dbConnection);
                    updateCommand.ExecuteNonQuery();

                    this.Close();
                }
            }
            else
            {
                label4.Hide();
                label3.Show();
            }
        }
    }
}
