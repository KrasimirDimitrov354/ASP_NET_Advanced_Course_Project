namespace SithAcademy.Web.ViewModels.Query;

using System.ComponentModel.DataAnnotations;

using SithAcademy.Web.ViewModels.Homework;
using SithAcademy.Web.ViewModels.Query.Enums;

public class AllHomeworksQueryModel : GenericQueryModel
{
    public AllHomeworksQueryModel()
    {
        Trials = new HashSet<string>();
        Homeworks = new HashSet<HomeworkSortingViewModel>();
    }

    public string? Trial { get; set; }

    [Display(Name = "Sort homeworks")]
    public HomeworkSorting HomeworkSorting { get; set; }

    public IEnumerable<string> Trials { get; set; }

    public IEnumerable<HomeworkSortingViewModel> Homeworks { get; set; }
}
