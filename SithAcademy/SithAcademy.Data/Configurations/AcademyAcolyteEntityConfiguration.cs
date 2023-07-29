namespace SithAcademy.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SithAcademy.Data.Models;
using SithAcademy.Data.Seeders;

public class AcademyAcolyteEntityConfiguration : IEntityTypeConfiguration<AcademyAcolyte>
{
    private readonly AcademyAcolyteSeeder academyAcolyteSeeder;

    public AcademyAcolyteEntityConfiguration()
    {
        academyAcolyteSeeder = new AcademyAcolyteSeeder();
    }

    public void Configure(EntityTypeBuilder<AcademyAcolyte> builder)
    {
        builder.HasKey(aa => new { aa.AcademyId, aa.AcolyteId });

        builder.HasData(academyAcolyteSeeder.GenerateAcademyAcolytes());
    }
}
