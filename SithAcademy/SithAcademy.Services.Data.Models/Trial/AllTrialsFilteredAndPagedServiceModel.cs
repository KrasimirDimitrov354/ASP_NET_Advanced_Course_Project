namespace SithAcademy.Services.Data.Models.Trial;

using SithAcademy.Web.ViewModels.Trial;

public class AllTrialsFilteredAndPagedServiceModel
{
    public AllTrialsFilteredAndPagedServiceModel()
    {
        Trials = new HashSet<TrialSortingViewModel>();
    }

    public int TotalTrialsCount { get; set; }

    public IEnumerable<TrialSortingViewModel> Trials { get; set; }
}
