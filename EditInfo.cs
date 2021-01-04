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
    public partial class EditInfo : Form
    {
        public string name;
        public string surname;
        public DateTime birthdate;
        public string username;
        public string password;
        public string imagelocation;
        public string tempUsername;
        public string[] users;
        public string date;
        public string tempUsername2;
        public EditInfo(string Username)
        {
            InitializeComponent();
            label6.Hide();
            label7.Hide();
            label8.Hide();

                
                username = Username;
                tempUsername = Username;

            SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
            dbConnection.Open();

            string sql = "select * from user where username='" + username + "'";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                name = reader[0].ToString();
                surname = reader[1].ToString();
                date = reader[2].ToString();
            }

            if(username == "admin")
            {
                textBox4.Visible = false;
                label4.Visible = false;
            }

            birthdate = DateTime.ParseExact(date, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);

            textBox1.Text = name;
            textBox2.Text = surname;
            dateTimePicker1.Value = birthdate;
            textBox4.Text = username;
                
        }
                

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            
            
                birthdate = dateTimePicker1.Value.Date;
                if ((DateTime.Today - birthdate).TotalDays / 365 < 14)
                {
                    label6.Show();
                    label7.Hide();
                    label8.Hide();
                }
                else
                {
                    if (name == null || string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name) ||
                        surname == null || string.IsNullOrWhiteSpace(surname) || string.IsNullOrEmpty(surname) ||
                        username == null || string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(username))
                    {
                        label6.Hide();
                        label7.Hide();
                        label8.Show();
                    }
                    else
                    {
                        SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
                        dbConnection.Open();

                        if(username != "admin")
                        {
                            tempUsername2 = "";
                            string getUser = "select username from user where username='" + textBox4.Text + "'";
                            SQLiteCommand commandGetAccount = new SQLiteCommand(getUser, dbConnection);
                            SQLiteDataReader readerAccount = commandGetAccount.ExecuteReader();
                            while (readerAccount.Read())
                            {
                                tempUsername2 = readerAccount[0].ToString();
                            }

                            if (tempUsername2 == "")
                            {
                                name = textBox1.Text;
                                surname = textBox2.Text;
                                birthdate = dateTimePicker1.Value;
                                date = birthdate.ToString("yyyy/MM/dd");
                                username = textBox4.Text;

                                string updateSql = "update user set name='" + name + "', surname='" + surname + "', birthdate='" + date + "', username='" + username + "' where username='" + tempUsername + "'";
                                SQLiteCommand updateCommand = new SQLiteCommand(updateSql, dbConnection);
                                updateCommand.ExecuteNonQuery();
                                this.Close();
                            }
                            else
                            {
                                label6.Hide();
                                label7.Show();
                                label8.Hide();
                            }
                        }
                        else
                        {
                            name = textBox1.Text;
                            surname = textBox2.Text;
                            birthdate = dateTimePicker1.Value;
                            date = birthdate.ToString("yyyy/MM/dd");
                            username = textBox4.Text;

                            string updateSql = "update user set name='" + name + "', surname='" + surname + "', birthdate='" + date + "', username='" + username + "' where username='" + tempUsername + "'";
                            SQLiteCommand updateCommand = new SQLiteCommand(updateSql, dbConnection);
                            updateCommand.ExecuteNonQuery();
                            this.Close();
                        }
                        
                    }
                }
            
            
            
        }
    }
}