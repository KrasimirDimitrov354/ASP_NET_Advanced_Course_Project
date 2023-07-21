namespace SithAcademy.Data.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using static SithAcademy.Common.EntityFieldValidation.Overseer;
using static SithAcademy.Common.EntityColumnInformation.Overseer;

public class Overseer
{
    public Overseer()
    {
        Id = Guid.NewGuid();
    }

    [Key]
    [Comment(IdComment)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(TitleMaxLength)]
    [Comment(TitleComment)]
    public string Title { get; set; } = null!;

    [Required]
    [Comment(UserIdComment)]
    public Guid UserId { get; set; }
    public virtual AcademyUser User { get; set; } = null!;

    [Required]
    [Comment(AcademyIdComment)]
    public int AcademyId { get; set; }
    public virtual Academy Academy { get; set; } = null!;
}