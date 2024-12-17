using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Vml;
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
    public partial class InsertUpdatePatientForm : InsertUpdateFormBase
    {
        DataTable cities, streets;
        public InsertUpdatePatientForm(DataTable cities, DataTable streets) : base()
        {
            InitializeComponent();
            this.Text = "Добавление пациента";
            button1.Text = "Добавить";

            this.cities = cities;
            this.streets = streets;

            comboBox2.DataSource = cities;
            comboBox2.DisplayMember = "название"; // Название города, отображаемое пользователю
            comboBox2.ValueMember = "id";     // ID города, который будет сохранен

            comboBox3.DataSource = streets;
            comboBox3.DisplayMember = "название"; // Название улицы
            comboBox3.ValueMember = "id";     // ID улицы

            Theme.UpdateForm(this);
            SetDefaultValues();
        }
        public void SetDefaultValues()
        {
            textBox1.Text = "Петров";
            textBox2.Text = "Петр";
            textBox3.Text = "Петрович";
            comboBox1.Text = "мужской";
            Random random = new Random();
            textBox4.Text = random.Next(0, 1000000).ToString();
            textBox5.Text = "52";
        }

        public override void CollectParameters()
        {
            parameters.Add(textBox1.Text);
            parameters.Add(textBox2.Text);
            parameters.Add(textBox3.Text);
            parameters.Add(comboBox1.Text);
            parameters.Add(dateTimePicker1.Value.Year);
            parameters.Add(textBox4.Text);
            parameters.Add(checkBox1.Checked);
            parameters.Add(textBox5.Text);
            parameters.Add(Convert.ToInt32(comboBox2.SelectedValue));
            parameters.Add(Convert.ToInt32(comboBox3.SelectedValue));

        }

        public override void SetData(List<object> list)
        {
            this.Text = "Изменение пациента";
            button1.Text = "Изменить";

            textBox1.Text = list[1].ToString();
            textBox2.Text = list[2].ToString();
            textBox3.Text = list[3].ToString();
            comboBox1.Text = list[4].ToString();
            dateTimePicker1.Value = new DateTime(Convert.ToInt32(list[5]), 1, 1);
            textBox4.Text = list[6].ToString();
            checkBox1.Checked = list[7] != DBNull.Value && Convert.ToInt32(list[7]) == 1;
            textBox5.Text = list[8].ToString();
            var cityRow = cities.AsEnumerable().FirstOrDefault(row => row.Field<string>("название") == list[9]?.ToString());
            if (cityRow != null && !cityRow.IsNull("id"))
            {
                comboBox2.SelectedValue = Convert.ToInt64(cityRow["id"]);
            }
            var streetRow = streets.AsEnumerable().FirstOrDefault(row => row.Field<string>("название") == list[10]?.ToString());
            if (streetRow != null && !streetRow.IsNull("id"))
            {
                comboBox3.SelectedValue = Convert.ToInt64(streetRow["id"]);
            }
        }


        public override bool Validation()
        {
            bool check = false;

            if (textBox1.Text.Length >= 25 || textBox2.Text.Length >= 25 || textBox3.Text.Length >= 25)
                MessageBox.Show("Длина ФИО не более 25 символов!");
            else if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                MessageBox.Show("Введите ФИО!");
            else if (comboBox1.Text != "мужской" && comboBox1.Text != "женский")
                MessageBox.Show("Выберите пол!");
            else if (dateTimePicker1.Value.Year < 1925 || dateTimePicker1.Value.Year > DateTime.Now.Year)
                MessageBox.Show("Год рождения может быть только в пределах 1925 и 2024 года!");
            else if (textBox4.Text.Length >= 25)
                MessageBox.Show("Длина номера полиса не более 25 символов!");
            else if (textBox5.Text.Length >= 10)
                MessageBox.Show("Длина номера дома не более 10 символов!");
            else
                check = true;
            return check;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox5.Text == "")
                MessageBox.Show("Введите все данные!");
            else if (Validation())
            {
                CollectParameters();
                this.Close();
            }
        }
    }
}
