namespace SithAcademy.Web.ViewModels.Query;

using System.ComponentModel.DataAnnotations;

using static SithAcademy.Common.GeneralConstants;

public class GenericQueryModel
{
    public GenericQueryModel()
    {
        CurrentPage = SortingDefaultPage;
        RecordsPerPage = SortingDefaultEntitiesPerPage;
    }

    [Display(Name = "Enter a term to search for")]
    public string? SearchTerm { get; set; }

    public int CurrentPage { get; set; }

    [Display(Name = "Records Per Page")]
    public int RecordsPerPage { get; set; }

    public int TotalRecords { get; set; }
}
