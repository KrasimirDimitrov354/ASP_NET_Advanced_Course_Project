namespace SithAcademy.Web.ViewModels.Academy;

using System.ComponentModel.DataAnnotations;

using SithAcademy.Web.Views.Location;

public class AddAcademyViewModel : AcademyFormViewModel
{
    public AddAcademyViewModel()
    {
        Locations = new HashSet<LocationDropdownViewModel>();
    }

    [Required]
    [Display(Name = "Location")]
    public int LocationId { get; set; }

    public virtual IEnumerable<LocationDropdownViewModel> Locations { get; set; }
}
