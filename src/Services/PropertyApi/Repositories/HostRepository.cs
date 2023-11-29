using PropertyApi.Data;
using PropertyApi.Models;

namespace PropertyApi.Repository;

public class HostRepository : GenericRepository<Householder>
{
      public HostRepository(PropertyDbContext context) : base(context)
      {

      }

}