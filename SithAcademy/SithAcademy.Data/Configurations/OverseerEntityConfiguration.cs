namespace SithAcademy.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SithAcademy.Data.Models;

public class OverseerEntityConfiguration : IEntityTypeConfiguration<Overseer>
{
    public void Configure(EntityTypeBuilder<Overseer> builder)
    {
        builder
            .HasOne(o => o.Academy)
            .WithMany(a => a.Overseers)
            .HasForeignKey(o => o.AcademyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
