namespace Mvc.Models;
public class PaginatedItemsViewModel<TEntity> where TEntity : class
{
      public int PageIndex { get; private set; }

      public int PageSize { get; private set; }

      public double Count { get; private set; }

      public IEnumerable<TEntity> Data { get; private set; }

      public PaginatedItemsViewModel(int pageIndex, int pageSize, double count, IEnumerable<TEntity> data)
      {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
      }
}