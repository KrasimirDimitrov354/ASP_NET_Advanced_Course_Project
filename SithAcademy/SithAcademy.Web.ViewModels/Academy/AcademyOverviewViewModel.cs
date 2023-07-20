namespace SithAcademy.Web.ViewModels.Academy;

public class AcademyOverviewViewModel : AcademySummaryViewModel
{
    public int LocationId { get; set; }

    public string LocationName { get; set; } = null!;
}
