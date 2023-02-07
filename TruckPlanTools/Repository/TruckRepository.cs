using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckPlanTools.Interfaces.Repository;
using TruckPlanTools.Models;
namespace TruckPlanTools.Repository
{
    internal class TruckRepository : BaseRepository<Truck>, ITruckRepository
    {
    }
}
