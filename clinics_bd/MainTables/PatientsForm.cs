using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class PatientsForm : BaseTableForm
    {
        protected override string QuerySelect => "SELECT patient.id, surname as 'Фамилия', patient.name as 'Имя', middlename as 'Отчество', " +
                    "gender as 'Пол', birth_year as 'Год рождения', policy_number as 'Номер полиса', " +
                    "is_pensioner as 'Пенсионер', house_number as 'Номер дома', " +
                    "city.name as 'Город', street.name as 'Улица' FROM clinics_registry.patient " +
                    "join city on id_city = city.id " +
                    "join street on id_street = street.id;";
        protected override string QueryUpdate => "UPDATE patient SET id_city = @id_city, id_street = @id_street, surname = @surname, name = @name, middlename = @middlename," +
                     "gender = @gender, birth_year = @birth_year, policy_number = @policy_number, is_pensioner = @is_pensioner, house_number = @house_number WHERE id = @id";
        protected override string QueryInsert => "INSERT INTO patient(id_city, id_street, surname, name, middlename, gender, birth_year, policy_number, is_pensioner, house_number) " +
                     "value (@id_city, @id_street, @surname, @name, @middlename, @gender, @birth_year, @policy_number, @is_pensioner, @house_number)";
        protected override string QueryDelete => "DELETE FROM patient where id = @id";
        protected override string QuerySearch => "SELECT patient.id, surname as 'Фамилия', patient.name as 'Имя', middlename as 'Отчество', " +
                    "gender as 'Пол', birth_year as 'Год рождения', policy_number as 'Номер полиса', " +
                    "is_pensioner as 'Пенсионер', house_number as 'Номер дома', " +
                    "city.name as 'Город', street.name as 'Улица' FROM clinics_registry.patient " +
                    "join city on id_city = city.id " +
                    "join street on id_street = street.id " +
                    "WHERE surname like CONCAT('%', @param, '%') or patient.name like CONCAT('%', @param, '%') " +
                    "or middlename like CONCAT('%', @param, '%') or gender like CONCAT('%', @param, '%') " +
                    "or birth_year like CONCAT('%', @param, '%') or policy_number like CONCAT('%', @param, '%') " +
                    "or house_number like CONCAT('%', @param, '%') or city.name like CONCAT('%', @param, '%') " +
                    "or street.name like CONCAT('%', @param, '%')";
        protected override string QuerySelectById => "SELECT patient.id, surname, patient.name, middlename, " +
                    "gender, birth_year, policy_number, is_pensioner, house_number, city.name, street.name FROM patient " +
                    "join street on id_street = street.id join city on id_city = city.id " +
                    "WHERE patient.id = @id";
        protected override string QueryForOther => "SELECT id, CONCAT_WS(' ', surname, name, middlename) as 'название' FROM patient;";

        public PatientsForm(string name, DB db) : base(name, db)
        {
        }
    }
}
