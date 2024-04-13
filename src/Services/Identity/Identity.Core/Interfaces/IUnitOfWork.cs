
using Identity.Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Identity.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    int SaveChanges();
    IRepository<User> UserRepository { get; }
    IRepository<Role> RoleRepository { get; }
    IRepository<Feature> FeatureRepository { get; }
    IRepository<RoleFeature> RoleFeatureRepository { get; }
    ValueTask<EntityEntry<T>> InsertAsync<T>(T model, int userId) where T : BaseEntity;
    T Insert<T>(T model, int userId) where T : BaseEntity;
}