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
            /* TODO:以下を実装
             * 実績登録
             * 実績出力
             * 実績削除
             * 人出力
             * 人登録
             * 人削除
             * 会登録
             * 会表示
             * 会削除
             * */

            IDbConnection conn = GetConnection();

            conn.Open();
            MeetingRepository meetingRepository = new MeetingRepository(conn);
            MeetingInputSequence meetingInputSequence = new MeetingInputSequence(meetingRepository);
            meetingInputSequence.Start();
            conn.Close();

            conn.Open();
            meetingRepository = new MeetingRepository(conn);
            OutputMeetingListSequence outputMeetingListSequence = new OutputMeetingListSequence(meetingRepository);
            outputMeetingListSequence.Start();
            conn.Close();

            Console.Write("会ID：");
            string meetingID = Console.ReadLine();
            Console.WriteLine("");

            conn.Open();

            PeopleRepository peopleRepository = new PeopleRepository(conn);
            IEnumerable<Person> people = peopleRepository.GetAll();

            foreach (Person p in people)
            {
                Console.WriteLine(p.PersonId + "-" + p.Name + "-" + p.Kana);
            }

            conn.Close();

            Console.Write("参加者(カンマ区切り)：");
            string InParticipant = Console.ReadLine();
            Console.WriteLine("");

            Console.Write("実績時間(分)：");
            string time = Console.ReadLine();
            Console.WriteLine("");

            Participant participant = new Participant(InParticipant);
            List<int> particiantList = participant.CreateParticiantList();
            Achievement achivement = null;

            foreach (int l in particiantList)
            {
                achivement = new Achievement();
                achivement.PersonId = l;
                //achivement.Date = DateTime.Parse(date);
                if (meetingID == "")
                {
                    achivement.MeetingId = 0;
                }
                else
                {
                    achivement.MeetingId = int.Parse(meetingID);
                }
                achivement.Time = int.Parse(time);

                conn.Open();

                AchievementRepository achivementRepository = new AchievementRepository(conn);
                achivementRepository.Insert(achivement);

                conn.Close();
            }
        }

        public static IDbConnection GetConnection()
        {
            IDbConnection conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source = C:\川口\社内\勉強会\勉強会実績.mdb";
            return conn;
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
}
