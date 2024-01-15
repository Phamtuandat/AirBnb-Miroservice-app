using Microsoft.EntityFrameworkCore;

namespace PropertyApi.Data;

public class PropertyDbContext : DbContext
{
      private readonly IConfiguration _config;
      public PropertyDbContext(DbContextOptions<PropertyDbContext> options, IConfiguration config) : base(options)
      {
            _config = config;
      }

      protected override void OnConfiguring(DbContextOptionsBuilder options)
      {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(_config.GetConnectionString("MyAppContext"));
      }
      protected override void OnModelCreating(ModelBuilder builder)
      {

            base.OnModelCreating(builder);
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                  var tableName = entityType.GetTableName();
                  if (tableName == null)
                  {
                        continue;
                  }
                  if (tableName.StartsWith("AspNet"))
                  {
                        entityType.SetTableName(tableName.Substring(6));
                  }

            }
            builder.Entity<PropertyLabel>().HasKey(x => new { x.PropertyId, x.LabelId });
      }
      public DbSet<PlaceType> Types { set; get; }
      public DbSet<Property> Properties { get; set; }
      public DbSet<Householder> Hosts { get; set; }
      public DbSet<Media> Medias { get; set; }
      public DbSet<Review> Reviews { get; set; }
      public DbSet<Label> Labels { get; set; }
      public DbSet<PropertyLabel> propertyLabels { get; set; }
}