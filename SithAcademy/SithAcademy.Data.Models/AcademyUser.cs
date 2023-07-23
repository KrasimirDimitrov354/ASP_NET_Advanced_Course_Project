namespace SithAcademy.Data.Models;

using System;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static SithAcademy.Common.EntityColumnInformation.Acolyte;

/// <summary>
/// This is a custom user class that works with the default ASP.NET Core Identity.
/// ID of users is Guid instead of String. Additional info to the built-in users can be added.
/// </summary>
public class AcademyUser : IdentityUser<Guid>
{
    public AcademyUser()
    {
        JoinedAcademies = new HashSet<AcademyAcolyte>();
        AssignedTrials = new HashSet<TrialAcolyte>();
        PublishedHomeworks = new HashSet<Homework>();
    }

    [Comment(LocationIdComment)]
    public int? LocationId { get; set; }
    public virtual Location? Location { get; set; }

    public virtual ICollection<AcademyAcolyte> JoinedAcademies { get; set; }
    public virtual ICollection<TrialAcolyte> AssignedTrials { get; set; }
    public virtual ICollection<Homework> PublishedHomeworks { get; set; }
}
