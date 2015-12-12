using AchievementReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Data.OleDb;

namespace AchievementReportsTest
{
    
    
    /// <summary>
    ///MeetingRepositoryTest のテスト クラスです。すべての
    ///MeetingRepositoryTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class MeetingRepositoryTest
    {
        private IDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source = C:\Users\CIS\Desktop\AchievementReports\AchievementReports\App_Data\勉強会実績.mdb");
        [TestMethod()]
        public void MeetingRepositoryInsertTest()
        {

            MeetingRepository target = new MeetingRepository(conn);
            Meeting meeting = new Meeting();
            meeting.date = (DateTime.Parse("2015/1/1"));
            meeting.descripshon = ("内容");
            meeting.dayDuty = (int.Parse("1"));

            target.Insert(meeting);
        }
    }
}
