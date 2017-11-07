namespace LittleSocialNetwork.Services.Services
{
    public interface IHashingService
    {
        string Hash(string data);

        bool VerifyHashed(string hashed, string data);
    }
}
