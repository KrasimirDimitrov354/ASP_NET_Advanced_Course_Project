namespace SithAcademy.Web.ViewModels.Trial;

using SithAcademy.Web.ViewModels.Resource;

public class TrialDetailsViewModel : TrialOverviewViewModel
{
    public TrialDetailsViewModel()
    {
        Resources = new HashSet<ResourceDetailsViewModel>();
    }

    public string Description { get; set; } = null!;

    public bool IsLocked { get; set; }

    public virtual IEnumerable<ResourceDetailsViewModel> Resources { get; set; }
}
