using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AuthenticationAndAuthorization.Infrastructure.Repositories.Interfaces
{
    public interface IIdentityUserRepository
    {
        #region Commands
        Task<IdentityResult> CreateAsync(IdentityUser user, string password);

        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);

        Task SignInAsync(IdentityUser user, bool isPersistent = false);

        Task SignOutAsync();

        Task<IdentityResult> AddToRoleAsync(IdentityUser user, string role);

        Task<IdentityResult> RemoveFromRoleAsync(IdentityUser user, string role);

        Task<IdentityResult> AddClaimAsync(IdentityUser user, Claim claim);
        #endregion

        #region Queries

        IQueryable<IdentityUser> Users { get; }

        Task<IdentityUser?> FindByIdAsync(string userId);

        Task<IdentityUser?> FindByNameAsync(string userName);

        Task<IdentityUser?> FindByEmailAsync(string email);

        Task<IList<string>> GetRolesAsync(IdentityUser user);

        Task<IList<Claim>> GetClaimsAsync(IdentityUser user);

        Task<bool> IsInRoleAsync(IdentityUser user, string role);
        #endregion
    }
}
