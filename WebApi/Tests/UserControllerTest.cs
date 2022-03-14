using BLL.Interfaces;
using BLL.Models;
using Moq;
using NUnit.Framework;
using WebApi.Controllers;

namespace WebApi.Tests
{
	public class UserControllerTest
	{
		UserController controller;
		[SetUp]
		public void TestUp()
		{
			var usersMock = new Mock<IUsersManager>();
			IUsersManager usersManager = usersMock.Object;
			usersManager = Mock.Of<IUsersManager>();

			var roleMock = new Mock<IRoleManager>();
			IRoleManager roleManager = roleMock.Object;
			roleManager = Mock.Of<IRoleManager>();

			controller = new UserController(usersManager, roleManager);
		}

		[Test]
		public void CreateTest()
		{
			var result = controller.Create(new UsersDto
			{
				UserIdDto = 901,
				LoginDto = "TEST",
				PasswordDto = "test"
			});

			Assert.AreEqual("OkResult", result.GetType().ToString());
		}

		[Test]
		public void UpdateTest()
		{
			var result = controller.UpdateAsync(new UsersDto
			{
				UserIdDto = 901,
				LoginDto = "TEST",
				PasswordDto = "test1"
			});

			Assert.AreEqual("OkResult", result.GetType().ToString());
		}

		[Test]
		public void DelelteTest()
		{
			var result = controller.DeleteAsync("TEST");

			Assert.AreEqual("OkResult", result.Result.GetType().ToString());
		}
	}
}
