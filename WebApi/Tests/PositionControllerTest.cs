using BLL.Interfaces;
using BLL.Models;
using Moq;
using NUnit.Framework;
using WebApi.Controllers;

namespace WebApi.Tests
{
	public class PositionControllerTest
	{
		PositionController controller;
		[SetUp]
		public void TestUp()
		{
			var posMock = new Mock<IPositionManager>();
			IPositionManager positionManager = posMock.Object;
			positionManager = Mock.Of<IPositionManager>();

			controller = new PositionController(positionManager);
		}

		[Test]
		public void CreateTest()
		{
			var result = controller.CreateAsync(new PositionDto
			{
				PositionIdDto = 100,
				NameDto = "TEST"
			});

			Assert.AreEqual("OkResult", result.Result.GetType().ToString());
		}

		[Test]
		public void UpdateTest()
		{
			var result = controller.CreateAsync(new PositionDto
			{
				PositionIdDto = 100,
				NameDto = "TEST UPDATE"
			});

			Assert.AreEqual("OkResult", result.Result.GetType().ToString());
		}

		[Test]
		public void DeleteTest()
		{
			var result = controller.DeleteAsync(100);

			Assert.AreEqual("OkResult", result.Result.GetType().ToString());
		}

	}
}
