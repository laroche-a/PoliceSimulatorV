using CitizenFX.Core;
using System.Threading.Tasks;
using System;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;

namespace POLSIM.Client
{
    internal class BrokenWindows
    {
        public BrokenWindows(Vehicle randomVehicle)
        {
            Random random = new Random();
            Debug.WriteLine("BrokenWindows() running.");

            int brokenWindowAmount = random.Next(1, 6);
            Debug.WriteLine($"BrokenWindows() brokenWindowAmount: {brokenWindowAmount}");
            for (int i = 1; i <= brokenWindowAmount; i++)
            {
                Debug.WriteLine($"BrokenWindows() SmashVehicleWindow for window id: {i}");
                SmashVehicleWindow(randomVehicle.Handle, i);
            }
        }
    }
}