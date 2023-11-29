namespace Mvc.Infrastructure;

public static class API
{
      public static class Catalog
      {
            public static string GetAllProducts(string baseUri, int page, int take, int? brand, int? type)
            {
                  var filterQs = "";

                  if (type.HasValue)
                  {
                        var brandQs = (brand.HasValue) ? brand.Value.ToString() : string.Empty;
                        filterQs = $"/category/{type.Value}/brand/{brandQs}";

                  }
                  else if (brand.HasValue)
                  {
                        var brandQs = (brand.HasValue) ? brand.Value.ToString() : string.Empty;
                        filterQs = $"/type/all/brand/{brandQs}";
                  }
                  else
                  {
                        filterQs = string.Empty;
                  }

                  return $"{baseUri}items{filterQs}?pageIndex={page}&pageSize={take}";
            }

            public static string GetAllBrands(string baseUri)
            {
                  return $"{baseUri}Brand";
            }

            public static string GetAllCategories(string baseUri)
            {
                  return $"{baseUri}Category";
            }
      }
}