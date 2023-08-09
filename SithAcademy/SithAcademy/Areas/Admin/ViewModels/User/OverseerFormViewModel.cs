namespace SithAcademy.Web.Areas.Admin.ViewModels.User;

using System.ComponentModel.DataAnnotations;

using SithAcademy.Web.ViewModels.Academy;

using static SithAcademy.Common.EntityFieldValidation.Overseer;

public class OverseerFormViewModel
{
    public OverseerFormViewModel()
    {
        Academies = new HashSet<AcademyDropdownViewModel>();
    }

    [Required]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; } = null!;

    public int AcademyId { get; set; }

    public virtual IEnumerable<AcademyDropdownViewModel> Academies { get; set; }
}
