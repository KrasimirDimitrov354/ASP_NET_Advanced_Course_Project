namespace SithAcademy.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SithAcademy.Data.Models;

public class HomeworkEntityConfiguration : IEntityTypeConfiguration<Homework>
{
    public void Configure(EntityTypeBuilder<Homework> builder)
    {
        builder
            .Property(h => h.IsApproved)
            .HasDefaultValue(false);

        builder
            .HasOne(h => h.Trial)
            .WithMany(t => t.PublishedHomeworks)
            .HasForeignKey(h => h.TrialId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(h => h.Acolyte)
            .WithMany(a => a.PublishedHomeworks)
            .HasForeignKey(h => h.AcolyteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
