namespace SithAcademy.Web.ViewModels.Trial;

public class AcolyteTrialViewModel : TrialOverviewViewModel
{
    public bool IsCompleted { get; set; }

    public int AcademyId { get; set; }

    public string AcademyTitle { get; set; } = null!;
}
