using CitizenFX.Core;
using System.Threading.Tasks;
using System;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;

namespace POLSIM.Client
{
    internal class SmokingEngine
    {
        public SmokingEngine(Vehicle randomVehicle)
        {
            Random random = new Random();
            Debug.WriteLine("SmokingEngine() running.");

            // Picking a random engine health value and assigning it to the car
            int engineHealth = random.Next(100, 350);
            Debug.WriteLine($"SmokingEngine() engineHealth: {engineHealth}");
            randomVehicle.EngineHealth = engineHealth;
        }
    }
}