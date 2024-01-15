

namespace PropertyApi.Repository;

public class PropertyRepository : GenericRepository<Property>
{
      public PropertyRepository(PropertyDbContext context) : base(context)
      {
      }



}