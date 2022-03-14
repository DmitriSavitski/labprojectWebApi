using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Models;


namespace WebApi.Controllers
{
	[Route("authorization")]
	[ApiController]
	public class AuthorizationController : ControllerBase
	{
		private readonly ILogger<AuthorizationController> _logger;
		private readonly IUsersManager _userManager;

		public AuthorizationController(ILogger<AuthorizationController> logger, IUsersManager userManager)
		{
			_logger = logger;
			_userManager = userManager;
		}

		[HttpPost("signin")]
		[AllowAnonymous]
		public async Task<ActionResult> SignInAsync(string login, string password)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var user = _userManager.GetUserByLogin(login).GetAwaiter().GetResult();

			bool isSuccses = _userManager.GetUserByLogin(login).GetAwaiter().GetResult() == null ? false : true;

			switch (isSuccses)
			{
				case true:
					await Authenticate(user);
					return Ok();
				case false:
					return BadRequest();
			}
		}

		private async Task Authenticate(UsersDto user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.LoginDto),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleDto?.Name)
			};

			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
				ClaimsIdentity.DefaultRoleClaimType);

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}

		[HttpPost("logoff")]
		public async Task<ActionResult> LogOffAsync()
		{
			await HttpContext.SignOutAsync();
			return Ok();
		}
	}
}
