namespace SithAcademy.Services.Data.Interfaces;

using SithAcademy.Web.ViewModels.Homework;

public interface IOverseerService
{
    Task<bool> UserIsOverseerAsync(string userId);

    Task<string> GetOverseerIdAsync(string userId);

    Task<int> GetAcademyIdByOverseerIdAsync(string overseerId);

    Task<bool> OverseerCanModifyAsync(int academyId, string overseerId);

    Task GradeHomeworkAsync(string overseerId, GradeHomeworkViewModel viewModel);
}
