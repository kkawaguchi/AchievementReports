using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementReports
{
    abstract class InputCommand<T> : ResultCommand<T>
    {
        protected static string GetStringFromConsole(string displayText)
        {
            Console.Write(displayText);
            string inputText =  Console.ReadLine();
            Console.WriteLine("");
            return inputText;
        }
    }
}
