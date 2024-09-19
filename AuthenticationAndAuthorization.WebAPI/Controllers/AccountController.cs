using AuthenticationAndAuthorization.Infrastructure.Services.Commands;
using AuthenticationAndAuthorization.WebAPI.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AccountController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        [Route(nameof(AccessDenied))]
        public IActionResult AccessDenied() => Unauthorized();

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(LoginAsync))]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO loginDTO)
        {
            //ClaimsIdentity? identity = null;

            var result = await mediator.Send(new IdentityUserCommandPasswordSignInAsync(loginDTO.UserName, loginDTO.Password, false));

            //IdentityUser? findUser = await mediator.Send(new IdentityUserQueryFindByNameAsync(loginDTO.UserName));

            if (result.Succeeded)
            {
                return Ok(loginDTO);
            }

            return RedirectToAction(nameof(AccessDenied));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(RegisterAsync))]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO registerDTO)
        {
            var user = new IdentityUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email
            };

            var result = await mediator.Send(new IdentityUserCommandPasswordCreateAsync(user, registerDTO.Password));

            if (result.Succeeded)
            {
                await mediator.Send(new IdentityUserCommandSignInAsync(user));
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route(nameof(Logout))]
        public async Task<IActionResult> Logout()
        {
            await mediator.Send(new IdentityUserCommandSignOutAsync());
            return NoContent();
        }
    }
}
