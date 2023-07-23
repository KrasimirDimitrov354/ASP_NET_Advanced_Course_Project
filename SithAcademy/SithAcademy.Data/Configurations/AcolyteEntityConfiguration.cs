namespace SithAcademy.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SithAcademy.Data.Models;

public class AcolyteEntityConfiguration : IEntityTypeConfiguration<AcademyUser>
{
    public void Configure(EntityTypeBuilder<AcademyUser> builder)
    {
        builder
            .HasOne(a => a.Location)
            .WithMany(l => l.Acolytes)
            .HasForeignKey(a => a.LocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
