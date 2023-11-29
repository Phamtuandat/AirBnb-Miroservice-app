using PropertyApi.Data;
using PropertyApi.Models;

namespace PropertyApi.Repository;

public class TypeRepository : GenericRepository<PlaceType>
{
      public TypeRepository(PropertyDbContext context) : base(context)
      {
      }

}