using AuthenticationAndAuthorization.Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAndAuthorization.Infrastructure.Services.Queries
{
    #region Users
    public sealed record IdentityUserQueryUsers : IRequest<IQueryable<IdentityUser>>;

    public sealed class IdentityUserHandlerQueryUsers(IIdentityUserRepository identityUserRepository ) 
        : IRequestHandler<IdentityUserQueryUsers, IQueryable<IdentityUser>>
    {
        public Task<IQueryable<IdentityUser>> Handle(IdentityUserQueryUsers request, CancellationToken cancellationToken)
            => Task.Run(() => identityUserRepository.Users);
    }
    #endregion

    #region FindByIdAsync
    public sealed record IdentityUserQueryFindByIdAsync(string UserName) : IRequest<IdentityUser?>;

    public sealed class IdentityUserHandlerQueryFindByIdAsync(IIdentityUserRepository identityUserRepository)
        : IRequestHandler<IdentityUserQueryFindByIdAsync, IdentityUser?>
    {
        public Task<IdentityUser?> Handle(IdentityUserQueryFindByIdAsync request, CancellationToken cancellationToken)
            => identityUserRepository.FindByIdAsync(request.UserName);
    }
    #endregion

    #region FindByNameAsync
    public sealed record IdentityUserQueryFindByNameAsync(string UserName) : IRequest<IdentityUser?>;

    public sealed class IdentityUserHandlerQueryFindByNameAsync(IIdentityUserRepository identityUserRepository)
        : IRequestHandler<IdentityUserQueryFindByNameAsync, IdentityUser?>
    {
        public Task<IdentityUser?> Handle(IdentityUserQueryFindByNameAsync request, CancellationToken cancellationToken)
            => identityUserRepository.FindByNameAsync(request.UserName);
    }
    #endregion

    #region GetRolesAsync
    public sealed record IdentityUserQueryGetRolesAsync(IdentityUser User) : IRequest<IList<string>>;

    public sealed class IdentityUserHandlerQueryGetRolesAsync(IIdentityUserRepository identityUserRepository)
        : IRequestHandler<IdentityUserQueryGetRolesAsync, IList<string>>
    {
        public Task<IList<string>> Handle(IdentityUserQueryGetRolesAsync request, CancellationToken cancellationToken)
            => identityUserRepository.GetRolesAsync(request.User);
    }
    #endregion

    #region IsInRoleAsync
    public sealed record IdentityUserQueryIsInRoleAsync(IdentityUser User, string Role) : IRequest<bool>;

    public sealed class IdentityUserHandlerQueryIsInRoleAsync(IIdentityUserRepository identityUserRepository)
        : IRequestHandler<IdentityUserQueryIsInRoleAsync, bool>
    {
        public Task<bool> Handle(IdentityUserQueryIsInRoleAsync request, CancellationToken cancellationToken)
            => identityUserRepository.IsInRoleAsync(request.User, request.Role);
    }
    #endregion
}
