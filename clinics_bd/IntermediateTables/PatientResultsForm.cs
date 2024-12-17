using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class PatientResultsForm : BaseTableForm
    {
        protected override string QuerySelect => "SELECT patient_analysis_results.id, number as 'Номер карточки пациента', " +
                    "CONCAT_WS(' - ', analysis_indicators.name, analysis_results.value) as 'Результат' " +
                    "FROM patient_analysis_results join patient_card on id_patient_card = patient_card.id " +
                    "join analysis_results on id_analysis_results = analysis_results.id " +
                    "join analysis_indicators on id_analysis_indicators = analysis_indicators.id;";
        protected override string QueryUpdate => "UPDATE patient_analysis_results SET id_patient_card = @id_patient_card, id_analysis_results = @param WHERE id = @id";
        protected override string QueryInsert => "INSERT INTO patient_analysis_results(id_patient_card, id_analysis_results) " +
                     "value (@id_patient_card, @param)";
        protected override string QueryDelete => "DELETE FROM patient_analysis_results where id = @id";
        protected override string QuerySearch => "SELECT patient_analysis_results.id, number as 'Номер карточки', " +
                    "CONCAT_WS(' - ', analysis_indicators.name, analysis_results.value) as 'Результат' " +
                    "FROM patient_analysis_results join patient_card on id_patient_card = patient_card.id " +
                    "join analysis_results on id_analysis_results = analysis_results.id " +
                    "join analysis_indicators on id_analysis_indicators = analysis_indicators.id " +
                    "WHERE number like CONCAT('%', @param, '%') or CONCAT_WS(' - ', analysis_indicators.name, analysis_results.value) like CONCAT('%', @param, '%')";
        protected override string QuerySelectById => "SELECT patient_analysis_results.id, id_patient_card as 'Номер карточки', id_analysis_results as 'Название' " +
                    "FROM patient_analysis_results " +
                    "WHERE patient_analysis_results.id = @id";
        protected override string QueryForOther => "";

        public PatientResultsForm(string name, DB db) : base(name, db)
        {
        }
    }
}
