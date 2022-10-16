using FuelManagementSystem.BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelManagementSystem.BL.Interfaces
{
    public interface IUserService
    {
        Task UpdateUserArrivalTime(Guid id, DateTime arrivalTime);
        Task UpdateUserDepartureTime(Guid id, DateTime arrivalTime);

    }
}
