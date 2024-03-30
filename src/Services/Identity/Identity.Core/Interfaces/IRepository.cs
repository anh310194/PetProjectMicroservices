using Identity.Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Identity.Core.Interfaces;
public interface IRepository<TEntity> where TEntity : BaseEntity
{
    TEntity Insert(TEntity entity, int userId);
    void InsertRange(ICollection<TEntity> entities, int userId);
    ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, int userId, CancellationToken cancellationToken = default);
    Task InsertRangeAsync(ICollection<TEntity> entities, int userId, CancellationToken cancellationToken = default);
    TEntity Update(TEntity entity, int userId);
    void UpdateRange(ICollection<TEntity> entities, int userId);
    void Delete(int id);
    void Delete(TEntity entity);
    void DeleteRange(ICollection<TEntity> entities);
    TEntity? Find(int id);
    ValueTask<TEntity?> FindAsync(int id);
    IQueryable<TEntity> Queryable();
    IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
    IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties);
}