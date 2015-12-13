using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementReports
{
    class GetMeetingListCommand : ResultCommand<IEnumerable<Meeting>>
    {
        MeetingRepository _repo;
        public GetMeetingListCommand(MeetingRepository repo)
        {
            _repo = repo;
        }

        public override void Execute()
        {
            result = _repo.GetAll();
        }
    }
}
