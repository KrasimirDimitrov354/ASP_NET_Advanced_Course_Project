namespace SithAcademy.Services.Data.Interfaces;

public interface IHomeworkService
{
    Task<bool> TrialHasHomework(string trialId, string acolyteId);

    Task<string> GetHomeworkIdByAcolyteIdAndTrialId(string acolyteId, string trialId);
}
