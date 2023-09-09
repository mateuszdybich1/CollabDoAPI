using CollabDo.Application.Exceptions;
using CollabDo.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using ValidationException = CollabDo.Application.Exceptions.ValidationException;

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
        public async Task<IActionResult> Comment([FromRoute] Guid taskId, [FromBody] string content)
        {
            return Ok(await _commentService.AddComment(taskId, content));
        }

        [HttpPut]
        public async Task<IActionResult> Comment([FromRoute] Guid taskId, [FromQuery] Guid commentId, [FromBody] string content)
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
        public async Task<IActionResult> Comment([FromRoute] Guid taskId, [FromQuery] Guid commentId)
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

        [HttpGet("comments")]
        public IActionResult Comments([FromRoute] Guid taskId, [FromQuery][Range(1,int.MaxValue)] int pageNumber, [FromQuery] DateTime requestDate) 
        { 
            return Ok(_commentService.GetTaskComments(taskId, pageNumber, requestDate));
        }

        [HttpGet("latest")]
        public IActionResult LatestComment([FromRoute] Guid taskId,[FromQuery] Guid? latestCommentId)
        {
            try
            {
                return Ok(_commentService.GetLatestComment(taskId, latestCommentId));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return StatusCode(404,ex.Message);
            }
           
        }
    }
}
