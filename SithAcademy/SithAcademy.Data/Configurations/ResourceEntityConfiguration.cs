namespace SithAcademy.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SithAcademy.Data.Models;
using SithAcademy.Data.Seeders;

public class ResourceEntityConfiguration : IEntityTypeConfiguration<Resource>
{
    private readonly ResourceSeeder resourceSeeder;

    public ResourceEntityConfiguration()
    {
        resourceSeeder = new ResourceSeeder();
    }

    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder
            .Property(r => r.IsDeleted)
            .HasDefaultValue(false);

        builder
            .HasOne(r => r.Trial)
            .WithMany(t => t.Resources)
            .HasForeignKey(r => r.TrialId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(resourceSeeder.GenerateResources());
    }
}
