using BookReaderDataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookReaderDataAccess.Repository;

public class GenericRepository<TEntity>(BookReaderContext context) : IGenericRepository<TEntity> where TEntity : class
{
    private readonly BookReaderContext context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null.");
    private readonly DbSet<TEntity> table = context.Set<TEntity>() ?? throw new InvalidOperationException($"Unable to retrieve DbSet for {typeof(TEntity)}.");

    public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null,
    params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = GetQueryableWithInclude(filter, includeProperties);

        return query.ToList();
    }

    public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>>? filter = null,
    params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = GetQueryableWithInclude(filter, includeProperties);

        return query.FirstOrDefault();
    }

    public TEntity InsertWithSave(TEntity entity)
    {
        this.table.Add(entity);
        this.context.SaveChanges();
        return entity;
    }

    public TEntity GetById(object id)
    {
        return this.table.Find(id);
    }

    public void DeleteWithSave(object id)
    {
        TEntity existing = this.table.Find(id);
        this.table.Remove(existing);
        this.context.SaveChanges();
    }

    private IQueryable<TEntity> GetQueryableWithInclude(Expression<Func<TEntity, bool>>? filter = null,
    params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = context.Set<TEntity>().AsNoTracking().AsQueryable();

        query = includeProperties?
        .Where(includeProperty => includeProperty != null)
        .Aggregate(query, (current, includeProperty) => current.Include(includeProperty))
        ?? query;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return query;
    }
}
