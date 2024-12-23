using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using Newtonsoft.Json;
using static CitizenFX.Core.Native.API;

namespace POLSIM.Client
{

    public class PostalManager : BaseScript
    {
        private List<Postal> postals;

        private string nearestPostalCode = string.Empty; // Store the nearest postal code

        public string NearestPostalCode => nearestPostalCode; // Property to access nearest postal code

        public PostalManager()
        {
            // Load postals.json and initialize postals list
            LoadPostals();

            // Register the command handler
            RegisterCommand("postal", new Action<int, List<object>, string>(OnPostalCommand), false);
        }

        private void LoadPostals()
        {
            // Loading departments.json with error catching
            try
            {
                string filePath = "postals.json";
                string resource = GetCurrentResourceName();

                if (resource != null && filePath != null)
                {
                    string fileContents = LoadResourceFile(resource, filePath);

                    if (fileContents != null)
                    {
                        int fileSizeInBytes = fileContents.Length;
                        double fileSizeInKilobytes = fileSizeInBytes / 1024.0;
                        double roundedFileSizeInKilobytes = Math.Round(fileSizeInKilobytes, 2); // Round to 2 decimal places
                        Debug.WriteLine($"Loaded {filePath} Size: {roundedFileSizeInKilobytes}kB.");
                        this.postals = JsonConvert.DeserializeObject<List<Postal>>(fileContents);
                    }
                    else
                    {
                        Debug.WriteLine("File contents are null. Check if the file exists or if there was an issue loading it.");
                    }
                }
                else
                {
                    Debug.WriteLine("Resource name or file path is null. Check if they are properly initialized.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while loading/processing the file: {ex.Message}");
            }
        }

        public string GetNearestPostal(double playerX, double playerY)
        {
            if (postals == null || postals.Count == 0)
            {
                Debug.WriteLine("Postals list is not initialized or empty.");
                return string.Empty; // or handle the situation accordingly
            }

            double shortestDistance = double.MaxValue;
            string nearestPostalCode = "";

            foreach (Postal postal in postals)
            {
                double distance = CalculateDistance(playerX, playerY, postal.X, postal.Y);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestPostalCode = postal.Code;
                }
            }

            return nearestPostalCode;
        }

        private void OnPostalCommand(int source, List<object> args, string raw)
        {
            // Check if the command has the correct number of arguments
            if (args.Count != 1)
            {
                Debug.WriteLine("Usage: /postal [postal code]");
                return;
            }

            // Extract the postal code from the command arguments
            string postalCode = args[0].ToString();

            // Find the postal with the given postal code
            Postal postal = postals.Find(p => p.Code == postalCode);

            // Check if the postal code is valid
            if (postal == null)
            {
                Debug.WriteLine($"Postal code '{postalCode}' not found.");
                return;
            }

            // Set GPS waypoint to the postal location
            SetNewWaypoint((float)postal.X, (float)postal.Y);
        }

        private double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
    }
}
