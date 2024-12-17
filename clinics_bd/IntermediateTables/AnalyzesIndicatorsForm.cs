using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class AnalyzesIndicatorsForm : BaseTableForm
    {
        protected override string QuerySelect => "SELECT patient_analysis_indicators.id, analysis.name as 'Анализ', analysis_indicators.name as 'Показатель' " +
                    "FROM patient_analysis_indicators " +
                    "join analysis_indicators on id_analysis_indicators = analysis_indicators.id join analysis on id_analysis = analysis.id";
        protected override string QueryUpdate => "UPDATE patient_analysis_indicators SET id_analysis = @id_patient_card, id_analysis_indicators = @param WHERE id = @id";
        protected override string QueryInsert => "INSERT INTO patient_analysis_indicators(id_analysis, id_analysis_indicators) " +
                     "value (@id_patient_card, @param)";
        protected override string QueryDelete => "DELETE FROM patient_analysis_indicators where id = @id";
        protected override string QuerySearch => "SELECT patient_analysis_indicators.id, analysis.name as 'Анализ', analysis_indicators.name as 'Показатель' " +
                    "FROM patient_analysis_indicators " +
                    "join analysis_indicators on id_analysis_indicators = analysis_indicators.id join analysis on id_analysis = analysis.id " +
                    "WHERE analysis.name like CONCAT('%', @param, '%') or analysis_indicators.name like CONCAT('%', @param, '%')";
        protected override string QuerySelectById => "SELECT patient_analysis_indicators.id, id_analysis, id_analysis_indicators as 'Название' " +
                    "FROM patient_analysis_indicators " +
                    "WHERE patient_analysis_indicators.id = @id";
        protected override string QueryForOther => "";

        public AnalyzesIndicatorsForm(string name, DB db) : base(name, db)
        {
        }
    }
}
