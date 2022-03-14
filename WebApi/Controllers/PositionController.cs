using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[Route("position")]
	[ApiController]
	public class PositionController : ControllerBase
	{
		private readonly IPositionManager _positionManager;

		public PositionController(IPositionManager positionManager)
		{
			_positionManager = positionManager;
		}

		[HttpGet("index")]
		public ActionResult Index()
		{
			var pos = _positionManager.GetAllPosition();
			return Ok(pos);
		}

		[HttpPost("create")]
		public async Task<ActionResult> CreateAsync(PositionDto positionDto)
		{
			await _positionManager.CreatePositionAsync(positionDto);
			return Ok();
		}

		[HttpPost("delete")]
		public async Task<ActionResult> DeleteAsync(int id)
		{
			await _positionManager.DeletePositionAsync(id);

			return Ok();
		}

		[HttpGet("update")]
		public ActionResult Update(int id)
		{
			var pos = _positionManager.GetPositionByIdAsync(id).GetAwaiter().GetResult();

			return Ok(new PositionDto
			{
				PositionIdDto = pos.PositionId,
				NameDto = pos.Name
			});
		}

		[HttpPost("update")]
		public async Task<ActionResult> UpdateAsync(PositionDto positionDto)
		{
			await _positionManager.UpdatePosition(positionDto);

			return Ok();
		}
	}
}
