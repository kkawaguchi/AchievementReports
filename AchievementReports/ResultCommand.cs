using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementReports
{
    abstract class ResultCommand<T> : IResultCommand<T>
    {
        protected T result { private get; set; }

        public T GetResult()
        {
            return result;
        }

        public abstract void Execute();
    }
}
