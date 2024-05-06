using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminSysWF
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Database.Login(txb_Username.Text, txb_Password.Text))
            {
                int userID = Database.GetIdByUsername(txb_Username.Text);
                this.Hide();
                var mainPanel = new MainPanel(userID, txb_Username.Text);
                mainPanel.Closed += (s, args) => this.Close();
                mainPanel.Show();
            }
            else
            {
                MessageBox.Show("Utilizador e/ou a palavra-passe estão incorretos.");
            }
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            var Register = new Register();
            Register.Closed += (s, args) => this.Close();
            Register.Show();
        }
    }
}
