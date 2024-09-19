using AuthenticationAndAuthorization.Domain.Context;
using AuthenticationAndAuthorization.Infrastructure.Helpers;
using AuthenticationAndAuthorization.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAndAuthorization.Infrastructure.Repositories.Implementations
{
    public sealed class IdentityRoleRepository
        (RoleManager<IdentityRole> roleManager)
        : IIdentityRoleRepository
    {
        #region Commands
        public Task<IdentityResult> CommandAsync(IdentityRole identityRole, CommandMode commandMode)
            => commandMode switch
            {
                CommandMode.Create => roleManager.CreateAsync(identityRole),
                CommandMode.Update => roleManager.UpdateAsync(identityRole),
                CommandMode.Delete => roleManager.DeleteAsync(identityRole),
                _ => throw new NotImplementedException(),
            };
        #endregion

        #region Queries
        public IQueryable<IdentityRole> Roles => roleManager.Roles;

        public Task<IdentityRole?> FindByIdAsync(string id)
            => roleManager.FindByIdAsync(id);
        #endregion
    }
}
