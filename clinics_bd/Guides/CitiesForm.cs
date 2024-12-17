using System;

namespace clinics_bd
{
    public class CitiesForm : BaseGuideForm
    {
        protected override string QuerySelect => "SELECT id, name AS 'Название' FROM city;";
        protected override string QueryUpdate => "UPDATE city SET name = @newName where id = @id";
        protected override string QueryInsert => "INSERT INTO city(name) value (@newName)";
        protected override string QueryDelete => "DELETE FROM city where id = @id";
        protected override string QuerySearch => "SELECT id, name AS 'Название' FROM city where name like CONCAT('%', @param, '%')";

        public CitiesForm(string name, DB db) : base(name, db)
        {
        }
    }
}
