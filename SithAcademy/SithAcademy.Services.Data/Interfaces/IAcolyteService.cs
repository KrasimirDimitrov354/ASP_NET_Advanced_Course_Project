namespace SithAcademy.Services.Data.Interfaces;

public interface IAcolyteService
{
    Task<int?> GetAcolyteCurrentLocationAsync(string acolyteId);

    Task<bool> AcolyteIsInLocationAsync(int locationId, string acolyteId);

    Task RemoveAcolyteFromLocationAsync(int locationId, string acolyteId);
}
