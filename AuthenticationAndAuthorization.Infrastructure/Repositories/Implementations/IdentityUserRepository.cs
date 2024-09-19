using AuthenticationAndAuthorization.Domain.Context;
using AuthenticationAndAuthorization.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;

namespace AuthenticationAndAuthorization.Infrastructure.Repositories.Implementations
{
    public sealed class IdentityUserRepository
        (UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
        : IIdentityUserRepository
    {
        #region Commands
        public Task<IdentityResult> CreateAsync(IdentityUser user, string password)
            => userManager.CreateAsync(user, password);

        public Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
            => signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);

        public Task SignOutAsync()
            => signInManager.SignOutAsync();

        public Task SignInAsync(IdentityUser user, bool isPersistent = false)
            => signInManager.SignInAsync(user, isPersistent);

        public Task<IdentityResult> AddToRoleAsync(IdentityUser user, string role)
            => userManager.AddToRoleAsync(user, role);

        public Task<IdentityResult> RemoveFromRoleAsync(IdentityUser user, string role)
            => userManager.RemoveFromRoleAsync(user, role);

        public Task<IdentityResult> AddClaimAsync(IdentityUser user, Claim claim)
            => userManager.AddClaimAsync(user, claim);
        #endregion

        #region Queries
        public IQueryable<IdentityUser> Users => userManager.Users;

        public Task<IdentityUser?> FindByIdAsync(string userId)
            => userManager.FindByIdAsync(userId);

        public Task<IdentityUser?> FindByNameAsync(string userName)
            => userManager.FindByNameAsync(userName);

        public Task<IdentityUser?> FindByEmailAsync(string email)
            => userManager.FindByEmailAsync(email);

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
            => userManager.GetRolesAsync(user);

        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
            => userManager.GetClaimsAsync(user);

        public Task<bool> IsInRoleAsync(IdentityUser user, string role)
            => userManager.IsInRoleAsync(user, role);
        #endregion
    }
}
