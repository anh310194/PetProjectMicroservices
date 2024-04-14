using Identity.Core.Interfaces;
using Identity.Core.Models;
using Identity.Infrastructure.Persistence;

namespace Identity.Infrastructure.Repositories;

public class UnitOfWork(IdentityContext context) : IUnitOfWork
{
    private Dictionary<string, dynamic> repositories = new Dictionary<string, dynamic>();
    private bool disposed = false;

    public IRepository<Tenant> TenantRepository
    {
        get
        {
            return GetRepository<Tenant>();
        }
    }

    public IRepository<User> UserRepository
    {
        get
        {
            return GetRepository<User>();
        }
    }

    public IRepository<Role> RoleRepository
    {
        get
        {
            return GetRepository<Role>();
        }
    }

    public IRepository<Feature> FeatureRepository
    {
        get
        {
            return GetRepository<Feature>();
        }
    }

    public IRepository<RoleFeature> RoleFeatureRepository
    {
        get
        {
            return GetRepository<RoleFeature>();
        }
    }

    private Repository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
    {
        string name = typeof(TEntity).Name;
        var repository = repositories.GetValueOrDefault(name);
        if (repository == null)
        {
            repository = new Repository<TEntity>(context);
            repositories.Add(name, repository);
        }
        return repository;
    }

    public int SaveChanges()
    {
        return context.SaveChanges();
    }

    public Task<int> SaveChangesAsync()
    {
        return context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}