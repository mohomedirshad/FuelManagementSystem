using MongoDB.Bson;
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
        public ObjectId Id { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }

        public void SetCreatedDate()
        {
            CreatedOn = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
        }

        public void SetUpdatedDate()
        {
            UpdatedOn = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
        }
    }
}
