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
    public partial class InsertUpdateResultForm : InsertUpdateFormBase
    {
        DataTable indicators;
        public InsertUpdateResultForm(DataTable indicators) : base()
        {
            InitializeComponent();
            this.Text = "Добавление результата анализа";
            button1.Text = "Добавить";

            this.indicators = indicators;

            comboBox1.DataSource = indicators;
            comboBox1.DisplayMember = "название";
            comboBox1.ValueMember = "id";

            Theme.UpdateForm(this);
        }

        

        public override void SetData(List<object> list)
        {
            this.Text = "Изменение результата анализа";
            button1.Text = "Изменить";

            var indicatorsRow = indicators.AsEnumerable().FirstOrDefault(row => row.Field<string>("название") == list[1]?.ToString());
            if (indicatorsRow != null && !indicatorsRow.IsNull("id"))
            {
                comboBox1.SelectedValue = Convert.ToInt64(indicatorsRow["id"]);
            }
            numericUpDown1.Value = Convert.ToDecimal(list[2]);
        }

        public override bool Validation()
        {
            bool check = false;

            if (numericUpDown1.Value < 0 || numericUpDown1.Value >= 10000)
                MessageBox.Show("Значение показателя не может быть меньше 0 и больше 10000");
            else
                check = true;
            return check;
        }

        public override void CollectParameters()
        {
            parameters.Add((double)numericUpDown1.Value);
            parameters.Add(Convert.ToInt32(comboBox1.SelectedValue));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                CollectParameters();
                this.Close();
            }
;        }
    }
}
