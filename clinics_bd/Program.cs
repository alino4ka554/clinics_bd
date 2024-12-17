using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clinics_bd
{

    public enum Color
    {
        White, Pink
    }
    public static class Theme
    {
        public static Color AppColor = Color.White;
        public static Font AppFont = new Font("Microsoft Sans Serif", 8);

        public static void LoadTheme()
        {
            string savedColor = Properties.Settings.Default.AppColor;

            if (savedColor == "Pink")
                AppColor = Color.Pink;
            else
                AppColor = Color.White;
            string fontName = Properties.Settings.Default.FontName;
            float fontSize = Properties.Settings.Default.FontSize;

            if (!string.IsNullOrEmpty(fontName) && fontSize > 0)
            {
                AppFont = new Font(fontName, fontSize);
            }
        }

        public static void SaveTheme()
        {
            Properties.Settings.Default.FontName = AppFont.FontFamily.Name;
            Properties.Settings.Default.FontSize = AppFont.Size;
            Properties.Settings.Default.AppColor = (AppColor == Color.Pink) ? "Pink" : "White";
            Properties.Settings.Default.Save();
        }

        public static void UpdateForm(Form form)
        {
            form.Font = AppFont;
            switch (AppColor)
            {
                case Color.White:
                    form.BackColor = System.Drawing.SystemColors.Window;
                    break;
                case Color.Pink:
                    form.BackColor = System.Drawing.Color.MistyRose;
                    break;
            }

            ApplyThemeToControls(form, AppColor);
        }

        public static void ApplyThemeToControls(Control control, Color color)
        {
            control.Font = AppFont;
            control.BackColor = (color == Color.Pink) ? System.Drawing.Color.MistyRose : System.Drawing.SystemColors.Window;

            foreach (Control childControl in control.Controls)
            {
                if (childControl is MenuStrip menuStrip)
                {
                    menuStrip.BackColor = (color == Color.Pink) ? System.Drawing.Color.MistyRose : System.Drawing.SystemColors.Window;
                    menuStrip.Font = AppFont;
                    UpdateMenuItemsColor(menuStrip.Items);
                }
                if (childControl is DataGridView dataGridView)
                {
                    switch (AppColor)
                    {
                        case Color.White:
                            dataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
                            dataGridView.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
                            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
                            dataGridView.RowHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
                            dataGridView.Font = AppFont;
                            break;
                        case Color.Pink:
                            dataGridView.BackgroundColor = System.Drawing.Color.MistyRose;
                            dataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.MistyRose;
                            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.MistyRose;
                            dataGridView.RowHeadersDefaultCellStyle.BackColor = System.Drawing.Color.MistyRose;
                            dataGridView.Font = AppFont;
                            break;
                    }
                }
                if (childControl is ToolStrip toolStrip)
                {
                    switch (AppColor)
                    {
                        case Color.White:
                            toolStrip.BackColor = System.Drawing.SystemColors.Window;
                            break;
                        case Color.Pink:
                            toolStrip.BackColor = System.Drawing.Color.MistyRose;
                            break;
                    }
                }
                else
                {
                    ApplyThemeToControls(childControl, color);
                }
            }
        }

        public static void UpdateMenuItemsColor(ToolStripItemCollection menuItems)
        {
            foreach (ToolStripItem item in menuItems)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    menuItem.BackColor = (AppColor == Color.Pink) ? System.Drawing.Color.MistyRose : System.Drawing.SystemColors.Window;
                    menuItem.DropDown.BackColor = (AppColor == Color.Pink) ? System.Drawing.Color.MistyRose : System.Drawing.SystemColors.Window;
                    menuItem.Font = AppFont;

                    foreach (ToolStripItem subItem in menuItem.DropDownItems)
                    {
                        subItem.BackColor = (AppColor == Color.Pink) ? System.Drawing.Color.MistyRose : System.Drawing.SystemColors.Window;
                        subItem.Font = AppFont;
                    }
                    if (menuItem.HasDropDownItems)
                    {
                        UpdateMenuItemsColor(menuItem.DropDownItems);
                    }
                }
            }
        }
    }

    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Authorization());
        }
    }
}
