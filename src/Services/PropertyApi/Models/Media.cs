namespace PropertyApi.Models;

public class Media
{
      public string Id { get; set; }
      public string Url { get; set; }
      public string PhysicalPath { get; set; }
      public bool IsThumb { get; set; }
      public string PropertyId { get; set; }
      public Property Property { get; set; }
}