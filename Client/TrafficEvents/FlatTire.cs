using CitizenFX.Core;
using System.Threading.Tasks;
using System;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;

namespace POLSIM.Client
{
    internal class FlatTire
    {
        public FlatTire(Vehicle randomVehicle)
        {
            Debug.WriteLine("FlatTire() running.");
            SetTyreHealth(randomVehicle.Handle, 1, 0);
            Debug.WriteLine("FlatTire() completed.");
        }
    }
}