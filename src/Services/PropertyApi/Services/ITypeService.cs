
namespace PropertyApi.Services;

public interface ITypeService
{
      Task<PlaceType> CreateAsync(SaveTypeDto saveProperty);
      Task<PlaceType> UpdateAsync(SaveTypeDto saveProperty, string id);
      Task DeleteAsync(string Id);
      ICollection<PlaceType> GetAll();
      PlaceType? GetById(string id);
      ICollection<PlaceType>? Find(Expression<Func<PlaceType, bool>> predicate);
}