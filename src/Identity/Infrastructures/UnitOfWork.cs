
using Identity.Core.Entities.BaseEntity;
using Identity.Core.Interfaces;

namespace Identity.Infrastructures;

public class UnitOfWork(IdentityContext context) : IUnitOfWork
{
    private Dictionary<string, dynamic> repositories = new Dictionary<string, dynamic>();
    private IdentityContext context = context;
    private bool disposed = false;

    private Repository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
    {
        string name = typeof(TEntity).Name;
        var repository = repositories[name];
        if (repository == null)
        {
            repository = new Repository<TEntity>(context);
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