
using System.Data.Entity.Core;

namespace PropertyApi.Services;

public class HostService : IHostService
{
      private readonly IUnitOfWork _unitOfWork;
      private readonly IMapper _mapper;
      public HostService(IUnitOfWork unitOfWork, IMapper mapper)
      {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
      }
      public async Task DeleteAsync(string id)
      {
            var host = _unitOfWork.HostRepository.Get(id) ?? throw new ObjectNotFoundException("Can not find this host!");
            try
            {
                  _unitOfWork.HostRepository.Delete(host);
                  await _unitOfWork.SaveChangeAsync();
            }
            catch (System.Exception)
            {

                  throw;
            }

      }

      public ICollection<Householder>? Find(Expression<Func<Householder, bool>> expression)
      {
            return _unitOfWork.HostRepository.Find(expression)?.ToList();
      }

      public Householder? Get(string id)
      {
            return _unitOfWork.HostRepository.Get(id);
      }

      public ICollection<Householder> GetAll(int pageIndex = 0, int pageSize = 10)
      {
            return _unitOfWork.HostRepository.All().Skip(pageIndex * pageSize).Take(pageSize).ToList();
      }

      public async Task<Householder> SigninAsync(SaveHouseholder householder)
      {
            var host = _mapper.Map<SaveHouseholder, Householder>(householder);
            try
            {
                  host.CreateAt = DateTime.Now.ToString();
                  host.Id = Guid.NewGuid().ToString();
                  var newHost = _unitOfWork.HostRepository.Add(host);
                  await _unitOfWork.SaveChangeAsync();
                  return newHost;
            }
            catch (System.Exception)
            {
                  throw;
            }
      }

      public async Task<Householder> UpdateAsync(SaveHouseholder saveHouseholder, string id)
      {
            var host = _unitOfWork.HostRepository.Get(id) ?? throw new ObjectNotFoundException("Can not find this host id");

            host.Phone = saveHouseholder.Phone;
            host.AboutMe = saveHouseholder.AboutMe;
            host.Email = saveHouseholder.Email;
            try
            {
                  var updated = _unitOfWork.HostRepository.Update(host);
                  await _unitOfWork.SaveChangeAsync();
                  return updated;
            }
            catch (System.Exception)
            {

                  throw;
            }
      }
}