using Mvc.Areas.Property.ViewModels;

namespace Mvc.Models;

public class FilterViewModel
{
      public PlaceTypeViewModel? CurrentType { get; set; }
      public List<PlaceTypeViewModel> Types { get; set; }
      public Func<string?, string> GenerateUrl { get; set; }

}