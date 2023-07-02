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
        public const int PasswordMaxLength = 40;
        public const string PasswordLengthErrorMessage = "Password should be between 3 and 40 symbols long.";
        public const string PasswordNotMatchingErrorMessage = "The password and confirmation password do not match.";
    }
}
