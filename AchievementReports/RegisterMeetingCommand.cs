using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementReports
{
    class RegisterMeetingCommand
    {
        Meeting meeting;
        MeetingRepository meetingRepository;
        public RegisterMeetingCommand(Meeting meeting, MeetingRepository meetingRepository)
        {
            this.meeting = meeting;
            this.meetingRepository = meetingRepository;
        }

        public void Execute()
        {
            meetingRepository.Insert(meeting);
        }
    }
}
