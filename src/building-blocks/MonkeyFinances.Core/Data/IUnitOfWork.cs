namespace MonkeyFinances.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}