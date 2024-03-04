namespace Core.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    int SaveChanges();
    IRepository<Country> CountryRepository { get; }
    IRepository<State> StateRepository { get; }
}