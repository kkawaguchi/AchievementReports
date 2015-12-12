using AchievementReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.OleDb;

namespace AchievementReportsTest
{
    [TestClass()]
    public class PeopleRepositoryTest
    {
        private IDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source = C:\Users\CIS\Desktop\AchievementReports\AchievementReports\App_Data\勉強会実績.mdb");

        [TestMethod()]
        public void TestGetAll()
        {
            conn.Open();
            PeopleRepository target = new PeopleRepository(conn);
            List<Person> people = target.GetAll();

            conn.Close();

            Person actual = people.Find(p => p.personID == 1);
            Person expected = new Person() { personID = 1, kana = "ｷﾀﾑﾗ", name = "北村" };

            Assert.AreEqual(actual.personID, expected.personID);
            Assert.AreEqual(actual.kana, expected.kana);
            Assert.AreEqual(actual.name, expected.name);
       }
    }
}
