using CollabDo.Application.Dtos;
using CollabDo.Application.Exceptions;
using CollabDo.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollabDo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegisterDto userDto)
        {
            try
            {
                Guid result = await _userService.Register(userDto);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserDto()
        {
            return Ok(await _userService.GetUser());
        }

        [HttpGet("isleader")]
        [Authorize]
        public IActionResult IsUserLeader()
        {
            return Ok(_userService.IsUserLeader());
        }

        

        [HttpPut("verify")]
        [Authorize]
        public async Task<IActionResult> VerifyUserEmail()
        {
            try
            {
                return Ok(await _userService.VerifyEmail());
            }
            catch(EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("password")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] string userEmail)
        {
            try
            {
                return Ok(await _userService.ResetPassword(userEmail));
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ValidationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
