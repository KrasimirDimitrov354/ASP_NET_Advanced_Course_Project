namespace SithAcademy.Common;

public static class EntityFieldValidation
{
    public static class User
    {
        public const int UsernameMinLength = 3;
        public const int UsernameMaxLength = 50;
        public const string UsernameLengthErrorMessage = "Username should be between 3 and 50 symbols long.";
    }

    public static class Password
    {
        public const int PasswordMinLength = 3;
        public const int PasswordMaxLength = 50;
        public const string PasswordLengthErrorMessage = "Password should be between 3 and 40 symbols long.";
        public const string PasswordNotMatchingErrorMessage = "The password and confirmation password do not match.";
    }

    public static class Academy
    {
        public const int TitleMinLength = 6;
        public const int TitleMaxLength = 60;

        public const int DescriptionMinLength = 25;
        public const int DescriptionMaxLength = 1000;
    }

    public static class Overseer
    {
        public const int HoloFrequencyMinLength = 7;
        public const int HoloFrequencyMaxLength = 15;
    }

    public static class Location
    {
        public const int NameMinLength = 6;
        public const int NameMaxLength = 60;

        public const int DescriptionMinLength = 25;
        public const int DescriptionMaxLength = 1000;
    }

    public static class Trial
    {
        public const int TitleMinLength = 5;
        public const int TitleMaxLength = 25;

        public const int DescriptionMinLength = 25;
        public const int DescriptionMaxLength = 1000;
    }

    public static class Resource
    {
        public const int NameMinLength = 6;
        public const int NameMaxLength = 60;

        public const int LinkMaxLength = 2048;
    }
}
