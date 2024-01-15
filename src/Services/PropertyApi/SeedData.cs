
using Bogus;
using Bogus.DataSets;
using CountryData;
using Humanizer;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class SeedData
{
      public static async Task EnsureSeedData(WebApplication app)
      {
            using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                  // setup the data provider
                  var context = scope.ServiceProvider.GetService<PropertyDbContext>();
                  context.Database.Migrate();
                  var typeList = new List<PlaceType>();
                  var hostList = new List<Householder>();
                  var labels = new List<Label>();
                  var propertyList = new List<Property>();

                  // Seed data for the host table
                  var testHost = new Faker<Householder>();
                  if (!context.Hosts.Any(x => x.Email.Contains("test_")))
                  {
                        testHost
                              .RuleFor(h => h.Id, f => Guid.NewGuid().ToString())
                              .RuleFor(h => h.Name, f => f.Name.FullName())
                              .RuleFor(h => h.Email, (f, u) => "test_" + f.Internet.Email(u.Name, u.Phone))
                              .RuleFor(h => h.AboutMe, (f, u) => f.Lorem.Paragraph(10))
                              .RuleFor(h => h.Phone, f => f.Phone.PhoneNumber())
                              .RuleFor(h => h.CreateAt, (f) => f.Date.BetweenDateOnly(DateOnly.FromDateTime(DateTime.Now.AddYears(-4)), DateOnly.FromDateTime(DateTime.Now)).ToString());
                        for (var i = 0; i <= 30; i++)
                        {
                              var host = testHost.Generate();
                              hostList.Add(host);
                        }
                        await context.AddRangeAsync(hostList);
                  }
                  else
                  {
                        Log.Information("Datas in host table is existed!");
                  }
                  if (!context.Types.Any())
                  {

                        string[] propertyTypes = {
                        "Entire home/apartment",
                        "Private room",
                        "Shared room",
                        "Guesthouse",
                        "Boutique hotel",
                        "Bed and breakfast",
                        "Cabin",
                        "Villa",
                        "Condominium",
                        "Treehouse",
                        "Tiny house",
                        "Castle",
                        "Houseboat",
                        "Camper/RV",
                        "Farm stay",
                        "Chalet",
                        "Earth house",
                        "Tent",
                        "Loft",
                        "Barn"
                        };
                        //seed data for the type table

                        foreach (var item in propertyTypes)
                        {
                              var type = new PlaceType()
                              {
                                    Id = Guid.NewGuid().ToString(),
                                    Name = item
                              };
                              typeList.Add(type);
                        }
                        await context.AddRangeAsync(typeList);
                  }
                  else
                  {
                        Log.Information("Type is existed!");
                  }
                  if (!context.Labels.Any())
                  {
                        var labelNames = new string[]{
                              "a-frames.png",
                              "farms.png",
                              "top-of-the-world.png",
                              "amazing-pools.png",
                              "golfing.png",
                              "towers.png",
                              "amazing-view.png",
                              "grand-piano.png",
                              "treehouses.png",
                              "arctic.png",
                              "hanoks.png",
                              "trending.png",
                              "barns.png",
                              "historical-homes.png",
                              "tropical.png",
                              "beach.png",
                              "houseboats.png",
                              "trulli.png",
                              "beachfront.png",
                              "iconic-cities.png",
                              "vineyards.png",
                              "bed-breakfasts.png",
                              "islands.png",
                              "windmills.png",
                              "boats.png",
                              "lake.png",
                              "yurts.png",
                              "cabins.png",
                              "lakefront.png",
                              "campers.png",
                              "luxe.png",
                              "camping.png",
                              "mansion.png",
                              "casas-particulares.png",
                              "minsus.png",
                              "castles.png",
                              "national-parks.png",
                              "caves.png",
                              "new.png",
                              "chef's-kitchens.png",
                              "omg.png",
                              "contryside.png",
                              "play.png",
                              "creative-space.png",
                              "riads.png",
                              "cycladic-homes.png",
                              "rooms.png",
                              "dammusi.png",
                              "ryokans.png",
                              "desert.png",
                              "shepherd's-huts.png",
                              "design.png",
                              "skiing.png",
                              "domes.png",
                              "surfing.png",
                              "earth-home.png",
                              "tiny-homes.png"
                        };


                        foreach (var item in labelNames)
                        {
                              var label = new Label()
                              {
                                    ImgUrl = "https://localhost:7074/static/labels/" + item,
                                    Name = item.Replace("-", " ").Replace(".png", "").Titleize(),
                                    Id = Guid.NewGuid().ToString()
                              };
                              labels.Add(label);
                        }

                        await context.Labels.AddRangeAsync(labels);
                  }
                  // Seed data for the property table
                  if (context.Properties.Where(h => h.Title.Contains("test_")).Count() < 400)
                  {
                        if (hostList.Count == 0)
                        {
                              hostList = context.Hosts.ToList();
                        }
                        if (typeList.Count == 0)
                        {
                              typeList = context.Types.ToList();
                        }
                        if (labels.Count == 0)
                        {
                              labels = context.Labels.ToList();
                        }

                        string[] countryCodes = {
                              "ad", "ar", "as", "at", "au", "ax", "az", "bd", "be", "bg", "bm", "br", "by", "ca", "ch", "cl", "co", "cr",
                              "cy", "cz", "de", "dk", "do", "dz", "ec", "ee", "es", "fi", "fm", "fo", "fr", "gb", "gf", "gg", "gl", "gp",
                              "gt", "gu", "hr", "ht", "hu", "ie", "im", "in", "is", "it", "je", "jp", "kr", "li", "lk", "lt", "lu", "lv",
                              "ma", "mc", "md", "mh", "mk", "mp", "mq", "mt", "mw", "mx", "my", "nc", "nl", "no", "nz", "pe", "ph", "pk",
                              "pl", "pm", "pr", "pt", "pw", "re", "ro", "rs", "ru", "se", "sg", "si", "sj", "sk", "sm", "th", "tr", "ua",
                              "us", "uy", "va", "vi", "wf", "yt", "za"
                        };

                        var testProperty = new Faker<Property>();
                        string[] amenities = {
                              "Wi-Fi",
                              "Air conditioning",
                              "Heating",
                              "Essentials (towels, bed sheets, soap, and toilet paper)",
                              "TV",
                              "Washer",
                              "Dryer",
                              "Hot water",
                              "Iron",
                              "Hair dryer",
                              "Workspace (desk or table with space for a laptop and a chair)",
                              "Coffee maker",
                              "Refrigerator",
                              "Dishwasher",
                              "Microwave",
                              "Cooking basics (pots and pans, oil, salt, and pepper)",
                              "Oven",
                              "Stove",
                              "Free parking on premises",
                              "Elevator",
                              "Gym",
                              "Pool",
                              "Hot tub",
                              "Pets allowed",
                              "Smoking allowed",
                              "Wheelchair accessible",
                              "BBQ grill",
                              "Balcony or patio",
                              "Garden or backyard",
                              "Children's toys"
                        };
                        testProperty
                              .RuleFor(p => p.Id, (f, u) => f.Random.Guid().ToString())
                              .RuleFor(p => p.Title, f => $"test_{f.Lorem.Sentence(null, 6)}")
                              .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
                              .RuleFor(p => p.Slug, (f, u) => Extensions.GenerateSlug(f.Lorem.Slug()))
                              .RuleFor(p => p.PricePerNight, f => f.Random.Float(50, 500))
                              .RuleFor(p => p.Amenities, f => GetRandomAmenities(amenities, f.Random.Int(1, 5)))
                              .RuleFor(p => p.HostId, f => hostList[f.Random.Int(0, hostList.Count - 1)].Id)
                              .RuleFor(p => p.TypeId, f => typeList[f.Random.Int(0, typeList.Count() - 1)].Id)
                              .RuleFor(p => p.NumberOfBethroom, f => f.Random.Number(1, 5))
                              .RuleFor(p => p.NumberOfBedroom, f => f.Random.Number(1, 5))
                              .RuleFor(p => p.MaxGuests, f => f.Random.Number(1, 10))
                              .RuleFor(p => p.AverageRate, f => f.Random.Number(1, 5))
                              .RuleFor(p => p.CreateAt, f => f.Date.Past().ToString());
                        for (int i = 0; i < 400; i++)
                        {
                              Random random = new();
                              int randomIndex = random.Next(countryCodes.Length);
                              string randomCountryCode = countryCodes[randomIndex].ToUpper();
                              var locationData = CountryLoader.LoadLocationData(randomCountryCode);
                              var state = locationData.States[random.Next(locationData.States.Count)];
                              var province = state.Provinces[random.Next(state.Provinces.Count)];
                              var community = province.Communities[random.Next(province.Communities.Count)];
                              var place = community.Places[random.Next(community.Places.Count)];
                              if (state.Name == null)
                              {
                                    i--;
                                    continue;
                              }
                              testProperty.RuleFor(p => p.City, (f, u) => state.Name)
                                          .RuleFor(p => p.Country, (f, u) => locationData.Name)
                                          .RuleFor(p => p.LocationDetail, (f, u) => $"{f.Address.BuildingNumber()} {place.Name}, {(community.Name.IsNullOrEmpty() ? f.Address.StreetName() : community.Name)}, {state.Name}, {locationData.Name}");
                              var property = testProperty.Generate();
                              propertyList.Add(property);
                        }
                        await context.AddRangeAsync(propertyList);
                  }
                  else
                  {
                        Log.Information("seed property data is existed!");

                  }
                  if (!context.Medias.Any(x => x.PhysicalPath.Contains("test_")))
                  {

                        var mediaList = new List<Media>();
                        if (propertyList.Count == 0)
                        {

                              propertyList = context.Properties.ToList();
                        }
                        var testMedia = new Faker<Media>()
                                          .RuleFor(m => m.Id, f => Guid.NewGuid().ToString())
                                          .RuleFor(m => m.Url, f => f.Image.LoremFlickrUrl(640, 480, "house", false, false, null))
                                          .RuleFor(m => m.PhysicalPath, f => "test_" + f.Lorem.Slug());
                        foreach (var item in propertyList)
                        {
                              for (int i = 0; i < 3; i++)
                              {
                                    if (i == 0)
                                    {
                                          testMedia.RuleFor(m => m.IsThumb, true);
                                    }
                                    else
                                    {
                                          testMedia.RuleFor(m => m.IsThumb, false);
                                    }
                                    testMedia.RuleFor(m => m.PropertyId, item.Id);
                                    var media = testMedia.Generate();
                                    mediaList.Add(media);
                              }
                        }
                        await context.Medias.AddRangeAsync(mediaList);
                  }

                  if (!context.propertyLabels.Any())
                  {
                        var propertyLabels = new List<PropertyLabel>();
                        if (propertyList.Count == 0)
                        {
                              propertyList = context.Properties.ToList();
                        }
                        if (labels.Count == 0)
                        {
                              labels = context.Labels.ToList();
                        }

                        foreach (var property in propertyList)
                        {
                              var randomLabels = GetRandomlabels(labels, 2, 4);
                              foreach (var label in randomLabels)
                              {
                                    propertyLabels.Add(new PropertyLabel()
                                    {
                                          Property = property,
                                          Label = label
                                    });
                              }
                        }
                        await context.AddRangeAsync(propertyLabels);

                  }

                  await context.SaveChangesAsync();
            }
      }

      private static object GetRandomAmenities(string[] amenities, int v)
      {
            Random random = new Random();
            return amenities.OrderBy(a => random.Next()).Take(v).ToArray();
      }

      private static List<Label> GetRandomlabels(List<Label> labels, int min, int max)
      {
            Random random = new();
            var count = random.Next(min, max);
            return labels.OrderBy(a => random.Next()).Take(count).ToList();
      }


}
