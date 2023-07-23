namespace SithAcademy.Services.Data;

using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using SithAcademy.Data;
using SithAcademy.Data.Models;
using SithAcademy.Web.ViewModels.Trial;
using SithAcademy.Services.Data.Interfaces;

public class TrialService : ITrialService
{
    private readonly AcademyDbContext dbContext;

    public TrialService(AcademyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AssignTrialsToAcolyteAsync(int academyId, string acolyteId)
    {
        Academy academy = await dbContext.Academies
            .Include(a => a.Trials)
            .ThenInclude(t => t.AssignedAcolytes)
            .FirstAsync(a => a.Id == academyId);

        foreach (Trial trial in academy.Trials)
        {
            if (!trial.AssignedAcolytes.Any(aa => aa.AcolyteId.ToString() == acolyteId))
            {
                trial.AssignedAcolytes.Add(new TrialAcolyte()
                {
                    TrialId = trial.Id,
                    AcolyteId = Guid.Parse(acolyteId)
                });
            }
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveTrialsFromAcolyteAsync(string acolyteId)
    {
        IEnumerable<TrialAcolyte> trialsToRemove = await dbContext.TrialsAcolytes
            .Where(ta => ta.AcolyteId.ToString() == acolyteId &&
                        !ta.IsCompleted)
            .ToArrayAsync();

        dbContext.TrialsAcolytes.RemoveRange(trialsToRemove);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<AcolyteTrialViewModel>> GetAllTrialsOfAcolyteAsync(string acolyteId)
    {
        IEnumerable<AcolyteTrialViewModel> trials = await dbContext.Users
            .Where(u => u.Id.ToString() == acolyteId)
            .Select(u => u.AssignedTrials
                .Select(t => new AcolyteTrialViewModel()
                {
                    Id = t.TrialId.ToString(),
                    Title = t.Trial.Title,
                    IsCompleted = t.IsCompleted,
                    AcademyId = t.Trial.AcademyId,
                    AcademyTitle = t.Trial.Academy.Title
                })
                .ToArray())
            .FirstAsync();

        return trials;
    }
}
