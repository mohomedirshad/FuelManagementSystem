using FuelManagementSystem.BL.Enums;

namespace FuelManagementSystem.BL.Entities
{
    public class User: BaseEntity
    {
        public string Name { get; set; }
        public UserType UserType { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public string Password { get; set; }
        public VehicleType VehicleType { get; set; }

        public void UpdateArrivalTime()
        {
            ArrivalTime = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
        }

        public void UpdateDepartureTime()
        {
            DepartureTime = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
        }

    }

}
