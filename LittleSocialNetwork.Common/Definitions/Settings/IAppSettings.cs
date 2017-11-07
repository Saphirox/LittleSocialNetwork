namespace LittleSocialNetwork.Common.Definitions.Settings
{
    public interface IAppSettings
    {
        AuthenticationSettings AuthenticationSettings { get; }  
        DatabaseSettings DatabaseSettings { get; }

        string APPLICATION_ROOT { get; }
    }
}