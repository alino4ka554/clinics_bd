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
    public partial class InsertUpdateTimingForm : InsertUpdateFormBase
    {
        DataTable doctors;
        public InsertUpdateTimingForm(DataTable doctors) : base()
        {
            InitializeComponent();

            this.Text = "Добавление записи в расписание";
            button1.Text = "Добавить";

            this.doctors = doctors;

            comboBox1.DataSource = doctors;
            comboBox1.DisplayMember = "название";
            comboBox1.ValueMember = "id";

            Theme.UpdateForm(this);
            comboBox2.Text = "вторник";
            textBox1.Text = "101";
        }

        public override void CollectParameters()
        {
            parameters.Add(Convert.ToInt32(comboBox1.SelectedValue));
            parameters.Add(comboBox2.Text);
            parameters.Add(dateTimePicker1.Value);
            parameters.Add(dateTimePicker2.Value);
            parameters.Add(textBox1.Text);
        }

        public override void SetData(List<object> list)
        {
            this.Text = "Изменение записи в расписании";
            button1.Text = "Изменить";

            var doctorRow = doctors.AsEnumerable().FirstOrDefault(row => row.Field<string>("название") == list[1]?.ToString());
            if (doctorRow != null && !doctorRow.IsNull("id"))
            {
                comboBox1.SelectedValue = Convert.ToInt64(doctorRow["id"]);
            }
            comboBox2.Text = list[2].ToString();
            dateTimePicker1.Value = Convert.ToDateTime(list[3].ToString());
            dateTimePicker2.Value = Convert.ToDateTime(list[4].ToString());
            textBox1.Text = list[5].ToString();
        }

        public override bool Validation()
        {
            bool check = false;

            if (textBox1.Text.Length > 15)
                MessageBox.Show("Длина номера кабинета не может быть больше 15 символов!");
            else if (string.IsNullOrEmpty(textBox1.Text))
                MessageBox.Show("Введите все данные!");
            else if (comboBox2.Text != "понедельник" && comboBox2.Text != "вторник" && comboBox2.Text != "среда" && comboBox2.Text != "четверг" && comboBox2.Text != "пятница" && comboBox2.Text != "суббота" && comboBox2.Text != "воскресенье")
                MessageBox.Show("Выберите день недели!");
            else if (dateTimePicker2.Value < dateTimePicker1.Value)
                MessageBox.Show("Время конца приема не может быть раньше времени начала приема!");
            else check = true;

            return check;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                parameters.Add(Convert.ToInt32(comboBox1.SelectedValue));
                parameters.Add(comboBox2.Text);
                parameters.Add(dateTimePicker1.Value);
                parameters.Add(dateTimePicker2.Value);
                parameters.Add(textBox1.Text);

                this.Close();
            }
        }
    }
}
