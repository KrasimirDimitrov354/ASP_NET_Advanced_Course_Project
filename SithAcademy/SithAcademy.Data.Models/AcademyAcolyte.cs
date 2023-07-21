namespace SithAcademy.Data.Models;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using static SithAcademy.Common.EntityColumnInformation.AcademyAcolyte;

public class AcademyAcolyte
{
    [Required]
    [Comment(AcademyIdComment)]
    public int AcademyId { get; set; }
    public virtual Academy Academy { get; set; } = null!;

    [Required]
    [Comment(AcolyteIdComment)]
    public Guid AcolyteId { get; set; }
    public virtual AcademyUser Acolyte { get; set; } = null!;

    [Required]
    [Comment(IsGraduatedComment)]
    public bool IsGraduated { get; set; }
}
