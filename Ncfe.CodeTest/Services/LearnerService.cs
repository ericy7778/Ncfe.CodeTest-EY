using Ncfe.CodeTest.Contracts;
using Ncfe.CodeTest.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ncfe.CodeTest.Services
{
    public class LearnerService : ILearnerService
    {
        private IFailoverService _failoverService;
        private IEnumerable<ILoadLearnerService> _loadLearnerServices;

        public LearnerService(IFailoverService failoverService,
            IEnumerable<ILoadLearnerService> loadLearnerServices)
        {
            _failoverService = failoverService;
            _loadLearnerServices = loadLearnerServices;
        }

        public Learner GetLearner(int learnerId, bool isLearnerArchived)
        {
            var response = GetLoadLearnerServiceStrategy(isLearnerArchived, _failoverService.IsFailoverActive())
                           .LoadLearner(learnerId);

            return response.Learner;
        }

        private ILoadLearnerService GetLoadLearnerServiceStrategy(bool isArchived, bool isFailOverEnabled)
        {
            if (isArchived) return _loadLearnerServices.FirstOrDefault(x => x.GetDataAccessType == Models.DataAccessType.Archived);

            if (isFailOverEnabled) return _loadLearnerServices.FirstOrDefault(x => x.GetDataAccessType == Models.DataAccessType.Failover);

            return _loadLearnerServices.FirstOrDefault(x => x.GetDataAccessType == Models.DataAccessType.Learner);
        }

    }
}
