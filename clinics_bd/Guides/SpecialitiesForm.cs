using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class SpecialitiesForm : BaseGuideForm
    {
        protected override string QuerySelect => "SELECT id, name AS 'Название' FROM speciality;";
        protected override string QueryUpdate => "UPDATE speciality SET name = @newName where id = @id";
        protected override string QueryInsert => "INSERT INTO speciality(name) value (@newName)";
        protected override string QueryDelete => "DELETE FROM speciality where id = @id";
        protected override string QuerySearch => "SELECT id, name AS 'Название' FROM speciality where name like CONCAT('%', @param, '%')";

        public SpecialitiesForm(string name, DB db) : base(name, db)
        {
        }
    }
}
