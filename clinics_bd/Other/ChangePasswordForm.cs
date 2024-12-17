using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clinics_bd
{
    public partial class ChangePasswordForm : Form, IHandler
    {
        public DB db;
        public ChangePasswordForm(string menuName, DB dB)
        {
            this.db = dB;
            InitializeComponent();
            Text = menuName;
            Theme.UpdateForm(this);
        }

        public void LoadData()
        {
            this.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nowPassword = textBox1.Text;
            string newPassword = textBox2.Text;
            string verification = textBox3.Text;
            if (db.Connecting(db.userName, nowPassword))
            {
                if (newPassword == verification)
                {
                    if (db.ChangePassword(newPassword))
                    {
                        MessageBox.Show("Пароль успешно изменен!");
                        this.Close();
                    }
                }
                else MessageBox.Show("Пароли не совпадают!");
            }
            else MessageBox.Show("Вы ввели неверный пароль!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
