using FuelManagementSystem.BL.Enums;

namespace FuelManagementSystem.BL.Entities
{
    public class FuelStation: BaseEntity
    {
        public string Location { get; set; }
        public FuelStatus FuelAvailabilityStatus { get; set; }
        public string FuelArrivalTime { get; set; }
        public string FuelFinishTime { get; set; }
        public FuelType FuelType { get; set; }
        public List<string> UserIds { get; set; } = new List<string>();

        public void UpdateFuelAvailabilityStatus(FuelStatus fuelAvailabilityStatus)
        {
            FuelAvailabilityStatus = fuelAvailabilityStatus;
        }

        public void SetFuelArrivalTime(DateTime arrivalTime)
        {
            FuelArrivalTime = arrivalTime.ToString("MM/dd/yyyy h:mm tt");
        }

        public void SetFuelFinishTime(DateTime finishTime)
        {
            FuelFinishTime = finishTime.ToString("MM/dd/yyyy h:mm tt");
        }

        public void AddUserToFuelStationQueue(string userId)
        {
            UserIds.Add(userId);
        }

        public void RemoveUserFromFuelStationQueue(string userId)
        {
            UserIds.Remove(userId);
        }
    }
}
