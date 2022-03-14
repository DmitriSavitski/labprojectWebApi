using BLL.Interfaces;
using BLL.Models;
using DAL.Context;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[Route("department")]
	[ApiController]
	public class DepartmentController : ControllerBase
	{
		private readonly IDepartmentsManager _departmentsManager;
		private readonly IEmployeesManages _employeesManages;
		private readonly ApplicationContext _applicationContext;

		public DepartmentController(IDepartmentsManager departmentsManager, IEmployeesManages employeesManages, ApplicationContext applicationContext)
		{
			_departmentsManager = departmentsManager;
			_employeesManages = employeesManages;
			_applicationContext = applicationContext;
		}
		[HttpGet("index")]
		public ActionResult Index()
		{
			var departments = _departmentsManager.GetAllDepartments().ToList();
			departments.ForEach(c => c.Employee = _departmentsManager.GetDepartmnetEmployees(c.NameDto));

			return Ok(departments);
		}

		[HttpPost("create")]
		public async Task<ActionResult> CreateAsync(DepartmentsDto departmentsDto)
		{
			await _departmentsManager.CreateDepartmentAsync(departmentsDto);

			return Ok();
		}

		[HttpGet("update")]
		public async Task<ActionResult> UpdateAsync(int id)
		{
			var x = await _departmentsManager.GetDepartmentByIdAsync(id);
			if (x == null)
				return NotFound();

			return Ok(x);
		}

		[HttpPost("update")]
		public async Task<ActionResult> UpdateAsync(Departments departments)
		{
			await _departmentsManager.UpdateDepartmentInfo(departments);

			return Ok();
		}

		[HttpPost("delete")]
		public async Task<ActionResult> DeleteAsync(int id)
		{
			_applicationContext.Departments.Find(id).Employee.Clear();

			await _departmentsManager.DeleteDepartmentAsync(id);

			return Ok();
		}

		[HttpGet("setleader")]
		public async Task<ActionResult> SetLeaderAsync(int id)
		{
			var dep = await _departmentsManager.GetDepartmentDtoByIdAsync(id);

			if (dep == null)
				return NotFound("Не найден отдел");

			var emp = _departmentsManager.GetAllDepartmentEmployees(dep.NameDto);

			if (emp == null)
				return NotFound("В бд не найдены работники");


			var employees = new List<EmployeeViewModel>();

			foreach (var item in emp)
			{
				var fioArr = new string[3] { item.FIODto.LastName, item.FIODto.FirstName, item.FIODto.MiddleName };
				employees.Add(new EmployeeViewModel
				{
					EmployeesId = item.EmployeeIdDto,
					fio = String.Join(" ", fioArr)
				});
			}

			var deps = new DepViewModel
			{
				employees = employees,
				department = dep
			};

			return Ok(deps);
		}

		[HttpPost("setleader")]
		public async Task<ActionResult> SetLeaderAsync(Departments departmentsDto)
		{
			await _departmentsManager.UpdateDepartmentInfo(departmentsDto);
			return Ok();
		}
	}

	public class EmployeeViewModel
	{
		public int EmployeesId { get; set; }

		public string fio { get; set; }
	}

	public class DepViewModel
	{
		public List<EmployeeViewModel> employees { get; set; }

		public DepartmentsDto department { get; set; }
	}
}

