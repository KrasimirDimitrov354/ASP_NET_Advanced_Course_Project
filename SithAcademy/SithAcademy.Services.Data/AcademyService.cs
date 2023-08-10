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
            .Include(a => a.Trials)
            .Include(a => a.Acolytes)
            .Where(a => a.Id == academyId)
            .Select(a => new AcademyDetailsViewModel()
            {
                Id = a.Id,
                Title = a.Title,
                ImageUrl = a.ImageUrl,
                Description = a.Description,
                IsLocked = a.IsLocked,
                Trials = a.Trials
                        .Select(t => new TrialDropdownViewModel()
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

    public async Task<int> GetLocationIdByAcademyIdAsync(int academyId)
    {
        Academy academy = await dbContext.Academies
            .FirstAsync(a => a.Id == academyId);

        return academy.LocationId;
    }

    public async Task<int> AddAcademyAndReturnIdAsync(AddAcademyViewModel viewModel)
    {
        Academy academy = new Academy()
        {
            Title = viewModel.Title,
            Description = viewModel.Description,
            ImageUrl = viewModel.ImageUrl,
            IsLocked = viewModel.IsLocked,
            LocationId = viewModel.LocationId,
        };

        await dbContext.Academies.AddAsync(academy);
        await dbContext.SaveChangesAsync();

        return academy.Id;
    }

    public async Task<AcademyFormViewModel> GetAcademyForModificationAsync(int academyId)
    {
        AcademyFormViewModel academyToModify = await dbContext.Academies
            .Where(a => a.Id == academyId)
            .Select(a => new AcademyFormViewModel()
            {
                Title = a.Title,
                ImageUrl = a.ImageUrl,
                Description = a.Description,
                IsLocked = a.IsLocked
            })
            .FirstAsync();

        return academyToModify;
    }

    public async Task EditAcademyAsync(int academyId, AcademyFormViewModel viewModel)
    {
        Academy academy = await dbContext.Academies
            .FirstAsync(a => a.Id == academyId);

        academy.Title = viewModel.Title;
        academy.ImageUrl = viewModel.ImageUrl;
        academy.Description = viewModel.Description;

        await dbContext.SaveChangesAsync();
    }

    public async Task ChangeAcademyLockStatusAsync(int academyId)
    {
        Academy academy = await dbContext.Academies
            .FirstAsync(a => a.Id == academyId);

        switch (academy.IsLocked)
        {
            case true:
                academy.IsLocked = false;
                break;
            case false:
                academy.IsLocked= true;
                break;
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> AcademyExistsAsync(int academyId)
    {
        return await dbContext.Academies.AnyAsync(a => a.Id == academyId);
    }

    public async Task<bool> AcademyIsLockedAsync(int academyId)
    {
        Academy academy = await dbContext.Academies.FirstAsync(a => a.Id == academyId);

        return academy.IsLocked;
    }

    public async Task<bool> AcolyteExistsInAcademyAsync(int academyId, string acolyteId)
    {
        return await dbContext.AcademiesAcolytes
            .AnyAsync(academy => academy.AcademyId == academyId && 
                      academy.AcolyteId.ToString() == acolyteId);
    }

    public async Task AddAcolyteToAcademyAsync(int academyId, string acolyteId)
    {
        Academy academy = await dbContext.Academies
            .FirstAsync(a => a.Id == academyId);

        academy.Acolytes.Add(new AcademyAcolyte()
        {
            AcademyId = academyId,
            AcolyteId = Guid.Parse(acolyteId)
        });

        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveAcolyteFromAcademyAsync(int academyId, string acolyteId)
    {
        Academy academy = await dbContext.Academies
            .Include(a => a.Acolytes)
            .FirstAsync(a => a.Id == academyId);

        AcademyAcolyte acolyteToRemove = academy.Acolytes.First(a => a.AcolyteId.ToString() == acolyteId);
        academy.Acolytes.Remove(acolyteToRemove);
        
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<string>> GetAllAcademyNamesForQuerySelectAsync()
    {
        IEnumerable<string> allAcademies = await dbContext.Academies
            .Select(a => a.Title)
            .ToArrayAsync();

        return allAcademies;
    }

    public async Task<IEnumerable<AcademyDropdownViewModel>> GetAllAcademiesForDropdownSelectAsync()
    {
        IEnumerable<AcademyDropdownViewModel> allAcademies = await dbContext.Academies
            .Select(a => new AcademyDropdownViewModel()
            {
                Id = a.Id,
                Title = a.Title
            })
            .ToArrayAsync();

        return allAcademies;
    }
}
