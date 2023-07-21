namespace SithAcademy.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SithAcademy.Data.Models;
using SithAcademy.Data.Seeders;

public class LocationEntityConfiguration : IEntityTypeConfiguration<Location>
{
    private readonly LocationSeeder locationSeeder;

    public LocationEntityConfiguration()
    {
        locationSeeder = new LocationSeeder();
    }

    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder
            .Property(l => l.IsLocked)
            .HasDefaultValue(false);

        builder.HasData(locationSeeder.GenerateLocations());
    }
}
