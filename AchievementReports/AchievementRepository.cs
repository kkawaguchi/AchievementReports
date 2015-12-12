using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AchievementReports
{
    public class AchievementRepository
    {
        private IDbConnection conn;
        public AchievementRepository(IDbConnection conn)
        {
            this.conn = conn;
        }

        public void Insert(Achievement achievement)
        {
            String sql;
            sql = "";
            sql = "INSERT INTO 実績(日付,人ID,実績分,会ID) VALUES(#" + achievement.date + "#," + achievement.personID + "," + achievement.time + "," + achievement.meetingID + ");";

            IDbCommand command = this.conn.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }
    }
}
