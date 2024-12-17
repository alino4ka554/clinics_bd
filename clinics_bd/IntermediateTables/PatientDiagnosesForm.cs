using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class PatientDiagnosesForm : BaseTableForm
    {
        protected override string QuerySelect => "SELECT patient_diagnose.id, number as 'Номер карточки пациента', name as 'Название' " +
                    "FROM patient_diagnose " +
                    "join patient_card on id_patient_card = patient_card.id join diagnose on id_diagnose = diagnose.id";
        protected override string QueryUpdate => "UPDATE patient_diagnose SET id_patient_card = @id_patient_card, id_diagnose = @param WHERE id = @id";
        protected override string QueryInsert => "INSERT INTO patient_diagnose(id_patient_card, id_diagnose) " +
                     "value (@id_patient_card, @param)";
        protected override string QueryDelete => "DELETE FROM patient_diagnose where id = @id";
        protected override string QuerySearch => "SELECT patient_diagnose.id, number as 'Номер карточки пациента', name as 'Название' " +
                    "FROM patient_diagnose " +
                    "join patient_card on id_patient_card = patient_card.id join diagnose on id_diagnose = diagnose.id " +
                    "WHERE number like CONCAT('%', @param, '%') or name like CONCAT('%', @param, '%')";
        protected override string QuerySelectById => "SELECT patient_diagnose.id, id_patient_card as 'Номер карточки пациента', id_diagnose as 'Название' " +
                    "FROM patient_diagnose " +
                    "join patient_card on id_patient_card = patient_card.id join diagnose on id_diagnose = diagnose.id " +
                    "WHERE patient_diagnose.id = @id";
        protected override string QueryForOther => "";

        public PatientDiagnosesForm(string name, DB db) : base(name, db)
        {
        }
    }
}
