namespace SithAcademy.Services.Data.Models.Homework;

using SithAcademy.Web.ViewModels.Homework;

public class AllHomeworksFilteredAndPagedServiceModel
{
    public AllHomeworksFilteredAndPagedServiceModel()
    {
        Homeworks = new HashSet<HomeworkSortingViewModel>();
    }

    public int HomeworksCount { get; set; }

    public IEnumerable<HomeworkSortingViewModel> Homeworks { get; set; }
}
