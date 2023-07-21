namespace SithAcademy.Data.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using static SithAcademy.Common.EntityFieldValidation.Trial;
using static SithAcademy.Common.EntityColumnInformation.Trial;

public class Trial
{
    public Trial()
    {
        Id = Guid.NewGuid();
        Resources = new HashSet<Resource>();
        PublishedHomeworks = new HashSet<Homework>();
        AssignedAcolytes = new HashSet<TrialAcolyte>();
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
    public virtual ICollection<Homework> PublishedHomeworks { get; set; }
    public virtual ICollection<TrialAcolyte> AssignedAcolytes { get; set; }
}