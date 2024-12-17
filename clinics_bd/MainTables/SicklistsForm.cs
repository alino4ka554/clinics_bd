using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class SicklistsForm : BaseTableForm
    {
        protected override string QuerySelect => "SELECT id, start_data as 'Дата начала', end_data as 'Дата окончания'" +
                    " FROM sick_list;";
        protected override string QueryUpdate => "UPDATE sick_list SET start_data = @start_data, end_data = @end_data WHERE id = @id";
        protected override string QueryInsert => "INSERT INTO sick_list(start_data, end_data) " +
                     "value (@start_data, @end_data)";
        protected override string QueryDelete => "DELETE FROM sick_list where id = @id";
        protected override string QuerySearch => "SELECT id, start_data as 'Дата начала', end_data as 'Дата окончания'" +
                    "FROM sick_list WHERE start_data like CONCAT('%', @param, '%') or end_data like CONCAT('%', @param, '%')";
        protected override string QuerySelectById => "SELECT id, start_data as 'Дата начала', end_data as 'Дата окончания'" +
                    " FROM sick_list WHERE id = @id";
        protected override string QueryForOther => "SELECT id, CONCAT_WS(' - ', start_data, end_data) as 'Даты' FROM sick_list;";

        public SicklistsForm(string name, DB db) : base(name, db)
        {
        }
    }
}
