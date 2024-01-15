
using System.Text;
using System.Text.RegularExpressions;
internal static class Extensions
{
      public static void AddScopedServices(this IServiceCollection services)
      {
            // Register unit of work service 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Register scope services
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IHostService, HostService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddCors(options =>
                  {
                        options.AddPolicy("AllowSpecificOrigin",
                                    builder =>
                                    {
                                          builder
                                    .AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    ;
                                    });
                  });
      }

      public static string GenerateSlug(this string phrase)
      {
            string str = phrase.ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", "").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
      }


}