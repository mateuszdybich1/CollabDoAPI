using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollabDo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderController : ControllerBase
    {
        [HttpPost]
        public IActionResult AssignEmployee()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult EmployeeAssignToLeaderRequests()
        {
            return Ok();

        }


    }
}
