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
    public partial class Account : Form
    {
        public Account()
        {
            InitializeComponent();

        }

        private void Register_Click(object sender, EventArgs e)
        {
            new Register().Show();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            new Login().Show();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckShop_Click(object sender, EventArgs e)
        {
            string username = "";
            bool anonymous = true;
            new Shop(anonymous, username).Show();
        }
    }
}
