﻿namespace SithAcademy.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SithAcademy.Data.Models;

public class AcademyAcolyteEntityConfiguration : IEntityTypeConfiguration<AcademyAcolyte>
{
    public void Configure(EntityTypeBuilder<AcademyAcolyte> builder)
    {
        builder.HasKey(aa => new { aa.AcademyId, aa.AcolyteId });
    }
}