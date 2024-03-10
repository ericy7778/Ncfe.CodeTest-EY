using Microsoft.Extensions.DependencyInjection;
using Ncfe.CodeTest.Contracts;
using Ncfe.CodeTest.DataAccess;
using Ncfe.CodeTest.Repositories;
using Ncfe.CodeTest.Services;
using System;

namespace Ncfe.CodeTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IServiceProvider serviceProvider = BuildServices();

            var service = serviceProvider.GetService<ILearnerService>();
            //Example call
            service.GetLearner(1, false);

        }

        private static ServiceProvider BuildServices()
        {
            return new ServiceCollection()
                .AddSingleton<ILearnerService, LearnerService>()
                .AddSingleton<IFailoverService, FailoverService>()
                .AddSingleton<IFailoverRepository, FailoverRepository>()
                .AddSingleton<IConfigService, ConfigService>()
                .AddTransient<ILoadLearnerService, LearnerDataAccess>()
                .AddTransient<ILoadLearnerService, ArchivedDataService>()
                .AddTransient<ILoadLearnerService, FailoverLearnerDataAccess>()
                .BuildServiceProvider();
        }
    }
}
