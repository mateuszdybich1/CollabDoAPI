using CollabDo.Application.Exceptions;
using CollabDo.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CollabDo.Web.Controllers
{
    [Route("api/[controller]/{taskId}")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Comment([FromRoute] Guid taskId, string content)
        {
            return Ok(await _commentService.AddComment(taskId, content));
        }

        [HttpPut]
        public async Task<IActionResult> Comment([FromRoute] Guid taskId, Guid commentId, string content)
        {
            try
            {
                return Ok(await _commentService.EditComment(taskId, commentId, content));
            }
            catch(Application.Exceptions.ValidationException ex )
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Comment([FromRoute] Guid taskId, Guid commentId)
        {
            try
            {
                return Ok(await _commentService.DeleteComment(taskId, commentId));
            }
            catch (Application.Exceptions.ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Comments([FromRoute] Guid taskId, [FromQuery][Range(1,int.MaxValue)] int pageNumber) 
        { 
            return Ok(_commentService.GetTaskComments(taskId, pageNumber));
        }
    }
}
