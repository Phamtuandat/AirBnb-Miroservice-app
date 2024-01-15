
namespace PropertyApi.Repository;

public class PropertyLabelRepository : GenericRepository<PropertyLabel>
{
      public PropertyLabelRepository(PropertyDbContext context) : base(context)
      {
      }
}