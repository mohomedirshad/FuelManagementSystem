using FuelManagementSystem.BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelManagementSystem.Application.Interfaces
{
    public interface IUserRepository
    {
        Task UpdateUser(User user);
        Task<User> GetUserById(Guid id);
    }
}
