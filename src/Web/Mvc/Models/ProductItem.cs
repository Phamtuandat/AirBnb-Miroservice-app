namespace Mvc.Models;
public class ProductItem
{
      public int Id { get; set; }
      public string Name { get; set; } = string.Empty;
      public float Price { get; set; }
      public int BrandId { get; set; }
      public string CreateAt { get; set; }
      public string? UpdateAt { get; set; }
      public string ExpiryDate { get; set; }
      public string Description { get; set; }
}