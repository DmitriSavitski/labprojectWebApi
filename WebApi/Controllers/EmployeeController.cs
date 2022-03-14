using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[Route("employee")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeesManages _employeeManager;
		private readonly INationalityManager _nationalityManager;
		private readonly IPositionManager _positionManager;
		private readonly IFIOManager _fioManager;
		private readonly IDepartmentsManager _departmentsManager;


		public EmployeeController(IEmployeesManages employeeManager, INationalityManager nationalityManager, IPositionManager positionManager, IFIOManager fioManager, IDepartmentsManager departmentsManager)
		{
			_employeeManager = employeeManager ?? throw new ArgumentNullException(nameof(employeeManager));
			_nationalityManager = nationalityManager ?? throw new ArgumentNullException(nameof(nationalityManager));
			_positionManager = positionManager ?? throw new ArgumentNullException(nameof(positionManager));
			_fioManager = fioManager ?? throw new ArgumentNullException(nameof(fioManager));
			_departmentsManager = departmentsManager;
		}
		[HttpGet("index")]
		public ActionResult Index()
		{
			var employees = _employeeManager.GetAllEmployees();

			return Ok(employees);
		}

		[HttpGet("create")]
		public ActionResult Create()
		{
			var positions = _positionManager.GetAllPosition().ToList();
			var departments = _departmentsManager.GetAllDepartments();

			var emps = new EmpViewModel
			{
				positions = positions,
				departments = (List<DepartmentsDto>)departments
			};

			return Ok(emps);
		}

		[HttpPost("create")]
		public IActionResult Create(EmployeesDto employeesDto)
		{
			_employeeManager.CreateEmployeeAsync(employeesDto).GetAwaiter().GetResult();

			return Ok();
		}

		[HttpPost("delete")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			await _employeeManager.DeleteEmployeeAsync(id);

			return Ok();
		}

		[HttpGet("update")]
		public async Task<IActionResult> UpdateAsync(int id)
		{
			var employee = await _employeeManager.GetEmployeeByIdAsync(id);
			var positions = _positionManager.GetAllPosition().ToList();
			var departments = _departmentsManager.GetAllDepartments().ToList();

			var emps = new EmpViewModel
			{
				employee = employee,
				positions = positions,
				departments = departments
			};

			return Ok(emps);
		}

		[HttpPost("update")]
		public async Task<IActionResult> UpdateAsync(EmployeesDto employeesDto)
		{
			await _employeeManager.UpdateEmployeeInfo(employeesDto);

			return Ok();
		}
	}

	public class DepartmnetsViewModel
	{
		public int DepartmentsId { get; set; }

		public string Name { get; set; }
	}

	public class EmpViewModel
	{
		public List<DepartmentsDto> departments { get; set; }

		public EmployeesDto employee { get; set; }

		public List<PositionDto> positions { get; set; }
	}
}

