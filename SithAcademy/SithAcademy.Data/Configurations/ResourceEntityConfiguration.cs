namespace SithAcademy.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SithAcademy.Data.Models;

public class ResourceEntityConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder
            .HasOne(r => r.Trial)
            .WithMany(t => t.Resources)
            .HasForeignKey(r => r.TrialId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
