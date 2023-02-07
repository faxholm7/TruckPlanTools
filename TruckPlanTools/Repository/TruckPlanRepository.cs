using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckPlanTools.Interfaces.Repository;
using TruckPlanTools.Models;

namespace TruckPlanTools.Repository
{
    internal class TruckPlanRepository : BaseRepository<TruckPlan>, ITruckPlanRepository
    {
        public TruckPlan? GetActivePlanByTruckId(Guid truckId)
        {
            return _collection.FirstOrDefault(x => x.TruckId == truckId && x.Status == Enums.Status.Active);
        }
    }
}
