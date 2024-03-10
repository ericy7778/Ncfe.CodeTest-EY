using Ncfe.CodeTest.Contracts;
using System;

namespace Ncfe.CodeTest.Services
{
    public class FailoverService : IFailoverService
    {
        private readonly IFailoverRepository _failoverRepository;
        private readonly IConfigService _configService;
        private const string AllowedFailoverAttemptsConfig = "AllowedFailoverAttempts";
        private const string FailoverTimePeriodConfig = "FailoverTimePeriod";
        private const string IsFailoverModeEnabledConfig= "IsFailoverModeEnabled";

        public FailoverService(IFailoverRepository failoverRepository, IConfigService configService)
        {
            _failoverRepository = failoverRepository;
            _configService = configService;
        }

        public bool IsFailoverActive()
        {
            var failoverTimePeriod = _configService.GetConfigInteger(FailoverTimePeriodConfig);
            var allowedFailoverAttempts = _configService.GetConfigInteger(AllowedFailoverAttemptsConfig);
            var failoverEnabled = _configService.GetConfigBoolean(IsFailoverModeEnabledConfig);
            var failoverEntriesCount = _failoverRepository.GetCountFailOverEntriesInLastTimePeriod(failoverTimePeriod);

            return failoverEntriesCount > allowedFailoverAttempts && failoverEnabled;
        }

        public void LogFailover(DateTime errorDateTime, string errorMessage)
        {
            _failoverRepository.LogFailover(errorDateTime, errorMessage);
        }

    }
}
