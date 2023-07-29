namespace SithAcademy.Web.ViewModels.Trial;

using System.ComponentModel.DataAnnotations;

public class TrialSortingViewModel : TrialOverviewViewModel
{
    [Display(Name = "Is Locked")]
    public bool IsLocked { get; set; }
}
