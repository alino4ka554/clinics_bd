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
    public partial class InsertUpdateDoctorForm : InsertUpdateFormBase
    {
        public InsertUpdateDoctorForm() : base() 
        {
            InitializeComponent();
            this.Text = "Добавление врача";
            button1.Text = "Добавить";
            Theme.UpdateForm(this);
            SetDefaultValues();
        }

        public void SetDefaultValues()
        {
            textBox1.Text = "Петров";
            textBox2.Text = "Петр";
            textBox3.Text = "Петрович";
            comboBox1.Text = "ординатура";
            textBox4.Text = "4 года";
            Random random = new Random();
            textBox5.Text = "+7" + random.Next(1000000000, 1999999999).ToString();
            textBox6.Text = " - ";
        }

        public override void SetData(List<object> list)
        {
            this.Text = "Изменение врача";
            button1.Text = "Изменить";
            textBox1.Text = list[1].ToString();
            textBox2.Text = list[2].ToString();
            textBox3.Text = list[3].ToString();
            dateTimePicker1.Value = new DateTime(Convert.ToInt32(list[4]), 1, 1);
            comboBox1.Text = list[5].ToString();
            dateTimePicker2.Value = new DateTime(Convert.ToInt32(list[6]), 1, 1);
            textBox4.Text = list[7].ToString();
            textBox5.Text = list[8].ToString();
            textBox6.Text = list[9].ToString();
        }

        public override bool Validation()
        {
            bool check = false;

            if (textBox1.Text.Length >= 25 || textBox2.Text.Length >= 25 || textBox3.Text.Length >= 25)
                MessageBox.Show("Длина ФИО не более 25 символов!");
            else if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                MessageBox.Show("Введите ФИО!");
            else if (dateTimePicker1.Value.Year < 1925 || dateTimePicker1.Value.Year > DateTime.Now.Year)
                MessageBox.Show("Год рождения может быть только в пределах 1925 и 2024 года!");
            else if (dateTimePicker2.Value.Year < 1925 || dateTimePicker2.Value.Year > DateTime.Now.Year)
                MessageBox.Show("Год окончания может быть только в пределах 1925 и 2024 года!");
            else if (dateTimePicker2.Value.Year - 18 < dateTimePicker1.Value.Year)
                MessageBox.Show("Год окончания не сопоставим с годом рождения");
            else if (comboBox1.Text != "колледж" && comboBox1.Text != "специалитет" && comboBox1.Text != "ординатура" && comboBox1.Text != "аспирантура")
                MessageBox.Show("Выберите корректный тип образования!");
            else if (textBox4.Text.Length >= 25)
                MessageBox.Show("Длина поля стажа работы не более 25 символов!");
            else if (textBox5.Text.Length >= 25)
                MessageBox.Show("Длина номера телефона не более 25 символов!");
            else if (textBox6.Text.Length >= 50)
                MessageBox.Show("Длина ссылки на фотографию не более 50 символов!");
            else
                check = true;
            return check;
        }

        public override void CollectParameters()
        {
            parameters.Add(textBox1.Text);
            parameters.Add(textBox2.Text);
            parameters.Add(textBox3.Text);
            parameters.Add(dateTimePicker1.Value.Year);
            parameters.Add(comboBox1.Text);
            parameters.Add(dateTimePicker2.Value.Year);
            parameters.Add(textBox4.Text);
            parameters.Add(textBox5.Text);
            parameters.Add(textBox6.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
                MessageBox.Show("Введите все данные!");
            else if (Validation())
            {
                CollectParameters();
                this.Close();
            }
        }
    }
}
