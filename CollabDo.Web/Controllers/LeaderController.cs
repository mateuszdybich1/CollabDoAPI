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

        [HttpPost]
        public IActionResult AssignEmployee(Guid employeeRequestId, string employeeEmail)
        {
            try
            {
                return Ok(_leaderService.ApproveEmployeeRequest(employeeRequestId, employeeEmail));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
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


    }
}
