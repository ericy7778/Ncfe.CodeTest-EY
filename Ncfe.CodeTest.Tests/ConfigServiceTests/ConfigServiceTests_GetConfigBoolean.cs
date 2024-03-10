using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using Ncfe.CodeTest.Services;
using Ncfe.CodeTest.Contracts;

namespace Ncfe.CodeTest.Config.Tests
{
    [TestClass]
    public class ConfigServiceTests_GetConfigBoolean
    {
        private IConfigService _configService;
        private string _appSettingName;

        [TestInitialize]
        public void TestInitalise()
        {
            _configService = new ConfigService();
            _appSettingName = "testAppSetting";
            ConfigurationManager.AppSettings["testAppSetting"] = "true";
        }

        [TestMethod]
        public void GetConfigBoolean_WhenAppSettingIsNotFound_ExpectApplicationException()
        {
            ConfigurationManager.AppSettings["testAppSetting"] = null;
            var ex = Assert.ThrowsException<ApplicationException>(() => Act());
            Assert.AreEqual($"{_appSettingName} does not exist.", ex.Message);
        }

        [TestMethod]
        public void GetConfigBoolean_WhenAppSettingIsNotABoolean_ExpectApplicationException()
        {
            ConfigurationManager.AppSettings["testAppSetting"] = "123";
            var ex = Assert.ThrowsException<ApplicationException>(() => Act());
            Assert.AreEqual($"{_appSettingName} failed to parse to bool.", ex.Message);
        }

        [TestMethod]
        public void GetConfigBoolean_WhenAppSettingIsFoundAndValid_ExpectSuccess()
        {
            var actual = Act();
            Assert.AreEqual(true, actual);
        }

        private bool Act()
        {
            return _configService.GetConfigBoolean(_appSettingName);
        }
    }
}
