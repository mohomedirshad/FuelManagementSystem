using FuelManagementSystem.Application.Interfaces;
using FuelManagementSystem.BL.Enums;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FuelManagementSystem.Controllers
{
    [Route("api/fuelstation")]
    [ApiController]
    public class FuelStationController : ControllerBase
    {
        private readonly IFuelStationService _fuelStationService;

        public FuelStationController(IFuelStationService fuelStationService)
        {
            _fuelStationService = fuelStationService;
        }

        private static ObjectId ConvertToObjectId(string fuelStationId)
        {
            return ObjectId.Parse(fuelStationId);
        }

        [HttpPut("fuel-availability/{fuelstationid}")]
        public async Task<IActionResult> UpdateFuelAvailabilityStatus(string fuelstationid, FuelStatus fuelAvailabilityStatus)
        {
            var fuelStationObjectId = ConvertToObjectId(fuelstationid);
            if (fuelStationObjectId == ObjectId.Empty)
            {
                return BadRequest("Invalid fuel station Id");
            }
            await _fuelStationService.UpdateAvailabilityFuelStatus(fuelStationObjectId, fuelAvailabilityStatus);
            return NoContent();
        }

        [HttpPut("fuel-arrival-time/{fuelstationid}")]
        public async Task<IActionResult> UpdateFuelArrivalTime(string fuelstationid, DateTime fuelArrivalTime)
        {
            var fuelStationObjectId = ConvertToObjectId(fuelstationid);
            if (fuelStationObjectId == ObjectId.Empty)
            {
                return BadRequest("Invalid fuel station Id");
            }
            await _fuelStationService.UpdateFuelArrivalTime(fuelStationObjectId, fuelArrivalTime);
            return NoContent();
        }

        [HttpPut("fuel-finish-time/{fuelstationid}")]
        public async Task<IActionResult> UpdateFuelFinishTime(string fuelstationid, DateTime fuelFinishTime)
        {
            var fuelStationObjectId = ConvertToObjectId(fuelstationid);
            if (fuelStationObjectId == ObjectId.Empty)
            {
                return BadRequest("Invalid fuel station Id");
            }
            await _fuelStationService.UpdateFuelFinishTime(fuelStationObjectId, fuelFinishTime);
            return NoContent();
        }

        // create

        [HttpGet("location")]
        public  async Task<IActionResult>SearchLocation(string location)
        {
            
            if (string.IsNullOrWhiteSpace(location))
            {
                return BadRequest("location is null or empty");
            }
            var locations = await _fuelStationService.SearchFuelStation(location);
            return Ok(locations);
        }

        [HttpPut("jointoqueue")]
        public async Task<IActionResult> AddUserToFuelStationQueue(string userId, string fuelStationId)
        {
            var fuelStationObjectId = ConvertToObjectId(fuelStationId);
            var userObjectId = ObjectId.Parse(userId);
            
            await _fuelStationService.AddUserToFuelStationQueue(userObjectId,fuelStationObjectId);
            return NoContent();
        }

        [HttpPut("removefromqueue")]
        public async Task<IActionResult> RemoveUserFromFuelStationQueue(string userId, string fuelStationId)
        {
            ObjectId fuelStationObjectId = ConvertToObjectId(fuelStationId);
            var userObjectId = ObjectId.Parse(userId);

            await _fuelStationService.RemoveUserFromFuelStationQueue(userObjectId, fuelStationObjectId);
            return NoContent();
        }

        // get count of users in a fuel station
        [HttpGet("users/count")]
        public async Task<IActionResult> FuelStationUsersCount(string fuelStationId)
        {
            var fuelStationObjectId = ConvertToObjectId(fuelStationId);
            var usersCount = await _fuelStationService.FuelStationQueueUsersCount(fuelStationObjectId);
            return Ok(usersCount);
        }
    }
}
