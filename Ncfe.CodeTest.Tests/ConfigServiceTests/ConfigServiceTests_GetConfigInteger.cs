using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using Ncfe.CodeTest.Services;
using Ncfe.CodeTest.Contracts;

namespace Ncfe.CodeTest.Config.Tests
{
    [TestClass]
    public class ConfigServiceTests_GetConfigInteger
    {
        private IConfigService _configService;
        private string _appSettingName;

        [TestInitialize]
        public void TestInitalise()
        {
            _configService = new ConfigService();
            _appSettingName = "testAppSetting";
            ConfigurationManager.AppSettings["testAppSetting"] = "1";
        }

        [TestMethod]
        public void GetConfigInteger_WhenAppSettingIsNotFound_ExpectApplicationException()
        {
            ConfigurationManager.AppSettings["testAppSetting"] = null;
            var ex = Assert.ThrowsException<ApplicationException>(() => Act());
            Assert.AreEqual($"{_appSettingName} does not exist.", ex.Message);
        }

        [TestMethod]
        public void GetConfigInteger_WhenAppSettingIsNotAnInteger_ExpectApplicationException()
        {
            ConfigurationManager.AppSettings["testAppSetting"] = "abc";
            var ex = Assert.ThrowsException<ApplicationException>(() => Act());
            Assert.AreEqual($"{_appSettingName} failed to parse to integer.", ex.Message);
        }

        [TestMethod]
        public void GetConfigInteger_WhenAppSettingIsFoundAndValid_ExpectSuccess()
        {
            var actual = Act();
            Assert.AreEqual(1, actual);
        }

        private int Act()
        {
            return _configService.GetConfigInteger(_appSettingName);
        }
    }
}
