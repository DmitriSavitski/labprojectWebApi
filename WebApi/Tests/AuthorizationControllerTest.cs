using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebApi.Controllers;
using Xunit;

namespace WebApi.Tests
{
	public class AuthorizationControllerTest
	{
		[Fact]
		public void SignInTest()
		{
			var usersMock = new Mock<IUsersManager>();
			IUsersManager usersManager = usersMock.Object;
			usersManager = Mock.Of<IUsersManager>();

			var loggerMock = new Mock<ILogger<AuthorizationController>>();
			ILogger<AuthorizationController> logger = loggerMock.Object;
			logger = Mock.Of<ILogger<AuthorizationController>>();

			var controller = new AuthorizationController(logger, usersManager);
			var result = controller.SignInAsync("savok", "savok");

			Assert.IsType<OkResult>(result.Result.GetType().ToString());
		}
	}
}
