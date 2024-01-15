namespace PropertyApi.Dto;
public class SaveImageDto
{
      public IFormFile FormFile { get; set; }
      public string Name { get; set; }
      public string PropertyId { get; set; }
}