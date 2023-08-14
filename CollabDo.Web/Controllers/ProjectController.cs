using CollabDo.Application.Dtos;
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
            return Ok(_projectService.SaveProject(projectDto));
        }

        [HttpPut]
        public IActionResult UpdateProject([FromQuery] string projectId)
        {
            return Ok(_projectService.UpdateProjectState(projectId));
        }

    }
}
