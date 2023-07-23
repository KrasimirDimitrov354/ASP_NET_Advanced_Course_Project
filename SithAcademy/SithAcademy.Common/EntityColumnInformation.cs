namespace SithAcademy.Common;

public static class EntityColumnInformation
{
    public static class Academy
    {
        public const string IdComment = "ID of the academy";
        public const string TitleComment = "Title of the academy";
        public const string DescriptionComment = "Brief description of the academy";
        public const string ImageUrlComment = "URL of the image that will be used to visualize the academy";
        public const string IsLockedComment = "Boolean showing whether or not the academy is accessible for new acolytes";
        public const string LocationIdComment = "ID of the academy's location";
    }

    public static class Overseer
    {
        public const string IdComment = "ID of the overseer";
        public const string TitleComment = "Title of the overseer";
        public const string UserIdComment = "ID of the existing user that is also an overseer";
        public const string AcademyIdComment = "ID of the academy in which the overseer is assigned to";
    }

    public static class Acolyte
    {
        public const string LocationIdComment = "ID of the location on which the acolyte is currently located";
    }

    public static class Location
    {
        public const string IdComment = "ID of the location";
        public const string NameComment = "Name of the location";
        public const string DescriptionComment = "Brief description of the location";
        public const string ImageUrlComment = "URL of the image that will be used to visualize the location";
        public const string IsLockedComment = "Boolean showing whether or not the location is accessible for new acolytes";
    }

    public static class Trial
    {
        public const string IdComment = "ID of the trial";
        public const string TitleComment = "Title of the trial";
        public const string DescriptionComment = "Description of the trial";
        public const string IsLockedComment = "Boolean showing whether or not the trial can be participated in";
        public const string AcademyIdComment = "ID of the academy which hosts the trial";
    }

    public static class Resource
    {
        public const string IdComment = "ID of the resource";
        public const string NameComment = "Name of the resource";
        public const string UrlComment = "URL for the resource's location";
        public const string IsDeletedComment = "Boolean showing whether or not the resource should be displayed";
    }

    public static class Homework
    {
        public const string IdComment = "ID of the homework";
        public const string ContentComment = "Content of the homework";
        public const string ScoreComment = "Score of the homework";
        public const string CreatedOnComment = "Date when the homework was created";
        public const string ReviewerNameComment = "Name of the user that has reviewed the homework - overseer or admin";
        public const string ReviewerFeedbackComment = "Feedback left by the user that has reviewed the homework - overseer or admin";
        public const string TrialIdComment = "ID of the trial for which the homework is";
        public const string AcolyteIdComment = "ID of the acolyte to which the homework belongs";
    }

    public static class AcademyAcolyte
    {
        public const string AcademyIdComment = "ID of the academy in which the acolyte is assigned to";
        public const string AcolyteIdComment = "ID of the acolyte";
    }

    public static class TrialAcolyte
    {
        public const string TrialIdComment = "ID of the trial which the acolyte must complete";
        public const string AcolyteIdComment = "ID of the acolyte";
        public const string IsCompletedComment = "Boolean showing whether or not the acolyte has an approved homework for the trial";
    }
}
