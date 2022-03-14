using BLL.Interfaces;
using BLL.Models;
using DAL.Context;
using DAL.Models;
using Moq;
using NUnit.Framework;
using WebApi.Controllers;

namespace WebApi.Tests
{
	[TestFixture]
	public class DepartmentControllerTest
	{
		DepartmentController controller;
		IEmployeesManages employeeManager;

		[SetUp]
		public void TestUp()
		{
			var departmentMock = new Mock<IDepartmentsManager>();
			IDepartmentsManager departmentManager = departmentMock.Object;
			departmentManager = Mock.Of<IDepartmentsManager>();

			var employeesMock = new Mock<IEmployeesManages>();
			employeeManager = employeesMock.Object;
			employeeManager = Mock.Of<IEmployeesManages>();

			var appContextMock = new Mock<ApplicationContext>();
			ApplicationContext context = appContextMock.Object;
			context = Mock.Of<ApplicationContext>();

			controller = new DepartmentController(departmentManager, employeeManager, context);
		}

		[Test]
		public void CreateTest()
		{
			var result = controller.CreateAsync(new DepartmentsDto
			{
				DepartmentIdDto = 100,
				NameDto = "Test Name"
			});

			Assert.AreEqual("OkResult", result.Result.GetType().ToString());
		}

		[Test]
		public void UpdateTest()
		{
			var result = controller.UpdateAsync(new Departments
			{
				DepartmentsId = 1002,
				Name = "Test Name"
			});

			Assert.AreEqual("OkResult", result.Result.GetType().ToString());
		}

		[Test]
		public void DeleteTest()
		{
			var result = controller.DeleteAsync(1002);

			Assert.AreEqual("OkResult", result.Result.GetType().ToString());
		}

		[Test]
		public void SetLeaderTest()
		{
			var result = controller.SetLeaderAsync(new Departments
			{
				DepartmentsId = 1005,
				Leader = new Leaders
				{
					LeadersId = 900,
				},
				LeadersId = 900
			});

			Assert.AreEqual("OkResult", result.Result.GetType().ToString());
		}
	}
}
