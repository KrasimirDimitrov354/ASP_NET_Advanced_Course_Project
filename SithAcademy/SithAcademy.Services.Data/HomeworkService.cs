namespace SithAcademy.Services.Data;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SithAcademy.Data;
using SithAcademy.Data.Models;
using SithAcademy.Services.Data.Interfaces;

public class HomeworkService : IHomeworkService
{
    private readonly AcademyDbContext dbContext;

    public HomeworkService(AcademyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> TrialHasHomework(string trialId, string acolyteId)
    {
        return await dbContext.Homeworks
            .AnyAsync(h => h.TrialId.ToString() == trialId && 
                           h.AcolyteId.ToString() == acolyteId);
    }

    public async Task<string> GetHomeworkIdByAcolyteIdAndTrialId(string acolyteId, string trialId)
    {
        Homework homework = await dbContext.Homeworks
            .FirstAsync(h => h.AcolyteId.ToString() == acolyteId &&
                             h.TrialId.ToString() == trialId);

        return homework.Id.ToString();
    }
}
