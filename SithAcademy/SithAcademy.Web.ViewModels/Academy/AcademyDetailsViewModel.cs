namespace SithAcademy.Web.ViewModels.Academy;

using SithAcademy.Web.ViewModels.Acolyte;
using SithAcademy.Web.ViewModels.Trial;

public class AcademyDetailsViewModel : AcademySummaryViewModel
{
    public AcademyDetailsViewModel()
    {
        Trials = new HashSet<TrialDropdownViewModel>();
        Acolytes = new HashSet<AcolyteViewModel>();
    }

    public string Description { get; set; } = null!;

    public bool IsLocked { get; set; }

    public virtual IEnumerable<TrialDropdownViewModel> Trials { get; set; }

    public virtual IEnumerable<AcolyteViewModel> Acolytes { get; set; }
}
