using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class StreetsForm : BaseGuideForm
    {
        protected override string QuerySelect => "SELECT id, name AS 'Название' FROM street;";
        protected override string QueryUpdate => "UPDATE street SET name = @newName where id = @id";
        protected override string QueryInsert => "INSERT INTO street(name) value (@newName)";
        protected override string QueryDelete => "DELETE FROM street where id = @id";
        protected override string QuerySearch => "SELECT id, name AS 'Название' FROM street where name like CONCAT('%', @param, '%')";

        public StreetsForm(string name, DB db) : base(name, db) 
        {
        }
    }
}
