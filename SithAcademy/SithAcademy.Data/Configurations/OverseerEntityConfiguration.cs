namespace SithAcademy.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SithAcademy.Data.Models;
using SithAcademy.Data.Seeders;

public class OverseerEntityConfiguration : IEntityTypeConfiguration<Overseer>
{
    private readonly OverseerSeeder overseerSeeder;

    public OverseerEntityConfiguration()
    {
        overseerSeeder = new OverseerSeeder();
    }

    public void Configure(EntityTypeBuilder<Overseer> builder)
    {
        builder
            .HasOne(o => o.Academy)
            .WithMany(a => a.Overseers)
            .HasForeignKey(o => o.AcademyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(overseerSeeder.GenerateOverseers());
    }
}
