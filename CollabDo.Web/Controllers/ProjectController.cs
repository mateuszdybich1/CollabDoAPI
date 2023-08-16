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
            catch (ValidationException ex) 
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
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(FormatException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}
