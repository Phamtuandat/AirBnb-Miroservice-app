
namespace PropertyApi.Repository;

public class LabelRepository : GenericRepository<Label>
{
      public LabelRepository(PropertyDbContext context) : base(context)
      {
      }
}