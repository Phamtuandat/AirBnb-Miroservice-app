namespace PropertyApi.Services;

public interface IMediaService
{
      Task<Media> SaveAsync(SaveImageDto model);
      Task<Media> UpdateAsync(SaveImageDto mode, string id);
      Task DeleteAsync(string id);
      Media? GetById(string id);
      ICollection<Media> GetAll();
}