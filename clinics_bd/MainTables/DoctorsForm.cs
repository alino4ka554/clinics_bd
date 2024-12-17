using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class DoctorsForm : BaseTableForm
    {
        protected override string QuerySelect => "SELECT id, surname as 'Фамилия', name as 'Имя', middlename as 'Отчество', " +
                    "birth_year as 'Год рождения', education as 'Образование', " +
                    "end_year as 'Год окончания', experience as 'Стаж', telephone as 'Служебный телефон', photo as 'Фотография' " +
                    "FROM doctor ";
        protected override string QueryUpdate => "UPDATE doctor SET surname = @surname, name = @name, middlename = @middlename," +
                     "birth_year = @birth_year, education = @education, end_year = @end_year, experience = @experience, " +
                     "telephone = @telephone, photo = @photo where id = @id";
        protected override string QueryInsert => "INSERT INTO doctor(surname, name, middlename, birth_year, education, end_year, experience, telephone, photo) " +
                     "value (@surname, @name, @middlename, @birth_year, @education, @end_year, @experience, @telephone, @photo)";
        protected override string QueryDelete => "DELETE FROM doctor where id = @id";
        protected override string QuerySearch => "SELECT id, surname as 'Фамилия', name as 'Имя', middlename as 'Отчество', " +
                    "birth_year as 'Год рождения', education as 'Образование', " +
                    "end_year as 'Год окончания', experience as 'Стаж', telephone as 'Служебный телефон', photo as 'Фотография' " +
                    "FROM doctor where surname like CONCAT('%', @param, '%') or name like CONCAT('%', @param, '%') " +
                    "or middlename like CONCAT('%', @param, '%') or birth_year like CONCAT('%', @param, '%') or education like CONCAT('%', @param, '%') " +
                    "or end_year like CONCAT('%', @param, '%') or experience like CONCAT('%', @param, '%') or telephone like CONCAT('%', @param, '%') " +
                    "or photo like CONCAT('%', @param, '%')";
        protected override string QuerySelectById  => "SELECT id, surname, name, middlename, " +
                    "birth_year, education, end_year, experience, telephone, photo " +
                    "FROM doctor WHERE id = @id";
        protected override string QueryForOther => "SELECT id, CONCAT_WS(' ', surname, name, middlename) as 'название' FROM doctor;";

        public DoctorsForm(string name, DB db) : base(name, db)
        {
        }

    }
}
