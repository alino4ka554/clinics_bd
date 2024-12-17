using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class MedicamentsForm : BaseGuideForm
    {
        protected override string QuerySelect => "SELECT id, name AS 'Название' FROM medicament;";
        protected override string QueryUpdate => "UPDATE medicament SET name = @newName where id = @id";
        protected override string QueryInsert => "INSERT INTO medicament(name) value (@newName)";
        protected override string QueryDelete => "DELETE FROM medicament where id = @id";
        protected override string QuerySearch => "SELECT id, name AS 'Название' FROM medicament where name like CONCAT('%', @param, '%')";

        public MedicamentsForm(string name, DB db) : base(name, db)
        {
        }
    }
}
