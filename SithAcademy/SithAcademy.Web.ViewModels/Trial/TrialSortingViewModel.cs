namespace SithAcademy.Web.ViewModels.Trial;

using System.ComponentModel.DataAnnotations;

public class TrialSortingViewModel : TrialDropdownViewModel
{
    [Display(Name = "Is Locked")]
    public bool IsLocked { get; set; }
}
