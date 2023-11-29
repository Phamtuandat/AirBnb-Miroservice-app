using System.Data.Entity.Core;
namespace PropertyApi.Services;

public class PropertyService : IPropertyService
{
      private readonly IUnitOfWork _unitOfWork;
      private readonly IMapper _mapper;
      public PropertyService(IUnitOfWork unitOfWork, IMapper mapper)
      {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
      }
      public async Task<Property> CreateAsync(SavePropertyDto saveProperty)
      {
            var ImageList = _unitOfWork.MediaRepository.Find(m => saveProperty.ImageIds.Contains(m.Id))?.ToList();
            var property = _mapper.Map<SavePropertyDto, Property>(saveProperty);
            property.Id = Guid.NewGuid().ToString();
            property.Medias = ImageList;
            property.CreateAt = DateTime.Now.ToString();
            try
            {
                  property.Slug = Extensions.GenerateSlug(saveProperty.Title + saveProperty.LocationDetail);
                  property = _unitOfWork.PropertyRepository.Add(property);
                  await _unitOfWork.SaveChangeAsync();
                  return property;
            }
            catch (System.Exception)
            {
                  await SavePropertyFaild(saveProperty.ImageIds);
                  throw;
            }

      }


      public async Task DeleteAsync(string Id)
      {
            var property = _unitOfWork.PropertyRepository.Get(Id) ?? throw new ObjectNotFoundException();
            try
            {
                  _unitOfWork.PropertyRepository.Delete(property);
                  await _unitOfWork.SaveChangeAsync();
            }
            catch (System.Exception)
            {
                  throw;
            }
      }

      public ICollection<Property>? Find(Expression<Func<Property, bool>> predicate)
      {
            return _unitOfWork.PropertyRepository.Find(predicate)?.ToList();
      }

      public ICollection<Property> GetAll(int pageIndex, int pageSize)
      {
            return _unitOfWork.PropertyRepository.All().Skip(pageIndex * pageSize).Take(pageSize).ToList();
      }

      public Property? GetById(string id)
      {
            return _unitOfWork.PropertyRepository.Get(id);
      }
      public async Task<Property> UpdateAsync(SavePropertyDto saveProperty, string id)
      {
            var property = _unitOfWork.PropertyRepository.Get(id) ?? throw new ObjectNotFoundException();
            try
            {
                  var newImages = _unitOfWork.MediaRepository.Find(x => saveProperty.ImageIds.Contains(x.Id))?.ToList();
                  property.Slug = Extensions.GenerateSlug(saveProperty.Title + saveProperty.LocationDetail);
                  property.Title = saveProperty.Title;
                  property.NumberOfBedroom = saveProperty.NumberOfBedroom;
                  property.PricePerNight = saveProperty.PricePerNight;
                  property.NumberOfBethroom = saveProperty.NumberOfBethroom;
                  property.Description = saveProperty.Description;
                  property.Amenities = saveProperty.Amenities;
                  property.City = saveProperty.City;
                  property.Country = saveProperty.Country;
                  property.MaxGuests = saveProperty.MaxGuests;
                  property.LocationDetail = saveProperty.LocationDetail;
                  property.Medias = newImages;

                  var updated = _unitOfWork.PropertyRepository.Update(property);
                  await _unitOfWork.SaveChangeAsync();

                  return updated;
            }
            catch (Exception)
            {
                  throw;
            }
      }


      private async Task SavePropertyFaild(string[] imageIds)
      {
            var ImageList = _unitOfWork.MediaRepository.Find(x => imageIds.Contains(x.Id));
            try
            {

                  _unitOfWork.MediaRepository.DeleteRange(ImageList);
                  await _unitOfWork.SaveChangeAsync();
            }
            catch (System.Exception)
            {
                  throw;
            }
      }
}