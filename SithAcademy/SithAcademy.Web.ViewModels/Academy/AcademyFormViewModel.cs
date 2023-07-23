namespace SithAcademy.Web.ViewModels.Academy;

using System.ComponentModel.DataAnnotations;

using static SithAcademy.Common.EntityFieldValidation.Academy;

public class AcademyFormViewModel
{
    [Required]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(UrlMaxLength)]
    public string ImageUrl { get; set; } = null!;

    [Required]
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
    public string Description { get; set; } = null!;

    public bool IsLocked { get; set; }
}
