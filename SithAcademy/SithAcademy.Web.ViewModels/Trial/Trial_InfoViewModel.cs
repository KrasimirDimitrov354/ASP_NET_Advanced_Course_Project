namespace SithAcademy.Web.ViewModels.Trial;

public class Trial_InfoViewModel : TrialDropdownViewModel
{
    public string Description { get; set; } = null!;

    public decimal ScoreToPass { get; set; }
}
