using System.Data.Entity;
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
            var labels = new List<Label>();
            foreach (var id in saveProperty.Labels)
            {
                  var label = _unitOfWork.LabelRepository.Get(id);
                  if (label != null)
                  {

                        labels.Add(label);
                  }
            }



            var property = _mapper.Map<SavePropertyDto, Property>(saveProperty);
            property.Id = Guid.NewGuid().ToString();
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
                  throw;
            }

      }
      public PaginatedItemsViewModel<PropertyViewModel> GetPropertiesByLabel(string labelId, int pageIndex, int pageSize)
      {
            var listProduct = _unitOfWork.PropertyLabelRepository.All().Where(x => x.LabelId == labelId).Select(x => x.Property).ToList();
            var count = listProduct.Count;
            var itemOnPages = listProduct.GroupJoin(
                  _unitOfWork.MediaRepository.All(),
                  property => property.Id,
                  media => media.PropertyId,
                  (property, medias) => new PropertyViewModel
                  {
                        Id = property.Id,
                        Title = property.Title,
                        PricePerNight = property.PricePerNight,
                        Description = property.Description,
                        LocationDetail = property.LocationDetail,
                        City = property.City,
                        Country = property.Country,
                        Slug = property.Slug,
                        Amenities = property.Amenities,
                        HostId = property.HostId,
                        TypeId = property.TypeId,
                        NumberOfBethroom = property.NumberOfBethroom,
                        NumberOfBedroom = property.NumberOfBedroom,
                        MaxGuests = property.MaxGuests,
                        AverageRate = property.AverageRate,
                        CreateAt = property.CreateAt,
                        Medias = medias.Select(x => new Media()
                        {
                              Id = x.Id,
                              IsThumb = x.IsThumb,
                              PropertyId = x.PropertyId,
                              Url = x.Url,
                        }).ToList(),
                  }
            );
            return new PaginatedItemsViewModel<PropertyViewModel>(pageIndex, pageSize, count, itemOnPages);

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

      public PaginatedItemsViewModel<PropertyViewModel>? Find(Expression<Func<Property, bool>> predicate, int pageIndex, int pageSize)
      {
            var context = _unitOfWork.PropertyRepository.All().Where(predicate);
            var count = context != null ? (int)context?.ToList().Count : 0;
            var items = context?.Skip(pageSize * pageIndex).Take(pageSize);
            var itemOnPages = items?.GroupJoin(
                        _unitOfWork.MediaRepository.All(),
                        property => property.Id,
                        media => media.PropertyId,
                        (property, medias) => new PropertyViewModel()
                        {
                              Id = property.Id,
                              Title = property.Title,
                              PricePerNight = property.PricePerNight,
                              Description = property.Description,
                              LocationDetail = property.LocationDetail,
                              City = property.City,
                              Country = property.Country,
                              Slug = property.Slug,
                              Amenities = property.Amenities,
                              HostId = property.HostId,
                              TypeId = property.TypeId,
                              NumberOfBethroom = property.NumberOfBethroom,
                              NumberOfBedroom = property.NumberOfBedroom,
                              MaxGuests = property.MaxGuests,
                              AverageRate = property.AverageRate,
                              CreateAt = property.CreateAt,
                              Medias = medias.Select(x => new Media()
                              {
                                    Id = x.Id,
                                    IsThumb = x.IsThumb,
                                    PropertyId = x.PropertyId,
                                    Url = x.Url,
                              }).ToList(),
                        })
                        .ToList() ?? new List<PropertyViewModel>();
            return new PaginatedItemsViewModel<PropertyViewModel>(pageIndex, pageSize, count, itemOnPages);
      }

      public PaginatedItemsViewModel<PropertyViewModel> GetAll(int pageIndex, int pageSize, string? sortBy = "Title")
      {
            var count = _unitOfWork.PropertyRepository.All().Count();
            var items = _unitOfWork.PropertyRepository.All();

            switch (sortBy?.ToLower())
            {
                  case "title":
                        items = items.OrderBy(x => x.Title);
                        break;
                  case "price asc":
                        items = items.OrderBy(x => x.PricePerNight);
                        break;
                  case "price desc":
                        items = items.OrderByDescending(x => x.PricePerNight);
                        break;
                  default:
                        Console.WriteLine("Invalid input.");
                        break;
            }
            items = items.Skip(pageIndex * pageSize).Take(pageSize);
            var itemOnPages = items.GroupJoin(
                  _unitOfWork.MediaRepository.All(),
                  property => property.Id,
                  media => media.PropertyId,
                  (property, medias) => new PropertyViewModel()
                  {
                        Id = property.Id,
                        Title = property.Title,
                        PricePerNight = property.PricePerNight,
                        Description = property.Description,
                        LocationDetail = property.LocationDetail,
                        City = property.City,
                        Country = property.Country,
                        Slug = property.Slug,
                        Amenities = property.Amenities,
                        HostId = property.HostId,
                        TypeId = property.TypeId,
                        NumberOfBethroom = property.NumberOfBethroom,
                        NumberOfBedroom = property.NumberOfBedroom,
                        MaxGuests = property.MaxGuests,
                        AverageRate = property.AverageRate,
                        CreateAt = property.CreateAt,
                        Medias = medias.Select(x => new Media()
                        {
                              Id = x.Id,
                              IsThumb = x.IsThumb,
                              PropertyId = x.PropertyId,
                              Url = x.Url,
                        }).ToList(),
                  })
                  .ToList();
            return new PaginatedItemsViewModel<PropertyViewModel>(pageIndex, pageSize, count, itemOnPages);
      }
      public PropertyViewModel? GetById(string id)
      {
            var imgList = _unitOfWork.MediaRepository.Find(x => x.PropertyId == id);
            var property = _unitOfWork.PropertyRepository.Get(id);
            return new PropertyViewModel()
            {
                  Id = property?.Id,
                  Title = property.Title,
                  PricePerNight = property.PricePerNight,
                  Description = property.Description,
                  LocationDetail = property.LocationDetail,
                  City = property.City,
                  Country = property.Country,
                  Slug = property.Slug,
                  Amenities = property.Amenities,
                  HostId = property.HostId,
                  TypeId = property.TypeId,
                  NumberOfBethroom = property.NumberOfBethroom,
                  NumberOfBedroom = property.NumberOfBedroom,
                  MaxGuests = property.MaxGuests,
                  AverageRate = property.AverageRate,
                  CreateAt = property.CreateAt,
                  Medias = imgList?.ToList(),
            };
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
                  var updated = _unitOfWork.PropertyRepository.Update(property);
                  await _unitOfWork.SaveChangeAsync();

                  return updated;
            }
            catch (Exception)
            {
                  throw;
            }
      }



}