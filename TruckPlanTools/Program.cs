using Microsoft.Extensions.Configuration;
using TruckPlanTools.Handlers;
using TruckPlanTools.Models;
using TruckPlanTools.Options;
using TruckPlanTools.Repository;
using TruckPlanTools.Services;

namespace TruckPlanTools
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var settings = config.GetRequiredSection("ApiOptions").Get<ApiOptions>();

            TestSetup(settings);

            Console.ReadLine();
        }

        static void TestSetup(ApiOptions apiOptions)
        {
            var currentTruck = new Truck()
            {
                Id = Guid.NewGuid(),
                LicenPlate = "ABCDEFG",
                Manufactor = "Volvo",
                Model = "1.1",
                TotalKM = 65430
            };

            var currentGPSTracker = new GPSTracker()
            {
                Id = Guid.NewGuid(),
                TruckId = currentTruck.Id
            };

            //Only the Id is used in this case. But if the LINQ logic to answer question 4 was added this would be needed.
            var currentDriver = new Driver()
            {
                Id = Guid.NewGuid(),
                Birthday = new DateOnly(1970, 5, 2),
                FirstName = "Bob",
                LastName = "Man",
                LicensNumber = "1234663"
            };

            var currentTruckPlan = new TruckPlan()
            {
                Id = Guid.NewGuid(),
                DriverId = currentDriver.Id,
                ShipmentId = Guid.NewGuid(),
                Status = Enums.Status.Active,
                TruckId = currentTruck.Id,
                StartCordinate = "52.437871,9.882710"
            };

            var gpsTrackerRepositry = new GPSTrackerRepository();
            var truckPlanRepository = new TruckPlanRepository();
            var truckLocationRepository = new TruckLocationRepository();
            var geoLocationService = new GeolocationService(apiOptions);

            gpsTrackerRepositry.Add(currentGPSTracker);
            truckPlanRepository.Add(currentTruckPlan);

            var gpsLocationHandler = new GPSLocationEventHandler(gpsTrackerRepositry, truckPlanRepository, truckLocationRepository, geoLocationService,  null);

            var gpsLocations = LoadCordinates(currentGPSTracker.Id);

            foreach (var location in gpsLocations)
            {
                gpsLocationHandler.Handle(location);
                Thread.Sleep(1000); //Sleep to make a clear seperation for viewing the TruckLocations
            }

            Console.WriteLine($"Distancn driven for truckplan with id: {currentTruckPlan.Id} is: {double.Round(currentTruckPlan.TotalDistanceKM, 2)} km");

            //TO get answer for question 4, if the solution was implemeted in SQL
            //JOIN TruckPlan TruckPlan.DriverID = Driver.Id
            //JOIN TruckLocation TruckLocation.TruckPlanId = TruckPlan.Id
            //SELECT SUM(DistanceSinceLastLocation) as TotalDistanceDriven
            //WHERE Driver.Birthday > x
            //  AND (TruckLocation.TimeStamp > y AND TruckLocation.TimeStamp < z)
            //  AND TruckLocation.Country = 'Germany'
        }

        static List<GPSLocationEvent> LoadCordinates(Guid gpsTrackerId)
        {
            var gpsLocations = new List<GPSLocationEvent>();
            var cordinates = System.IO.File.ReadAllLines(@".\Cordinates.txt");

            foreach (var cordinate in cordinates)
            {
                gpsLocations.Add(new GPSLocationEvent()
                {
                    Cordinate = cordinate,
                    GPSTrackerId = gpsTrackerId
                });
            }
            return gpsLocations;
        }
    }
}