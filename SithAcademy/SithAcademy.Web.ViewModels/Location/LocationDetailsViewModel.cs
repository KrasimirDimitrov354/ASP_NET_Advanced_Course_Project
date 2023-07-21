namespace SithAcademy.Web.ViewModels.Location;

using SithAcademy.Web.ViewModels.Academy;

public class LocationDetailsViewModel : LocationOverviewViewModel
{
    public LocationDetailsViewModel()
    {
        Academies = new HashSet<AcademySummaryViewModel>();
    }

    public string Description { get; set; } = null!;

    public bool IsLocked { get; set; }

    public virtual IEnumerable<AcademySummaryViewModel> Academies { get; set; }
}
