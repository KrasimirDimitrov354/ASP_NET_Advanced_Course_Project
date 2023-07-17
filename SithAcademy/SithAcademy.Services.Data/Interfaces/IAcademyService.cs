namespace SithAcademy.Services.Data.Interfaces;

using SithAcademy.Web.ViewModels.Academy;

public interface IAcademyService
{
    Task<IEnumerable<AcademyOverviewViewModel>> GetAllAcademiesAsync();
}
