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
        public IActionResult UpdateUserArrivalTime(string userid, string fuelstationid, DateTime arrivalTime)
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
            _userService.UpdateUserArrivalTime(userObjectId, fuelStationObjectId, arrivalTime);
            return NoContent();
        }

        [HttpPut("departuretime/{userid}/fuelstation/{fuelstationid}")]
        public IActionResult UpdateUserDepartureTime(string userid, string fuelstationid, DateTime departureTime)
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
            _userService.UpdateUserDepartureTime(objectId, fuelStationObjectId, departureTime);
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
    }
}
