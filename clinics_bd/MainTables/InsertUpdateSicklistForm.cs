using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace clinics_bd
{
    public partial class InsertUpdateSicklistForm : InsertUpdateFormBase
    {
        public InsertUpdateSicklistForm() : base()
        {
            InitializeComponent();
            this.Text = "Добавление больничного листа";
            button1.Text = "Добавить";

            Theme.UpdateForm(this);
        }

        public override void SetData(List<object> list)
        {
            this.Text = "Изменение больничного листа";
            button1.Text = "Изменить";

            dateTimePicker1.Value = Convert.ToDateTime(list[1]);
            dateTimePicker2.Value = Convert.ToDateTime(list[2]);
        }

        public override void CollectParameters()
        {
            parameters.Add(dateTimePicker1.Value.Date);
            parameters.Add(dateTimePicker2.Value.Date);
        }

        public override bool Validation()
        {
            bool check = false;

            if (dateTimePicker1.Value >= dateTimePicker2.Value)
                MessageBox.Show("Дата начала не может быть позже даты окончания!");
            else if (dateTimePicker1.Value.Year < 1925 || dateTimePicker1.Value > DateTime.Now)
                MessageBox.Show("Дата начала не может быть раньше 1925 года и позже нынешней даты!");
            else if (dateTimePicker2.Value.Year < 1925)
                MessageBox.Show("Дата окончания не может быть раньше 1925 года!");
            else check = true;

            return check;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                CollectParameters();
                this.Close();
            }
        }
    }
}
