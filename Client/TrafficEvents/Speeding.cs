using CitizenFX.Core;
using System.Threading.Tasks;
using System;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;

namespace POLSIM.Client
{
    internal class Speeding
    {
        public Speeding(Vehicle randomVehicle)
        {
            //Random random = new Random();
            Debug.WriteLine("Speeding() running.");
            // Retrieve the driver Ped from the vehicle
            Ped driver = randomVehicle.GetPedOnSeat(VehicleSeat.Driver);
            if (driver != null)
            {
                Debug.WriteLine($"Driver found: {driver.Handle} assigning driving style.");

                SetEntityAsMissionEntity(driver.Handle, true, true);
                TaskSetBlockingOfNonTemporaryEvents(driver.Handle, true);

                SetDriveTaskDrivingStyle(driver.Handle, (int)DrivingStyle.Rushed);
            }
            else
            {
                Debug.WriteLine("No driver found. Aborting.");
            }
        }
    }
}

/*using CitizenFX.Core;
using System.Threading.Tasks;
using System;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;

namespace POLSIM.Client
{
    // This selects a random nearby NPC driver and changes its driving style to be more aggressive... driving trough stop signs, overtaking etc
    internal class Speeding
    {
        private float oldSpeed;
        //private float oldDriveInertia;
        //private float oldInitialDriveForce;
        private Vehicle car;
        private Ped driver;
        private bool eventRunning;
        private Blip driverBlip;

        public Speeding(Vehicle randomVehicle)
        {
            Debug.WriteLine("Speeding() invoked");
            car = randomVehicle;
            driver = car.Driver;
            oldSpeed = car.Speed;
            Debug.WriteLine($"oldSpeed: {oldSpeed} car.Speed: {car.Speed}");
            MainLogic();
        }
        protected async void MainLogic()
        {
            Debug.WriteLine("Waiting 1s...");
            await BaseScript.Delay(1000);
            Debug.WriteLine("Continue...");
            float newSpeed;
            eventRunning = true;

            if (oldSpeed >= 27f)
            {
                newSpeed = 55f;
                if (car.Speed <= 55f)
                {
                    car.Speed = 55f;
                }
            }
            else
            {
                newSpeed = oldSpeed * 2.0f;
                if (newSpeed < 18f) { newSpeed = 30.1f; }
                if (newSpeed > 40f) { newSpeed = 40f; }
            }

            Debug.WriteLine($"newSpeed: {newSpeed}");
            Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, driver.Handle);
            Function.Call(Hash.SET_PED_KEEP_TASK, driver.Handle, true);
            driver.Task.CruiseWithVehicle(car, newSpeed, (int)DrivingStyle.Rushed);
            //driver.BlockPermanentEvents = true;

            while (eventRunning)
            {
                DrawRect(0.991f, 0.009f, 0.028f, 0.028f, 0, 255, 0, 255);
                SetDriveTaskDrivingStyle(driver.Handle, (int)DrivingStyle.Rushed);
                await BaseScript.Delay(500);
            }
        }
    }
}*/