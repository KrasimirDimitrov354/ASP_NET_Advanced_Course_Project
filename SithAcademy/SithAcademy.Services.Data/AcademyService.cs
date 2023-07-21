namespace SithAcademy.Services.Data;

using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using SithAcademy.Data;
using SithAcademy.Data.Models;
using SithAcademy.Web.ViewModels.Trial;
using SithAcademy.Web.ViewModels.Acolyte;
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
            .Where(a => !a.Location.IsLocked)
            .Select(a => new AcademyOverviewViewModel()
            {
                Id = a.Id,
                Title = a.Title,
                ImageUrl = a.ImageUrl,
                LocationId = a.LocationId,
                LocationName = a.Location.Name
            })
            .ToArrayAsync();

        return academies;
    }

    public async Task<AcademyDetailsViewModel> DisplayAcademyDetailsAsync(int academyId)
    {
        AcademyDetailsViewModel academyDetails = await dbContext.Academies
            .Include(a => a.Location)
            .Include(a => a.Trials)
            .Include(a => a.Acolytes)
            .Where(a => a.Id == academyId && !a.Location.IsLocked)
            .Select(a => new AcademyDetailsViewModel()
            {
                Id = a.Id,
                Title = a.Title,
                ImageUrl = a.ImageUrl,
                LocationId = a.LocationId,
                LocationName = a.Location.Name,
                Description = a.Description,
                IsLocked = a.IsLocked,
                Trials = a.Trials
                        .Select(t => new TrialOverviewViewModel()
                        {
                            Id = t.Id.ToString(),
                            Title = t.Title,
                        })
                        .ToArray(),
                Acolytes = a.Acolytes
                                .Select(acolyte => new AcolyteViewModel()
                                {
                                    Id = acolyte.AcolyteId.ToString()
                                })
                                .ToArray()
            })
            .FirstAsync();

        return academyDetails;
    }

    public async Task<bool> AcademyExistsAndIsNotLocked(int academyId)
    {
        return await dbContext.Academies
            .Where(a => !a.IsLocked)
            .AnyAsync(a => a.Id == academyId);
    }

    public async Task<bool> AcolyteExistsInAcademyAsync(int academyId, string acolyteId)
    {
        Academy academy = await dbContext.Academies
            .Where(a => !a.IsLocked)
            .FirstAsync(a => a.Id == academyId);

        return academy.Acolytes.Any(a => a.AcolyteId.ToString() == acolyteId);
    }

    public async Task AddAcolyteToAcademyAsync(int academyId, string acolyteId)
    {
        Academy academy = await dbContext.Academies
            .Where(a => !a.IsLocked)
            .FirstAsync(a => a.Id == academyId);

        academy.Acolytes.Add(new AcademyAcolyte()
        {
            AcademyId = academyId,
            AcolyteId = Guid.Parse(acolyteId)
        });

        await dbContext.SaveChangesAsync();
    }
}
