using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementReports
{
    class Achievement
    {
        public int AchievementId { get; set; }
        public int PersonId { get; set; }
        public int MeetingId { get; set; }
        public DateTime Date { get; set; }
        public int Time { get; set; }
    }
}
