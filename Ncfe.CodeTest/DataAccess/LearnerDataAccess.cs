using Ncfe.CodeTest.Contracts;
using Ncfe.CodeTest.Models;

namespace Ncfe.CodeTest.DataAccess
{
    public class LearnerDataAccess : ILoadLearnerService
    {
        public LoadLearnerResponse LoadLearner(int learnerId)
        {
            // rettrieve learner from 3rd party webservice
            return new LoadLearnerResponse();
        }

        public DataAccessType GetDataAccessType => DataAccessType.Archived;
    }
}
