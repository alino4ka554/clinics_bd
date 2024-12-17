using DocumentFormat.OpenXml.Office2010.Excel;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
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
    public enum Function
    {
        insert, update, delete
    }

    public partial class GuideForm : Form
    {
        public List<string> guides = new List<string>();
        public DB db;
        public GuideForm(string menuName, List<string> queryList, DB db)
        {
            InitializeComponent();
            this.Text = menuName;
            guides = queryList;
            this.db = db;

            if (!db.grants[menuName]["writing"])
                toolStripButton1.Visible = false;
            if (!db.grants[menuName]["editing"])
                toolStripButton2.Visible = false;
            if (!db.grants[menuName]["deleting"])
                toolStripButton3.Visible = false;

            Theme.UpdateForm(this);
        }

        public void SetTable()
        {
            string query = guides[0]; 

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

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count == 1)
                {
                    int id = GetId();

                    string fieldForUpdate = dataGridView1.SelectedCells[0].Value.ToString();
                    var nameRow = dataGridView1.SelectedCells[0].OwningRow.Cells[1].OwningColumn.Name;

                    InsertUpdateGuideForm updateForm = new InsertUpdateGuideForm(Function.update, nameRow.ToLower(), fieldForUpdate, db);
                    updateForm.ShowDialog();

                    string updateField = updateForm.updateName;

                    if (updateField != null)
                    {
                        string query = guides[2];

                        Dictionary<string, object> parametres = new Dictionary<string, object>
                    {
                        {"@id", id },
                        {"@newName", updateField }
                    };
                        db.ExecuteQuery(query, parametres);

                        SetTable();
                    }
                }
                else MessageBox.Show("Выберите одно поле для редактирования");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка изменения данных для вкладки '{this.Text}': {ex.Message}");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var nameRow = dataGridView1.SelectedCells[0].OwningRow.Cells[1].OwningColumn.Name;

                InsertUpdateGuideForm updateForm = new InsertUpdateGuideForm(Function.insert, nameRow.ToLower(), nameRow, db);
                updateForm.ShowDialog();

                string newField = updateForm.updateName;

                if (newField != null)
                {
                    string query = guides[1];

                    Dictionary<string, object> parametres = new Dictionary<string, object>
                {
                    {"@newName", newField }
                };
                    db.ExecuteQuery(query, parametres);

                    SetTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления данных для вкладки '{this.Text}': {ex.Message}");
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    int id = GetId();

                    string fieldName = dataGridView1.SelectedCells[1].Value.ToString();

                    DialogResult result = MessageBox.Show(
                        $"Вы уверены, что хотите удалить {fieldName} из таблицы '{this.Text}'?",
                        "Удаление строки",
                        MessageBoxButtons.YesNo
                        );

                    if (result == DialogResult.Yes)
                    {
                        string query = guides[3];

                        Dictionary<string, object> parametres = new Dictionary<string, object>
                        {
                            {"@id", id }
                        };
                        db.ExecuteQuery(query, parametres);

                        SetTable();
                    }
                }
                else MessageBox.Show("Выберите строку для удаления");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления данных для вкладки '{this.Text}': {ex.Message}");
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    string searchName = textBox1.Text;

                    string query = guides[4];

                    DataTable table = db.SearchData(query, searchName);

                    dataGridView1.DataSource = table;
                    dataGridView1.Columns["id"].Visible = false;
                }
                else
                {
                    SetTable();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка добавления данных для вкладки '{this.Text}': {ex.Message}");
            }
        }
    }
}
