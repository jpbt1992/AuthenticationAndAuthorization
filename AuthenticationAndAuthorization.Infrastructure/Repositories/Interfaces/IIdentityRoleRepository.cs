using AuthenticationAndAuthorization.Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAndAuthorization.Infrastructure.Repositories.Interfaces
{
    public interface IIdentityRoleRepository
    {
        #region Commands
        Task<IdentityResult> CommandAsync(IdentityRole identityRole, CommandMode commandMode);
        #endregion

        #region Queries
        public IQueryable<IdentityRole> Roles { get; }

        Task<IdentityRole?> FindByIdAsync(string id);
        #endregion
    }
}
