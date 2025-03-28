namespace ServiceLayer.Setups.RepositoryInterfaces
{
    public interface UnitOfWork
    {
        Task SaveAllChangesAsync();
        void SaveAllChanges();
        Task BeginTransaction();
        Task Commit();
        Task CommitPartial();
    }
}
