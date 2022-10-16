using FuelManagementSystem.BL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelManagementSystem.BL.Entities
{
    public class FuelStation: BaseEntity
    {
        public string Location { get; set; }
        public bool FuelAvailabilityStatus { get; set; }
        public DateTime FuelArrivalTime { get; set; }
        public DateTime FuelFinishTime { get; set; }
        public FuelType FuelType { get; set; }
        public List<User> Users { get; set; } = new List<User>();   // Not sure, may need to remove depending on the usage
    }
}
