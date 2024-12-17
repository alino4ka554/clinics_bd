using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace clinics_bd
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
            Theme.LoadTheme();
            Theme.UpdateForm(this);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            string loginUser = login.Text;
            string passwordUser = password.Text;

            DB db = new DB();

            if (db.Connecting(loginUser, passwordUser))
            {
                db.userName = loginUser;
                db.GetUserPermissions(loginUser);
                MessageBox.Show("Добро пожаловать", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Menu menu = new Menu(db);
                menu.ShowDialog();
            }
            else MessageBox.Show("Неправильно введен логин или пароль");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
