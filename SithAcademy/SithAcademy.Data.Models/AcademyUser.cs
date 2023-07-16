namespace SithAcademy.Data.Models;

using System;

using Microsoft.AspNetCore.Identity;

/// <summary>
/// This is a custom user class that works with the default ASP.NET Core Identity.
/// ID of users is Guid instead of String. Additional info to the built-in users can be added.
/// </summary>
public class AcademyUser : IdentityUser<Guid>
{
    public AcademyUser()
    {
        AcademyStatistics = new HashSet<AcademyStatistic>();
    }

    public virtual ICollection<AcademyStatistic> AcademyStatistics { get; set; }
}
