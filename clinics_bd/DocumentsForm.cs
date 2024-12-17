using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace clinics_bd
{
    public partial class DocumentsForm : Form, IHandler
    {
        public DB db;
        public DocumentsForm(string menuName, DB db)
        {
            this.db = db;
            InitializeComponent();
            Text = menuName;
            Theme.UpdateForm(this);
        }

        public void ApplyThemeToControls(Control control, Color color)
        {
            switch (color)
            {
                case Color.White:
                    control.BackColor = System.Drawing.SystemColors.Window;
                    break;
                case Color.Pink:
                    control.BackColor = System.Drawing.Color.MistyRose; 
                    break;
            }

            foreach (Control childControl in control.Controls)
            {
                ApplyThemeToControls(childControl, color);
            }
        }


        public void LoadData()
        {
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT CONCAT_WS(' ', patient.surname, patient.name, patient.middlename) as 'Пациент', " +
                    "CONCAT_WS(' - ', start_data, end_data) as 'Больничный лист' " +
                    "FROM patient_card " +
                    "join patient on id_patient = patient.id join sick_list on id_sick_list = sick_list.id " +
                    "WHERE patient_card.id = ";
            richTextBox1.Text = query;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.GetData(richTextBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT CONCAT_WS(' ', patient.surname, patient.name, patient.middlename) as 'Пациент', " +
                    "diagnose.name as 'Поставленный диагноз' " +
                    "FROM patient_card " +
                    "join patient on id_patient = patient.id join patient_diagnose on id_patient_card = patient_card.id " +
                    "join diagnose on id_diagnose = diagnose.id " +
                    "WHERE patient_card.id = ";
            richTextBox1.Text = query;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "SELECT CONCAT_WS(' ', patient.surname, patient.name, patient.middlename) as 'Пациент', " +
                    "analysis.name as 'Название анализа' " +
                    "FROM patient_card " +
                    "join patient on id_patient = patient.id join prescribed_analysis on id_patient_card = patient_card.id " +
                    "join analysis on id_analysis = analysis.id " +
                    "WHERE patient_card.id = ";
            richTextBox1.Text = query;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string query = "SELECT CONCAT_WS(' ', patient.surname, patient.name, patient.middlename) as 'Пациент', " +
                    "medicament.name as 'Название лекарства', " +
                    "CONCAT_WS(' ', doctor.surname, doctor.name, doctor.middlename) as 'Лечащий врач' " +
                    "FROM patient_card " +
                    "join patient on id_patient = patient.id join doctor on id_doctor = doctor.id " +
                    "join prescribed_medicaments on id_patient_card = patient_card.id " +
                    "join medicament on id_medicament = medicament.id " +
                    "WHERE patient_card.id = ";
            richTextBox1.Text = query;
        }

        public void ExportToExcel(DataTable table, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(table, "Документ");

                worksheet.Columns().AdjustToContents();

                workbook.SaveAs(filePath);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = richTextBox1.Text;

            if (string.IsNullOrWhiteSpace(query))
            {
                MessageBox.Show("Пожалуйста, заполните запрос перед экспортом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string folderPath = Path.Combine(Directory.GetCurrentDirectory().Replace("\\bin\\Debug", ""), "Properties", "Documents");
                Directory.CreateDirectory(folderPath); 
                string filePath = Path.Combine(folderPath, $"result_{timestamp}.xlsx");
                if (db.GetData(query) != null)
                {
                    ExportToExcel(db.GetData(query), filePath);

                    MessageBox.Show("Файл успешно сохранён", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при экспорте:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
