namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    int SaveChanges();
    IRepository<Feature> FeatureRepository { get; }
    IRepository<Role> RoleRepository { get; }
    IRepository<User> UserRepository { get; }
}