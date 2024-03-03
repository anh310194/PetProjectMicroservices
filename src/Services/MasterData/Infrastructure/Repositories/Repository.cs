using System.Linq.Expressions;
using Core.Entities.BaseEntity;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly ICacheService _cacheService;
    private string entityName = typeof(TEntity).Name;

    public Repository(MasterDataContext context, ICacheService cacheService)
    {
        _dbSet = context.Set<TEntity>();
        this._cacheService = cacheService;
    }
    public void Delete(int id)
    {
        var entity = _dbSet.Find(id);
        if (entity == null) return;
        Delete(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void DeleteRange(ICollection<TEntity> entities)
    {
        var enumerable = entities.AsEnumerable();
        _dbSet.RemoveRange(enumerable);
    }

    public TEntity? Find(int id)
    {
        return _dbSet.Find(id);
    }

    public async ValueTask<TEntity?> FindAsync(int id)
    {
        string key = $"{entityName}{id}";
        var cachedEntity = await this._cacheService.GetDataAsync<TEntity>(key);
        if(cachedEntity != null)
        {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return await ValueTask.FromResult(cachedEntity).ConfigureAwait(false);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }
        var entity = await _dbSet.FindAsync(id);
        if(entity == null)
        {
            return null;
        }
        DateTimeOffset expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
        _cacheService.SetData(key, entity, expirationTime);
        return entity;

    }

    public ValueTask<TEntity?> FindAsync(int id, CancellationToken cancellationToken)
    {
        return _dbSet.FindAsync(cancellationToken, id);
    }

    private void SetBaseValueInsert(TEntity entity, int userId)
    {
        entity.CreatedBy = userId;
        entity.CreatedTime = DateTime.UtcNow;
    }

    public TEntity Insert(TEntity entity, int userId)
    {
        SetBaseValueInsert(entity, userId);
        return _dbSet.Add(entity).Entity;
    }

    public ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, int userId)
    {
        SetBaseValueInsert(entity, userId);
        return _dbSet.AddAsync(entity);
    }

    public ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, int userId, CancellationToken cancellationToken)
    {
        SetBaseValueInsert(entity, userId);
        return _dbSet.AddAsync(entity, cancellationToken);
    }

    public void InsertRange(ICollection<TEntity> entities, int userId)
    {
        var enumerable = entities.Select(entity =>
        {
            SetBaseValueInsert(entity, userId);
            return entity;
        }).AsEnumerable();
        _dbSet.AddRange(enumerable);
    }

    public Task InsertRangeAsync(ICollection<TEntity> entities, int userId)
    {
        var enumerable = entities.AsEnumerable().Select(s =>
        {
            SetBaseValueInsert(s, userId);
            return s;
        });
        return _dbSet.AddRangeAsync(enumerable);
    }

    public Task InsertRangeAsync(ICollection<TEntity> entities, int userId, CancellationToken cancellationToken)
    {
        var enumerable = entities.AsEnumerable().Select(s =>
        {
            SetBaseValueInsert(s, userId);
            return s;
        });
        return _dbSet.AddRangeAsync(enumerable, cancellationToken);
    }

    public IQueryable<TEntity> Queryable()
    {
        return _dbSet.AsQueryable();
    }

    public IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate)
    {
        return Queryable().Where(predicate);
    }

    public IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
    {
        return orderBy(Queryable(predicate));
    }

    public IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties)
    {
        var result = Queryable(predicate, orderBy);

        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            result = result.Include(includeProperty);
        }
        return result;
    }
    private void SetBaseValueUpdate(TEntity entity, int userId)
    {
        entity.UpdatedBy = userId;
        entity.UpdatedTime = DateTime.UtcNow;
    }

    public TEntity Update(TEntity entity, int userId)
    {
        SetBaseValueUpdate(entity, userId);
        return _dbSet.Update(entity).Entity;
    }

    public void UpdateRange(ICollection<TEntity> entities, int userId)
    {
        var enumerable = entities.AsEnumerable().Select(s =>
        {
            SetBaseValueUpdate(s, userId);
            return s;
        });
        _dbSet.UpdateRange(enumerable);
    }
}