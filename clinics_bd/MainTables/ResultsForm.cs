using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class ResultsForm : BaseTableForm
    {
        protected override string QuerySelect => "SELECT analysis_results.id, name as 'Показатель', value as 'Значение' FROM analysis_results " +
                     "join analysis_indicators on id_analysis_indicators = analysis_indicators.id;";
        protected override string QueryUpdate => "UPDATE  analysis_results SET value = @value, " +
                    "id_analysis_indicators = @id_analysis_indicators WHERE id = @id";
        protected override string QueryInsert => "INSERT INTO analysis_results(value, id_analysis_indicators) " +
                     "value (@value, @id_analysis_indicators)";
        protected override string QueryDelete => "DELETE FROM analysis_results where id = @id";
        protected override string QuerySearch => "SELECT analysis_results.id, name as 'Показатель', value as 'Значение' FROM analysis_results " +
                     "join analysis_indicators on id_analysis_indicators = analysis_indicators.id " + 
                    "WHERE name like CONCAT('%', @param, '%') or value like CONCAT('%', @param, '%')";
        protected override string QuerySelectById => "SELECT analysis_results.id, name as 'Показатель', value as 'Значение' FROM analysis_results " +
                     "join analysis_indicators on id_analysis_indicators = analysis_indicators.id " +
                     "WHERE analysis_results.id = @id";
        protected override string QueryForOther => "SELECT analysis_results.id, CONCAT_WS(' - ', name, value) as 'Название' FROM analysis_results " +
                     "join analysis_indicators on id_analysis_indicators = analysis_indicators.id ";

        public ResultsForm(string name, DB db) : base(name, db)
        {
        }
    }
}
