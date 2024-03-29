using Mvc.Models;

namespace Mvc.Areas.Property.ViewModels;
public class PropertyViewModel
{
      public string Id { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public string LocationDetail { get; set; }
      public string City { get; set; }
      public string Country { get; set; }
      public string Slug { get; set; }
      public double PricePerNight { get; set; }
      public string[] Amenities { get; set; }
      public string HostId { get; set; }
      public string TypeId { get; set; }
      public int NumberOfBethroom { get; set; }
      public int NumberOfBedroom { get; set; }
      public int MaxGuests { get; set; }
      public int AverageRate { get; set; }
      public DateTime CreateAt { get; set; }
      public List<Media> Medias { get; set; }
}