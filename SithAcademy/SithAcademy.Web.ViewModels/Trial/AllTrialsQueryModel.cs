namespace SithAcademy.Web.ViewModels.Trial;

using System.ComponentModel.DataAnnotations;

using SithAcademy.Web.ViewModels.Trial.Enums;

using static SithAcademy.Common.GeneralConstants;

public class AllTrialsQueryModel
{
    public AllTrialsQueryModel()
    {
        Academies = new HashSet<string>();
        Trials = new HashSet<TrialSortingViewModel>();

        CurrentPage = SortingDefaultPage;
        TrialsPerPage = SortingDefaultEntitiesPerPage;
    }

    public string? Academy { get; set; }

    [Display(Name = "Enter a term to search for")]
    public string? SearchTerm { get; set; }

    [Display(Name = "Lock status")]
    public TrialSelect TrialSelect { get; set; }

    public int CurrentPage { get; set; }

    [Display(Name = "Trials Per Page")]
    public int TrialsPerPage { get; set; }

    public int TotalTrials { get; set; }

    public IEnumerable<string> Academies { get; set; }

    public IEnumerable<TrialSortingViewModel> Trials { get; set; }
}
