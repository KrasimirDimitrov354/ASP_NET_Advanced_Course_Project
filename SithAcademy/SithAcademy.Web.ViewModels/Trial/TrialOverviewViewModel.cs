namespace SithAcademy.Web.ViewModels.Trial;

public class TrialOverviewViewModel
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set;} = null!;

    public bool IsCompleted { get; set; }

    public int AcademyId { get; set; }

    public string AcademyTitle { get; set; } = null!;
}
