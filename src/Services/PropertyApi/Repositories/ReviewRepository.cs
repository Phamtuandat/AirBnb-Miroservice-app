using PropertyApi.Data;
using PropertyApi.Models;

namespace PropertyApi.Repository;

public class ReviewRepository : GenericRepository<Review>
{
      public ReviewRepository(PropertyDbContext context) : base(context)
      {
      }
}