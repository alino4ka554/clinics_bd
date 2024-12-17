using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class ContentForm : IHandler
    {
        string path;
        public ContentForm(string menuName, DB db) 
        {
            this.path = Directory.GetCurrentDirectory().Replace("\\bin\\Debug", "") + "\\Properties\\Содержание.html";
        }
        public void LoadData()
        {
            Process.Start(this.path);
        }
    }
}
