using System.Data.Entity.Core;
using System.Text.RegularExpressions;

namespace PropertyApi.Services;

public class MediaService : IMediaService
{
      private readonly IUnitOfWork _unitOfWork;
      private readonly IWebHostEnvironment _env;
      public MediaService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
      {
            _unitOfWork = unitOfWork;
            _env = env;
      }
      public async Task DeleteAsync(string id)
      {
            var media = _unitOfWork.MediaRepository.Get(id) ?? throw new ObjectNotFoundException();
            try
            {
                  File.Delete(media.Url);
                  _unitOfWork.MediaRepository.Delete(media);
                  await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception)
            {
                  throw;
            }
      }

      public ICollection<Media> GetAll()
      {
            return _unitOfWork.MediaRepository.All().ToList();
      }

      public Media? GetById(string id)
      {
            return _unitOfWork.MediaRepository.Get(id);
      }

      public async Task<Media> SaveAsync(SaveImageDto model)
      {
            var formFile = model.FormFile;
            if (formFile.Length > 0)
            {
                  List<string> ImageExtensions = new() { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG", ".WEBP" };
                  var fileName = RemoveSpecialCharacters(model.FormFile.FileName);
                  var extension = Path.GetExtension(fileName);
                  if (ImageExtensions.Contains(extension.ToUpperInvariant()))
                  {
                        var filePath = Path.Combine(_env.ContentRootPath, "images", $"test_{model.Name}{extension}");
                        var url = filePath
                              .Replace(_env.ContentRootPath, Environment.GetEnvironmentVariable("APP_URL"))
                              .Replace("\\", "/").Replace("/images/", "/static/");
                        try
                        {
                              using (var stream = File.Create(filePath))
                              {
                                    await formFile.CopyToAsync(stream);
                                    stream.Close();
                              }
                              var media = new Media()
                              {
                                    Url = url,
                                    PhysicalPath = filePath,
                                    Id = Guid.NewGuid().ToString()
                              };
                              media = _unitOfWork.MediaRepository.Add(media);
                              await _unitOfWork.SaveChangeAsync();
                              return media;
                        }
                        catch (Exception)
                        {
                              File.Delete(filePath);
                              throw;
                        }
                  }
            }
            throw new Exception("Form file is required!");
      }

      public Task<Media> UpdateAsync(SaveImageDto mode, string id)
      {

            throw new NotImplementedException();
      }
      private string RemoveSpecialCharacters(string str)
      {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
      }
}