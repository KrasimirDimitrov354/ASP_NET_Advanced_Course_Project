namespace SithAcademy.Web;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using SithAcademy.Data;
using SithAcademy.Data.Models;
using SithAcademy.Services.Data.Interfaces;
using SithAcademy.Web.Infrastructure.Extensions;
using SithAcademy.Web.Infrastructure.ModelBinders;
using SithAcademy.Web.Areas.Admin.Services.Interfaces;

using static SithAcademy.Common.GeneralConstants;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        string connectionString = builder.Configuration
            .GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services
            .AddDbContext<AcademyDbContext>(options => options.UseSqlServer(connectionString));

        builder.Services
            .AddDefaultIdentity<AcademyUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AcademyDbContext>();

        builder.Services.AddApplicationServices(typeof(ITrialService));
        builder.Services.AddApplicationServices(typeof(IUserService));

        builder.Services
            .AddControllersWithViews()
            .AddMvcOptions(options =>
            {
                options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error/500");
            app.UseStatusCodePagesWithRedirects("/Home/Error/?statusCode={0}");

            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.SeedAdministrator(AdminEmail);
        }

        app.UseEndpoints(config =>
        {
            config.MapControllerRoute
            (
                name: "areas",
                pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );
            config.MapDefaultControllerRoute();
            config.MapRazorPages();
        });

        app.Run();
    }
}