using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckPlanTools.Interfaces.Repository;
using TruckPlanTools.Models;

namespace TruckPlanTools.Repository
{
    internal class TruckLocationRepository : BaseRepository<TruckLocation>, ITruckLocationRepository
    {
        public TruckLocation? GetPreviousLocationByTruckPlanId(Guid truckPlanId)
        {
            return _collection.Where(x => x.TruckPlanId == truckPlanId).OrderByDescending(x => x.TimeStamp).FirstOrDefault();
        }
    }
}
