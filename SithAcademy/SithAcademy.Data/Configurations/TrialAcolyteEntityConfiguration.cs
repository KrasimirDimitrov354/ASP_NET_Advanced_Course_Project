namespace SithAcademy.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SithAcademy.Data.Models;
using SithAcademy.Data.Seeders;

public class TrialAcolyteEntityConfiguration : IEntityTypeConfiguration<TrialAcolyte>
{
    private readonly TrialAcolyteSeeder trialAcolyteSeeder;

    public TrialAcolyteEntityConfiguration()
    {
        trialAcolyteSeeder = new TrialAcolyteSeeder();
    }

    public void Configure(EntityTypeBuilder<TrialAcolyte> builder)
    {
        builder
            .Property(ta => ta.IsCompleted)
            .HasDefaultValue(false);

        builder.HasKey(ta => new { ta.TrialId, ta.AcolyteId });

        builder.HasData(trialAcolyteSeeder.GenerateTrialAcolytes());
    }
}
