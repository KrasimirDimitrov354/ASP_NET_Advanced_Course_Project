namespace SithAcademy.Data;

using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using SithAcademy.Data.Models;

public class AcademyDbContext : IdentityDbContext<AcademyUser, IdentityRole<Guid>, Guid>
{
    public AcademyDbContext(DbContextOptions<AcademyDbContext> options)
        : base(options)
    {

    }

    public DbSet<Location> Locations { get; set; } = null!;
    public DbSet<Academy> Academies { get; set; } = null!;
    public DbSet<Overseer> Overseers { get; set; } = null!;
    public DbSet<Trial> Trials { get; set; } = null!;
    public DbSet<Resource> Resources { get; set; } = null!;
    public DbSet<Homework> Homeworks { get; set; } = null!;
    public DbSet<AcademyAcolyte> AcademiesAcolytes { get; set; } = null!;
    public DbSet<TrialAcolyte> TrialsAcolytes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        Assembly configAssembly = Assembly.GetAssembly(typeof(AcademyDbContext)) ??
                                  Assembly.GetExecutingAssembly();

        builder.ApplyConfigurationsFromAssembly(configAssembly);

        base.OnModelCreating(builder);
    }
}