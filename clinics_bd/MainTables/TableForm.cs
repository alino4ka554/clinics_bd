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

namespace clinics_bd
{
    public partial class TableForm : Form
    {
        public List<string> queries = new List<string>();
        public DB db;
        string menuName;
        public DataTable dataCities, dataStreets, dataIndicators,
            dataPatients, dataDoctors, dataSisklists, dataPatientsCard,
            dataMedicaments, dataDiagnoses, dataResults, dataProcedures,
            dataAnalyzes, dataSpecialities, dataBenefits;

        public TableForm(string menuName, List<string> queryList, DB db)
        {
            InitializeComponent();
            this.menuName = menuName;
            this.Text = menuName;
            queries = queryList;
            this.db = db;

            if (!db.grants[menuName]["writing"])
                toolStripButton1.Visible = false;
            if (!db.grants[menuName]["editing"])
                toolStripButton2.Visible = false;
            if (!db.grants[menuName]["deleting"])
                toolStripButton3.Visible = false;

            Theme.UpdateForm(this);

            GetDataForTables();
        }

        public void GetDataForTables()
        {
            CitiesForm cities = new CitiesForm("Города", db);
            this.dataCities = db.GetData(cities.queryList[0]);
            StreetsForm streets = new StreetsForm("Улицы", db);
            this.dataStreets = db.GetData(streets.queryList[0]);

            IndicatorsForm indicators = new IndicatorsForm("Показатели анализов", db);
            this.dataIndicators = db.GetData(indicators.queryList[0]);

            PatientsForm patients = new PatientsForm("Пациенты", db);
            this.dataPatients = db.GetData(patients.queryList[6]);
            DoctorsForm doctors = new DoctorsForm("Список врачей", db);
            this.dataDoctors = db.GetData(doctors.queryList[6]);
            SicklistsForm sicklists = new SicklistsForm("Больничные листы", db);
            this.dataSisklists = db.GetData(sicklists.queryList[6]);

            PatientsCardForm patientsCard = new PatientsCardForm("", db);
            this.dataPatientsCard = db.GetData(patientsCard.queryList[6]);
            MedicamentsForm medicaments = new MedicamentsForm("", db);
            this.dataMedicaments = db.GetData(medicaments.queryList[0]);
            DiagnosesForm diagnoses = new DiagnosesForm("", db);
            this.dataDiagnoses = db.GetData(diagnoses.queryList[0]);
            ResultsForm results = new ResultsForm("", db);
            this.dataResults = db.GetData(results.queryList[6]);
            ProceduresForm procedures = new ProceduresForm("", db);
            this.dataProcedures = db.GetData(procedures.queryList[0]);
            AnalyzesForm analyzes = new AnalyzesForm("", db);
            this.dataAnalyzes = db.GetData(analyzes.queryList[0]);
            SpecialitiesForm specialities = new SpecialitiesForm("", db);
            this.dataSpecialities = db.GetData(specialities.queryList[0]);
            BenefitsForm benefits = new BenefitsForm("", db);
            this.dataBenefits = db.GetData(benefits.queryList[0]);
        }

        public void SetTable()
        {
            string query = queries[0];

            DataTable table = db.GetData(query);

            dataGridView1.DataSource = table;
            dataGridView1.Columns["id"].Visible = false;
            this.Show();
        }
        public int GetId()
        {
            int id = Convert.ToInt32(dataGridView1.SelectedCells[0].OwningRow.Cells["id"].Value);

            return id;
        }

        public void InsertUpdateEntity(Function function, InsertUpdateFormBase form)
        {
            switch (function)
            {
                case Function.insert:
                    form.ShowDialog();
                    break;
                case Function.update:
                    if (dataGridView1.SelectedRows.Count == 1)
                    {
                        List<object> data = new List<object>();
                        data = db.ExecuteQueryList(queries[5], GetId());
                        form.SetData(data);
                        form.ShowDialog();
                    }
                    else MessageBox.Show("Выберите строку для изменения");
                    break;
            }
        }

