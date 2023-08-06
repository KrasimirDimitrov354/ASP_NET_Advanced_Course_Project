namespace SithAcademy.Web.ViewModels.Query;

using System.ComponentModel.DataAnnotations;

using SithAcademy.Web.ViewModels.Trial;
using SithAcademy.Web.ViewModels.Query.Enums;

public class AllTrialsQueryModel : GenericQueryModel
{
    public AllTrialsQueryModel()
    {
        Academies = new HashSet<string>();
        Trials = new HashSet<TrialSortingViewModel>();
    }

    public string? Academy { get; set; }

    [Display(Name = "Lock status")]
    public TrialSelect TrialSelect { get; set; }

    public IEnumerable<string> Academies { get; set; }

    public IEnumerable<TrialSortingViewModel> Trials { get; set; }
}
