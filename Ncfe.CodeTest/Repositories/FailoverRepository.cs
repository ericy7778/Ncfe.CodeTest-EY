using Ncfe.CodeTest.Contracts;
using Ncfe.CodeTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ncfe.CodeTest.Repositories
{
    public class FailoverRepository : IFailoverRepository
    {
        private List<FailoverEntry> failoverEntries;
        public FailoverRepository()
        {
            failoverEntries = new List<FailoverEntry>();
        }
        public int GetCountFailOverEntriesInLastTimePeriod(int minutes)
        {
            return failoverEntries
                .Where(f => f.ErrorDateTime > DateTime.Now.AddMinutes(-minutes))
                .Count();
        }

        public void LogFailover(DateTime errorDateTime, string errorMessage)
        {
            failoverEntries.Add(new FailoverEntry(Guid.NewGuid(), errorDateTime, errorMessage));       
        }
    }
}
