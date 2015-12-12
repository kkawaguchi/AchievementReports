using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AchievementReports
{

    public class PeopleRepository
    {
        private IDbConnection conn;

        public PeopleRepository(IDbConnection conn)
        {
            this.conn = conn;
        }

        public List<Person> GetAll()
        {
            string sql;

            sql = "";
            sql = "SELECT * FROM 人 ;";

            IDbCommand command = this.conn.CreateCommand();
            command.CommandText = sql;
            IDataReader reader = command.ExecuteReader();
            List<Person> people = new List<Person>();

            while (reader.Read())
            {
                Person person = new Person();
                person.personID = reader.GetInt32(0);
                person.name = reader.GetString(1);
                person.kana = reader.GetString(2);

                people.Add(person);
            }

            return people;
        }
    }
}
