using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace clinics_bd
{
    public partial class Menu : Form
    {
        public Dictionary<string, Type> functionsForMenu;
        public DB db;
        public Color color;

        public Menu(DB db)
        {
            InitializeComponent();
            this.db = db;
            LoadHandlersDictionary();
            BuildingMenu();
            Theme.UpdateForm(this);
        }

        private void LoadHandlersDictionary()
        {
            functionsForMenu = db.LoadHandlersDictionary();
        }

        public void BuildingMenu()
        {
            db.GetMenuItem(menuStrip1);

            foreach (ToolStripMenuItem menuItem in menuStrip1.Items)
            {
                AddClickEventToMenuItems(menuItem);
            }
        }

        private void AddClickEventToMenuItems(ToolStripMenuItem menuItem)
        {
            menuItem.Click += TabControl1_SelectedIndexChanged;

            foreach (ToolStripMenuItem submenuItem in menuItem.DropDownItems)
            {
                AddClickEventToMenuItems(submenuItem);
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenu = sender as ToolStripMenuItem;
            if (toolStripMenu != null)
            {
                string menuName = toolStripMenu.Text;
                if (functionsForMenu.ContainsKey(menuName))
                {
                    Type handlerType = functionsForMenu[menuName];
                    var handler = (IHandler)Activator.CreateInstance(handlerType, menuName, db);
                    handler.LoadData();
                }
                else if (menuName == "Выход")
                    this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoctorsForm doctors = new DoctorsForm("Список врачей", db);
            DataTable dataDoctors = db.GetData(doctors.queryList[6]);

            InsertUpdateTimingForm timing = new InsertUpdateTimingForm(dataDoctors);
            timing.ShowDialog();
            if (timing.parameters.Count == 5)
            {
                List<object> data = timing.parameters;
                Dictionary<string, object> parametres = new Dictionary<string, object>
                            {
                            { "@id_doctor", data[0] },
                            { "@weekday", data[1] },
                            { "@start_at", data[2] },
                            { "@end_at", data[3] },
                            { "@cabinet", data[4] }
                    };
                TimingForm timingForm = new TimingForm("", db);
                string query = timingForm.queryList[1];

                db.ExecuteQuery(query, parametres);
                MessageBox.Show("Вы записались!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DocumentsForm documentsForm = new DocumentsForm("Документы", db);
            documentsForm.ShowDialog();
        }
    }

    public interface IHandler
    {
        void LoadData();
    }
}
