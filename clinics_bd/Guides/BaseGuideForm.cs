using System;
using System.Collections.Generic;

namespace clinics_bd
{
    public abstract class BaseGuideForm : IHandler
    {
        protected abstract string QuerySelect { get; }
        protected abstract string QueryUpdate { get; }
        protected abstract string QueryInsert { get; }
        protected abstract string QueryDelete { get; }
        protected abstract string QuerySearch { get; }

        public List<string> queryList = new List<string>();
        public string thisName;
        public DB db;

        public BaseGuideForm(string name, DB db)
        {
            thisName = name;
            queryList.Add(QuerySelect);
            queryList.Add(QueryInsert);
            queryList.Add(QueryUpdate);
            queryList.Add(QueryDelete);
            queryList.Add(QuerySearch);

            this.db = db;
        }

        public void LoadData()
        {
            GuideForm guideForm = new GuideForm(thisName, queryList, db);
            guideForm.SetTable();
        }
    }
}
