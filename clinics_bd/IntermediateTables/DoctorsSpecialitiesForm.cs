using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class DoctorsSpecialitiesForm : BaseTableForm
    {
        protected override string QuerySelect => "SELECT doctor_speciality.id, CONCAT_WS(' ', doctor.surname, doctor.name, doctor.middlename) as 'Врач', " +
                    "speciality.name as 'Специализация' " +
                    "FROM doctor_speciality " +
                    "join doctor on id_doctor = doctor.id join speciality on id_speciality = speciality.id ";
        protected override string QueryUpdate => "UPDATE doctor_speciality SET id_doctor = @id_patient_card, id_speciality = @param WHERE id = @id";
        protected override string QueryInsert => "INSERT INTO doctor_speciality(id_doctor, id_speciality) " +
                     "value (@id_patient_card, @param)";
        protected override string QueryDelete => "DELETE FROM doctor_speciality where id = @id";
        protected override string QuerySearch => "SELECT doctor_speciality.id, CONCAT_WS(' ', doctor.surname, doctor.name, doctor.middlename) as 'Врач', " +
                    "speciality.name as 'Специализация' " +
                    "FROM doctor_speciality " +
                    "join doctor on id_doctor = doctor.id join speciality on id_speciality = speciality.id " +
                    "WHERE doctor.surname like CONCAT('%', @param, '%') or doctor.name like CONCAT('%', @param, '%') or doctor.middlename like CONCAT('%', @param, '%') " +
                    "or speciality.name like CONCAT('%', @param, '%')";
        protected override string QuerySelectById => "SELECT doctor_speciality.id, id_doctor, id_speciality " +
                    "FROM doctor_speciality " +
                    "WHERE doctor_speciality.id = @id";
        protected override string QueryForOther => "";

        public DoctorsSpecialitiesForm(string name, DB db) : base(name, db)
        {
        }
    }
}
