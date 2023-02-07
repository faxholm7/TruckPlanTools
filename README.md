# TruckPlanTools

## Assumptions:
The domain in the described task is only a small slice, therefore the domain model/entities have created with the thoughts below.

A GPS tracker and a truck can be independent, if a tracker is broken the ID it emits will properly change. Because of this a truck and a GPS tracker is two separate entities.

A TruckPlan can be made in advance and will still be saved once it's are finished.

A TruckPlan will always have a shipment.

Shipments can have different priorties. These are set when the shipment is created.

When a truck arrives at its destination it will be shut off, and that will stop in GPS tracker.

## Left out of scope:
Task 3, but the functionality could have been made with OpenCage’s API. Using a GET request to their API with the API key and the specific coordinates, would have resulted in a model that would contain the country. 
Another solution could have been to have a DB of cities with their respective cordites, whenever a GPSLocationEvent was handled it would look up to find the coordinate closest to the event coordinate. This solution would eliminate having an external service, but could come with some uncertainty, with driving near boarder. Depending on how the solution was implemented, performance could be a challenge as well.

Task 4, the solution has been added if the entire solution was done with a relational database, but when the fake DB is made with collections the LINQ equivalent could take up quite a long time.
At the moment there is no was to either create or finish a TruckPlan, because of the last assumption, the TruckPlan can not be automatically finished when the truck arrives, because the tracker might not get to send the event before the truck and the tracker is turned off. When the TruckPlan is created in the system it would be created in the DB. A TruckArrivalEvent and handler could be added, in this the current plan would change status and the distance between the last location and the end coordinate would be added to TotalDistanceKM, also the trucks TotalKM would be updated.

# Domain model
![Domain model](https://user-images.githubusercontent.com/84728646/217355268-2666aa7a-ccfc-4ba1-909d-f3e78998a596.png)
