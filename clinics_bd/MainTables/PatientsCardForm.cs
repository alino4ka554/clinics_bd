using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinics_bd
{
    public class PatientsCardForm : BaseTableForm
    {
        protected override string QuerySelect => "SELECT patient_card.id, number as 'Номер', CONCAT_WS(' ', patient.surname, patient.name, patient.middlename) as 'Пациент', " +
                    "CONCAT_WS(' ', doctor.surname, doctor.name, doctor.middlename) as 'Врач', CONCAT_WS(' - ', start_data, end_data) as 'Больничный лист' " +
                    "FROM patient_card " +
                    "join patient on id_patient = patient.id join doctor on id_doctor = doctor.id join sick_list on id_sick_list = sick_list.id ";
        protected override string QueryUpdate => "UPDATE patient_card SET number = @number, id_patient = @id_patient, id_doctor = @id_doctor, id_sick_list = @id_sick_list WHERE id = @id";
        protected override string QueryInsert => "INSERT INTO patient_card(number, id_patient, id_doctor, id_sick_list) " +
                     "value (@number, @id_patient, @id_doctor, @id_sick_list)";
        protected override string QueryDelete => "DELETE FROM patient_card where id = @id";
        protected override string QuerySearch => "SELECT patient_card.id, number as 'Номер', CONCAT_WS(' ', patient.surname, patient.name, patient.middlename) as 'Пациент', " +
                    "CONCAT_WS(' ', doctor.surname, doctor.name, doctor.middlename) as 'Врач', CONCAT_WS(' - ', start_data, end_data) as 'Больничный лист' " +
                    "FROM patient_card " +
                    "join patient on id_patient = patient.id join doctor on id_doctor = doctor.id join sick_list on id_sick_list = sick_list.id " +
                    "WHERE number like CONCAT('%', @param, '%') or CONCAT_WS(' ', patient.surname, patient.name, patient.middlename) like CONCAT('%', @param, '%') " +
                    "or CONCAT_WS(' ', doctor.surname, doctor.name, doctor.middlename) like CONCAT('%', @param, '%') or CONCAT_WS(' - ', start_data, end_data) like CONCAT('%', @param, '%')";
        protected override string QuerySelectById => "SELECT patient_card.id, number as 'Номер', CONCAT_WS(' ', patient.surname, patient.name, patient.middlename) as 'Пациент', " +
                    "CONCAT_WS(' ', doctor.surname, doctor.name, doctor.middlename) as 'Врач', CONCAT_WS(' - ', start_data, end_data) as 'Больничный лист' " +
                    "FROM patient_card " +
                    "join patient on id_patient = patient.id join doctor on id_doctor = doctor.id join sick_list on id_sick_list = sick_list.id " +
                    "WHERE patient_card.id = @id";
        protected override string QueryForOther => "SELECT id, number as 'Название' FROM patient_card";

        public PatientsCardForm(string name, DB db) : base(name, db)
        {
        }
    }
}
