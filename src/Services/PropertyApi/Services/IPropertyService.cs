
namespace PropertyApi.Services;

public interface IPropertyService
{
      Task<Property> CreateAsync(SavePropertyDto saveProperty);
      Task<Property> UpdateAsync(SavePropertyDto saveProperty, string id);
      Task DeleteAsync(string Id);
      PaginatedItemsViewModel<PropertyViewModel> GetAll(int pageIndex, int pageSize, string? sortBy);
      PropertyViewModel? GetById(string id);
      PaginatedItemsViewModel<PropertyViewModel> GetPropertiesByLabel(string labelId, int pageIndex, int pageSize);
      PaginatedItemsViewModel<PropertyViewModel> Find(Expression<Func<Property, bool>> predicate, int pageIndex, int pageSize);

}