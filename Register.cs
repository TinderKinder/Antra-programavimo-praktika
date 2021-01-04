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
    public partial class Register : Form
    {
        public string Name;
        public string Surname;
        public DateTime BirthDate;
        public string birthdate;
        public string Username;
        public string Password;
        public string[] Users;
        public Register()
        {
            InitializeComponent();
            label8.Hide();
            label6.Hide();
            label7.Hide();
            label9.Hide();
            label10.Hide();
        }

        private void enterName_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void enterSurname_TextChanged(object sender, EventArgs e)
        {

        }

        private void selectBirthDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void enterUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void enterPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegisterAccount_Click(object sender, EventArgs e)
        {
            Name = enterName.Text;
            Surname = enterSurname.Text;
            BirthDate = selectBirthDate.Value.Date;
            birthdate = BirthDate.ToString("yyyy/MM/dd");
            Username = enterUsername.Text;
            Password = enterPassword.Text;
            if(Username != "admin")
            {
                label9.Hide();
                label10.Hide();
                if ((DateTime.Today - BirthDate).TotalDays / 365 >= 14)
                {
                    if (Name == null || string.IsNullOrWhiteSpace(Name) || string.IsNullOrEmpty(Name) ||
                        Surname == null || string.IsNullOrWhiteSpace(Surname) || string.IsNullOrEmpty(Surname) ||
                        Username == null || string.IsNullOrWhiteSpace(Username) || string.IsNullOrEmpty(Username) ||
                        Password == null || string.IsNullOrWhiteSpace(Password) || string.IsNullOrEmpty(Password))
                    {
                        label6.Hide();
                        label8.Show();
                        label10.Hide();
                    }
                    else
                    {
                        SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
                        dbConnection.Open();

                        SQLiteCommand checkIfExists = new SQLiteCommand(dbConnection);
                        checkIfExists.CommandText = "SELECT count(*) FROM user WHERE username='"+Username+"'";
                        int count = Convert.ToInt32(checkIfExists.ExecuteScalar());

                        if (count != 0){
                            label8.Hide();
                            label6.Hide();
                            label7.Hide();
                            label9.Hide();
                            label10.Show();
                        }
                        else
                        {
                            string insertSql = "insert into user values ('" + Name + "', '" + Surname + "', '" + birthdate + "', '" + Username + "', '" + Password + "', '')";
                            SQLiteCommand insertCommand = new SQLiteCommand(insertSql, dbConnection);
                            insertCommand.ExecuteNonQuery();

                            label6.Hide();
                            label8.Hide();
                            label10.Hide();
                            label7.Show();
                        }
                        dbConnection.Close();
                    }
                }
                else
                {
                    label8.Hide();
                    label6.Show();
                    label10.Hide();
                }
            }
            else
            {
                label10.Hide();
                label9.Show();
            }
            
        }
        private void goBacktoAccount_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
