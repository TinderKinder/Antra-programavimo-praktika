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
    public partial class Login : Form
    {
        public string Name;
        public string Surname;
        public string BirthDate;
        public string Username;
        public string Password;

        public Login()
        {
            InitializeComponent();
            label3.Hide();
            label4.Hide();
        }

        private void goBackLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginAccount_Click(object sender, EventArgs e)
        {
            SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
            dbConnection.Open();

            SQLiteCommand checkIfExists = new SQLiteCommand(dbConnection);
            checkIfExists.CommandText = "SELECT count(*) FROM user WHERE username='" + textBox1.Text + "'";
            int count = Convert.ToInt32(checkIfExists.ExecuteScalar());


            if (count != 0)
            {
                label3.Hide();
                label4.Hide();

                SQLiteCommand checkPassword = new SQLiteCommand(dbConnection);
                checkPassword.CommandText = "Select password from user where username='"+textBox1.Text+"'";
                Password = checkPassword.ExecuteScalar().ToString();

                if (textBox1.Text == "admin")
                {
                    if (textBox2.Text == Password)
                    {
                        string sql = "select * from user where username='"+ textBox1.Text+"'";
                        SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Name = reader[0].ToString();
                            Surname = reader[1].ToString();
                            BirthDate = reader[2].ToString();
                            Username = reader[3].ToString();
                            Password = reader[4].ToString();
                        }
                        

                        label3.Hide();
                        new AccountLoggedIn(textBox1.Text).Show();
                        this.Close();
                        
                    }
                    else
                    {
                        label4.Hide();
                        label3.Show();
                    }
                    
                }
                else
                {
                    if (textBox2.Text == Password)
                    {
                        Username = textBox1.Text;
                        label3.Hide();
                        this.Close();
                        new AccountLoggedIn(Username).Show();
                    }
                    else
                    {
                        label3.Show();
                        label4.Hide();
                    }
                }
            }
            else
            {
                label4.Show();
                label3.Hide();
            }
            dbConnection.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
