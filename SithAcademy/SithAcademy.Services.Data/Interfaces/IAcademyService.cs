namespace SithAcademy.Services.Data.Interfaces;

using SithAcademy.Web.ViewModels.Academy;

public interface IAcademyService
{
    Task<IEnumerable<AcademyOverviewViewModel>> GetAllAcademiesAsync();

    Task<AcademyDetailsViewModel> DisplayAcademyDetailsAsync(int academyId);

    Task<bool> AcademyExistsAndIsNotLocked(int academyId);

    Task<bool> AcolyteExistsInAcademyAsync(int academyId, string acolyteId);

    Task AddAcolyteToAcademyAsync(int academyId, string acolyteId);
}
