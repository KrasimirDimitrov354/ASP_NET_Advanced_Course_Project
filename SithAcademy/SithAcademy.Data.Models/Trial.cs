namespace SithAcademy.Data.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using static SithAcademy.Common.EntityFieldValidation.Trial;
using static SithAcademy.Common.EntityColumnInformation.Trial;
using System.ComponentModel.DataAnnotations.Schema;

public class Trial
{
    public Trial()
    {
        Acolytes = new HashSet<AcademyUser>();
        Resources = new HashSet<Resource>();
    }

    [Key]
    [Comment(IdComment)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(TitleMaxLength)]
    [Comment(TitleComment)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(DescriptionMaxLength)]
    [Comment(DescriptionComment)]
    public string Description { get; set;} = null!;

    [Required]
    [Comment(AcademyIdComment)]
    public int AcademyId { get; set; }
    public virtual Academy Academy { get; set; } = null!;

    public virtual ICollection<Resource> Resources { get; set; }

    //TODO - FIX THIS
    [NotMapped]
    public virtual ICollection<AcademyUser> Acolytes { get; set; }
}