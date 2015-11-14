using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementReports
{
    class GetMeetingListCommand : ICommand
    {
        MeetingRepository _repo;
        IEnumerable<Meeting> _meeting;
        public GetMeetingListCommand(MeetingRepository repo)
        {
            _repo = repo;
        }

        public void Execute()
        {
            _meeting = _repo.GetAll();
        }

        public IEnumerable<Meeting> GetResult()
        {
            return _meeting;
        }

    }
}
