using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckPlanTools.Models;

namespace TruckPlanTools.Interfaces.Repository
{
    internal interface ITruckLocationRepository : IRepository<TruckLocation>
    {
        TruckLocation GetPreviousLocationByTruckPlanId(Guid truckPlanId);
    }
}
