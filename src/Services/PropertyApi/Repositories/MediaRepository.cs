using PropertyApi.Data;
using PropertyApi.Models;

namespace PropertyApi.Repository;

public class MediaRepository : GenericRepository<Media>
{
      public MediaRepository(PropertyDbContext context) : base(context)
      {

      }
}