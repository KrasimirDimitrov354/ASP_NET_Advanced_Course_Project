namespace SithAcademy.Services.Data.Interfaces;

public interface IOverseerService
{
    Task<bool> UserIsOverseerAsync(string userId);
}
