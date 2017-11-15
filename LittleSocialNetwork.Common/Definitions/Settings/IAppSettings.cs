namespace LittleSocialNetwork.Common.Definitions.Settings
{
    public interface IAppSettings
    {
        AuthenticationSettings AuthenticationSettings { get; }  
        DatabaseSettings DatabaseSettings { get; }
        EmailSettings EmailSettings { get; }
        ChatSettings ChatSettings { get; }

        string APPLICATION_ROOT { get; }
    }
}