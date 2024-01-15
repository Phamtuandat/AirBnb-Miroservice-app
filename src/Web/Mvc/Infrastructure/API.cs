namespace Mvc.Infrastructure;

public static class API
{
      public static class Catalog
      {
            public static string GetProperties(string baseUri, int page, int take, string? host, string? type, string? sortOrder)
            {
                  var filterQs = "";
                  if (host != null && type != null)
                  {
                        filterQs = $"/type/{type}/host/{host}";
                  }
                  else if (host == null && type != null)
                  {
                        filterQs = $"/type/{type}";
                  }
                  else if (host != null && type == null)
                  {
                        filterQs = $"/host/{host}";
                  }
                  else
                  {
                        filterQs = string.Empty;
                  }

                  return $"{baseUri}items{filterQs}?sortBy={sortOrder}&lt=10000&gt=0&pageIndex={page - 1}&pageSize={take}";
            }
            public static string GetPropertyById(string baseUri, string id)
            {
                  return $"{baseUri}items/{id}";
            }
            public static string SearchProperties(string baseUri, int page, int take, string? city, string? country)
            {
                  var searhQs = $"?country={country}&city={city}";
                  return $"{baseUri}items/search{searhQs}&sortBy=Title&lt=10000&gt=0&pageIndex={page - 1}&pageSize={take}";
            }

            public static string GetAllHost(string baseUri)
            {
                  return $"{baseUri}Host";
            }

            public static string GetAllTypes(string baseUri)
            {
                  return $"{baseUri}Type";
            }
      }
}