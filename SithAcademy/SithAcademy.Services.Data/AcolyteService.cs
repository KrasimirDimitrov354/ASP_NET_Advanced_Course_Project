namespace SithAcademy.Services.Data;

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SithAcademy.Data;
using SithAcademy.Data.Models;
using SithAcademy.Services.Data.Interfaces;

public class AcolyteService : IAcolyteService
{
    private readonly AcademyDbContext dbContext;

    public AcolyteService(AcademyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int?> GetAcolyteCurrentLocationAsync(string acolyteId)
    {
        AcademyUser acolyte = await dbContext.Users
            .FirstAsync(u => u.Id.ToString() == acolyteId);

        return acolyte.LocationId;
    }

    public async Task<bool> AcolyteIsInLocationAsync(int locationId, string acolyteId)
    {
        AcademyUser acolyte = await dbContext.Users
            .FirstAsync(u => u.Id.ToString() == acolyteId);

        if (acolyte.LocationId == null)
        {
            acolyte.LocationId = locationId;
            await dbContext.SaveChangesAsync();
        }

        return acolyte.LocationId == locationId;
    }

    public async Task RemoveAcolyteFromLocationAsync(int locationId, string acolyteId)
    {
        Location location = await dbContext.Locations
            .FirstAsync(l => l.Id == locationId);

        AcademyUser acolyte = await dbContext.Users
            .Include(u => u.JoinedAcademies)
            .FirstAsync(u => u.Id.ToString() == acolyteId);

        if (!acolyte.JoinedAcademies.Any())
        {
            location.Acolytes.Remove(acolyte);
            await dbContext.SaveChangesAsync();
        }
    }
}
