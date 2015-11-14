using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementReports
{
    class MeetingInputSequence : ISequence
    {
        private MeetingRepository repo;

        public MeetingInputSequence(MeetingRepository repo)
        {
            this.repo = repo;
        }

        public void Start() 
        {
             InputMeetingCommand com = new InputMeetingCommand();
             com.Execute();
             var meetingInnput = com.GetResult();
             new RegisterMeetingCommand(MapMeeting(meetingInnput), repo).Execute();            
        }

        private Meeting MapMeeting(MeetingInput meetingInnput)
        {
            Meeting meeting = new Meeting();
            meeting.Date = (DateTime.Parse(meetingInnput.Date));
            meeting.Description = (meetingInnput.Description);
            meeting.DayDuty = (int.Parse(meetingInnput.DayDuty));
            return meeting;
        }
    }
}
