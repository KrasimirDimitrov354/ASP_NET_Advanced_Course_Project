namespace SithAcademy.Web.ViewModels.Academy;

using SithAcademy.Web.ViewModels.Acolyte;
using SithAcademy.Web.ViewModels.Trial;

public class AcademyDetailsViewModel : AcademyOverviewViewModel
{
    public AcademyDetailsViewModel()
    {
        Trials = new HashSet<TrialOverviewViewModel>();
        SignedAcolytes = new HashSet<AcolyteViewModel>();
    }

    public string Description { get; set; } = null!;

    public virtual IEnumerable<TrialOverviewViewModel> Trials { get; set; }

    public virtual IEnumerable<AcolyteViewModel> SignedAcolytes { get; set; }
}
