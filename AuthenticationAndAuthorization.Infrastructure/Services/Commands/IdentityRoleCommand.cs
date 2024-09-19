using AuthenticationAndAuthorization.Infrastructure.Helpers;
using AuthenticationAndAuthorization.Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAndAuthorization.Infrastructure.Services.Commands
{
    #region CommandAsync
    public sealed record IdentityRoleCommandAsync(IdentityRole IdentityRole, CommandMode CommandMode) : IRequest<IdentityResult>;

    public sealed class IdentityRoleHandlerCommandAsync(IIdentityRoleRepository identityRoleRepository)
        : IRequestHandler<IdentityRoleCommandAsync, IdentityResult>
    {
        public Task<IdentityResult> Handle(IdentityRoleCommandAsync request, CancellationToken cancellationToken)
            => identityRoleRepository.CommandAsync(request.IdentityRole, request.CommandMode);
    }
    #endregion
}
