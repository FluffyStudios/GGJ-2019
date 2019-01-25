namespace FluffyBox
{
    public interface IDatabaseService : IService
    {
        IDatabase GetDatabase<T>();

        bool TryGetDatabase<T>(out IDatabase database);
    }
}
