using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CollabDo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }


        [HttpPost]
        public IActionResult CreateProject(ProjectDto projectDto)
        {
            try
            {
                return Ok(_projectService.SaveProject(projectDto));
            }
            catch (Application.Exceptions.ValidationException ex) 
            { 
                return BadRequest(ex.Message);
            }   
        }

        [HttpPut("{projectId}")]
        public IActionResult ProjectState([FromRoute] Guid projectId)
        {
            try
            {
                return Ok(_projectService.UpdateProjectState(projectId));
            }
            catch(Application.Exceptions.ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(FormatException ex)
            {
                return BadRequest(ex.Message);
            }   
        }

        [HttpDelete("{projectId}")]
        public IActionResult Project([FromRoute] Guid projectId)
        {
            try
            {
                return Ok(_projectService.DeleteProject(projectId));
            }
            catch (Application.Exceptions.ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Projects(
            [FromQuery] DateTime requestDate,
            [FromQuery] Guid? leaderId = null,
            [FromQuery][Range(1,2)] ProjectStatus projectStatus = ProjectStatus.InProgress,
            [FromQuery][Range(1,int.MaxValue)] int pageNumber = 1)
        {
            try
            {
                if (leaderId != null)
                {
                    return Ok(_projectService.GetProjects(leaderId, projectStatus, pageNumber, requestDate));
                }
                else
                {
                    return Ok(_projectService.GetProjects(projectStatus, pageNumber, requestDate));
                }
            }
            catch (Application.Exceptions.ValidationException ex)
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
