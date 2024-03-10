using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using Ncfe.CodeTest.Services;
using Ncfe.CodeTest.Contracts;

namespace Ncfe.CodeTest.Config.Tests
{
    [TestClass]
    public class ConfigServiceTests_GetConfigString
    {
        private IConfigService _configService;
        private string _appSetting;

        [TestInitialize]
        public void TestInitalise()
        {
            _configService = new ConfigService();
            _appSetting = "testAppSetting";
            ConfigurationManager.AppSettings["testAppSetting"] = "true";
        }

        [TestMethod]
        public void GetConfigString_WhenAppSettingIsNotFound_ExpectApplicationException()
        {
            ConfigurationManager.AppSettings["testAppSetting"] = null;
            var ex = Assert.ThrowsException<ApplicationException>(() => Act());
            Assert.AreEqual($"{_appSetting} does not exist.", ex.Message);
        }

        [TestMethod]
        public void GetConfigString_WhenAppSettingIsFound_ExpectSuccess()
        {
            var actual = Act();
            Assert.AreEqual("true", actual);
        }

        private string Act()
        {
            return _configService.GetConfigString(_appSetting);
        }
    }
}
