using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Pioneer.Blog.DAL.Entites;

namespace Pioneer.Blog.DAL
{
    public class IdentitySetup
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserEntity> _userManager;

        public IdentitySetup(RoleManager<IdentityRole> roleManager,
            UserManager<UserEntity> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task Setup()
        {
            // Create roles and role claims 
            var user = await _userManager.FindByNameAsync("dev@dev.com");

            // Add user to claim and role
            if (user != null) return;

            // Create roles and role claims 
            var adminRole = await _roleManager.FindByNameAsync("admin");
            if (adminRole == null)
            {
                adminRole = new IdentityRole("admin");
                adminRole.Claims.Add(new IdentityRoleClaim<string> { ClaimType = "isAdmin", ClaimValue = "true" });
                await _roleManager.CreateAsync(adminRole);
            }

            user = new UserEntity
            {
                UserName = "dev@dev.com"
            };

            var userResult = await _userManager.CreateAsync(user, "P@assw0rd");
            var roleResult = await _userManager.AddToRoleAsync(user, "admin");
            var claimResult = await _userManager.AddClaimAsync(user, new Claim("isSuperUser", "true"));

            if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
            {
                throw new InvalidOperationException("Failed to build user and roles");
            }
        }
    }
}
