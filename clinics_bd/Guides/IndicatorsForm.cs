using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class IndicatorsForm : BaseGuideForm
    {
        protected override string QuerySelect => "SELECT id, name AS 'Название' FROM analysis_indicators;";
        protected override string QueryUpdate => "UPDATE analysis_indicators SET name = @newName where id = @id";
        protected override string QueryInsert => "INSERT INTO analysis_indicators(name) value (@newName)";
        protected override string QueryDelete => "DELETE FROM analysis_indicators where id = @id";
        protected override string QuerySearch => "SELECT id, name AS 'Название' FROM analysis_indicators where name like CONCAT('%', @param, '%')";

        public IndicatorsForm(string name, DB db) : base(name, db)
        {
        }
    }
}
