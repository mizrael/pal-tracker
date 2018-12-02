namespace PalTracker
{
    public interface ITimeEntryRepositoryFactory
    {
        ITimeEntryRepository Create();
    }
}