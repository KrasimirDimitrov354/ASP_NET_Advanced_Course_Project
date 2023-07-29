namespace SithAcademy.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SithAcademy.Data.Models;
using SithAcademy.Data.Seeders;

public class AcolyteEntityConfiguration : IEntityTypeConfiguration<AcademyUser>
{
    private readonly AcolyteSeeder acolyteSeeder;

    public AcolyteEntityConfiguration()
    {
        acolyteSeeder = new AcolyteSeeder();
    }

    public void Configure(EntityTypeBuilder<AcademyUser> builder)
    {
        builder
            .HasOne(a => a.Location)
            .WithMany(l => l.Acolytes)
            .HasForeignKey(a => a.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(acolyteSeeder.GenerateAcolytes());
    }
}
