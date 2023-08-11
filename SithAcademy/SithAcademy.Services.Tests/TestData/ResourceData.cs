namespace SithAcademy.Services.Tests.TestData;

using SithAcademy.Web.ViewModels.Resource;

public static class ResourceData
{
    public static string ExistingResourceId = "b9da4d71-52bc-451e-951f-c46e04e8293c";
    public static string ExistingResourceTrialId = "aa37b907-5d8b-439c-a719-2a784c07744a";
    public static string NonExistingResourceId = Guid.NewGuid().ToString();

    public static ResourceFormViewModel GenerateResource()
    {
        ResourceFormViewModel GeneratedResource = new ResourceFormViewModel()
        {
            Name = "History of the Valley of the Dark Lords",
            ImageUrl = "https://ddx5i92cqts4o.cloudfront.net/images/1ejq0l57t_Fearful_Landscape_CotG.png",
            SourceUrl = "https://starwars.fandom.com/wiki/Valley_of_the_Dark_Lords/Legends",
            TrialId = ExistingResourceTrialId
        };

        return GeneratedResource;
    }
}
