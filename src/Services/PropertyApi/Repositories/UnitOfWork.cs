using PropertyApi.Data;
using PropertyApi.Models;

namespace PropertyApi.Repository;

public class UnitOfWork : IUnitOfWork
{
      private readonly PropertyDbContext _context;
      public UnitOfWork(PropertyDbContext context)
      {
            _context = context;

      }
      private IRepository<Media>? _mediaRepository;
      private IRepository<Householder>? _hostRepository;
      private IRepository<PlaceType>? _placeTypeRepository;
      private IRepository<Review>? _reviewRepository;
      private IRepository<Property>? propertyRepository;
      public IRepository<Property> PropertyRepository
      {
            get
            {
                  propertyRepository ??= new PropertyRepository(_context);
                  return propertyRepository;
            }
      }


      public IRepository<PlaceType> PlaceTypeRepository
      {
            get
            {
                  _placeTypeRepository ??= new TypeRepository(_context);
                  return _placeTypeRepository;
            }
      }

      public IRepository<Householder> HostRepository
      {
            get
            {
                  _hostRepository ??= new HostRepository(_context);
                  return _hostRepository;
            }
      }



      public IRepository<Media> MediaRepository
      {
            get
            {
                  _mediaRepository ??= new MediaRepository(_context);
                  return _mediaRepository;
            }
      }

      public IRepository<Review> ReviewRepository
      {
            get
            {
                  _reviewRepository ??= new ReviewRepository(_context);
                  return _reviewRepository;
            }
      }

      public async Task SaveChangeAsync()
      {
            await _context.SaveChangesAsync();
      }
}