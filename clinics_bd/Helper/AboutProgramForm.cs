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
    public partial class AboutProgramForm : Form, IHandler
    {
        public AboutProgramForm(string menuName, DB db)
        {
            InitializeComponent();
            this.Text = menuName;
            Theme.UpdateForm(this);
        }
        
        public void LoadData()
        {
            this.Show();
        }
    }
}
