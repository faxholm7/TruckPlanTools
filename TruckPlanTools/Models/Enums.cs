using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckPlanTools.Models
{
    internal class Enums
    {
        public enum Priorty
        {
            None,
            Low,
            Medium,
            High
        }
        public enum Status
        {
            Created,
            Active,
            Finished
        }
    }
}
