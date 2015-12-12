using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementReports
{
    class Program
    {
        static void Main(string[] args)
        {
            string descripshon;                                                    //内容
            string dayDuty;                                                        //日直
            string date;
            string InParticipant;                                                    //参加者
            string time;
            string meetingID;


            Console.Write("内容：");                                               //内容入力
            descripshon = Console.ReadLine();
            Console.WriteLine("");

            Console.Write("日直：");                                               //日直入力
            dayDuty = Console.ReadLine();
            Console.WriteLine("");

            Console.Write("日付：");
            date = Console.ReadLine();
            Console.WriteLine("");

            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source = 勉強会実績.mdb";

            if (descripshon != "" || dayDuty != "")
            {
                Meeting meeting = new Meeting();
                meeting.date = (DateTime.Parse(date));
                meeting.descripshon = (descripshon);
                meeting.dayDuty = (int.Parse(dayDuty));

                conn.Open();

                MeetingRepository meetingRepository = new MeetingRepository(conn);
                meetingRepository.Insert(meeting);

                meetingID = meetingRepository.GetNotID(meeting).ToString();

                conn.Close();
            }
            else
            {
                conn.Open();

                MeetingRepository meetingRepository = new MeetingRepository(conn);
                IEnumerable<Meeting> meeting = meetingRepository.GetAll();

                foreach (Meeting m in meeting)
                {

                    Console.WriteLine(m.meetingID + "-" + m.date.ToString() + "-" + m.descripshon + "-" + m.dayDuty);
                }

                conn.Close();

                Console.Write("会ID：");
                meetingID = Console.ReadLine();
                Console.WriteLine("");
            }

            conn.Open();

            PeopleRepository peopleRepository = new PeopleRepository(conn);
            IEnumerable<Person> people = peopleRepository.GetAll();

            foreach (Person p in people)
            {
                Console.WriteLine(p.personID + "-" + p.name + "-" + p.kana);
            }

            conn.Close();

            Console.Write("参加者(カンマ区切り)：");
            InParticipant = Console.ReadLine();
            Console.WriteLine("");

            Console.Write("実績時間(分)：");
            time = Console.ReadLine();
            Console.WriteLine("");

            Participant participant = new Participant(InParticipant);
            List<int> particiantList = participant.CreateParticiantList();
            Achievement achivement = null;

            foreach (int l in particiantList)
            {
                achivement = new Achievement();
                achivement.personID = l;
                achivement.date = DateTime.Parse(date);
                if (meetingID == "")
                {
                    achivement.meetingID = 0;
                }
                else
                {
                    achivement.meetingID = int.Parse(meetingID);
                }
                achivement.time = int.Parse(time);

                conn.Open();

                AchievementRepository achivementRepository = new AchievementRepository(conn);
                achivementRepository.Insert(achivement);

                conn.Close();
            }
        }
    }

    class MeetingRepository
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

    class AchievementRepository
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

    public class Person
    {
        public string name { get; set; }
        public string kana { get; set; }
        public int personID { get; set; }
    }

    class Meeting
    {
        public string descripshon { get; set; }
        public int dayDuty { get; set; }
        public DateTime date { get; set; }
        public int meetingID { get; set; }
    }

    class Achievement
    {
        public int achievementID { get; set; }
        public int personID { get; set; }
        public int meetingID { get; set; }
        public DateTime date { get; set; }
        public int time { get; set; }
    }

    //人IDの文字列をもらい、リストにして返す。
    class Participant
    {
        private string participant;

        public Participant(string participant)
        {
            this.participant = participant;
        }

        public List<int> CreateParticiantList()
        {
            string[] p = this.participant.Split(',');
            List<int> particiantList = new List<int>();

            foreach (string personID in p)
            {
                particiantList.Add(int.Parse(personID));
            }

            return particiantList;
        }
    }
}
