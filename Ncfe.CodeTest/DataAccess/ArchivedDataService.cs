using Ncfe.CodeTest.Contracts;
using Ncfe.CodeTest.Models;

namespace Ncfe.CodeTest.DataAccess
{
    public class ArchivedDataService : ILoadLearnerService
    {
        public LoadLearnerResponse LoadLearner(int learnerId)
        {
            // retrieve learner from archive data service
            return new LoadLearnerResponse();
        }

        public DataAccessType GetDataAccessType => DataAccessType.Archived;
    }
}
