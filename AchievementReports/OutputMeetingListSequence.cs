using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementReports
{
    class OutputMeetingListSequence : ISequence
    {
        MeetingRepository _repo;
        public OutputMeetingListSequence(MeetingRepository repo)
        {
            _repo = repo;
        }
        public void Start()
        {
            var com = new GetMeetingListCommand(_repo);
            com.Execute();

            var output = new MeetingListOutput(com.GetResult());
            output.Show();
        }
    }
}
