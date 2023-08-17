
using CollabDo.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CollabDo.Web.Controllers
{
    [Route("api/[controller]/{projectId}")]
    [ApiController]
    [Authorize]
    public class EmployeeTaskController : ControllerBase
    {
        private readonly IEmployeeTaskService _employeeTaskService;

        public EmployeeTaskController(IEmployeeTaskService employeeTaskService)
        {
            _employeeTaskService = employeeTaskService;
        }


        [HttpGet]
        public IActionResult EmployeeTasks(
            [FromRoute]Guid projectId, 
            [FromQuery][Range(1, 2)] Application.Entities.TaskStatus taskStatus = Application.Entities.TaskStatus.Created,
            [FromQuery][Range(1, int.MaxValue)] int pageNumber = 1) 
        {
            try
            {
                return Ok(_employeeTaskService.GetEmployeeTasks(projectId, taskStatus, pageNumber));
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult TaskState([FromRoute] Guid projectId, [FromQuery] Guid taskId, [FromQuery][Range(0,4)] Application.Entities.TaskStatus status)
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
