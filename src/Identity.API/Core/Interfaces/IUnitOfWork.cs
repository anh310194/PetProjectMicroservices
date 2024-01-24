namespace Identity.API.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    int SaveChanges();
    TResult ExecuteTransaction<TResult>(Func<TResult> func) where TResult : class;
    Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func) where TResult : class;
    void ExecuteTransaction(Action action);
    Task ExecuteTransactionAsync(Func<Task> action);
    IRepository<Country> CountryRepository { get; }
    IRepository<Feature> FeatureRepository { get; }
    IRepository<Role> RoleRepository { get; }
    IRepository<State> StateRepository { get; }
    IRepository<User> UserRepository { get; }
}