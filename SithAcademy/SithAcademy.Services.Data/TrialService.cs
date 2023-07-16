namespace SithAcademy.Services.Data;

using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using SithAcademy.Data;
using SithAcademy.Web.ViewModels.Trial;
using SithAcademy.Services.Data.Interfaces;

public class TrialService : ITrialService
{
    private readonly AcademyDbContext dbContext;

    public TrialService(AcademyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<TrialOverviewViewModel>> GetInProgressTrialsOfUserAsync(string acolyteId)
    {
        IEnumerable<TrialOverviewViewModel> trials = await dbContext.Users
            .Where(u => u.Id.ToString() == acolyteId)
            .Select(u => u.AcademyStatistics
                .Select(t => new TrialOverviewViewModel()
                {
                    Id = t.TrialId.ToString(),
                    Title = t.Trial.Title,
                    Description = t.Trial.Description,
                    IsCompleted = t.IsCompleted,
                    AcademyId = t.Trial.AcademyId,
                    AcademyTitle = t.Trial.Academy.Title
                })
                .ToArray())
            .FirstAsync();

        return trials;
    }
}
