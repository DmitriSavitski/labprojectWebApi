using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[Route("user")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUsersManager _userManager;
		private readonly IRoleManager _roleManager;

		public UserController(IUsersManager userManager, IRoleManager roleManager)
		{
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			_roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
		}

		[HttpGet("create")]
		public ActionResult Create()
		{
			var roles = _roleManager.GetAllRoles();

			return Ok(roles);
		}

		[HttpPost("create")]
		public ActionResult Create(UsersDto usersDto)
		{
			_userManager.CreateUserAsync(usersDto).GetAwaiter().GetResult();

			return Ok();
		}

		[HttpGet("index")]
		public ActionResult Index()
		{
			var users = _userManager.GetAllUsers();

			return Ok(users);
		}

		[HttpPost("delete")]
		public async Task<ActionResult> DeleteAsync(string login)
		{
			await _userManager.DeleteUserAsync(login);

			return Ok();
		}

		[HttpGet("update")]
		public ActionResult Update(string login)
		{
			var roles = _roleManager.GetAllRoles();
			var user = _userManager.GetUserByLogin(login).GetAwaiter().GetResult();

			var mod = new UserModel
			{
				roles = (List<RoleDto>)roles,
				user = user
			};

			return Ok(mod);
		}

		[HttpPost("update")]
		public async Task<IActionResult> UpdateAsync(UsersDto usersDto)
		{
			await _userManager.UpdateUserAsync(usersDto);

			return Ok();
		}
	}

	public class UserModel
	{
		public List<RoleDto> roles { get; set; }

		public UsersDto user { get; set; }
	}
}
