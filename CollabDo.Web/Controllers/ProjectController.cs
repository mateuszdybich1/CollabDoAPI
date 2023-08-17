using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.Exceptions;
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

        [HttpPut]
        public IActionResult ProjectState([FromQuery] Guid projectId)
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

        [HttpGet]
        public IActionResult Projects(
            [FromQuery] Guid leaderId,
            [FromQuery][Range(1,2)] ProjectStatus projectStatus = ProjectStatus.InProgress,
            [FromQuery][Range(1,int.MaxValue)] int pageNumber = 1)
        {
            try
            {
                if (leaderId == Guid.Empty)
                {
                    return Ok(_projectService.GetProjects(leaderId, projectStatus, pageNumber));
                }
                else
                {
                    return Ok(_projectService.GetProjects(projectStatus, pageNumber));
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
