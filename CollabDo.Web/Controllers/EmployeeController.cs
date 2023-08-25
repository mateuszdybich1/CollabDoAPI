using CollabDo.Application.Exceptions;
using CollabDo.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollabDo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpGet]
        public IActionResult Employee()
        {
            try
            {
                return Ok(_employeeService.GetEmployee());
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> AssignToLeaderRequest([FromBody] string leaderEmail)
        {
            try
            {
                return Ok(await _employeeService.CreateLeaderAssignmentRequest(leaderEmail));
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

        [HttpDelete]
        public async Task<IActionResult> LeaderRequest([FromBody] string leaderEmail)
        {
            try
            {
                return Ok(await _employeeService.DeleteLeaderAssignmentRequest(leaderEmail));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
