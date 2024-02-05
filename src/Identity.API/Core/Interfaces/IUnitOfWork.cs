namespace Identity.API.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    int SaveChanges();
    IRepository<Country> CountryRepository { get; }
    IRepository<Feature> FeatureRepository { get; }
    IRepository<Role> RoleRepository { get; }
    IRepository<State> StateRepository { get; }
    IRepository<User> UserRepository { get; }
}