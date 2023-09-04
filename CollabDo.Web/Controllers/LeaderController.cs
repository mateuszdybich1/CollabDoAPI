using CollabDo.Application.Exceptions;
using CollabDo.Application.IServices;
using CollabDo.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollabDo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class LeaderController : ControllerBase
    {
        private readonly ILeaderService _leaderService;

        public  LeaderController(ILeaderService leaderService)
        {
            _leaderService = leaderService;
        }


        [HttpGet("requests")]
        [Authorize]
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

        [HttpGet("{leaderId}/email")]
        public async Task<IActionResult> GetLeaderEmail([FromRoute] Guid leaderId)
        {
            try
            {
                return Ok(await _leaderService.GetLederEmail(leaderId));
            }
            catch(EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("employee/{requestId}")]
        [Authorize]
        public async Task<IActionResult> AssignEmployee([FromRoute] Guid requestId)
        {
            try
            {
                return Ok(await _leaderService.ApproveEmployeeRequest(requestId));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("employee/{requestId}/request")]
        [Authorize]
        public async Task<IActionResult> DeleteEmployeeRequest([FromRoute] Guid requestId)
        {
            try
            {
                return Ok(await _leaderService.RemoveEmployeeRequest(requestId));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("employee/{employeeId}")]
        [Authorize]
        public async Task<IActionResult> RemoveEmployeeFromGroup([FromRoute] Guid employeeId)
        {
            try
            {
                return Ok(await _leaderService.RemoveEmployeeFromProject(employeeId));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("employees")]
        [Authorize]
        public async Task< IActionResult> LeaderEmployees([FromQuery] Guid? leaderId=null)
        {
            try
            {
                if(leaderId == null)
                {
                    return Ok(await _leaderService.GetEmployees());
                }
                else
                {
                    return Ok(await _leaderService.GetEmployees(leaderId));
                }
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
