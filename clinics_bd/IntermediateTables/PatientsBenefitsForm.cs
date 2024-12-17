using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class PatientsBenefitsForm : BaseTableForm
    {
        protected override string QuerySelect => "SELECT patient_benefits.id, CONCAT_WS(' ', patient.surname, patient.name, patient.middlename) as 'Пациент', " +
                    "benefits.name as 'Льгота' " +
                    "FROM patient_benefits " +
                    "join patient on id_patient = patient.id join benefits on id_benefits = benefits.id ";
        protected override string QueryUpdate => "UPDATE patient_benefits SET id_patient = @id_patient_card, id_benefits = @param WHERE id = @id";
        protected override string QueryInsert => "INSERT INTO patient_benefits(id_patient, id_benefits) " +
                     "value (@id_patient_card, @param)";
        protected override string QueryDelete => "DELETE FROM patient_benefits where id = @id";
        protected override string QuerySearch => "SELECT patient_benefits.id, CONCAT_WS(' ', patient.surname, patient.name, patient.middlename) as 'Пациент', " +
                    "benefits.name as 'Льгота' " +
                    "FROM patient_benefits " +
                    "join patient on id_patient = patient.id join benefits on id_benefits = benefits.id " + 
                    "WHERE patient.surname like CONCAT('%', @param, '%') or patient.name like CONCAT('%', @param, '%') or patient.middlename like CONCAT('%', @param, '%') " +
                    "or benefits.name like CONCAT('%', @param, '%')";
        protected override string QuerySelectById => "SELECT patient_benefits.id, id_patient, id_benefits " +
                    "FROM patient_benefits " +
                    "WHERE patient_benefits.id = @id";
        protected override string QueryForOther => "";

        public PatientsBenefitsForm(string name, DB db) : base(name, db)
        {
        }
    }
}
