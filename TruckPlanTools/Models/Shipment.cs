using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TruckPlanTools.Models.Enums;

namespace TruckPlanTools.Models
{
    internal class Shipment : EntityId
    {
        public string TrailerNumber { get; set; }
        public Priorty Priorty { get; set; }
    }
}
