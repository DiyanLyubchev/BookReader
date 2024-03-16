using System.Linq.Expressions;

namespace BookReaderDataAccess.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>>? filter = null,
                                  params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null,
                                    params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity GetById(object id);
        TEntity InsertWithSave(TEntity entity);
        void Delete(object id);
    }
}