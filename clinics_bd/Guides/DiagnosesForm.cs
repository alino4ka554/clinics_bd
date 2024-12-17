using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class DiagnosesForm : BaseGuideForm
    {
        protected override string QuerySelect => "SELECT id, name AS 'Название' FROM diagnose;";
        protected override string QueryUpdate => "UPDATE diagnose SET name = @newName where id = @id";
        protected override string QueryInsert => "INSERT INTO diagnose(name) value (@newName)";
        protected override string QueryDelete => "DELETE FROM diagnose where id = @id";
        protected override string QuerySearch => "SELECT id, name AS 'Название' FROM diagnose where name like CONCAT('%', @param, '%')";

        public DiagnosesForm(string name, DB db) : base(name, db)
        {
        }
    }
}
