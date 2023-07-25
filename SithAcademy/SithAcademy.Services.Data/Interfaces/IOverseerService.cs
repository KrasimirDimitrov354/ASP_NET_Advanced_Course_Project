namespace SithAcademy.Services.Data.Interfaces;

public interface IOverseerService
{
    Task<bool> UserIsOverseerAsync(string userId);

    Task<string> GetOverseerIdAsync(string userId);

    Task<int> GetAcademyIdByOverseerIdAsync(string overseerId);

    Task<bool> OverseerCanModifyAsync(int academyId, string overseerId);
}