        public Dictionary<string, object> CallingTheForm(Function function)
        {
            switch (menuName)
            {
                case "Список врачей":
                    InsertUpdateFormBase formDoctors = new InsertUpdateDoctorForm();
                    InsertUpdateEntity(function, formDoctors);
                    if (formDoctors.parameters.Count == 9)
                    {
                        List<object> data = formDoctors.parameters;
                        Dictionary<string, object> parametres = new Dictionary<string, object>
                            {
                            { "@surname", data[0] },
                            { "@name", data[1] },
                            { "@middlename", data[2] },
                            { "@birth_year", data[3] },
                            { "@education", data[4] },
                            { "@end_year", data[5] },
                            { "@experience", data[6] },
                            { "@telephone", data[7] },
                            { "@photo", data[8] }
                    };
                        return parametres;
                    }
                    else return null;
                    break;
                case "Список пациентов":
                    InsertUpdateFormBase formPatients = new InsertUpdatePatientForm(dataCities, dataStreets);
                    InsertUpdateEntity(function, formPatients);
                    if (formPatients.parameters.Count == 10)
                    {
                        List<object> data = formPatients.parameters;
                        Dictionary<string, object> parametres = new Dictionary<string, object>
                            {
                            { "@surname", data[0] },
                            { "@name", data[1] },
                            { "@middlename", data[2] },
                            { "@gender", data[3] },
                            { "@birth_year", data[4] },
                            { "@policy_number", data[5] },
                            { "@is_pensioner", data[6] },
                            { "@house_number", data[7] },
                            { "@id_city", data[8] },
                            { "@id_street", data[9] }
                    };
                        return parametres;
                    }
                    else return null;
                    break;
                case "Больничные листы":
                    InsertUpdateFormBase formSicklist = new InsertUpdateSicklistForm();
                    InsertUpdateEntity(function, formSicklist);
                    if (formSicklist.parameters.Count == 2)
                    {
                        List<object> data = formSicklist.parameters;
                        Dictionary<string, object> parametres = new Dictionary<string, object>
                            {
                            { "@start_data", data[0] },
                            { "@end_data", data[1] }
                    };
                        return parametres;
                    }
                    else return null;
                    break;
                case "Результаты анализов":
                    InsertUpdateFormBase formResults = new InsertUpdateResultForm(dataIndicators);
                    InsertUpdateEntity(function, formResults);
                    if (formResults.parameters.Count == 2)
                    {
                        List<object> data = formResults.parameters;
                        Dictionary<string, object> parametres = new Dictionary<string, object>
                            {
                            { "@value", data[0] },
                            { "@id_analysis_indicators", data[1] }
                    };
                        return parametres;
                    }
                    else return null;
                    break;
                case "Список карточек":
                    InsertUpdateFormBase formPatientsCard = new InsertUpdatePatientCardForm(dataPatients, dataDoctors, dataSisklists);
                    InsertUpdateEntity(function, formPatientsCard);
                    if (formPatientsCard.parameters.Count == 4)
                    {
                        List<object> data = formPatientsCard.parameters;
                        Dictionary<string, object> parametres = new Dictionary<string, object>
                            {
                             { "@number", data[0] },
                            { "@id_patient", data[1] },
                            { "@id_doctor", data[2] },
                            { "@id_sick_list", data[3] }
                    };
                        return parametres;
                    }
                    else return null;
                    break;
                case "Расписание врачей":
                    InsertUpdateFormBase formTiming = new InsertUpdateTimingForm(dataDoctors);
                    InsertUpdateEntity(function, formTiming);
                    if (formTiming.parameters.Count == 5)
                    {
                        List<object> data = formTiming.parameters;
                        Dictionary<string, object> parametres = new Dictionary<string, object>
                            {
                            { "@id_doctor", data[0] },
                            { "@weekday", data[1] }, 
                            { "@start_at", data[2] }, 
                            { "@end_at", data[3] },
                            { "@cabinet", data[4] }
                    };
                        return parametres;
                    }
                    else return null;
                    break;
                default:
                    return CallingTheFormForIntermediateTables(function);
            }
        }


