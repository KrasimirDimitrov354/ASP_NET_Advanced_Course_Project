namespace SithAcademy.Web.ViewModels.Homework;

using System.ComponentModel.DataAnnotations;

using static SithAcademy.Common.EntityFieldValidation.Homework;

public class GradeHomeworkViewModel
{
    public string Id { get; set; } = null!;

    [Required]
    [Range(typeof(decimal), ScoreMinValue, ScoreMaxValue, ErrorMessage = ScoreRangeErrorMessage)]
    public decimal Score { get; set; }

    [Required]
    [StringLength(FeedbackMaxLength, MinimumLength = FeedbackMinLength)]
    public string? Feedback { get; set; }
}
