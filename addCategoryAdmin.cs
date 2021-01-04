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
    public partial class addCategoryAdmin : Form
    {
        public string username;
        public bool anonymous;
        public addCategoryAdmin(bool Anonymous, string Username)
        {
            InitializeComponent();
            anonymous = Anonymous;
            username = Username;
            label2.Hide();
            label3.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                label2.Hide();
                SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
                dbConnection.Open();

                string sql = "select * from category where name='" + textBox1.Text+"'";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();


                if(reader.Read())
                {
                    label3.Show();
                }
                else
                {
                    string insertSql = "insert into category values ('" + textBox1.Text + "')";
                    SQLiteCommand insertCommand = new SQLiteCommand(insertSql, dbConnection);
                    insertCommand.ExecuteNonQuery();

                    dbConnection.Close();
                    new Shop(anonymous, username).Show();
                    this.Close();
                }

                
            }
            else
            {
                label2.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            new Shop(anonymous, username).Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
