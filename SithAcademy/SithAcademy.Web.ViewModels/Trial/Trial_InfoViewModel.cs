namespace SithAcademy.Web.ViewModels.Trial;

public class Trial_InfoViewModel : TrialOverviewViewModel
{
    public string Description { get; set; } = null!;

    public decimal ScoreToPass { get; set; }
}
