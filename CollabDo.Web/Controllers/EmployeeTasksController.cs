
using CollabDo.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CollabDo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeTasksController : ControllerBase
    {
        private readonly IEmployeeTaskService _employeeTaskService;

        public EmployeeTasksController(IEmployeeTaskService employeeTaskService)
        {
            _employeeTaskService = employeeTaskService;
        }


        [HttpGet]
        public IActionResult EmployeeTasks([FromQuery]Guid projectId) 
        {
            try
            {
                return Ok(_employeeTaskService.GetEmployeeTasks(projectId));
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult TaskState([FromQuery] Guid projectId, [FromQuery] Guid taskId, [FromQuery][Range(0,4)] Application.Entities.TaskStatus status)
        {
            try
            {
                return Ok(_employeeTaskService.SetTaskState(projectId,taskId,status));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
