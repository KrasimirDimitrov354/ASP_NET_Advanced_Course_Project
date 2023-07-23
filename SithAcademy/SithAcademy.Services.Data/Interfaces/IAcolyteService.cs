namespace SithAcademy.Services.Data.Interfaces;

public interface IAcolyteService
{
    Task<bool> AcolyteIsInLocationAsync(int locationId, string acolyteId);

    Task RemoveAcolyteFromLocationAsync(int locationId, string acolyteId);
}
