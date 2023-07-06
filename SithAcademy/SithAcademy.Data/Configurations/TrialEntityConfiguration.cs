namespace SithAcademy.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SithAcademy.Data.Models;

public class TrialEntityConfiguration : IEntityTypeConfiguration<Trial>
{
    public void Configure(EntityTypeBuilder<Trial> builder)
    {
        builder
            .HasOne(t => t.Academy)
            .WithMany(a => a.Trials)
            .HasForeignKey(t => t.AcademyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
