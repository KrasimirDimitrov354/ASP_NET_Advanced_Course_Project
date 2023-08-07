namespace SithAcademy.Web.ViewModels.Resource;

using SithAcademy.Web.ViewModels.Trial;
using System.ComponentModel.DataAnnotations;

using static SithAcademy.Common.EntityFieldValidation.Resource;

public class ResourceFormViewModel
{
    public ResourceFormViewModel()
    {
        Trials = new HashSet<TrialDropdownViewModel>();
    }

    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(UrlMaxLength)]
    [RegularExpression(SourceUrlRegexPattern, ErrorMessage = SourceUrlErrorMessage)]
    [Display(Name = "Source link")]
    public string SourceUrl { get; set; } = null!;

    [Required]
    [MaxLength(UrlMaxLength)]
    [Display(Name = "Image link")]
    public string ImageUrl { get; set; } = null!;

    [Required]
    [Display(Name = "Trial")]
    public string TrialId { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual IEnumerable<TrialDropdownViewModel> Trials { get; set; }
}
