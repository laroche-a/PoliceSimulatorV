using CitizenFX.Core;
using System.Threading.Tasks;
using System;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;

namespace POLSIM.Client
{
    internal class NoBrakeLights
    {
        private Vehicle car;

        public NoBrakeLights(Vehicle randomVehicle)
        {
            Debug.WriteLine("NoBrakeLights() running.");
            car = randomVehicle;

            //SetEntityAsMissionEntity(car.Handle, true, true);

            SetVehicleBrakeLights(car.Handle, false);
        }
    }
}