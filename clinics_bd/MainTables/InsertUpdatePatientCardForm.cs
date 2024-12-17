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
    public partial class InsertUpdatePatientCardForm : InsertUpdateFormBase
    {
        private DataTable patients, doctors, sicklists;
        public InsertUpdatePatientCardForm(DataTable patients, DataTable doctors, DataTable sicklists) : base()
        {
            InitializeComponent();

            this.Text = "Добавление электронной карточки";
            button1.Text = "Добавить";

            this.patients = patients;
            this.doctors = doctors;
            this.sicklists = sicklists;

            Random random = new Random();
            numericUpDown1.Value = random.Next(0, 100000);
            comboBox1.DataSource = patients;
            comboBox1.DisplayMember = "название";
            comboBox1.ValueMember = "id";

            comboBox2.DataSource = doctors;
            comboBox2.DisplayMember = "название"; 
            comboBox2.ValueMember = "id";     

            comboBox3.DataSource = sicklists;
            comboBox3.DisplayMember = "даты"; 
            comboBox3.ValueMember = "id";

            Theme.UpdateForm(this);
        }

        public override void CollectParameters()
        {
            parameters.Add(Convert.ToInt32(numericUpDown1.Value));
            parameters.Add(Convert.ToInt32(comboBox1.SelectedValue));
            parameters.Add(Convert.ToInt32(comboBox2.SelectedValue));
            parameters.Add(Convert.ToInt32(comboBox3.SelectedValue));
        }

        public override void SetData(List<object> list)
        {
            this.Text = "Изменение электронной карточки";
            button1.Text = "Изменить";

            numericUpDown1.Value = Convert.ToDecimal(list[1]);
            var patientRow = patients.AsEnumerable().FirstOrDefault(row => row.Field<string>("название") == list[2]?.ToString());
            if (patientRow != null && !patientRow.IsNull("id"))
            {
                comboBox1.SelectedValue = Convert.ToInt64(patientRow["id"]);
            }
            var doctorRow = doctors.AsEnumerable().FirstOrDefault(row => row.Field<string>("название") == list[3]?.ToString());
            if (doctorRow != null && !doctorRow.IsNull("id"))
            {
                comboBox2.SelectedValue = Convert.ToInt64(doctorRow["id"]);
            }
            var sicklistRow = sicklists.AsEnumerable().FirstOrDefault(row => row.Field<string>("даты") == list[4]?.ToString());
            if (sicklistRow != null && !sicklistRow.IsNull("id"))
            {
                comboBox3.SelectedValue = Convert.ToInt64(sicklistRow["id"]);
            }
        }

        public override bool Validation()
        {
            bool check = true;

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
