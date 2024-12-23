// Opticom Script
// Turns nearby traffic lights green

/*using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace POLSIM.Client
{
    internal class Opticom : BaseScript
    {
        private List<int> trafficLightModels = new List<int>();
        public List<int> allowedVehicles = new List<int>();
        public List<int> blacklistedVehicles = new List<int>();
        public int playerServerId;
        private int foundTrafficLight = 0;
        private int trafficLightBlip = 0;
        private DateTime searchStopTime;

        public Opticom()
        {
            // Define settings directly within the script
            trafficLightModels.Add(API.GetHashKey("prop_traffic_01a"));
            trafficLightModels.Add(API.GetHashKey("prop_traffic_01b"));
            trafficLightModels.Add(API.GetHashKey("prop_traffic_01d"));
            trafficLightModels.Add(API.GetHashKey("prop_traffic_02a"));
            trafficLightModels.Add(API.GetHashKey("prop_traffic_02b"));
            trafficLightModels.Add(API.GetHashKey("prop_traffic_03a"));
            trafficLightModels.Add(API.GetHashKey("prop_traffic_03b"));
            //trafficLightModels.Add(API.GetHashKey("prop_traffic_lightset_01"));

            allowedVehicles.Add(API.GetHashKey("police"));  // Example allowed vehicle models
            allowedVehicles.Add(API.GetHashKey("ambulance"));

            blacklistedVehicles.Add(API.GetHashKey("bus"));  // Example blacklisted vehicle models

            Debug.WriteLine("------------------------------------------");
            Debug.WriteLine($"Opticom Module started. {trafficLightModels.Count} traffic light models loaded.");
            Debug.WriteLine("------------------------------------------");

            Tick += OnTick;
        }

        private async Task OnTick()
        {
            await Delay(0);
            FindNextIntersection();
        }

        public async void DisplayNotification(string message, int durationInSeconds)
        {
            SetNotificationTextEntry("STRING");
            AddTextComponentString(message);
            int notificationHandle = DrawNotification(false, false);

            await Delay(durationInSeconds * 1000);

            RemoveNotification(notificationHandle);
        }

        private async void FindNextIntersection()
        {
            int playerPed = PlayerPedId();
            if (IsPedInAnyVehicle(playerPed, false))
            {
                int vehicle = GetVehiclePedIsIn(playerPed, false);

                if (allowedVehicles.Contains(GetEntityModel(vehicle)) && IsVehicleSirenOn(vehicle))
                {
                    Vector3 currentPlayerPos = GetEntityCoords(playerPed, true);
                    if (foundTrafficLight != 0 && (DateTime.Now - searchStopTime).TotalSeconds < 10)
                    {
                        Vector3 foundTrafficLightPos = GetEntityCoords(foundTrafficLight, true);
                        float distance = (foundTrafficLightPos - currentPlayerPos).Length();

                        if (distance <= 10.0f)
                        {
                            foundTrafficLight = 0;
                            RemoveBlip(ref trafficLightBlip);
                            DisplayNotification("Reached the traffic light!", 5);
                        }
                        else
                        {
                            await Delay(1000);
                            return;
                        }
                    }

                    Vector3 playerPos = GetEntityCoords(playerPed, true);
                    Vector3 forwardVector = GetEntityForwardVector(vehicle);
                    float maxDistance = 100.0f;
                    float coneAngle = 45.0f;

                    int nearestTrafficLight = 0;
                    float nearestDistance = maxDistance;

                    foreach (int trafficLightModel in trafficLightModels)
                    {
                        int trafficLight = GetClosestObjectOfType(playerPos.X, playerPos.Y, playerPos.Z, maxDistance, (uint)trafficLightModel, false, false, false);
                        if (trafficLight != 0)
                        {
                            Vector3 trafficLightPos = GetEntityCoords(trafficLight, true);
                            Vector3 directionToTrafficLight = trafficLightPos - playerPos;
                            float distance = directionToTrafficLight.Length();

                            float angle = Vector3.Dot(forwardVector, directionToTrafficLight) / (forwardVector.Length() * directionToTrafficLight.Length());
                            float degrees = (float)(Math.Acos(angle) * (180.0 / Math.PI));

                            if (degrees <= coneAngle && distance < nearestDistance)
                            {
                                nearestDistance = distance;
                                nearestTrafficLight = trafficLight;
                            }
                        }
                    }

                    if (nearestTrafficLight != 0)
                    {
                        foundTrafficLight = nearestTrafficLight;
                        searchStopTime = DateTime.Now;

                        trafficLightBlip = AddBlipForEntity(foundTrafficLight);
                        SetBlipSprite(trafficLightBlip, 1);  // Example sprite, change as needed
                        SetBlipColour(trafficLightBlip, 1);  // Example color, change as needed
                        BeginTextCommandSetBlipName("STRING");
                        AddTextComponentString("Traffic Light");
                        EndTextCommandSetBlipName(trafficLightBlip);

                        DisplayNotification("Traffic light found ahead!", 5);
                        // Additional logic for handling the traffic light can be placed here
                    }
                    else
                    {
                        DisplayNotification("No traffic light found ahead!", 5);
                    }
                }
                else
                {
                    //DisplayNotification("You are not in an emergency vehicle or the siren is off!", 5);
                }
            }
            else
            {
                DisplayNotification("You are not in a vehicle!", 5);
            }
        }
    }
}
*/