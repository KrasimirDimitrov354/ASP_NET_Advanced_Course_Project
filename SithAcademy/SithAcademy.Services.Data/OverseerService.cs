namespace SithAcademy.Services.Data;

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SithAcademy.Data;
using SithAcademy.Data.Models;
using SithAcademy.Services.Data.Interfaces;

public class OverseerService : IOverseerService
{
    private readonly AcademyDbContext dbContext;

    public OverseerService(AcademyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> UserIsOverseerAsync(string userId)
    {
        return await dbContext.Overseers.AnyAsync(o => o.UserId.ToString() == userId);
    }

    public async Task<string> GetOverseerIdAsync(string userId)
    {
        Overseer overseer = await dbContext.Overseers
            .FirstAsync(o => o.UserId.ToString() == userId);

        return overseer.Id.ToString();
    }

    public async Task<bool> OverseerCanModifyAsync(int academyId, string overseerId)
    {
        Overseer? overseer = await dbContext.Overseers
            .Where(o => o.AcademyId == academyId)
            .FirstOrDefaultAsync(o => o.Id.ToString() == overseerId);

        if (overseer == null)
        {
            return false;
        }

        return true;
    }
}
