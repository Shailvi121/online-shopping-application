//namespace Online_Shopping_Application.API.SeedDatabase
//{
//    public static class SeedDatabase
//    {
//        public static async Task AddAdminUser(IServiceProvider serviceProvider)
//        {
//            using (var scope = serviceProvider.CreateScope())
//            {
//                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

//                string[] roleNames = { "Admin", "User" };
//                foreach (var roleName in roleNames)
//                {
//                    var roleExist = await roleManager.RoleExistsAsync(roleName);
//                    if (!roleExist)
//                    {
//                        await roleManager.CreateAsync(new IdentityRole(roleName));
//                    }
//                }

//                var adminUser = await userManager.FindByNameAsync("shailvi");
//                if (adminUser == null)
//                {
//                    adminUser = new IdentityUser
//                    {
//                        UserName = "shailvi",
//                        Email = "shailvi@example.com",
//                        EmailConfirmed = true
//                    };

//                    var password = "shailvi@123";
//                    await userManager.CreateAsync(adminUser, password);

//                    await userManager.AddToRoleAsync(adminUser, "Admin"); 
//                }
//            }
//        }
//    }
//}
