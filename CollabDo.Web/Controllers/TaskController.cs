using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.IServices;
using CollabDo.Application.Services;
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
        public async Task<IActionResult> CreateTask(TaskDto taskDto)
        {
            try
            {
                return Ok(await _taskService.CreateTask(taskDto));
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{projectId}/{taskId}")]
        public IActionResult UpdateTask([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromQuery] Application.Entities.TaskStatus taskStatus, [FromQuery] bool isLeader)
        {
            try
            {
                return Ok(_taskService.SetTaskStatus(projectId, taskId, isLeader,taskStatus));
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


        [HttpDelete("{projectId}/{taskId}")]
        public IActionResult Task([FromRoute] Guid projectId, [FromRoute] Guid taskId)
        {
            try
            {
                return Ok(_taskService.DeleteTask(projectId, taskId));
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

        [HttpGet("employee/{projectId}")]
        public IActionResult EmployeeTasks(
            [FromRoute] Guid projectId,
            [FromQuery] DateTime requestDate,
            [FromQuery][Range(0, 4)] Application.Entities.TaskStatus taskStatus = Application.Entities.TaskStatus.Started,
            [FromQuery][Range(1, int.MaxValue)] int pageNumber = 1)
        {
            try
            {
                return Ok(_taskService.GetEmployeeTasks(projectId, requestDate, taskStatus, pageNumber));
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

        [HttpGet("leader/{projectId}")]
        public IActionResult LeaderTasks(
            [FromRoute] Guid projectId,
            [FromQuery] DateTime requestDate,
            [FromQuery][Range(0, 4)] Application.Entities.TaskStatus taskStatus = Application.Entities.TaskStatus.Started,
            [FromQuery][Range(1, int.MaxValue)] int pageNumber = 1)
        {
            try
            {
                return Ok(_taskService.GetLeaderTasks(projectId, requestDate, taskStatus, pageNumber));
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

        [HttpGet("all/{projectId}")]
        public async Task<IActionResult> ProjectTasks(
            [FromRoute] Guid projectId,
            [FromQuery] DateTime requestDate,
            [FromQuery][Range(0, 4)] Application.Entities.TaskStatus taskStatus = Application.Entities.TaskStatus.Started, 
            [FromQuery][Range(1, int.MaxValue)] int pageNumber = 1)
        {
            try
            {
                return Ok(await _taskService.GetAllTasks(projectId,requestDate, taskStatus,pageNumber));
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

        
    }
}