        public Dictionary<string, object> CallingTheFormForIntermediateTables(Function function)
        {
            string nameFirstAttribute = dataGridView1.SelectedCells[0].OwningRow.Cells[1].OwningColumn.HeaderText;
            string nameSecondAttribute = dataGridView1.SelectedCells[0].OwningRow.Cells[2].OwningColumn.HeaderText;
            bool thirdAttribute = false;
            string nameThirdAttribute = "";
            DataTable firstData = dataPatientsCard;
            DataTable secondData = null;
            switch (menuName)
            {
                case "Выписанные лекарства":
                    secondData = dataMedicaments;
                    break;
                case "Назначенные процедуры":
                    thirdAttribute = true;
                    secondData = dataProcedures;
                    nameThirdAttribute = dataGridView1.SelectedCells[0].OwningRow.Cells[3].OwningColumn.HeaderText;
                    break;
                case "Поставленные диагнозы":
                    secondData = dataDiagnoses;
                    break;
                case "Полученные результаты анализов":
                    secondData = dataResults;
                    break;
                case "Назначенные анализы":
                    secondData = dataAnalyzes;
                    break;
                case "Показатели назначенных анализов":
                    firstData = dataAnalyzes;
                    secondData = dataIndicators;
                    break;
                case "Специализации врачей":
                    firstData = dataDoctors;
                    secondData = dataSpecialities;
                    break;
                case "Перечень льгот пациентов":
                    firstData = dataPatients;
                    secondData = dataBenefits;
                    break;
            }
            InsertUpdateFormBase form = new InsertUpdateIntermediateTablesForm(nameFirstAttribute, nameSecondAttribute, firstData, secondData, thirdAttribute, nameThirdAttribute);
            InsertUpdateEntity(function, form);
            if (form.parameters.Count == 2)
            {
                List<object> data = form.parameters;
                Dictionary<string, object> parametres = new Dictionary<string, object>
                            {
                            { "@id_patient_card", data[0] },
                            { "@param", data[1] }
                    };
                return parametres;
            }
            else if (form.parameters.Count == 3)
            {
                List<object> data = form.parameters;
                Dictionary<string, object> parametres = new Dictionary<string, object>
                            {
                            { "@id_patient_card", data[0] },
                            { "@param", data[1] },
                            { "@number", data[2] },
                    };
                return parametres;
            }
            else return null;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, object> parametres = CallingTheForm(Function.insert);
                if (parametres != null)
                {
                    string query = queries[1];

                    db.ExecuteQuery(query, parametres);
                    MessageBox.Show("Данные успешно добавлены");
                }
                SetTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления данных для вкладки '{this.Text}': {ex.Message}");
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, object> parametres = CallingTheForm(Function.update);
                if (parametres != null)
                {
                    string query = queries[2];
                    int id = GetId();
                    parametres.Add("@id", id);

                    db.ExecuteQuery(query, parametres);
                    MessageBox.Show("Данные успешно изменены");
                }
                SetTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка изменения данных для вкладки '{this.Text}': {ex.Message}");
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    DialogResult result = MessageBox.Show(
                            $"Вы уверены, что хотите удалить запись из таблицы '{this.Text}'?",
                            "Удаление строки",
                            MessageBoxButtons.YesNo
                            );
                    Dictionary<string, object> parametres = new Dictionary<string, object>();

                    if (result == DialogResult.Yes)
                    {
                        string query = queries[3];
                        int id = GetId();

                        parametres.Add("@id", id);

                        db.ExecuteQuery(query, parametres);

                        SetTable();
                    }
                }
                else MessageBox.Show("Выберите строку для удаления");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления данных для вкладки '{this.Text}': {ex.Message}");
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    string searchName = textBox1.Text;
                    string query = queries[4];

                    DataTable table = db.SearchData(query, searchName);

                    dataGridView1.DataSource = table;
                    dataGridView1.Columns["id"].Visible = false;
                }
                else
                {
                    SetTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска данных для вкладки '{this.Text}': {ex.Message}");
            }
        }
    }
}
