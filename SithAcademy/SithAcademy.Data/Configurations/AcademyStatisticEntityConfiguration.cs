namespace SithAcademy.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SithAcademy.Data.Models;

public class AcademyStatisticEntityConfiguration : IEntityTypeConfiguration<AcademyStatistic>
{
    public void Configure(EntityTypeBuilder<AcademyStatistic> builder)
    {
        builder.HasKey(ck => new { ck.AcolyteId, ck.TrialId });
    }
}
