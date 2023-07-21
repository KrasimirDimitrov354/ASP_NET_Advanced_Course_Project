namespace SithAcademy.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SithAcademy.Data.Models;
using SithAcademy.Data.Seeders;

public class AcademyEntityConfiguration : IEntityTypeConfiguration<Academy>
{
    private readonly AcademySeeder academySeeder;

    public AcademyEntityConfiguration()
    {
        academySeeder = new AcademySeeder();
    }

    public void Configure(EntityTypeBuilder<Academy> builder)
    {
        builder
            .Property(a => a.IsLocked)
            .HasDefaultValue(false);

        builder
            .HasOne(a => a.Location)
            .WithMany(l => l.Academies)
            .HasForeignKey(a => a.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(academySeeder.GenerateAcademies());
    }
}
