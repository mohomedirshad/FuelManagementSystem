using FuelManagementSystem.BL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelManagementSystem.Application.Dto
{
    public class UserRegistrationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string NIC { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The Confirm Password does not match with the password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public UserType UserType { get; set; }
        [Required]
        public VehicleType VehicleType { get; set; }
    }
}
