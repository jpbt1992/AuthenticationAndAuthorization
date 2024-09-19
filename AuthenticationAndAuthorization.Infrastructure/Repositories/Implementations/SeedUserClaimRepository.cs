using AuthenticationAndAuthorization.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAndAuthorization.Infrastructure.Repositories.Implementations
{
    public sealed class SeedUserClaimRepository(IIdentityUserRepository identityUserRepository) : ISeedUserClaimRepository
    {
        public async Task SeedUsersClaims()
        {
            #region User Admin
            string email = "admin@gmail.com";

            IdentityUser? userAdmin = await identityUserRepository.FindByEmailAsync(email);

            if (userAdmin == null)
            {
                userAdmin = new()
                {
                    UserName = "admin@gmail.com",
                    Email = email,
                };

                IdentityResult result = await identityUserRepository.CreateAsync(userAdmin, "Numsey#2023");

                if (result.Succeeded)
                {
                    var claimList = (await identityUserRepository.GetClaimsAsync(userAdmin))
                               .Select(p => p.Type);

                    if (!claimList.Contains("RegisteredIn"))
                    {
                        var claimResultRegisteredIn = await identityUserRepository.AddClaimAsync(userAdmin,
                            new Claim("RegisteredIn", DateTime.Today.ToString("dd/MM/yyyy")));
                    }

                    if (!claimList.Contains("IsAdmin"))
                    {
                        var claimResultIsAdmin = await identityUserRepository.AddClaimAsync(userAdmin,
                            new Claim("IsAdmin", "true"));

                        if (!claimResultIsAdmin.Succeeded)
                        {
                            StringBuilder stringBuilder = new();

                            foreach (IdentityError identityError in claimResultIsAdmin.Errors)
                            {
                                stringBuilder.AppendLine($"{identityError.Code} - {identityError.Description}");
                            }

                            throw new Exception(stringBuilder.ToString());
                        }
                    }
                }
            }
            #endregion
        }
    }
}
