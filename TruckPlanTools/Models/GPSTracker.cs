using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckPlanTools.Models
{
    internal class GPSTracker : EntityId
    {
        public Guid TruckId { get; set; }
    }
}
