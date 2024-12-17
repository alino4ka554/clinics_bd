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
    public partial class InsertUpdateGuideForm : Form
    {
        public string updateName; 
        public InsertUpdateGuideForm(Function function, string updateString, string field, DB db)
        {
            InitializeComponent();
            label1.Text = "Введите " + updateString;
            textBox1.Text = field;
            Theme.UpdateForm(this);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                updateName = textBox1.Text;
                this.Close();
            }
            else MessageBox.Show("Заполните данные!");
        }
    }
}
