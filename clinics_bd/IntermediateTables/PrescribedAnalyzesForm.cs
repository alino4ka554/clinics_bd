using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class PrescribedAnalyzesForm : BaseTableForm
    {
        protected override string QuerySelect => "SELECT prescribed_analysis.id, number as 'Номер карточки пациента', name as 'Название' " +
                    "FROM prescribed_analysis " +
                    "join patient_card on id_patient_card = patient_card.id join analysis on id_analysis = analysis.id";
        protected override string QueryUpdate => "UPDATE prescribed_analysis SET id_patient_card = @id_patient_card, id_analysis = @param WHERE id = @id";
        protected override string QueryInsert => "INSERT INTO prescribed_analysis(id_patient_card, id_analysis) " +
                     "value (@id_patient_card, @param)";
        protected override string QueryDelete => "DELETE FROM prescribed_analysis where id = @id";
        protected override string QuerySearch => "SELECT prescribed_analysis.id, number as 'Номер карточки пациента', name as 'Название' " +
                    "FROM prescribed_analysis " +
                    "join patient_card on id_patient_card = patient_card.id join analysis on id_analysis = analysis.id " +
                    "WHERE number like CONCAT('%', @param, '%') or name like CONCAT('%', @param, '%')";
        protected override string QuerySelectById => "SELECT prescribed_analysis.id, id_patient_card as 'Номер карточки пациента', id_analysis as 'Название' " +
                    "FROM prescribed_analysis " +
                    "WHERE prescribed_analysis.id = @id";
        protected override string QueryForOther => "";

        public PrescribedAnalyzesForm(string name, DB db) : base(name, db)
        {
        }
    }
}
