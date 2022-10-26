using FuelManagementSystem.Application.Dto;
using FuelManagementSystem.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FuelManagementSystem.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("arrivaltime/{userid}/fuelstation/{fuelstationid}")]
        public IActionResult UpdateUserArrivalTime(string userid, string fuelstationid)
        {
            var userObjectId = ObjectId.Parse(userid);
            var fuelStationObjectId = ObjectId.Parse(fuelstationid);
            if (userObjectId == ObjectId.Empty)
            {
                return BadRequest("Invalid User Id");
            }
            if (fuelStationObjectId == ObjectId.Empty)
            {
                return BadRequest("Invalid fuel station Id");
            }
            _userService.UpdateUserArrivalTime(userObjectId, fuelStationObjectId);
            return NoContent();
        }

        [HttpPut("departuretime/{userid}/fuelstation/{fuelstationid}")]
        public IActionResult UpdateUserDepartureTime(string userid, string fuelstationid)
        {
            var objectId = ObjectId.Parse(userid);
            var fuelStationObjectId = ObjectId.Parse(fuelstationid);
            if (objectId == ObjectId.Empty)
            {
                return BadRequest("Invalid User Id");
            }
            if (fuelStationObjectId == ObjectId.Empty)
            {
                return BadRequest("Invalid fuel station Id");
            }
            _userService.UpdateUserDepartureTime(objectId, fuelStationObjectId);
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("duration/{userId}/fuelstation/{fuelStationId}")]
        public async Task<IActionResult> GetUsersWaitingDurationInFuelStationQueue(string userId, string fuelStationId)
        {
            var userObjectId = ObjectId.Parse(userId);
            var fuelStationObjectId = ObjectId.Parse(fuelStationId);
            var duration = await _userService.GetUserQueueWaitingDuration(userObjectId,fuelStationObjectId);
            return Ok(duration);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(UserRegistrationDto userRegistrationDto)
        {
            await _userService.AddNewUser(userRegistrationDto);
            return Ok(true);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginDto loginDto)
        {
            var user = await _userService.ValidateUser(loginDto);
            if (user is not null)
            {
                return Ok(user);
            }
            return Unauthorized();
        }
    }
}
