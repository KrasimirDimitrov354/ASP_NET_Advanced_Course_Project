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
        var acolyte = await dbContext.Users
            .Where(u => u.Id.ToString() == acolyteId)
            .FirstAsync();

        IEnumerable<TrialOverviewViewModel> trials = acolyte.InProgressTrials
            .Select(t => new TrialOverviewViewModel()
            {
                Id = t.Id.ToString(),
                Title = t.Title,
                Description = t.Description,
                AcademyId = t.AcademyId,
                AcademyTitle = t.Academy.Title
            })
            .ToArray();

        return trials;
    }
}
