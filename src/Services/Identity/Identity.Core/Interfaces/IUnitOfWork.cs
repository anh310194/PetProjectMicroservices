
using Identity.Core.Models;

namespace Identity.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    int SaveChanges();
    IRepository<User> UserRepository { get; }
    IRepository<Role> RoleRepository { get; }
    IRepository<Feature> FeatureRepository { get; }
    IRepository<RoleFeature> RoleFeatureRepository { get; }
    IRepository<Tenant> TenantRepository { get; }
}