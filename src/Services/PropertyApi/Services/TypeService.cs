
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;

namespace PropertyApi.Services;

public class TypeService : ITypeService
{
      private readonly IUnitOfWork _unitOfWork;
      private readonly IMapper _mapper;

      public TypeService(IUnitOfWork unitOfWork, IMapper mapper)
      {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
      }

      public async Task<PlaceType> CreateAsync(SaveTypeDto saveProperty)
      {
            try
            {
                  var type = _mapper.Map<SaveTypeDto, PlaceType>(saveProperty);
                  type.Id = Guid.NewGuid().ToString();
                  type = _unitOfWork.PlaceTypeRepository.Add(type);
                  await _unitOfWork.SaveChangeAsync();
                  return type;
            }
            catch (Exception)
            {
                  throw;
            }
      }

      public async Task DeleteAsync(string Id)
      {
            var type = _unitOfWork.PlaceTypeRepository.Get(Id) ?? throw new ObjectNotFoundException();
            try
            {
                  _unitOfWork.PlaceTypeRepository.Delete(type);
                  await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception)
            {
                  throw;
            }
      }

      public ICollection<PlaceType>? Find(Expression<Func<PlaceType, bool>> predicate)
      {
            return _unitOfWork.PlaceTypeRepository.Find(predicate)?.ToList();
      }

      public ICollection<PlaceType> GetAll()
      {
            return _unitOfWork.PlaceTypeRepository.All().ToList();
      }

      public PlaceType? GetById(string id)
      {
            return _unitOfWork.PlaceTypeRepository.Get(id);
      }

      public async Task<PlaceType> UpdateAsync(SaveTypeDto saveProperty, string id)
      {
            var type = _unitOfWork.PlaceTypeRepository.Get(id) ?? throw new ObjectNotFoundException("Can not find this type");
            try
            {
                  type.Name = saveProperty.Name;
                  var updatedType = _unitOfWork.PlaceTypeRepository.Update(type);
                  await _unitOfWork.SaveChangeAsync();
                  return updatedType;

            }
            catch (Exception)
            {

                  throw;
            }
      }
}