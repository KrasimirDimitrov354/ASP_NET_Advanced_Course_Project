namespace SithAcademy.Common;

public static class EntityColumnInformation
{
    public static class Academy
    {
        public const string IdComment = "ID of the academy";
        public const string TitleComment = "Title of the academy";
        public const string DescriptionComment = "Brief description of the academy";
        public const string LocationIdComment = "ID of the academy's location";
    }

    public static class Overseer
    {
        public const string IdComment = "ID of the overseer";
        public const string HoloFrequencyComment = "Phone number of the overseer";
        public const string UserIdComment = "ID of the existing user that is also an overseer";
        public const string AcademyIdComment = "ID of the academy in which the overseer is assigned to";
    }

    public static class Location
    {
        public const string IdComment = "ID of the location";
        public const string NameComment = "Name of the location";
        public const string DescriptionComment = "Brief description of the location";
    }

    public static class Trial
    {
        public const string IdComment = "ID of the trial";
        public const string TitleComment = "Title of the trial";
        public const string DescriptionComment = "Description of the trial";
        public const string AcademyIdComment = "ID of the academy which hosts the trial";
    }

    public static class Resource
    {
        public const string IdComment = "ID of the resource";
        public const string NameComment = "Name of the resource";
        public const string LinkComment = "Link leading to the resource's location on the Internet";
    }
}
