namespace SithAcademy.Web.ViewModels.Homework;

public class DisplayHomeworkViewModel : SubmitHomeworkViewModel
{
    public string Id { get; set; } = null!;

    public decimal Score { get; set; }

    public string CreatedOn { get; set; } = null!;

    public string? Reviewer { get; set; }

    public string? Feedback { get; set; }
}
