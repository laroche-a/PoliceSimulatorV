using CitizenFX.Core;
using System.Threading.Tasks;
using System;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;

namespace POLSIM.Client
{
    internal class BrokenDownVehicle
    {
        private Vehicle car;
        public BrokenDownVehicle(Vehicle randomVehicle)
        {
            Debug.WriteLine("BrokenDownVehicle() running.");
            car = randomVehicle;
            car.EngineHealth = 0;
        }
    }
}