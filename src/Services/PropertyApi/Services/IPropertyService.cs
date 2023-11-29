namespace PropertyApi.Services;

public interface IPropertyService
{
      Task<Property> CreateAsync(SavePropertyDto saveProperty);
      Task<Property> UpdateAsync(SavePropertyDto saveProperty, string id);
      Task DeleteAsync(string Id);
      ICollection<Property> GetAll(int pageIndex = 0, int pageSize = 10);
      Property? GetById(string id);
      ICollection<Property>? Find(Expression<Func<Property, bool>> predicate);
}