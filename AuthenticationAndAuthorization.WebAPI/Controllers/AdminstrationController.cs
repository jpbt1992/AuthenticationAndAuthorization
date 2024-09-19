using AuthenticationAndAuthorization.Infrastructure.Helpers;
using AuthenticationAndAuthorization.Infrastructure.Services.Commands;
using AuthenticationAndAuthorization.WebAPI.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AdminstrationController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route(nameof(CreateRole))]
        public async Task<IActionResult> CreateRole(CreateRoleDTO createRoleDTO)
        {
            IdentityRole identityRole = new()
            {
                Name = createRoleDTO.RoleName
            };

            IdentityResult result = await mediator.Send(new IdentityRoleCommandAsync(identityRole, CommandMode.Create));

            if (result.Succeeded)
            {
                return Ok(createRoleDTO);
            }

            return BadRequest(result.Errors);
        }
    }
}
