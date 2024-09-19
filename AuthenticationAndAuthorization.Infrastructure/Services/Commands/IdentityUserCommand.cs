using AuthenticationAndAuthorization.Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAndAuthorization.Infrastructure.Services.Commands
{
    #region CreateAsync
    public sealed record IdentityUserCommandPasswordCreateAsync(IdentityUser User, string Password) : IRequest<IdentityResult>;

    public sealed class IdentityUserHandlerCommandPasswordCreateAsync(IIdentityUserRepository identityUserRepository) :
        IRequestHandler<IdentityUserCommandPasswordCreateAsync, IdentityResult>
    {
        public  Task<IdentityResult> Handle(IdentityUserCommandPasswordCreateAsync request, CancellationToken cancellationToken)
            => identityUserRepository.CreateAsync(request.User, request.Password);
    }
    #endregion

    #region PasswordSignInAsync
    public sealed record IdentityUserCommandPasswordSignInAsync(string UserName, string Password, bool IsPersistent, bool LockoutOnFailure = false)
        : IRequest<SignInResult>;

    public sealed class IdentityUserHandlerCommandPasswordSignInAsync(IIdentityUserRepository identityUserRepository) :
        IRequestHandler<IdentityUserCommandPasswordSignInAsync, SignInResult>
    {
        public Task<SignInResult> Handle(IdentityUserCommandPasswordSignInAsync request, CancellationToken cancellationToken)
            => identityUserRepository.PasswordSignInAsync(request.UserName, request.Password, request.IsPersistent, request.LockoutOnFailure);
    }
    #endregion

    #region SignInAsync
    public sealed record IdentityUserCommandSignInAsync(IdentityUser IdentityUser) : IRequest;

    public sealed class IdentityUserHandlerCommandSignInAsync(IIdentityUserRepository identityUserRepository) : IRequestHandler<IdentityUserCommandSignInAsync>
    {
        public Task Handle(IdentityUserCommandSignInAsync request, CancellationToken cancellationToken)
            => identityUserRepository.SignInAsync(request.IdentityUser);
    }
    #endregion

    #region SignOutAsync
    public sealed record IdentityUserCommandSignOutAsync() : IRequest;

    public sealed class IdentityUserHandlerCommandSignOutAsync(IIdentityUserRepository identityUserRepository)
        : IRequestHandler<IdentityUserCommandSignOutAsync>
    {
        public Task Handle(IdentityUserCommandSignOutAsync request, CancellationToken cancellationToken)
            => identityUserRepository.SignOutAsync();
    }
    #endregion

    #region AddToRoleAsync
    public sealed record IdentityUserCommandAddToRoleAsync(IdentityUser User, string Role) : IRequest<IdentityResult>;

    public sealed class IdentityUserHandlerCommandAddToRoleAsync(IIdentityUserRepository identityUserRepository)
        : IRequestHandler<IdentityUserCommandAddToRoleAsync, IdentityResult>
    {
        public Task<IdentityResult> Handle(IdentityUserCommandAddToRoleAsync request, CancellationToken cancellationToken)
            => identityUserRepository.AddToRoleAsync(request.User, request.Role);
    }
    #endregion

    #region RemoveFromRoleAsync
    public sealed record IdentityUserCommandRemoveFromRoleAsync(IdentityUser User, string Role) : IRequest<IdentityResult>;

    public sealed class IdentityUserHandlerCommandRemoveFromRoleAsync(IIdentityUserRepository identityUserRepository)
        : IRequestHandler<IdentityUserCommandRemoveFromRoleAsync, IdentityResult>
    {
        public Task<IdentityResult> Handle(IdentityUserCommandRemoveFromRoleAsync request, CancellationToken cancellationToken)
            => identityUserRepository.RemoveFromRoleAsync(request.User, request.Role);
    }
    #endregion
}
