using GeoCoordinatePortable;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckPlanTools.Interfaces;
using TruckPlanTools.Models;
using TruckPlanTools.Repository;

namespace TruckPlanTools.Handlers
{
    internal class GPSLocationEvent
    {
        public Guid GPSTrackerId { get; set; }
        public string Cordinate { get; set; }
    }

    internal class GPSLocationEventHandler
    {
        private readonly IGPSTrackerRepository _gpsTrackerRepository;
        private readonly ITruckPlanRepository _truckPlanRepository;
        private readonly ITruckLocationRepository _truckLocationRepository;
        private readonly ILogger<GPSLocationEventHandler> _logger;

        public GPSLocationEventHandler(IGPSTrackerRepository gpsTrackerRepository,
            ITruckPlanRepository truckPlanRepository,
            ITruckLocationRepository truckLocationRepository,
            ILogger<GPSLocationEventHandler> logger)
        {
            _gpsTrackerRepository = gpsTrackerRepository;
            _truckPlanRepository = truckPlanRepository;
            _truckLocationRepository = truckLocationRepository;
            _logger = logger;
        }

        public void Handle(GPSLocationEvent @event)
        {
            var gpsTracker = _gpsTrackerRepository.GetById(@event.GPSTrackerId);

            if (gpsTracker == null)
            {
                _logger.LogError("GPS tracker Id dose not match in the database");
                return;
                //Depending on what implemtation is chosen, this would be different
            }

            var activeTruckPlan = _truckPlanRepository.GetActivePlanByTruckId(gpsTracker.TruckId);

            if (activeTruckPlan == null)
            {
                _logger.LogError($"No active truckplan for the truck with id: {gpsTracker.TruckId}");
                return;
                //Depending on what implemtation is chosen, this would be different
            }

            var currentLatitude = GetLatitudeFromStringCordinate(@event.Cordinate);
            var currentLongitude = GetLongitudeFromStringCordinate(@event.Cordinate);

            var previousLocation = _truckLocationRepository.GetPreviousLocationByTruckPlanId(activeTruckPlan.Id);

            var distance = (double)0;

            if (previousLocation != null)
                distance = CalculateDistanceInKM(currentLatitude, currentLongitude, previousLocation);
            else
            {
                var startLatitude = GetLatitudeFromStringCordinate(activeTruckPlan.StartCordinate);
                var startLongitude = GetLongitudeFromStringCordinate(activeTruckPlan.StartCordinate);
                distance = CalculateDistanceInKM(currentLatitude, currentLongitude, startLatitude, startLongitude);
            }
                
            var currentCountry = GetCountry(@event.Cordinate);

            var truckLocation = new TruckLocation(activeTruckPlan.Id, DateTime.UtcNow, currentLatitude, currentLongitude, currentCountry, distance);
            _truckLocationRepository.Add(truckLocation);

            activeTruckPlan.TotalDistanceKM += distance;
            _truckLocationRepository.Update(truckLocation);
        }

        private string GetCountry(string cordinate)
        {
            //Ommited
            return "Germany";
        }
        public double CalculateDistanceInKM(double currentLatitude, double currentLongitude, TruckLocation truckLocation)
        {
            return CalculateDistanceInKM(currentLatitude, currentLongitude,
                                            truckLocation.Latitude, truckLocation.Longitude);
        }
        public double CalculateDistanceInKM(double currentLatitude, double currentLongitude, double previousLatitude, double previousLongitude)
        {
            var firstCordinate = new GeoCoordinate(currentLatitude, currentLongitude);
            var lastCordinate = new GeoCoordinate(previousLatitude, previousLongitude);

            return firstCordinate.GetDistanceTo(lastCordinate) / 1000;
        }

        private double GetLatitudeFromStringCordinate(string cordinate)
        {
            return double.Parse(cordinate.Split(',')[0], CultureInfo.InvariantCulture);
        }
        private double GetLongitudeFromStringCordinate(string cordinate)
        {
            return double.Parse(cordinate.Split(',')[1], CultureInfo.InvariantCulture);
        }
    }
}
