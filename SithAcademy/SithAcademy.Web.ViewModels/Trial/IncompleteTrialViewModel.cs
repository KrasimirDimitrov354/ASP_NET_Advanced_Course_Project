namespace SithAcademy.Web.ViewModels.Trial;

public class IncompleteTrialViewModel : TrialDropdownViewModel
{
    public int AcademyId { get; set; }

    public string AcademyTitle { get; set; } = null!;
}
