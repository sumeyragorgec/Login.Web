using Login.Web.CustomValidations;
using Login.Web.NewFolder2;

namespace Login.Web.Models.NewFolder
{
    public static class ProgramcsExtensions
    {
        public static void AddIdentityExtensions(this IServiceCollection services)
        {




            services.AddIdentity<AppUser, AppRole>(

    options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;

    }

    ).AddPasswordValidator<PasswordValidator>().AddUserValidator<UserValidator>().AddErrorDescriber<IdentityErrorDescriberCustom>().AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
