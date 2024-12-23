using CitizenFX.Core;
using System.Threading.Tasks;
using System;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;

namespace POLSIM.Client
{
    internal class RevEngineWhenStationary
    {
        private Vehicle car;
        private Ped driver;
        private bool eventRunning;

        public RevEngineWhenStationary(Vehicle randomVehicle)
        {
            Debug.WriteLine("RevEngineWhenStationary() running.");
        }
        protected async void MainLogic()
        {
            eventRunning = true;
            while (eventRunning)
            {
                DrawRect(0.991f, 0.009f, 0.028f, 0.028f, 0, 255, 0, 255);
                if (car.Speed < 2f)
                {

                    await BaseScript.Delay(3000);
                }
                await BaseScript.Delay(1);
            }
        }
    }
}