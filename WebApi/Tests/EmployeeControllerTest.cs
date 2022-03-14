using BLL.Interfaces;
using BLL.Models;
using Moq;
using NUnit.Framework;
using WebApi.Controllers;

namespace WebApi.Tests
{
	public class EmployeeControllerTest
	{
		EmployeeController controller;

		[SetUp]
		public void TestUp()
		{
			var departmentMock = new Mock<IDepartmentsManager>();
			IDepartmentsManager departmentManager = departmentMock.Object;
			departmentManager = Mock.Of<IDepartmentsManager>();

			var nationalityMock = new Mock<INationalityManager>();
			INationalityManager nationalityManager = nationalityMock.Object;
			nationalityManager = Mock.Of<INationalityManager>();

			var posMock = new Mock<IPositionManager>();
			IPositionManager positionManager = posMock.Object;
			positionManager = Mock.Of<IPositionManager>();

			var fioMock = new Mock<IFIOManager>();
			IFIOManager fioManager = fioMock.Object;
			fioManager = Mock.Of<IFIOManager>();

			var employeeMock = new Mock<IEmployeesManages>();
			IEmployeesManages employeesManages = employeeMock.Object;
			employeesManages = Mock.Of<IEmployeesManages>();

			controller = new EmployeeController(employeesManages, nationalityManager, positionManager, fioManager, departmentManager);
		}

		[Test]
		public void CreateTest()
		{
			var result = controller.Create(new EmployeesDto
			{
				EmployeeIdDto = 100,
				EmailDto = "asdasdas@gmail.com"
			});

			Assert.AreEqual("OkResult", result.ToString());
		}
		[Test]
		public void UpdateTest()
		{
			var result = controller.UpdateAsync(new EmployeesDto
			{
				EmployeeIdDto = 100,
				EmailDto = "12312s@gmail.com"
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
