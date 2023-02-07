using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckPlanTools.Models
{
    internal class Driver : EntityId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LicensNumber { get; set; }
        public DateOnly Birthday { get; set; }
    }
}
