namespace SithAcademy.Services.Data;

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SithAcademy.Data;
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
}
