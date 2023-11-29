using System.ComponentModel.DataAnnotations;

namespace PropertyApi.Models;

public class Review
{
      public string Id { get; set; }
      public string? LastComment { get; set; }
      public string Comment { get; set; }
      [Range(0, 5)]
      public int Rating { get; set; }
      public string UserId { get; set; }
      public string PropertyId { get; set; }
      public Property Property { get; set; }
      public string CreateAt { get; set; }
      public string UpdateAt { get; set; }
}