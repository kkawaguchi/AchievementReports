using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AchievementReports
{
    public class Person
    {
        public string name { get; set; }
        public string kana { get; set; }
        public int personID { get; set; }
    }

    public class Meeting
    {
        public string descripshon { get; set; }
        public int dayDuty { get; set; }
        public DateTime date { get; set; }
        public int meetingID { get; set; }
    }

    public class Achievement
    {
        public int achievementID { get; set; }
        public int personID { get; set; }
        public int meetingID { get; set; }
        public DateTime date { get; set; }
        public int time { get; set; }
    }
}
