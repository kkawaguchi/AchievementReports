using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementReports
{
    interface IResultCommand<T> : ICommand
    {
        T GetResult();
    }
}
