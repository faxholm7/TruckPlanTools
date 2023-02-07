using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckPlanTools.Models
{
    internal class TruckLocation : EntityId
    {
        //As the only model, this has a contructor and the properties set to init.
        //This is done because once a truck location is created, its immutable.
        public TruckLocation(Guid truckPlanId,
            DateTime timeStamp,
            double latitude, 
            double longitude,
            string country,
            double distanceSinceLastLocation)
        {
            Id = Guid.NewGuid();
            TruckPlanId = truckPlanId;
            TimeStamp = timeStamp;
            Latitude = latitude;
            Longitude = longitude;
            Country = country;
            DistanceSinceLastLocation = distanceSinceLastLocation;
        }

        public Guid TruckPlanId { get; init; }
        public DateTime TimeStamp { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public string Country { get; init; } //Could also be either just contry codes, or a live table of contries and then making a refernce to this
        public double DistanceSinceLastLocation { get; init; }
    }
}
