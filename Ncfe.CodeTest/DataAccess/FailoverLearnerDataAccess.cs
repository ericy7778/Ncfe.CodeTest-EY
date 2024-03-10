using Ncfe.CodeTest.Contracts;
using Ncfe.CodeTest.Models;

namespace Ncfe.CodeTest.DataAccess
{
    public class FailoverLearnerDataAccess : ILoadLearnerService
    {
        public LoadLearnerResponse LoadLearner(int learnerId)
        {
            return new LoadLearnerResponse();               
        }

        public DataAccessType GetDataAccessType => DataAccessType.Failover;
    }
}
