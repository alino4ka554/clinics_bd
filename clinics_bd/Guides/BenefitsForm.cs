using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class BenefitsForm : BaseGuideForm
    {
        protected override string QuerySelect => "SELECT id, name AS 'Название' FROM benefits;";
        protected override string QueryUpdate => "UPDATE benefits SET name = @newName where id = @id";
        protected override string QueryInsert => "INSERT INTO benefits(name) value (@newName)";
        protected override string QueryDelete => "DELETE FROM benefits where id = @id";
        protected override string QuerySearch => "SELECT id, name AS 'Название' FROM benefits where name like CONCAT('%', @param, '%')";

        public BenefitsForm(string name, DB db) : base(name, db)
        {
        }
    }
}
