using CollabDo.Application.Dtos;
using CollabDo.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public IActionResult ProjectTasks(Guid projectId, 
            [FromQuery][Range(1, 2)] Application.Entities.TaskStatus taskStatus = Application.Entities.TaskStatus.Created, 
            [FromQuery][Range(1, int.MaxValue)] int pageNumber = 1)
        {
            try
            {
                return Ok(_taskService.GetAllTasks(projectId,taskStatus,pageNumber));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> AssignEmployeeToTask(Guid projectId, Guid taskId, string employeeEmail)
        {
            try
            {
                return Ok(await _taskService.AssignToEmployee(projectId, taskId, employeeEmail));
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
