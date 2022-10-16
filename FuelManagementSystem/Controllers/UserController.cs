using FuelManagementSystem.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FuelManagementSystem.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("arrivaltime/{id}")]
        public IActionResult UpdateUserArrivalTime(Guid id, DateTime arrivalTime)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid User Id");
            }
            _userService.UpdateUserArrivalTime(id, arrivalTime);
            return NoContent();
        }

        [HttpPut("departuretime/{id}")]
        public IActionResult UpdateUserDepartureTime(Guid id, DateTime departureTime)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid User Id");
            }
            _userService.UpdateUserDepartureTime(id, departureTime);
            return NoContent();
        }
    }
}
