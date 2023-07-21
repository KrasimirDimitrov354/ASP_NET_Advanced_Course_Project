namespace SithAcademy.Data.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using static SithAcademy.Common.EntityFieldValidation.Location;
using static SithAcademy.Common.EntityColumnInformation.Location;

public class Location
{
    public Location()
    {
        Academies = new HashSet<Academy>();
    }

    [Key]
    [Comment(IdComment)]
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    [Comment(NameComment)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(UrlMaxLength)]
    [Comment(ImageUrlComment)]
    public string ImageUrl { get; set; } = null!;

    [Required]
    [MaxLength(DescriptionMaxLength)]
    [Comment(DescriptionComment)]
    public string Description { get; set; } = null!;

    public virtual ICollection<Academy> Academies { get; set; }
}