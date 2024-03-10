using Ncfe.CodeTest.Models;

namespace Ncfe.CodeTest.Contracts
{
    public interface ILoadLearnerService
    {
        LoadLearnerResponse LoadLearner(int learnerId);

        DataAccessType GetDataAccessType { get; }
    }
}