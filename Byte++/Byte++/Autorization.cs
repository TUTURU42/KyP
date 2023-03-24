using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Byte__
{
    public partial class Autorization : Form
    {
        Boolean root;
        public Autorization()
        {
            InitializeComponent();
        }

        private void button_autoriz_Click(object sender, EventArgs e)
        {
            //textBox_login.Text = "admin";
            //textBox_pass.Text= "admin";
            if (textBox_login.Text == "admin" && textBox_pass.Text == "admin")
            {
                root = true;
                this.Hide();
                MainMenu mainMenu = new MainMenu(this, root);
                mainMenu.Show();
                
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.");
            }
        }

        private void textBox_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_autoriz_Click(sender, e);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            root = false;
            this.Hide();
            MainMenu mainMenu = new MainMenu(this, root);
            mainMenu.Show();
            
        }
    }
}
