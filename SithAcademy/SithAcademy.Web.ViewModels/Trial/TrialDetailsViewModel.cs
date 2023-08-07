namespace SithAcademy.Web.ViewModels.Trial;

using SithAcademy.Web.ViewModels.Resource;

public class TrialDetailsViewModel : TrialDropdownViewModel
{
    public TrialDetailsViewModel()
    {
        Resources = new HashSet<ResourcePreviewViewModel>();
    }

    public string Description { get; set; } = null!;

    public string ScoreToPass { get; set; } = null!;

    public bool IsLocked { get; set; }

    public virtual IEnumerable<ResourcePreviewViewModel> Resources { get; set; }
}
