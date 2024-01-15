namespace PropertyApi.Models;
public class Property
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string LocationDetail { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Slug { get; set; }
    public float PricePerNight { get; set; }
    public string[] Amenities { get; set; }
    public string HostId { get; set; }
    public Householder Host { get; set; }
    public string TypeId { get; set; }
    public PlaceType Type { get; set; }
    public int NumberOfBethroom { get; set; }
    public int NumberOfBedroom { get; set; }
    public int MaxGuests { get; set; }
    public int AverageRate { get; set; }
    public string CreateAt { get; set; }
}
