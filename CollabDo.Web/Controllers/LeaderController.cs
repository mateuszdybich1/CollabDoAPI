using CollabDo.Application.Exceptions;
using CollabDo.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollabDo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaderController : ControllerBase
    {
        private readonly ILeaderService _leaderService;

        public  LeaderController(ILeaderService leaderService)
        {
            _leaderService = leaderService;
        }


        [HttpGet("requests")]
        public IActionResult EmployeeAssignToLeaderRequests()
        {
            try
            {
                return Ok(_leaderService.GetEmployeeRequests());
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("employee")]
        public async Task<IActionResult> AssignEmployee(Application.Dtos.EmployeeRequestDto dto)
        {
            try
            {
                return Ok(await _leaderService.ApproveEmployeeRequest(dto));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("employee")]
        public async Task<IActionResult> RemoveEmployeeFromProject(Application.Dtos.EmployeeRequestDto dto)
        {
            try
            {
                return Ok(await _leaderService.RemoveEmployeeFromProject(dto));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("employees")]

        public IActionResult LeaderEmployees([FromQuery] Guid? leaderId=null)
        {
            try
            {
                if(leaderId == null)
                {
                    return Ok(_leaderService.GetEmployees());
                }
                else
                {
                    return Ok(_leaderService.GetEmployees(leaderId));
                }
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
