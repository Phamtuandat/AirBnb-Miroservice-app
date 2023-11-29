using PropertyApi.Data;

namespace PropertyApi.Repository;

public abstract class GenericRepository<T> : IRepository<T> where T : class
{
      protected PropertyDbContext _context;
      public GenericRepository(PropertyDbContext context)
      {
            _context = context;
      }

      public virtual T Add(T entity)
      {
            return _context
                  .Add(entity)
                  .Entity;
      }

      public virtual IQueryable<T> All()
      {
            return (from c in _context.Set<T>() select c)
                  .AsQueryable();
      }

      public virtual T Delete(T entity)
      {
            return _context.Remove<T>(entity).Entity;
      }



      public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
      {
            return _context.Set<T>()
                  .AsQueryable()
                  .Where(predicate).ToList();
      }

      public virtual T? Get(string id)
      {
            return _context.Set<T>().Find(id);
      }


      public virtual T Update(T entity)
      {
            return _context.Update(entity).Entity;
      }
      public virtual void DeleteRange(IEnumerable<T> entities)
      {
            _context.RemoveRange(entities);
      }
      public async Task SaveChangesAsync()
      {
            await _context.SaveChangesAsync();
      }
}