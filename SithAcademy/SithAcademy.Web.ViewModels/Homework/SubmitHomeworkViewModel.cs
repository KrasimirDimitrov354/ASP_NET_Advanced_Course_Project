namespace SithAcademy.Web.ViewModels.Homework;

using System.ComponentModel.DataAnnotations;

using SithAcademy.Web.ViewModels.Trial;

using static SithAcademy.Common.EntityFieldValidation.Homework;

public class SubmitHomeworkViewModel
{
    [Required]
    [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
    public string Content { get; set; } = null!;

    public Trial_InfoViewModel TrialInfo { get; set; } = null!;
}
