using System;

namespace Ncfe.CodeTest.Contracts
{
    public interface IFailoverService
    {
        bool IsFailoverActive();
        void LogFailover(DateTime errorDateTime, string errorMessage);
    }
}