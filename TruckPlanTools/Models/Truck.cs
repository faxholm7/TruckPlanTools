using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckPlanTools.Models
{
    internal class Truck : EntityId
    {
        public string Manufactor { get; set; }
        public string Model { get; set; }
        public string LicenPlate { get; set; }
        public double TotalKM { get; set; }
    }
}
