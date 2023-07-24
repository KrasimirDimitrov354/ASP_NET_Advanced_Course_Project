namespace SithAcademy.Web.ViewModels.Trial;

using System.ComponentModel.DataAnnotations;

using static SithAcademy.Common.EntityFieldValidation.Trial;

public class TrialFormViewModel
{
    [Required]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
    public string Description { get; set; } = null!;

    [Range(typeof(decimal), ScoreMinValue, ScoreMaxValue)]
    [Display(Name = "Score to pass")]
    public decimal ScoreToPass { get; set; }

    public bool IsLocked { get; set; }
}
