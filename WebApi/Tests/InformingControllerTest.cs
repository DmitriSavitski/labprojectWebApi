using BLL.Interfaces;
using Moq;
using NUnit.Framework;
using WebApi.Controllers;

namespace WebApi.Tests
{
	public class InformingControllerTest
	{
		InformingController controller;

		[SetUp]
		public void TestUp()
		{
			var departmentMock = new Mock<IDepartmentsManager>();
			IDepartmentsManager departmentManager = departmentMock.Object;
			departmentManager = Mock.Of<IDepartmentsManager>();

			var employeeMock = new Mock<IEmployeesManages>();
			IEmployeesManages employeesManages = employeeMock.Object;
			employeesManages = Mock.Of<IEmployeesManages>();

			var usersMock = new Mock<IUsersManager>();
			IUsersManager usersManager = usersMock.Object;
			usersManager = Mock.Of<IUsersManager>();

			controller = new InformingController(employeesManages, departmentManager, usersManager);
		}

		[Test]
		public void IndexTest()
		{
			var result = controller.Index(100, "BOJE KAK JE YA XOCHU ZAKOCNHIT` UNIVER.........");

			Assert.AreEqual("OkResult", result.GetType().ToString());
		}
	}
}
