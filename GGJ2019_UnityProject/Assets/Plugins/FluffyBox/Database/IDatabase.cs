namespace FluffyBox
{
    public interface IDatabase
    {
        DataElement TryGetValue(string name);

        bool TryGetValue(string name, out DataElement element);
    }
}