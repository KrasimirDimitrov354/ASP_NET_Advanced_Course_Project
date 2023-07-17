namespace SithAcademy.Services.Data;

using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using SithAcademy.Data;
using SithAcademy.Web.ViewModels.Academy;
using SithAcademy.Services.Data.Interfaces;

public class AcademyService : IAcademyService
{
    private readonly AcademyDbContext dbContext;

    public AcademyService(AcademyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<AcademyOverviewViewModel>> GetAllAcademiesAsync()
    {
        IEnumerable<AcademyOverviewViewModel> academies = await dbContext.Academies
            .Select(a => new AcademyOverviewViewModel()
            {
                Id = a.Id,
                Title = a.Title,
                LocationId = a.LocationId,
                LocationName = a.Location.Name
            })
            .ToArrayAsync();

        return academies;
    }
}
