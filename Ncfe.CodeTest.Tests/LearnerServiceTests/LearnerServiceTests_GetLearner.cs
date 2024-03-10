using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ncfe.CodeTest.Contracts;
using Ncfe.CodeTest.Models;
using Ncfe.CodeTest.Services;

namespace Ncfe.CodeTest.Tests
{
    [TestClass]
    public class LearnerServiceTests_GetLearner
    {
        private Mock<ILoadLearnerService> _archivedDataService;
        private Mock<ILoadLearnerService> _failoverDataService;
        private Mock<ILoadLearnerService> _learnerDataService;
        private Mock<IFailoverService> _failoverService;
        private LearnerService _learnerService;

        private bool _isFailOverActive;
        private int _learnerId;
        private bool _isLearnerArchived;
        private LoadLearnerResponse _loadLearnerResponse;

        [TestInitialize]
        public void TestInitialize()
        {
            _archivedDataService = new Mock<ILoadLearnerService>();
            _failoverDataService = new Mock<ILoadLearnerService>();
            _learnerDataService = new Mock<ILoadLearnerService>();
            _failoverService = new Mock<IFailoverService>();

            _learnerService = new LearnerService(_failoverService.Object, new List<ILoadLearnerService>
            {
                _archivedDataService.Object,
                _failoverDataService.Object,
                _learnerDataService.Object
            });

            _isFailOverActive = false;
            _learnerId = 1;
            _isLearnerArchived = false;

            _loadLearnerResponse = new LoadLearnerResponse()
            {
                Learner = new Learner()
            };
        }

        [TestMethod]
        public void GetLearner_LearnerIsArchived_CallsArchivedDataService()
        {
            // Setup
            _isLearnerArchived = true;
            Stub();

            var learner = Act();

            // Assert
            _archivedDataService.Verify(x => x.LoadLearner(_learnerId), Times.Once);
        }

        [TestMethod]
        public void GetLearner_FailoverEnabledAndLearnerNotArchived_CallsFailoverDataAccess()
        {
            // Setup
            _isFailOverActive = true;
            Stub();

            var learner = Act();

            // Assert
            _failoverDataService.Verify(x => x.LoadLearner(_learnerId), Times.Once);
        }


        [TestMethod]
        public void GetLearner_FailoverDisabledAndLearnerNotArchived_CallsLearnerDataAccess()
        {
            Stub();
            var learner = Act();

            // Assert
            _learnerDataService.Verify(x => x.LoadLearner(_learnerId), Times.Once);
        }


        private void Stub()
        {
            _archivedDataService.Setup(x => x.GetDataAccessType).Returns(DataAccessType.Archived);
            _archivedDataService.Setup(x => x.LoadLearner(It.IsAny<int>())).Returns(_loadLearnerResponse);

            _failoverDataService.Setup(x => x.GetDataAccessType).Returns(DataAccessType.Failover);
            _failoverDataService.Setup(x => x.LoadLearner(It.IsAny<int>())).Returns(_loadLearnerResponse);

            _learnerDataService.Setup(x => x.GetDataAccessType).Returns(DataAccessType.Learner);
            _learnerDataService.Setup(x => x.LoadLearner(It.IsAny<int>())).Returns(_loadLearnerResponse);

            _failoverService.Setup(x => x.IsFailoverActive()).Returns(_isFailOverActive);
        }

        private Learner Act()
        {
            return _learnerService.GetLearner(_learnerId, _isLearnerArchived);
        }
    }
}
