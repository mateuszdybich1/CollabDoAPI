using CollabDo.Application.Dtos;
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
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }


        [HttpPost]
        public IActionResult CreateTask(TaskDto taskDto)
        {
            try
            {
                return Ok(_taskService.CreateTask(taskDto));
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ProjectTasks(Guid projectId)
        {
            try
            {
                return Ok(_taskService.GetAllTasks(projectId));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> AssignEmployeeToTask([FromQuery] Guid projectId, Guid taskId, string employeeEmail)
        {
            try
            {
                return Ok(await _taskService.AssignToEmployee(projectId, taskId, employeeEmail));
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
