using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class AnalyzesForm : BaseGuideForm
    {
        protected override string QuerySelect => "SELECT id, name AS 'Название' FROM analysis;";
        protected override string QueryUpdate => "UPDATE analysis SET name = @newName where id = @id";
        protected override string QueryInsert => "INSERT INTO analysis(name) value (@newName)";
        protected override string QueryDelete => "DELETE FROM analysis where id = @id";
        protected override string QuerySearch => "SELECT id, name AS 'Название' FROM analysis where name like CONCAT('%', @param, '%')";

        public AnalyzesForm(string name, DB db) : base(name, db)
        {
        }
    }
}
