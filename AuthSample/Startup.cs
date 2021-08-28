using AuthSample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuthSample.Startup))]
namespace AuthSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        public void createRolesandUsers()
        {
            var context = new ApplicationDbContext();

            var roleManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManger.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManger.Create(role);

                var user = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@mvsolutions.com",
                    BirthDate = System.DateTime.Now
                };

                var password = "password";

                var usr = userManger.Create(user, password);
                if (usr.Succeeded)
                {
                    var result = userManger.AddToRole(user.Id, "Admin");
                }

            }

            if (!roleManger.RoleExists("Teacher"))
            {
                var role = new IdentityRole();
                role.Name = "Teacher";
                roleManger.Create(role);

            }
            
            if (!roleManger.RoleExists("Supervisor"))
            {
                var role = new IdentityRole();
                role.Name = "Supervisor";
                roleManger.Create(role);

            }

        }


    }
}
