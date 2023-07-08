namespace SithAcademy.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SithAcademy.Data.Models;
using SithAcademy.Data.Seeders;

public class TrialEntityConfiguration : IEntityTypeConfiguration<Trial>
{
    private readonly TrialSeeder trialSeeder;

    public TrialEntityConfiguration()
    {
        trialSeeder = new TrialSeeder();
    }

    public void Configure(EntityTypeBuilder<Trial> builder)
    {
        builder
            .HasOne(t => t.Academy)
            .WithMany(a => a.Trials)
            .HasForeignKey(t => t.AcademyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(trialSeeder.GenerateTrials());
    }
}
