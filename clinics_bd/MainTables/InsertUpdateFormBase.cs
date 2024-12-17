using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace clinics_bd
{
    public abstract partial class InsertUpdateFormBase : Form
    {
        public List<object> parameters { get; private set; } = new List<object>();

        protected InsertUpdateFormBase()
        {
            InitializeComponent();

            Theme.UpdateForm(this);

            MinimizeBox = false;
            MaximizeBox = false;
        }

        public virtual void SetData(List<object> data)
        {
        }

        public abstract bool Validation();

        public abstract void CollectParameters();

        protected virtual void Button1_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                CollectParameters();
                Close();
            }
        }
    }
}
