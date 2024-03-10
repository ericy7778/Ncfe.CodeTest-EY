using System;

namespace Ncfe.CodeTest.Contracts
{
    public interface IFailoverRepository
    {
        int GetCountFailOverEntriesInLastTimePeriod(int minutes);
        void LogFailover(DateTime errorDateTime, string errorMessage);
    }
}