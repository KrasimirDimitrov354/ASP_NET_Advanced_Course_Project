namespace SithAcademy.Web.ViewModels.Academy;

public class AcademyOverviewViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int LocationId { get; set; }

    public string LocationName { get; set; } = null!;
}
