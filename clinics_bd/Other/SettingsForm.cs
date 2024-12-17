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
    public partial class SettingsForm : Form, IHandler
    {
        public Color color;
        public SettingsForm(string menuName, DB db)
        {
            InitializeComponent();
            this.Text = menuName;
            Theme.UpdateForm(this);
            LoadFontList();
            LoadCurrentFont();
        }
        private void LoadFontList()
        {
            var fontFamilies = FontFamily.Families;
            foreach (var fontFamily in fontFamilies)
            {
                comboBox1.Items.Add(fontFamily.Name); // Добавляем шрифты в ComboBox
            }
        }
        private void LoadCurrentFont()
        {
            comboBox1.SelectedItem = Theme.AppFont.FontFamily.Name; // Устанавливаем текущий выбранный шрифт
        }

        public void LoadData()
        {
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked)
            {
                if (radioButton1.Checked)
                    Theme.AppColor = Color.White;
                else if (radioButton2.Checked)
                    Theme.AppColor = Color.Pink;
                string selectedFontName = comboBox1.SelectedItem?.ToString() ?? "Microsoft Sans Serif";
                float fontSize = 8;
                Font newFont = new Font(selectedFontName, fontSize);
                Theme.AppFont = newFont;
                foreach (Form form in Application.OpenForms)
                {
                    Theme.UpdateForm(form);
                }
                Theme.SaveTheme();
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите тему приложения!");
            }
        }

    }
}
