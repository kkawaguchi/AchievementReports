using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementReports
{
    class InputMeetingCommand : InputCommand<MeetingInput>
    {
        public override void Execute()
        {
            var input = new MeetingInput();
            input.Description = GetStringFromConsole("内容：");
            input.DayDuty = GetStringFromConsole("日直：");
            input.Date = GetStringFromConsole("日付：");
            result = input;
        }
    }
}
