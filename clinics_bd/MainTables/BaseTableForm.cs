using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public abstract class BaseTableForm : IHandler
    {
        protected abstract string QuerySelect { get; }
        protected abstract string QueryUpdate { get; }
        protected abstract string QueryInsert { get; }
        protected abstract string QueryDelete { get; }
        protected abstract string QuerySearch { get; }
        protected abstract string QuerySelectById { get; }
        protected abstract string QueryForOther { get; }

        public List<string> queryList = new List<string>();
        public string thisName;
        public DB db;

        public BaseTableForm(string name, DB db)
        {
            thisName = name;

            // Добавляем запросы в список, чтобы передать их в GuideForm
            queryList.Add(QuerySelect);
            queryList.Add(QueryInsert);
            queryList.Add(QueryUpdate);
            queryList.Add(QueryDelete);
            queryList.Add(QuerySearch);
            queryList.Add(QuerySelectById);
            queryList.Add(QueryForOther);

            this.db = db;
        }

        public void LoadData()
        {
            TableForm guideForm = new TableForm(thisName, queryList, db);
            guideForm.SetTable();
        }
    }
}
