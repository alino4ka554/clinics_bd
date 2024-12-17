using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class PrescribedProceduresForm : BaseTableForm
    {
        protected override string QuerySelect => "SELECT prescribed_procedures.id, number as 'Номер карточки пациента', name as 'Название', " +
                    "number_sessions as 'Количество сеансов' " +
                    "FROM prescribed_procedures " +
                    "join patient_card on id_patient_card = patient_card.id join procedures on id_procedures = procedures.id";
        protected override string QueryUpdate => "UPDATE prescribed_procedures SET id_patient_card = @id_patient_card, id_procedures = @param, number_sessions = @number WHERE id = @id";
        protected override string QueryInsert => "INSERT INTO prescribed_procedures(id_patient_card, id_procedures, number_sessions) " +
                     "value (@id_patient_card, @param, @number)";
        protected override string QueryDelete => "DELETE FROM prescribed_procedures where id = @id";
        protected override string QuerySearch => "SELECT prescribed_procedures.id, number as 'Номер карточки пациента', name as 'Название', " +
                    "number_sessions as 'Количество сеансов' " +
                    "FROM prescribed_procedures " +
                    "join patient_card on id_patient_card = patient_card.id join procedures on id_procedures = procedures.id " +
                    "WHERE number like CONCAT('%', @param, '%') or name like CONCAT('%', @param, '%') or number_sessions or like CONCAT('%', @param, '%')";
        protected override string QuerySelectById => "SELECT prescribed_procedures.id, id_patient_card as 'Номер карточки пациента', id_procedures as 'Название', number_sessions " +
                    "FROM prescribed_procedures " +
                    "join patient_card on id_patient_card = patient_card.id join procedures on id_procedures = procedures.id " +
                    "WHERE prescribed_procedures.id = @id";
        protected override string QueryForOther => "";

        public PrescribedProceduresForm(string name, DB db) : base(name, db)
        {
        }
    }
}
