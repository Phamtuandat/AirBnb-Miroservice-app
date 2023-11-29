using PropertyApi.Models;

namespace PropertyApi.Repository;

public interface IUnitOfWork
{
      IRepository<Property> PropertyRepository { get; }
      IRepository<PlaceType> PlaceTypeRepository { get; }
      IRepository<Householder> HostRepository { get; }
      IRepository<Media> MediaRepository { get; }
      IRepository<Review> ReviewRepository { get; }
      Task SaveChangeAsync();
}