using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AchievementReports
{
    public class MeetingRepository
    {
        private IDbConnection conn;
        public MeetingRepository(IDbConnection conn)
        {
            this.conn = conn;
        }

        public void Insert(Meeting meeting)
        {
            String sql;
            sql = "";
            sql = "INSERT INTO 会(日付,内容,司会者) VALUES(#" + meeting.date + "#,'" + meeting.descripshon + "'," + meeting.dayDuty + ");";

            IDbCommand command = this.conn.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }

        public Meeting Get(int id)
        {
            string sql;

            sql = "";
            sql = "SELECT 日付,内容,日直 FROM 会 WHERE 会ID = " + id + ";";

            IDbCommand command = this.conn.CreateCommand();
            command.CommandText = sql;
            IDataReader reader = command.ExecuteReader();
            Meeting meeting = null;

            if (reader.Read())
            {
                meeting = new Meeting();
                meeting.date = (reader.GetDateTime(0));
                meeting.descripshon = (reader.GetString(1));
                meeting.dayDuty = (reader.GetInt32(2));
            }

            return meeting;
        }

        public int GetNotID(Meeting m)
        {
            string sql;
            int meetingID;
            meetingID = 0;

            sql = "";
            sql = "SELECT 会ID FROM 会 WHERE 内容 = '" + m.descripshon + "' AND 司会者 = " + m.dayDuty + " AND 日付 = #" + m.date + "#;";

            IDbCommand command = this.conn.CreateCommand();
            command.CommandText = sql;
            IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                meetingID = (reader.GetInt32(0));
            }

            return meetingID;
        }

        public List<Meeting> GetAll()
        {
            string sql;

            sql = "";
            sql = "SELECT * FROM 会 ;";

            IDbCommand command = this.conn.CreateCommand();
            command.CommandText = sql;
            IDataReader reader = command.ExecuteReader();
            List<Meeting> meeting = new List<Meeting>();

            while (reader.Read())
            {
                Meeting m = new Meeting();
                m.meetingID = (reader.GetInt32(0));
                m.date = (reader.GetDateTime(1));
                if (reader["内容"] == DBNull.Value)
                {
                    m.descripshon = "";
                }
                else
                {
                    m.descripshon = (reader.GetString(2));
                }

                if (reader["司会者"] == DBNull.Value)
                {
                    m.dayDuty = 0;
                }
                else
                {
                    m.dayDuty = (reader.GetInt32(3));
                }


                meeting.Add(m);
            }

            return meeting;
        }
    }
}
