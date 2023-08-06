namespace SithAcademy.Web.ViewModels.Homework;

public class HomeworkSortingViewModel
{
    public string Id { get; set; } = null!;

    public string Submitter { get; set; } = null!;

    public string SubmittedOn { get; set; } = null!;

    public string TrialTitle { get; set; } = null!;

    public decimal HomeworkScore { get; set; }

    public decimal TrialScore { get; set; }
}
