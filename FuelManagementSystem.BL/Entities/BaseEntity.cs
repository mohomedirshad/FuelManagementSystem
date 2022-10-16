using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelManagementSystem.BL.Entities
{
    /// <summary>
    /// The Root entity which has defined the Id and Created Date
    /// </summary>
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public void SetCreatedDate()
        {
            CreatedOn = DateTime.Now;
        }

        public void SetUpdatedDate()
        {
            UpdatedOn = DateTime.Now;
        }
    }
}
