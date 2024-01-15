using System.Linq.Expressions;

namespace PropertyApi.Repository;


public interface IRepository<T>
{
      T Add(T entity);
      T Update(T entity);
      T? Get(string id);
      T Delete(T entity);
      IQueryable<T> All();
      IQueryable<T>? Find(Expression<Func<T, bool>> predicate);
      Task SaveChangesAsync();
      void DeleteRange(IEnumerable<T>? imageList);
}