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
    public partial class itemView : Form
    {
        public string item;
        public bool anonymous;
        public string username;

        public itemView(string Item, bool Anonymous, string Username)
        {
            username = Username;
            anonymous = Anonymous;
            item = Item;
            

            InitializeComponent();
            label7.Hide();
            label8.Hide();
            label9.Hide();
            button3.Visible = false;
            if (username == "admin")
            {
                button3.Visible = true;
            }
            if(anonymous == true)
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                textBox1.Visible = false;
            }
            SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
            dbConnection.Open();

            string sql = "select * from item where name='"+item+"'";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                label5.Text = reader[0].ToString();
                label6.Text = reader[2].ToString();
                listBox2.Items.Add(reader[1].ToString());
                pictureBox1.ImageLocation = reader[3].ToString();
            }
            reader.Close();
            string comment = "";
            string sql2 = "select * from comment where itemName='" + item + "'";
            SQLiteCommand command2 = new SQLiteCommand(sql2, dbConnection);
            SQLiteDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                comment = reader2[1] + " ("+ reader2[2] + "): " + reader2[0];
                listBox1.Items.Add(comment);
            }
            reader2.Close();

            dbConnection.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e) // Go back
        {
            new Shop(anonymous, username).Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e) // delete comment
        {
            SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
            dbConnection.Open();

            for (int i = listBox1.Items.Count - 1; i >= 0; i--)
            {
                if (listBox1.GetSelected(i))
                {
                    string commentName = listBox1.GetItemText(listBox1.Items[i]);

                    string[] commentDelete = commentName.Split(' ');

                    listBox1.Items.Remove(listBox1.Items[i]);
                    string deleteSql = "delete from comment where comment='" + commentDelete[4] + "' and commentedBy='" + commentDelete[0] + "'";
                    SQLiteCommand deleteCommand = new SQLiteCommand(deleteSql, dbConnection);
                    deleteCommand.ExecuteNonQuery();
                }
            }
            dbConnection.Close();
        }

        private void button2_Click(object sender, EventArgs e) // add comment
        {
            if (textBox1.Text.Length != 0)
            {
                SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
                dbConnection.Open();
                string date = DateTime.Now.ToString();
                string insertSql = "insert into comment values ('"+textBox1.Text+"','"+username+"','"+date+"','"+item+"')";
                SQLiteCommand insertCommand = new SQLiteCommand(insertSql, dbConnection);
                insertCommand.ExecuteNonQuery();
                dbConnection.Close();
                new itemView(item, anonymous, username).Show();
                this.Close();
            }
            else
            {
                label8.Hide();
                label9.Hide();
                label7.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e) // save to wishlist
        {
            SQLiteConnection dbConnection = new SQLiteConnection(@"Data Source=C:\SQLite\4nd.db");
            dbConnection.Open();

            string sql = "select item from wishlist where item='" + item + "' and username='"+username+"'";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                label7.Hide();
                label9.Hide();
                label8.Show();
            }
            else
            {
                string insertSql = "insert into wishlist values ('"+item+"', '"+username+"')";
                SQLiteCommand insertCommand = new SQLiteCommand(insertSql, dbConnection);
                insertCommand.ExecuteNonQuery();
                dbConnection.Close();
                label9.Show();
                label7.Hide();
                label8.Hide();
            }
            
        }
    }
}
