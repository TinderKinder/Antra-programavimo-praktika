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
    public partial class AccountLoggedIn : Form
    {
        public string imageLocation = "";
        public string name;
        public string surname;
        public string birthdate;
        public string username;
        public string password;
        public AccountLoggedIn(string Username)
        {
            InitializeComponent();
            EditUsers.Visible = false;
            username = Username;
            if (username == "admin")
            {
                EditUsers.Visible = true;
            }

            
            
            

            SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
            dbConnection.Open();

            string sql = "select profilePhoto from user where username='" + username + "'";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
                pictureBox1.ImageLocation = reader[0].ToString();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void UploadPicture_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files (*.png)|*.png| All Files(*.*)|*.*";

                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    //pictureBox1.ImageLocation = imageLocation;

                    SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
                    dbConnection.Open();

                    string getUser = "select name from user where username='" + username + "'";
                    SQLiteCommand commandGetAccount = new SQLiteCommand(getUser, dbConnection);
                    SQLiteDataReader readerAccount = commandGetAccount.ExecuteReader();
                    while (readerAccount.Read())
                    {
                        name = readerAccount[0].ToString();
                    }
                    

                    string updateSql = "update user set profilePhoto='" + imageLocation + "' where name='" + name + "'";
                    SQLiteCommand updateCommand = new SQLiteCommand(updateSql, dbConnection);
                    updateCommand.ExecuteNonQuery();


                    string sql = "select profilePhoto from user where name='" + name + "'";
                    SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    imageLocation = "";
                    while(reader.Read())
                        imageLocation = reader[0].ToString();
                    pictureBox1.ImageLocation = imageLocation;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

            
        }

        private void InfoEdit_Click(object sender, EventArgs e)
        {            
            new EditInfo(username).Show();
            this.Close();
        }

        private void NewPassword_Click(object sender, EventArgs e)
        {
            new PasswordChange(username).Show();
            this.Close();
        }

        private void EditUsers_Click(object sender, EventArgs e)
        {
            new UserList(username).Show();
            this.Close();
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewShop_Click(object sender, EventArgs e)
        {
            bool anonymous = false;
            new Shop(anonymous, username).Show();
            this.Close();
        }

        private void ViewWishlist_Click(object sender, EventArgs e)
        {
            new wishlist(username).Show();
            this.Close();
        }
    }
}
