using MasterData.Domain.Entities;
using MasterData.Domain.Interfaces;
using MasterData.Domain.Repositories;
using MasterData.Infrastructure.Persistence;

namespace MasterData.Infrastructure.Repositories;

public class UnitOfWork(MasterDataContext context, ICacheService cacheService) : IUnitOfWork
{
    private Dictionary<string, dynamic> repositories = new Dictionary<string, dynamic>();
    private MasterDataContext context = context;
    private bool disposed = false;

    private Repository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
    {
        string name = typeof(TEntity).Name;
        var repository = repositories.GetValueOrDefault(name);
        if (repository == null)
        {
            repository = new Repository<TEntity>(context, cacheService);
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

    public IRepository<State> StateRepository
    {
        get
        {
            return GetRepository<State>();
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