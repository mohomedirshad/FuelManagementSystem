using FuelManagementSystem.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelManagementSystem.BL.Entities
{
    public class User: BaseEntity
    {
        public string Name { get; set; }
        public UserType UserType { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public bool IsLoggedIn { get; set; }
        public VehicleType VehicleType { get; set; }

        public void UpdateArrivalTime(DateTime arrivalTime)
        {
            ArrivalTime = arrivalTime;
        }

        public void UpdateDepartureTime(DateTime departureTime)
        {
            DepartureTime = departureTime;
        }

        public void SetStatusToLogIn()
        {
            IsLoggedIn = true;
        }

        public void SetStatusToLogOut()
        {
            IsLoggedIn = false;
        }
    }

}
