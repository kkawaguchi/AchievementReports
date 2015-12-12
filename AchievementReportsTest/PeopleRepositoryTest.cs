using AchievementReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;

namespace AchievementReportsTest
{
    [TestClass()]
    public class PeopleRepositoryTest
    {
        private IDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source = C:\Temp\db.mdb");

        [TestMethod()]
        public void TestGetAll()
        {
            File.Copy(@"C:\Users\CIS\Desktop\AchievementReports\AchievementReportsTest\db.mdb", @"C:\Temp\db.mdb",true);
            List<Person> expected = new List<Person>();
            expected.Add(new Person() { personID = 1, kana = "ｶﾅ1", name = "名前1" });
            expected.Add(new Person() { personID = 2, kana = "ｶﾅ2", name = "名前2" });
            expected.Add(new Person() { personID = 3, kana = "ｶﾅ3", name = "名前3" });

            string sql;
            IDbCommand command = this.conn.CreateCommand();
            conn.Open();
            

            int i = 0;

            foreach (Person p in expected)
            {
                sql = "INSERT INTO 人(人ID,名前,カナ) VALUES('" + expected[i].personID + "','" + expected[i].name + "','" + expected[i].kana + "');";
                command.CommandText = sql;
                command.ExecuteNonQuery();
                i++;
            }

            PeopleRepository target = new PeopleRepository(conn);
            List<Person> people = target.GetAll();

            conn.Close();

            //Person actual = people.Find(p => p.personID == 1);
            i = 0;
            foreach (Person actual in people)
            {
                Assert.AreEqual(actual.personID, expected[i].personID);
                Assert.AreEqual(actual.name, expected[i].name);
                Assert.AreEqual(actual.kana, expected[i].kana);
                i++;
            }
       }
    }
}
