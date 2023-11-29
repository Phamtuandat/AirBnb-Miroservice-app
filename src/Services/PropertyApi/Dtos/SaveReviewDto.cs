using System.ComponentModel.DataAnnotations;

namespace PropertyApi.Dto;

public class SaveReviewDto
{
      public string Comment { get; set; }
      [Range(0, 5)]
      public int Rating { get; set; }
      public int UserId { get; set; }
      public int PropertyId { get; set; }
}