namespace Ncfe.CodeTest.Contracts
{
    public interface IConfigService
    {
        bool GetConfigBoolean(string appSettingName);
        int GetConfigInteger(string appSettingName);
        string GetConfigString(string appSettingName);
    }
}