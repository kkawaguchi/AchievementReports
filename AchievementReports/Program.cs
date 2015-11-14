﻿using System;
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
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source = C:\川口\社内\勉強会\勉強会実績.mdb";

            if (descripshon != "" || dayDuty != "")
            {
                Meeting meeting = new Meeting();
                meeting.Date = (DateTime.Parse(date));
                meeting.Description = (descripshon);
                meeting.DayDuty = (int.Parse(dayDuty));

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

                    Console.WriteLine(m.MeetingId + "-" + m.Date.ToString() + "-" + m.Description + "-" + m.DayDuty);
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
                Console.WriteLine(p.PersonId + "-" + p.Name + "-" + p.Kana);
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
                achivement.PersonId = l;
                achivement.Date = DateTime.Parse(date);
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
