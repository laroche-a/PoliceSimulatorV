using CitizenFX.Core;
using System.Threading.Tasks;
using System;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;

namespace POLSIM.Client
{
    internal class UsingPhone
    {
        private Vehicle car;
        private Ped driver;
        private bool eventRunning;

        public UsingPhone(Vehicle randomVehicle)
        {
            Debug.WriteLine("UsingPhone() running.");
            car = randomVehicle;
            driver = car.Driver;
            MainLogic();
        }
        protected async void MainLogic()
        {
            eventRunning = true;
            while (eventRunning)
            {
                DrawRect(0.991f, 0.009f, 0.028f, 0.028f, 0, 255, 0, 255);

                TaskUseMobilePhoneTimed(driver.Handle, 100000);
                await BaseScript.Delay(100);
            }
        }
    }
}