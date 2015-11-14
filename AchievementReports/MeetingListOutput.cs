using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementReports
{
    class MeetingListOutput:IOutput
    {
        IEnumerable<Meeting> _meetings;
        public MeetingListOutput(IEnumerable<Meeting> meetings)
        {
            _meetings = meetings;
        }
        public void Show()
        {
            foreach (Meeting m in _meetings)
            {

                Console.WriteLine(m.MeetingId + "-" + m.Date.ToString() + "-" + m.Description + "-" + m.DayDuty);
            }
        }
    }
}
