namespace SithAcademy.Data.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using static SithAcademy.Common.EntityFieldValidation.Academy;
using static SithAcademy.Common.EntityColumnInformation.Academy;

public class Academy
{
    public Academy()
    {
        Trials = new HashSet<Trial>();
        Overseers = new HashSet<Overseer>();
        Acolytes = new HashSet<AcademyAcolyte>();
    }

    [Key]
    [Comment(IdComment)]
    public int Id { get; set; }

    [Required]
    [MaxLength(TitleMaxLength)]
    [Comment(TitleComment)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(DescriptionMaxLength)]
    [Comment(DescriptionComment)]
    public string Description { get; set; } = null!;

    [Required]
    [MaxLength(UrlMaxLength)]
    [Comment(ImageUrlComment)]
    public string ImageUrl { get; set; } = null!;

    [Required]
    [Comment(LocationIdComment)]
    public int LocationId { get; set; }
    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<Trial> Trials { get; set; }
    public virtual ICollection<Overseer> Overseers { get; set; }
    public virtual ICollection<AcademyAcolyte> Acolytes { get; set; }
}
