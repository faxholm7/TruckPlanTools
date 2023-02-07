using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TruckPlanTools.Models.Enums;

namespace TruckPlanTools.Models
{
    internal class TruckPlan : EntityId
    {
        public Guid DriverId { get; set; }
        public Guid TruckId { get; set; }
        public Guid ShipmentId { get; set; }
        public string StartCordinate { get; set; }
        public string EndCordinate { get; set; }
        public double TotalDistanceKM { get; set; } = 0;
        public Status Status { get; set; } = Status.Created;
    }
}
