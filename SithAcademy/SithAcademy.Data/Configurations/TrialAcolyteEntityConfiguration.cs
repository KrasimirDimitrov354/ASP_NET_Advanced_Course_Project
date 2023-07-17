namespace SithAcademy.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SithAcademy.Data.Models;

public class TrialAcolyteEntityConfiguration : IEntityTypeConfiguration<TrialAcolyte>
{
    public void Configure(EntityTypeBuilder<TrialAcolyte> builder)
    {
        builder.HasKey(ta => new { ta.TrialId, ta.AcolyteId });
    }
}
