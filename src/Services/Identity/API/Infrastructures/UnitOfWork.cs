
using Core.Entities.BaseEntity;
using Core.Interfaces;
using Infrastructure.Repositories;

namespace Identity.Infrastructures;

public class UnitOfWork(IdentityContext context) : IUnitOfWork
{
    private Dictionary<string, dynamic> repositories = new Dictionary<string, dynamic>();
    private IdentityContext context = context;
    private bool disposed = false;

    private Repository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
    {
        string name = typeof(TEntity).Name;
        var repository = repositories.GetValueOrDefault(name);
        if (repository == null)
        {
#pragma warning disable CS0436 // Type conflicts with imported type
            repository = new Repository<TEntity>(context);
#pragma warning restore CS0436 // Type conflicts with imported type
            repositories.Add(name, repository);
        }
        return repository;
    }

    public IRepository<Country> CountryRepository
    {
        get
        {
            return GetRepository<Country>();
        }
    }

    public IRepository<Feature> FeatureRepository
    {
        get
        {
            return GetRepository<Feature>();
        }
    }

    public IRepository<Role> RoleRepository
    {
        get
        {
            return GetRepository<Role>();
        }
    }

    public IRepository<State> StateRepository
    {
        get
        {
            return GetRepository<State>();
        }
    }

    public IRepository<User> UserRepository
    {
        get
        {
            return GetRepository<User>();
        }
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
        if (!this.disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}