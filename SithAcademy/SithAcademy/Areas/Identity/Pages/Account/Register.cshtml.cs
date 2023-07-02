namespace SithAcademy.Web.Areas.Identity.Pages.Account;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

using SithAcademy.Data.Models;

using static SithAcademy.Common.EntityFieldValidation.User;
using static SithAcademy.Common.EntityFieldValidation.Password;

public class RegisterModel : PageModel
{
    private readonly SignInManager<AcademyUser> _signInManager;
    private readonly UserManager<AcademyUser> _userManager;
    private readonly IUserStore<AcademyUser> _userStore;

    public RegisterModel(
        UserManager<AcademyUser> userManager,
        IUserStore<AcademyUser> userStore,
        SignInManager<AcademyUser> signInManager)
    {
        _userManager = userManager;
        _userStore = userStore;
        _signInManager = signInManager;
    }

    [BindProperty]
    public InputModel Input { get; set; } = null!;

    public string? ReturnUrl { get; set; }

    public class InputModel
    {
        [Required]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength, ErrorMessage = UsernameLengthErrorMessage)]
        [Display(Name = "Username")]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = PasswordLengthErrorMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = PasswordNotMatchingErrorMessage)]
        public string ConfirmPassword { get; set; } = null!;
    }

#pragma warning disable CS1998
    public async Task OnGetAsync(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;
    }
#pragma warning restore CS1998

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, Input.Username, CancellationToken.None);
            await _userManager.SetEmailAsync(user, Input.Email);

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return Page();
    }

    private AcademyUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<AcademyUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(AcademyUser)}'. " +
                $"Ensure that '{nameof(AcademyUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        }
    }
}
