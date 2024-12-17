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
    public partial class InsertUpdateIntermediateTablesForm : InsertUpdateFormBase
    {
        DataTable dataFirst, dataSecond;
        bool thirdAttribute = false;
        public InsertUpdateIntermediateTablesForm(string firstAttribute, string secondAttribute, DataTable dataFirst, DataTable dataSecond, bool thirdAttribute, string nameThird) : base()
        {
            InitializeComponent();

            this.Text = "Добавление данных";
            button1.Text = "Добавить";

            label3.Text = nameThird;
            if (!thirdAttribute)
            {
                label3.Visible = false;
                numericUpDown1.Visible = false;
            } else this.thirdAttribute = true;

            label1.Text = firstAttribute; 
            label2.Text = secondAttribute;

            this.dataFirst = dataFirst;
            this.dataSecond = dataSecond;

            comboBox1.DataSource = dataFirst;
            comboBox1.DisplayMember = "название";
            comboBox1.ValueMember = "id";

            comboBox2.DataSource = dataSecond;
            comboBox2.DisplayMember = "название";
            comboBox2.ValueMember = "id";

            Theme.UpdateForm(this);
        }

        public override void SetData(List<object> list)
        {
            this.Text = "Изменение данных";
            button1.Text = "Изменить";

            comboBox1.SelectedValue = Convert.ToInt32(list[1]);
            comboBox2.SelectedValue = Convert.ToInt32(list[2]);
            if (thirdAttribute)
                numericUpDown1.Value = Convert.ToDecimal(list[3]);
        }

        public void ThirdAttributeTrue()
        {
            label3.Visible = true;
            numericUpDown1.Visible = true;
        }

        public override bool Validation()
        {
            return true;
        }

        public override void CollectParameters()
        {
            parameters.Add(Convert.ToInt32(comboBox1.SelectedValue));
            parameters.Add(Convert.ToInt32(comboBox2.SelectedValue));
            if(thirdAttribute)
                parameters.Add(Convert.ToInt32(numericUpDown1.Value));
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
