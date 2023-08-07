namespace SithAcademy.Web.ViewModels.Trial;

using SithAcademy.Web.ViewModels.Academy;
using System.ComponentModel.DataAnnotations;

using static SithAcademy.Common.EntityFieldValidation.Trial;

public class TrialFormViewModel
{
    public TrialFormViewModel()
    {
        Academies = new HashSet<AcademyDropdownViewModel>();
    }

    [Required]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
    public string Description { get; set; } = null!;

    [Required]
    [Range(typeof(decimal), ScoreMinValue, ScoreMaxValue, ErrorMessage = ScoreRangeErrorMessage)]
    [Display(Name = "Score to pass")]
    public decimal ScoreToPass { get; set; }

    public bool IsLocked { get; set; }

    [Display(Name = "Academy")]
    public int? AcademyId { get; set; }

    public virtual IEnumerable<AcademyDropdownViewModel> Academies { get; set; }
}
