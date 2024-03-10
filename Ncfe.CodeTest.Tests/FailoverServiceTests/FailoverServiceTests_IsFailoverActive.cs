using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ncfe.CodeTest.Contracts;
using Ncfe.CodeTest.Services;

namespace Ncfe.CodeTest.Failover.Tests
{
    [TestClass]
    public class FailoverServiceTests_IsFailoverActive
    {
        private Mock<IConfigService> _configService { get; set; }
        private Mock<IFailoverRepository> _failoverRepository { get; set; }
        private FailoverService _failoverService { get; set; }

        private int _allowedFailoverAttempts;
        private int _failOverEntriesInLastTimePeriod;
        private int _failoverTimePeriod;
        private bool _failoverEnabled;

        [TestInitialize]
        public void TestInitialise()
        {
            _allowedFailoverAttempts = 10;
            _failOverEntriesInLastTimePeriod = 5;
            _failoverTimePeriod = 10;
            _failoverEnabled = false;

            _configService = new Mock<IConfigService>();
            _failoverRepository = new Mock<IFailoverRepository>();
            _failoverService = new FailoverService(_failoverRepository.Object, _configService.Object);
        }

        [TestMethod]
        public void IsFailoverActive_FailoverEntriesGreaterThanAllowedAndFailoverEnabled_ReturnsTrue()
        {
            // Arrange
            _allowedFailoverAttempts = 5;
            _failoverEnabled = true;
            _failOverEntriesInLastTimePeriod = 10;
            Stub();

            // Act
            var result = Act();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsFailoverActive_FailoverEntriesGreaterThanAllowedAndFailoverDisabled_ReturnsFalse()
        {
            // Arrange
            _allowedFailoverAttempts = 5;
            _failoverEnabled = false;
            _failOverEntriesInLastTimePeriod = 10;
            Stub();

            // Act
            var result = Act();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsFailoverActive_FailoverEntriesLessThanAllowedAndFailoverEnabled_ReturnsFalse()
        {
            // Arrange
            _allowedFailoverAttempts = 10;
            _failoverEnabled = false;
            _failOverEntriesInLastTimePeriod = 5;
            Stub();

            // Act
            var result = Act();

            // Assert
            Assert.IsFalse(result);
        }

        private void Stub()
        {
            _configService.Setup(config => config.GetConfigInteger("AllowedFailoverAttempts")).Returns(_allowedFailoverAttempts);
            _configService.Setup(config => config.GetConfigBoolean("IsFailoverModeEnabled")).Returns(_failoverEnabled);
            _configService.Setup(config => config.GetConfigInteger("FailoverTimePeriod")).Returns(_failoverTimePeriod);
            _failoverRepository.Setup(repository => repository.GetCountFailOverEntriesInLastTimePeriod(It.IsAny<int>())).Returns(_failOverEntriesInLastTimePeriod);
        }

        private bool Act()
        {
            return _failoverService.IsFailoverActive();
        }
    }
}