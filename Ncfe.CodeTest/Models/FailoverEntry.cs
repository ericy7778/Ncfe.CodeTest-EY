using System;

namespace Ncfe.CodeTest.Models
{
    public class FailoverEntry
    {
        public FailoverEntry(
            Guid id,
            DateTime errorDateTime, 
            string errorMessage)
        {
            Id = id;
            ErrorDateTime = errorDateTime;
            ErrorMessage = errorMessage;
        }

        public Guid Id { get; }
        public DateTime ErrorDateTime { get; }
        public string ErrorMessage { get; }
    }
}
