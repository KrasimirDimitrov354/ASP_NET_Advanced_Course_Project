namespace SithAcademy.Data.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using static SithAcademy.Common.EntityFieldValidation.Overseer;
using static SithAcademy.Common.EntityColumnInformation.Overseer;

public class Overseer
{
    [Key]
    [Comment(IdComment)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(HoloFrequencyMaxLength)]
    [Comment(HoloFrequencyComment)]
    public string HoloFrequency { get; set; } = null!;

    [Required]
    [Comment(UserIdComment)]
    public Guid UserId { get; set; }
    public virtual AcademyUser User { get; set; } = null!;

    [Required]
    [Comment(AcademyIdComment)]
    public int AcademyId { get; set; }
    public virtual Academy Academy { get; set; } = null!;
}