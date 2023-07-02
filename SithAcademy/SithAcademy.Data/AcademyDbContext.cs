namespace SithAcademy.Data;

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
}