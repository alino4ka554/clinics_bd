using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class TimingForm : BaseTableForm
    {
        protected override string QuerySelect => "SELECT timing.id, CONCAT_WS(' ', doctor.surname, doctor.name, doctor.middlename) as 'Врач', weekday as 'День недели', " +
                    "start_at as 'Время начала приема', end_at as 'Время конца приема', cabinet as 'Кабинет' " +
                    "FROM timing join doctor on id_doctor = doctor.id";
        protected override string QueryUpdate => "UPDATE timing SET id_doctor = @id_doctor, weekday = @weekday, start_at = @start_at, end_at = @end_at, cabinet = @cabinet WHERE id = @id";
        protected override string QueryInsert => "INSERT INTO timing(id_doctor, weekday, start_at, end_at, cabinet) " +
                     "value (@id_doctor, @weekday, @start_at, @end_at, @cabinet)";
        protected override string QueryDelete => "DELETE FROM timing where id = @id";
        protected override string QuerySearch => "SELECT timing.id, CONCAT_WS(' ', doctor.surname, doctor.name, doctor.middlename) as 'Врач', weekday as 'День недели', " +
                    "start_at as 'Время начала приема', end_at as 'Время конца приема', cabinet as 'Кабинет' " +
                    "FROM timing join doctor on id_doctor = doctor.id " +
                    "WHERE weekday like CONCAT('%', @param, '%') or cabinet like CONCAT('%', @param, '%') or CONCAT_WS(' ', doctor.surname, doctor.name, doctor.middlename) like CONCAT('%', @param, '%') " +
                    "or start_at like CONCAT('%', @param, '%') or end_at like CONCAT('%', @param, '%')";
        protected override string QuerySelectById => "SELECT timing.id, CONCAT_WS(' ', doctor.surname, doctor.name, doctor.middlename) as 'Врач', weekday as 'День недели', " +
                    "start_at as 'Время начала приема', end_at as 'Время конца приема', cabinet as 'Кабинет' " +
                    "FROM timing join doctor on id_doctor = doctor.id " +
                    "WHERE timing.id = @id";
        protected override string QueryForOther => "SELECT id, number as 'Номер карточки' FROM patient_card";

        public TimingForm(string name, DB db) : base(name, db)
        {
        }
    }
}
