namespace SithAcademy.Web.ViewModels.Homework;

public class GradeHomeworkDetailsViewModel
{
    public string Content { get; set; } = null!;

    public string CreatedOn { get; set; } = null!;

    public string Submitter { get; set; } = null!;

    public decimal CurrentScore { get; set; }

    public string? LastReviewer { get; set; }

    public string? LastFeedback { get; set; }
}
