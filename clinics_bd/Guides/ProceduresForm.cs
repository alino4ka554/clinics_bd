using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class ProceduresForm : BaseGuideForm
    {
        protected override string QuerySelect => "SELECT id, name AS 'Название' FROM procedures;";
        protected override string QueryUpdate => "UPDATE procedures SET name = @newName where id = @id";
        protected override string QueryInsert => "INSERT INTO procedures(name) value (@newName)";
        protected override string QueryDelete => "DELETE FROM procedures where id = @id";
        protected override string QuerySearch => "SELECT id, name AS 'Название' FROM procedures where name like CONCAT('%', @param, '%')";

        public ProceduresForm(string name, DB db) : base(name, db)
        {
        }
    }
}
