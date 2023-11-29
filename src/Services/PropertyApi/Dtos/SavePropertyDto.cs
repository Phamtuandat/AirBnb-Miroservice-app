namespace PropertyApi.Dto;

public class SavePropertyDto
{
      public string Title { get; set; } = string.Empty;
      public string Description { get; set; }
      public string LocationDetail { get; set; }
      public float PricePerNight { get; set; }
      public string[] Amenities { get; set; }
      public string[] ImageIds { get; set; }
      public string TypeId { get; set; }
      public string HostId { get; set; }
      public int NumberOfBethroom { get; set; }
      public int NumberOfBedroom { get; set; }
      public int MaxGuests { get; set; }
      public string City { get; set; }
      public string Country { get; set; }
}