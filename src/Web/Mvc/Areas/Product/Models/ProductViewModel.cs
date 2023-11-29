namespace Mvc.Areas.Product.ViewModels;
public class ProductViewModel
{
      public int Id { get; set; }
      public string Name { get; set; } = string.Empty;
      public float Price { get; set; }
      public int BrandId { get; set; }
      public DateTime CreateAt { get; set; }
      public DateTime? UpdateAt { get; set; }
      public DateOnly ExpiryDate { get; set; }
      public string Description { get; set; }
}