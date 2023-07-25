namespace SithAcademy.Services.Data;

using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using SithAcademy.Data;
using SithAcademy.Data.Models;
using SithAcademy.Web.ViewModels.Trial;
using SithAcademy.Web.ViewModels.Resource;
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

    public async Task<IEnumerable<IncompleteTrialViewModel>> GetIncompleteTrialsOfAcolyteAsync(string acolyteId)
    {
        IEnumerable<IncompleteTrialViewModel> trials = await dbContext.Users
            .Include(u => u.AssignedTrials)
            .ThenInclude(t => t.Trial)
            .ThenInclude(t => t.Academy)
            .Where(u => u.Id.ToString() == acolyteId)
            .Select(u => u.AssignedTrials
                .Where(t => !t.IsCompleted)
                .Select(t => new IncompleteTrialViewModel()
                {
                    Id = t.TrialId.ToString(),
                    Title = t.Trial.Title,
                    AcademyId = t.Trial.AcademyId,
                    AcademyTitle = t.Trial.Academy.Title
                })
                .ToArray())
            .FirstAsync();

        return trials;
    }

    public async Task<TrialDetailsViewModel> GetTrialDetailsAsync(string trialId)
    {
        TrialDetailsViewModel trial = await dbContext.Trials
            .Include(t => t.Resources)
            .Where(t => t.Id.ToString() == trialId)
            .Select(t => new TrialDetailsViewModel()
            {
                Title = t.Title,
                Description = t.Description,
                ScoreToPass = t.ScoreToPass.ToString(),
                IsLocked = t.IsLocked,
                Resources = t.Resources
                            .Where(r => !r.IsDeleted)
                            .Select(r => new ResourcePreviewViewModel()
                            {
                                Name = r.Name,
                                SourceUrl = r.SourceUrl,
                                ImageUrl = r.ImageUrl
                            })
                            .ToArray()
            })
            .FirstAsync();

        return trial;
    }

    public async Task<bool> TrialExistsAsync(string trialId)
    {
        return await dbContext.Trials.AnyAsync(t => t.Id.ToString() == trialId);
    }

    public async Task<bool> UserCanAccessTrialAsync(string trialId, string userId)
    {
        return await dbContext.TrialsAcolytes
            .AnyAsync(trial => trial.TrialId.ToString() == trialId && 
                      trial.AcolyteId.ToString() == userId);
    }

    public async Task<int> GetAcademyIdByTrialIdAsync(string trialId)
    {
        Trial trial = await dbContext.Trials
            .FirstAsync(t => t.Id.ToString() == trialId);

        return trial.AcademyId;
    }

    public async Task<string> AddTrialAndReturnTrialIdAsync(int academyId, TrialFormViewModel viewModel)
    {
        Trial newTrial = new Trial()
        {
            Title = viewModel.Title,
            Description = viewModel.Description,
            ScoreToPass = viewModel.ScoreToPass,
            IsLocked = viewModel.IsLocked,
            AcademyId = academyId
        };

        await dbContext.Trials.AddAsync(newTrial);
        await dbContext.SaveChangesAsync();

        return newTrial.Id.ToString();
    }

    public async Task AddTrialToAllAcolytesInAcademyAsync(string trialId, int academyId)
    {
        IEnumerable<AcademyUser> acolytes = await dbContext.Users
            .Include(u => u.JoinedAcademies)
            .Include(u => u.AssignedTrials)
            .Where(u => u.JoinedAcademies.Any(a => a.AcademyId == academyId))
            .ToArrayAsync();

        foreach (AcademyUser acolyte in acolytes)
        {
            acolyte.AssignedTrials.Add(new TrialAcolyte()
            {
                TrialId = Guid.Parse(trialId),
                AcolyteId = acolyte.Id
            });
        }

        await dbContext.SaveChangesAsync();
    }
}
