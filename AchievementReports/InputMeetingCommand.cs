using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementReports
{
    class InputMeetingCommand : ICommand
    {
        private MeetingInput _result;

        public void Execute()
        {
            var input = new MeetingInput();
            input.Description = GetStringFromConsole("内容：");
            input.DayDuty = GetStringFromConsole("日直：");
            input.Date = GetStringFromConsole("日付：");
            _result = input;
        }

        private static string GetStringFromConsole(string displayText)
        {
            Console.Write(displayText);                                               //内容入力
            string inputText =  Console.ReadLine();
            Console.WriteLine("");
            return inputText;
        }
        public MeetingInput GetResult()
        {
            return _result;
        }
    }
}
