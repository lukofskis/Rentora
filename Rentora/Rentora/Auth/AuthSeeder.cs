using Microsoft.AspNetCore.Identity;
using Rentora.Auth.Model;

namespace Rentora.Auth;

public class AuthSeeder
{
    private readonly UserManager<ForumUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthSeeder(UserManager<ForumUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    
    public async Task SeedAsync()
    {
        await AddDefaultRoles();
        await AddAdminUser();
    }

    private async Task AddAdminUser()
    {
        var newAdminUser = new ForumUser
        {
            UserName = "admin",
            Email = "admin@admin.com"
        };
        
        var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
        if (existingAdminUser == null)
        {
            var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "VerySafePassword1!");
            if (createAdminUserResult.Succeeded)
            {
                await _userManager.AddToRolesAsync(newAdminUser, ForumRoles.All);
            }
        }
        await AddTestUser("rentee", "rentee@test.com", "TestPassword1!", ForumRoles.Rentee);
        await AddTestUser("renter", "renter@test.com", "TestPassword1!", ForumRoles.Renter);
    }
    
    private async Task AddTestUser(string userName, string email, string password, string role)
    {
        var newUser = new ForumUser
        {
            UserName = userName,
            Email = email
        };

        var existingUser = await _userManager.FindByNameAsync(newUser.UserName);
        if (existingUser == null)
        {
            var createUserResult = await _userManager.CreateAsync(newUser, password);
            if (createUserResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, role);
            }
        }
    }

    private async Task AddDefaultRoles()
    {
        foreach (var role in ForumRoles.All)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
                await _roleManager.CreateAsync(new IdentityRole(role));
        }
    }
    
    
}