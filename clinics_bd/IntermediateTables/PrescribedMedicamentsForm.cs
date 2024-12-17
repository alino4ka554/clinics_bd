using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class PrescribedMedicamentsForm : BaseTableForm
    {
        protected override string QuerySelect => "SELECT prescribed_medicaments.id, number as 'Номер карточки пациента', name as 'Название' " +
                    "FROM prescribed_medicaments " +
                    "join patient_card on id_patient_card = patient_card.id join medicament on id_medicament = medicament.id";
        protected override string QueryUpdate => "UPDATE prescribed_medicaments SET id_patient_card = @id_patient_card, id_medicament = @param WHERE id = @id";
        protected override string QueryInsert => "INSERT INTO prescribed_medicaments(id_patient_card, id_medicament) " +
                     "value (@id_patient_card, @param)";
        protected override string QueryDelete => "DELETE FROM prescribed_medicaments where id = @id";
        protected override string QuerySearch => "SELECT prescribed_medicaments.id, number as 'Номер карточки пациента', name as 'Название' " +
                    "FROM prescribed_medicaments " +
                    "join patient_card on id_patient_card = patient_card.id join medicament on id_medicament = medicament.id " +
                    "WHERE number like CONCAT('%', @param, '%') or name like CONCAT('%', @param, '%')";
        protected override string QuerySelectById => "SELECT prescribed_medicaments.id, id_patient_card as 'Номер карточки пациента', id_medicament as 'Название' " +
                    "FROM prescribed_medicaments " +
                    "join patient_card on id_patient_card = patient_card.id join medicament on id_medicament = medicament.id " +
                    "WHERE prescribed_medicaments.id = @id";
        protected override string QueryForOther => "";

        public PrescribedMedicamentsForm(string name, DB db) : base(name, db)
        {
        }
    }
}
