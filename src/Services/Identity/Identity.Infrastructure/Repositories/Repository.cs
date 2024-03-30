using Identity.Core.Interfaces;
using Identity.Core.Models;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Identity.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet;

    public Repository(IdentityContext context)
    {
        _dbSet = context.Set<TEntity>();
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

    public ValueTask<TEntity?> FindAsync(int id)
    {
        return _dbSet.FindAsync(id);
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

    public ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, int userId, CancellationToken cancellationToken = default)
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

    public Task InsertRangeAsync(ICollection<TEntity> entities, int userId, CancellationToken cancellationToken = default)
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