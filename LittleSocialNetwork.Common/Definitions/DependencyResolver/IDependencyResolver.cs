namespace LittleSocialNetwork.Common.Definitions.DependencyResolver
{
    public interface IDependencyResolver
    {
        TService GetService<TService>();
    }
}