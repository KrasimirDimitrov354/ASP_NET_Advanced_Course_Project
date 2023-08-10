namespace SithAcademy.Web.ViewModels.Location;

using System.ComponentModel.DataAnnotations;

using static SithAcademy.Common.EntityFieldValidation.Location;

public class LocationFormViewModel
{
    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
    public string Description { get; set; } = null!;

    [Required]
    [Display(Name = "Image Link")]
    [StringLength(UrlMaxLength)]
    public string ImageUrl { get; set; } = null!;

    public bool IsLocked { get; set; }
}
