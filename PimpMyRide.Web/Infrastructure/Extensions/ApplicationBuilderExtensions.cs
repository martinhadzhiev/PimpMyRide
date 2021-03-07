namespace PimpMyRide.Web.Infrastructure.Extensions
{
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using ImageSharp;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static void UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<PimpMyRideDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
                        var adminName = WebConstants.AdministratorRole;

                        var roles = new[]
                        {
                            WebConstants.AdministratorRole,
                            WebConstants.ModeratorRole
                        };

                        foreach (var role in roles)
                        {
                            var roleExists = await roleManager.RoleExistsAsync(role);

                            if (!roleExists)
                            {
                                await roleManager.CreateAsync(new IdentityRole
                                {
                                    Name = role
                                });
                            }
                        }

                        var adminEmail = "admin@admin.com";

                        var adminUser = await userManager.FindByEmailAsync(adminEmail);

                        if (adminUser == null)
                        {
                            adminUser = new User
                            {
                                Email = adminEmail,
                                UserName = adminName,
                                FirstName = adminName,
                                LastName = adminName
                            };

                            await userManager.CreateAsync(adminUser, "admin123");

                            await userManager.AddToRoleAsync(adminUser, WebConstants.AdministratorRole);
                        }
                    })
                    .Wait();
            }
        }
    }
}
