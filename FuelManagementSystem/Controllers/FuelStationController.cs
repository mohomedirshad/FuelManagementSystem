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

            try
            {
                await _fuelStationService.UpdateAvailabilityFuelStatus(fuelStationObjectId, fuelAvailabilityStatus);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong");
            }
        }

        [HttpPut("fuel-arrival-time/{fuelstationid}")]
        public async Task<IActionResult> UpdateFuelArrivalTime(string fuelstationid, string fuelArrivalTime)
        {
            var fuelStationObjectId = ConvertToObjectId(fuelstationid);
            if (fuelStationObjectId == ObjectId.Empty)
            {
                return BadRequest("Invalid fuel station Id");
            }

            try
            {
                await _fuelStationService.UpdateFuelArrivalTime(fuelStationObjectId, fuelArrivalTime);
                return Ok(new { arrivalTime = "Updated" });
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong");
            }
        }

        [HttpPut("fuel-finish-time/{fuelstationid}")]
        public async Task<IActionResult> UpdateFuelFinishTime(string fuelstationid, string fuelFinishTime)
        {
            var fuelStationObjectId = ConvertToObjectId(fuelstationid);
            if (fuelStationObjectId == ObjectId.Empty)
            {
                return BadRequest("Invalid fuel station Id");
            }
            try
            {
                await _fuelStationService.UpdateFuelFinishTime(fuelStationObjectId, fuelFinishTime);
                return Ok(new { finishTime = "Updated" });
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong");
            }
        }

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

        // how to send a response in a json format... create a class and send.

        // this sending result should be json format in each responses.

        [HttpPut("jointoqueue")]
        public async Task<IActionResult> AddUserToFuelStationQueue(string userId, string fuelStationId)
        {
            var fuelStationObjectId = ConvertToObjectId(fuelStationId);
            var userObjectId = ObjectId.Parse(userId);

            try
            {
                await _fuelStationService.AddUserToFuelStationQueue(userObjectId,fuelStationObjectId);
                return Ok(new {Join = "Joined"});
            }
            catch (Exception ex)
            {
                throw new Exception("something went wrong!");
            }
        }

        [HttpPut("removefromqueue")]
        public async Task<IActionResult> RemoveUserFromFuelStationQueue(string userId, string fuelStationId)
        {
            ObjectId fuelStationObjectId = ConvertToObjectId(fuelStationId);
            var userObjectId = ObjectId.Parse(userId);

            try
            {
                await _fuelStationService.RemoveUserFromFuelStationQueue(userObjectId, fuelStationObjectId);
                return Ok(new { Exit = "Exit" });
            }
            catch (Exception ex)
            {
                throw new Exception("something went wrong!");
            }
            
        }

        // get count of users in a fuel station
        [HttpGet("users/count")]
        public async Task<IActionResult> FuelStationUsersCount(string fuelStationId)
        {
            var fuelStationObjectId = ConvertToObjectId(fuelStationId);
            var usersCount = await _fuelStationService.FuelStationQueueUsersCount(fuelStationObjectId);
            return Ok(usersCount);
        }

        [HttpGet("{fuelStationId}")]
        public async Task<IActionResult> GetFuelStationById(string fuelStationId)
        {
            var fuelStationObjectId = ConvertToObjectId(fuelStationId);
            var fuelStation = await _fuelStationService.GetFuelStationById(fuelStationObjectId);
            return Ok(fuelStation);
        }

        [HttpGet]
        public async Task<IActionResult> GetFuelStations()
        {            
            var fuelStation = await _fuelStationService.GetFuelStations();
            return Ok(fuelStation);
        }
    }
}
