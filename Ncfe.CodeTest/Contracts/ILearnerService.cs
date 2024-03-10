using Ncfe.CodeTest.Models;

namespace Ncfe.CodeTest.Contracts
{
    public interface ILearnerService
    {
        Learner GetLearner(int learnerId, bool isLearnerArchived);
    }
}