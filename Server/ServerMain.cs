/*
   ___  ____  __   __________  ___
  / _ \\/ __ \\/ /  / __/  _/  |/  /
 / ___/ /_/ / /___\\ \\_/ // /|_/ /
/_/   \\____/____/___/___/_/  /_/

 Police Simulator V
 ServerMain.cs
 FiveM ressource

 */

using CitizenFX.Core;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using SharedNamespace;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace SharedNamespace
{
    [Serializable]
    public class Identity
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public string race { get; set; }
        public string sex { get; set; }
        public string dob { get; set; }
        public string height { get; set; }
        public string address { get; set; }
        public string aptNumber { get; set; }
        public string phoneNumber { get; set; }

        public string driversLicenseNumber { get; set; }
        public string driversLicenseConfirmationNumber { get; set; }
        public bool isDriversLicenseValid { get; set; }
        public string driversLicenseReason { get; set; }
        public string driversLicenseIssue { get; set; }
        public string driversLicenseExp { get; set; }
        public string driversLicenseClass { get; set; }
        public bool isDriversLicenseCommercial { get; set; }
        public bool isDonor { get; set; }

        public bool hasFirearmLicense { get; set; }
        public bool isFirearmLicenseValid { get; set; }
        public string firearmLicenseReason { get; set; }
        public string firearmLicenseType { get; set; }

        public List<Event> events { get; set; }
        public List<Warrant> warrants { get; set; }
        public string registeredVehicle { get; set; }
    }

    public class ArrestReport
    {
        public int ArrestID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string OfficerName { get; set; }
        public string OfficerID { get; set; }
        public string UnitNumber { get; set; }
        public string SuspectName { get; set; }
        public string IdType { get; set; }
        public string IdNumber { get; set; }
        public string NarrativeText { get; set; }
    }
    public class Event
    {
        public string eventName { get; set; }
        public string eventDescription { get; set; }
        public int eventID { get; set; }
    }

    public class ServerSettings
    {
        public bool AutomaticDispatch { get; set; }
        public int MinimumIntervalTime { get; set; }
        public int MaximumIntervalTime { get; set; }
        public int DispatchPostalLowerLimit { get; set; }
        public int DispatchPostalUpperLimit { get; set; }
        public int MaximumCalloutPriority { get; set; }
        public bool TrafficEventsEnabled { get; set; }
        public int TrafficMinimumIntervalTime { get; set; }
        public int TrafficMaximumIntervalTime { get; set; }
        public float PursuitChance { get; set; }
        public float SuspectAggression { get; set; }
        public float FirearmChance { get; set; }

        public float MeleeChance { get; set; }
        public float FleeChance { get; set; }
    }

    public class Warrant
    {
        public string warrantType { get; set; }
        public string warrantTitle { get; set; }
        public string warrantDetails { get; set; }
        public int warrantID { get; set; }
    }
    public class CalloutData
    {
        public string Street { get; set; }
        public string Crossroad { get; set; }
        public int Postal { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }

    public class CalloutsTypeData
    {
        public int CalloutTypeID { get; set; }
        public string CalloutTypeString { get; set; }
        public List<string> CalloutComments { get; set; }
        public int Priority { get; set; }
        public int minActors { get; set; }
        public int maxActors { get; set; }
    }

    public class RecentCallout
    {
        public string Location { get; set; }
        public int Postal { get; set; }
        public string CalloutType { get; set; }
        public string Comment { get; set; }
        public int Priority { get; set; }
        public int ActorCount { get; set; }
        public DateTime DateTime { get; set; }
    }
}


namespace POLSIM.Server
{
    public class ServerMain : BaseScript
    {
        // MAIN SERVER-SIDE SETTINGS
        private static string version = "0.3.1"; // Script version

        private Random random;

        string green = "\x1b[32m";
        string red = "\x1b[31m";
        string blue = "\x1b[34m";
        string yellow = "\x1b[33m";
        string pink = "\x1b[35m";
        string resetColor = "\x1b[0m"; // ANSI escape code to reset color

        private static List<ServerSettings> _serverSettings;
        private static string resource = GetCurrentResourceName(); // Getting ressource name (for loading files)
        private List<Player> connectedPlayers = new List<Player>();
        int pedNetId = 0;





        // Callout System
        private List<CalloutData> callouts = new List<CalloutData>(); // This is for picking random coordinates + postal and streetname
        private List<CalloutsTypeData> calloutTypes = new List<CalloutsTypeData>(); // This is for picking the scenario details
        private List<RecentCallout> recentCallouts = new List<RecentCallout>(); // List of recent callouts for MDC ui
        List<int> pedNetIds = new List<int>(); // Stores pedNetIds during callouts so they can be sent to client
        List<int> pedList = new List<int>(); // Local ped entities
        int currentCalloutID = 0;
        bool hasCalloutBeenStartedOnClient = false;

        private int minInterval = 300000;
        private int maxInterval = 600000;
        private DateTime lastDebugPrintTime = DateTime.MinValue;

        private List<string> whitelistedIdentifiers = new List<string>();

        Vector3 outboundCoords = new Vector3();

        public ServerMain()
        {
            Debug.WriteLine("\u001b[94m   ___  ____  __   __________  ___\u001b[0m");
            Debug.WriteLine("\u001b[94m  / _ \\/ __ \\/ /  / __/  _/  |/  /\u001b[0m");
            Debug.WriteLine("\u001b[91m / ___/ /_/ / /___\\ \\_/ // /|_/ /\u001b[0m");
            Debug.WriteLine($"\u001b[91m/_/   \\____/____/___/___/_/  /_/  v. {version}\u001b[0m");
            Debug.WriteLine(" ");


            LoadWhitelistedIdentifiers();


            API.RegisterCommand("listplayers", new Action<int, List<object>, string>((source, args, raw) =>
            {
                ListPlayers();
            }), false);


            // Client Events
            EventHandlers["clientEvent:RequestRandomIdentity"] += new Action<int>(OnRequestRandomIdentity);

            EventHandlers["GetIdentityById"] += new Action<Player, int>(OnGetIdentityById);

            EventHandlers["SavePlayerPosition"] += new Action<Player, string>(SavePlayerPosition);
            EventHandlers["clientEvent:ClientIsNearCallout"] += new Action<bool, int>(ClientIsNearCallout);
            EventHandlers["SendPedNetIdsToClient"] += new Action<List<int>>(SendPedNetIdsToClient);

            EventHandlers["submitArrest"] += new Action<Player, dynamic>(SubmitArrest); // Request from nui.js to submit arrest report

            // Server Events
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            EventHandlers["playerDropped"] += new Action<Player, string>(OnPlayerDropped);
            EventHandlers["onResourceStart"] += new Action<string>(OnResourceStart);

            EventHandlers["sendToServer"] += new Action<Player, IDictionary<string, object>>(OnDataReceivedFromClient);

            EventHandlers["playerSpawned2"] += new Action<Player>(OnPlayerSpawned);

            API.RegisterCommand("randomcallout", new Action<int, List<object>, string>((source, args, raw) =>
            {
                // Call the GetRandomCallout method
                GetRandomCallout();
            }), false);


            resource = GetCurrentResourceName(); // Setting the ressource name to this (should be POLSIM.Server)

            Debug.WriteLine($"[{blue}DATA FILES{resetColor}]");

            // Loading coordinates.json with error catching
            try
            {
                string filePath = "coordinates.json";
                //string resource = GetCurrentResourceName();

                if (resource != null && filePath != null)
                {
                    string fileContents = LoadResourceFile(resource, filePath);

                    if (fileContents != null)
                    {
                        int fileSizeInBytes = fileContents.Length;
                        double fileSizeInKilobytes = fileSizeInBytes / 1024.0;
                        double roundedFileSizeInKilobytes = Math.Round(fileSizeInKilobytes, 2); // Round to 2 decimal places
                        Debug.WriteLine($"{green}OK:{resetColor} {filePath} found. Size: {roundedFileSizeInKilobytes}kB.");
                        callouts = JsonConvert.DeserializeObject<List<CalloutData>>(fileContents);
                    }
                    else
                    {
                        Debug.WriteLine($"{red}FAIL:{resetColor} coordinates.json file contents null/file does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occurred while loading/processing the settings.json: {ex.Message}");
            }

            // Loading callout-types.json with error catching
            try
            {
                string filePath = "callout-types.json";
                //string resource = GetCurrentResourceName();

                if (resource != null && filePath != null)
                {
                    string fileContents = LoadResourceFile(resource, filePath);

                    if (fileContents != null)
                    {
                        int fileSizeInBytes = fileContents.Length;
                        double fileSizeInKilobytes = fileSizeInBytes / 1024.0;
                        double roundedFileSizeInKilobytes = Math.Round(fileSizeInKilobytes, 2); // Round to 2 decimal places
                        Debug.WriteLine($"{green}OK:{resetColor} {filePath} found. Size: {roundedFileSizeInKilobytes}kB.");
                        calloutTypes = JsonConvert.DeserializeObject<List<CalloutsTypeData>>(fileContents);
                    }
                    else
                    {
                        Debug.WriteLine($"{red}FAIL:{resetColor} callout-types.json file contents null/file does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occurred while loading/processing the settings.json: {ex.Message}");
            }

            // Loading identities.json with error catching
            try
            {
                string filePath = "identities.json";
                //string resource = GetCurrentResourceName();

                if (resource != null && filePath != null)
                {
                    string fileContents = LoadResourceFile(resource, filePath);

                    if (fileContents != null)
                    {
                        int fileSizeInBytes = fileContents.Length;
                        double fileSizeInKilobytes = fileSizeInBytes / 1024.0;
                        double roundedFileSizeInKilobytes = Math.Round(fileSizeInKilobytes, 2); // Round to 2 decimal places
                        Debug.WriteLine($"{green}OK:{resetColor} {filePath} found. Size: {roundedFileSizeInKilobytes}kB.");

                    }
                    else
                    {
                        Debug.WriteLine($"{red}FAIL:{resetColor} identities.json file contents null/file does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occurred while loading/processing the identities.json: {ex.Message}");
            }

            try
            {
                string filePath = "server-settings.json";
                //string resource = GetCurrentResourceName();

                if (resource != null && filePath != null)
                {
                    string fileContents = LoadResourceFile(resource, filePath);

                    if (fileContents != null)
                    {
                        int fileSizeInBytes = fileContents.Length;
                        double fileSizeInKilobytes = fileSizeInBytes / 1024.0;
                        double roundedFileSizeInKilobytes = Math.Round(fileSizeInKilobytes, 2); // Round to 2 decimal places
                        Debug.WriteLine($"{green}OK:{resetColor} {filePath} found. Size: {roundedFileSizeInKilobytes}kB.");

                        _serverSettings = JsonConvert.DeserializeObject<List<ServerSettings>>(fileContents);

                        if (_serverSettings != null && _serverSettings.Count > 0)
                        {
                            foreach (var settings in _serverSettings)
                            {
                                Debug.WriteLine($"[{yellow}DISPATCHER SETTINGS{resetColor}]");
                                Debug.WriteLine($"Automatic Dispatcher Enabled: {settings.AutomaticDispatch}");
                                Debug.WriteLine($"Minimum Interval: {settings.MinimumIntervalTime} min.");
                                Debug.WriteLine($"Maximum Interval: {settings.MaximumIntervalTime} min.");
                                Debug.WriteLine($"Minimum  Dispatch Postal: {settings.DispatchPostalLowerLimit}");
                                Debug.WriteLine($"Maximum Dispatch Postal: {settings.DispatchPostalUpperLimit}");
                                Debug.WriteLine($"Maximum Callout Priority: {settings.MaximumCalloutPriority}");

                                Debug.WriteLine($"[{red}SUSPECT SETTINGS{resetColor}]");
                                Debug.WriteLine($"Base aggression: {settings.SuspectAggression * 100}%");
                                Debug.WriteLine($"Flee factor: {settings.FleeChance * 100}%");
                                Debug.WriteLine($"Chance to carry firearm: {settings.FirearmChance * 100}%");
                                Debug.WriteLine($"Chance to carry melee: {settings.MeleeChance * 100}%");

                                Debug.WriteLine($"[{green}TRAFFIC ENFORCEMENT SETTINGS{resetColor}]");
                                Debug.WriteLine($"Traffic Events Enabled: {settings.TrafficEventsEnabled}");
                                Debug.WriteLine($"Traffic Events Min. Interval: {settings.TrafficMinimumIntervalTime} min.");
                                Debug.WriteLine($"Traffic Events Max. Interval: {settings.TrafficMaximumIntervalTime} min.");
                                Debug.WriteLine($"Pursuit Chance: {settings.PursuitChance}%");

                                // If AutomaticDispatch is true, start waiting for the next callout
                                if (settings.AutomaticDispatch)
                                {
                                    string lightBlue = "\x1b[33m";
                                    string resetColor = "\x1b[0m";
                                    Debug.WriteLine($"[{lightBlue}Dispatcher{resetColor}] Automatic Dispatcher started.");
                                    Debug.WriteLine($"[{lightBlue}Dispatcher{resetColor}] There are {calloutTypes.Count} missions available for {callouts.Count} locations.");
                                    minInterval = settings.MinimumIntervalTime * 60000;
                                    maxInterval = settings.MaximumIntervalTime * 60000;
                                    Tick += WaitingToStartNextCallout;
    }
                            }
                        }
                        else
                        {
                            Debug.WriteLine($"{red}FAIL:{resetColor} Unable to deserialize server settings.");
                        }

                    }
                    else
                    {
                        Debug.WriteLine($"{red}FAIL:{resetColor} server-settings.json file contents null/file does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{red}FAIL:{resetColor} An exception occurred while loading/processing the server-settings.json: {ex.Message}");
            }

            Tick += WaitingForNearbyPlayer;
        }





        // Method to add a recent callout to the list and maintain only the 10 most recent callouts
        private void AddRecentCallout(RecentCallout callout)
        {
            callout.DateTime = DateTime.Now; // Set the current date and time
            recentCallouts.Insert(0, callout); // Add the new callout at the beginning of the list
            if (recentCallouts.Count > 10)
            {
                recentCallouts.RemoveAt(recentCallouts.Count - 1); // Remove the oldest callout if the list exceeds 10
            }
        }

        private string SerializeRecentCallouts()
        {
            // Serialize the recentCallouts list into JSON format
            string recentCalloutsJson = JsonConvert.SerializeObject(recentCallouts);

            return recentCalloutsJson;
        }

        // Method to send the updated dispatch page to clients
        private void SendUpdatedDispatchPage()
        {
            Debug.WriteLine("SendUpdatedDispatchPage");
            string recentCalloutsJson = SerializeRecentCallouts();
            TriggerClientEvent("addRecentCalloutEntry", recentCalloutsJson);

        }

        private void OnResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            Debug.WriteLine($"The resource {resourceName} has been started.");
        }

        // =================================================================================
        // WaitingToStartNextCallout()
        // Waits for the specified amount of time before initiating the callout
        // =================================================================================
        private async Task WaitingToStartNextCallout()
        {
            // Generate a random delay within the specified range
            int randomDelay = new Random().Next(minInterval, maxInterval);

            // Getting time in minutes and seconds
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(randomDelay);
            int minutes = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;

            string lightBlue = "\x1b[33m";
            string resetColor = "\x1b[0m";
            Debug.WriteLine($"[{lightBlue}Dispatcher{resetColor}] Next callout starting in {minutes:D2}:{seconds:D2}.");

            // Wait for the random delay before executing the function
            await Delay(randomDelay);

            // Call the function to select a random callout (coordinates, postal, street, etc)
            GetRandomCallout();

            // Schedule the next execution
            ScheduleNextExecution();
        }


        // =================================================================================
        // GetRandomCallout()
        // Retrieves a random callout from coordinates.json
        // =================================================================================

        private void GetRandomCallout()
        {
            string lightBlue = "\x1b[33m";
            string resetColor = "\x1b[0m";

            // Retrieve server settings
            int dispatchPostalLowerLimit = _serverSettings[0].DispatchPostalLowerLimit;
            int dispatchPostalUpperLimit = _serverSettings[0].DispatchPostalUpperLimit;
            int dispatchPriority = _serverSettings[0].MaximumCalloutPriority;

            Random random = new Random();

            // Filter callouts by postal code limits
            List<CalloutData> eligibleCallouts = callouts.Where(callout => callout.Postal >= dispatchPostalLowerLimit && callout.Postal <= dispatchPostalUpperLimit).ToList();

            if (eligibleCallouts.Count == 0)
            {
                Debug.WriteLine($"[{lightBlue}Dispatcher{resetColor}] No callouts found between postal codes {dispatchPostalLowerLimit} and {dispatchPostalUpperLimit}.");
                return;
            }

            // Filter callout types by priority
            List<CalloutsTypeData> eligibleCalloutTypes = calloutTypes.Where(calloutType => calloutType.Priority <= dispatchPriority).ToList();

            if (eligibleCalloutTypes.Count == 0)
            {
                Debug.WriteLine($"[{lightBlue}Dispatcher{resetColor}] No eligible callout types found with priority equal to or lower than {dispatchPriority}.");
                return;
            }

            // Select a random callout type from eligible ones
            CalloutsTypeData selectedCalloutType = eligibleCalloutTypes[random.Next(eligibleCalloutTypes.Count)];

            string selectedComment = selectedCalloutType.CalloutComments[random.Next(selectedCalloutType.CalloutComments.Count)]; // Pick a random dispatch comment

            // Pick a random number of actors between minActors and maxActors
            int randomActorCount = random.Next(selectedCalloutType.minActors, selectedCalloutType.maxActors + 1);

            // Select a random callout from eligible ones
            CalloutData selectedCallout = eligibleCallouts[random.Next(eligibleCallouts.Count)];


            Debug.WriteLine($"[{lightBlue}Dispatcher{resetColor}] Generated {selectedCalloutType.CalloutTypeString} at postal: {selectedCallout.Postal} with {randomActorCount} actor(s).");

            string location = $"{selectedCallout.Street} / {selectedCallout.Crossroad}"; // Combine Street and Crossroad into one string

            outboundCoords = new Vector3(selectedCallout.X, selectedCallout.Y, selectedCallout.Z);
            currentCalloutID = selectedCalloutType.CalloutTypeID;

            string timeTest = "23:59";

            // Send Dispatch Notification to clients
            SendDispatchDataToClients(timeTest, location, selectedCallout.Postal, selectedCalloutType.CalloutTypeString, selectedComment, selectedCalloutType.Priority);

            // Add the callout to RecentCallout list
            RecentCallout recentCallout = new RecentCallout
            {
                Location = location,
                Postal = selectedCallout.Postal,
                CalloutType = selectedCalloutType.CalloutTypeString,
                Comment = selectedComment,
                Priority = selectedCalloutType.Priority,
                ActorCount = randomActorCount
            };
            AddRecentCallout(recentCallout);

            // Spawn the server-side actors
            SpawnMissionActors(randomActorCount, selectedCallout.X, selectedCallout.Y, selectedCallout.Z);


            SendUpdatedDispatchPage();
        }


        // =================================================================================
        // SendDispatchDataToClients()
        // Sends callout dispatch info to all clients
        // =================================================================================
        public void SendDispatchDataToClients(string currentTime, string locationString, int postalString, string calloutTypeString, string commentString, int calloutPriority)
        {
            // Trigger event to send data to all clients along with current time
            TriggerClientEvent("dispatchData", currentTime, locationString, postalString, calloutTypeString, commentString, calloutPriority);
            
        }

        // =================================================================================
        // SpawnMissionActors()
        // Spawns mission entities on the server side
        // =================================================================================
        public void SpawnMissionActors(int actorCount, float X, float Y, float Z)
        {
            string lightBlue = "\x1b[33m";
            string resetColor = "\x1b[0m";
            Debug.WriteLine($"[{lightBlue}Dispatcher{resetColor}] Spawning {actorCount} actors.");

            // Clear previously spawned peds
            ClearSpawnedPeds();

            float offsetRange = 0.5f;

            List<uint> pedHashUint = new List<uint>
    {
        // FEMALES
        808859815,
        3188223741,
        2688103263,
        // MALES
        1423699487,
        2992445106,
        803106487,

        // more
        1224306523,
        1240094341,
        1264851357,
        1268862154,
        1328415626,
        1346941736,
        1426951581
    };


            Random random = new Random();

            for (int i = 0; i < actorCount; i++)
            {
                // Calculate offset for X, Y, and Z positions
                float offsetX = (float)(random.NextDouble() * 2 * offsetRange - offsetRange);
                float offsetY = (float)(random.NextDouble() * 2 * offsetRange - offsetRange);
                float offsetZ = (float)(random.NextDouble() * 2 * offsetRange - offsetRange);

                // Randomly pick a ped hash from the list
                uint pedHash = pedHashUint[random.Next(pedHashUint.Count)];

                // Spawn the ped
                int ped = CreatePed(0, pedHash, X + offsetX, Y + offsetY, Z + offsetZ, 180f, true, true);

                pedNetId = NetworkGetNetworkIdFromEntity(ped);

                //Debug.WriteLine($"Adding pedNetId to list: {pedNetId}");

                // Add the pedNetId to the list
                pedNetIds.Add(pedNetId);
                // Add the local ped entity to the list
                pedList.Add(ped);
            }
        }

        // Method to clear previously spawned peds
        private void ClearSpawnedPeds()
        {
            string lightBlue = "\x1b[33m";
            string resetColor = "\x1b[0m";
            Debug.WriteLine($"[{lightBlue}Dispatcher{resetColor}] Clearing previously spawned peds.");
            for (int i = 0; i < pedList.Count; i++)
            {
                int ped = pedList[i];
                // Delete the ped entity
                DeleteEntity(ped);
            }

            // Clear the ped lists
            pedList.Clear();
            pedNetIds.Clear();
        }


        // Method to serialize the List<int> to JSON string
        private string SerializePedNetIds(List<int> pedNetIds)
        {
            return JsonConvert.SerializeObject(pedNetIds);
        }

        private void SendPedNetIdsToClient(List<int> pedNetIds)
        {
            string serializedData = SerializePedNetIds(pedNetIds);
            TriggerClientEvent("ReceivePedNetIds", serializedData);
        }

        // =================================================================================
        // ScheduleNextExecution()
        // Schedules the next callout
        // =================================================================================
        private void ScheduleNextExecution()
        {
            Tick += WaitingToStartNextCallout;
        }

        // =================================================================================
        // WaitingForNearbyPlayer()
        // Triggers an event on all clients every specified amount of time to check if any is near a callout location
        // =================================================================================
        private async Task WaitingForNearbyPlayer()
        {
            while(!hasCalloutBeenStartedOnClient && currentCalloutID > 0) // Run this code until a client sends nearby notification
            {
                if (outboundCoords != Vector3.Zero && pedNetIds != null && pedNetIds.Any())
                {
                    TimeSpan timeSinceLastPrint = DateTime.Now - lastDebugPrintTime;
                    if (timeSinceLastPrint.TotalMinutes >= 1)
                    {
                        Debug.WriteLine($"Searching for clients at: {outboundCoords}.");
                        lastDebugPrintTime = DateTime.Now;
                    }
                    TriggerClientEvent("outboundCoordsUpdate", outboundCoords);
                    await Delay(3000);
                }
                else
                {
                    // Handle the case where either outboundCoords is empty or pedNetIds is null or empty
                    Debug.WriteLine("outboundCoords or pedNetIds is empty or null. (Actors not yet spawned)");
                    await Delay(5000);
                }
            }
        }

        // =================================================================================
        // ClientIsNearCallout()
        // Runs some code when a client has been detected near a callout
        // =================================================================================
        private async void ClientIsNearCallout(bool isNear, int pedNetId)
        {
            if (isNear)
            {
                Debug.WriteLine($"ClientIsNearCallout() - triggered by network ID: {pedNetId} - mission ID: {currentCalloutID}");
                hasCalloutBeenStartedOnClient = true;

                int triggerPlayerEntity = NetworkGetEntityFromNetworkId(pedNetId);

                // int pedNetId is the ID of the player who triggered the callout
                // Apply code to each element in pedList using foreach loop

                // weapons
                // 137902532u vint pistol
                // 324215364u // microsmg
                // 453432689u // pistol
                // 487013001u // shotgun
                // 615608432u // molotov


                foreach (int ped in pedList)
                {
                    Debug.WriteLine($"Processing ped: {ped}");
                    Random random = new Random();

                    List<uint> weaponHashes = new List<uint>
                    {
                        137902532,    // vint pistol
                        324215364,    // microsmg
                        453432689,    // pistol
                        487013001,    // shotgun
                        615608432     // molotov
                    };

                    switch (currentCalloutID)
                    {
                        case 1: // PERSON OBSERVED WITH A FIREARM

                            Debug.WriteLine($"Running mission: {currentCalloutID}");

                            int randomIndex = random.Next(weaponHashes.Count);
                            uint selectedWeaponHash = weaponHashes[randomIndex];

                            GiveWeaponToPed(ped, selectedWeaponHash, 500, false, true);
                            await Delay(50);
                            TaskCombatPed(ped, triggerPlayerEntity, 0, 16);

                            //AddBlipForEntity(ped); //debug

                            break;
                        case 2: // MULTIPLE PERSONS OBSERVED WITH A FIREARM
                            Debug.WriteLine($"Running mission: {currentCalloutID}");

                            int randomIndex2 = random.Next(weaponHashes.Count);
                            uint selectedWeaponHash2 = weaponHashes[randomIndex2];

                            GiveWeaponToPed(ped, selectedWeaponHash2, 500, false, true);
                            await Delay(50);
                            TaskCombatPed(ped, triggerPlayerEntity, 0, 16);

                            AddBlipForEntity(ped); //debug

                            break;
                        case 3: // ARREST WARRANT
                            Debug.WriteLine($"Running mission: {currentCalloutID}");

                            foreach (int currentPed in pedList) // Renamed loop variable to avoid conflict
                            {
                                Debug.WriteLine($"Processing ped: {currentPed}");
                                AddBlipForEntity(currentPed); //debug
                                TaskReactAndFleePed(currentPed, pedNetId);

                            }
                            break;

                        case 4: // CARJACKING
                            break;

                        case 5: // physical fight
                            break;

                        default:
                            Debug.WriteLine($"Error running mission code. Callout ID was: {currentCalloutID}");
                            // Handle logic for unknown callout IDs
                            break;
                    }
                }



                /*
                Debug.WriteLine("Sending PedNetIds to client...");
                SendPedNetIdsToClient(pedNetIds);
                */
            }
        }

        private void LoadWhitelistedIdentifiers()
        {
            string identifiersString = GetConvar("sv_polsim_admins", "");
            if (!string.IsNullOrWhiteSpace(identifiersString))
            {
                string[] identifiers = identifiersString.Split(',');
                foreach (string identifier in identifiers)
                {
                    whitelistedIdentifiers.Add(identifier);
                }
                // Output the count of loaded admins
                int adminCount = identifiers.Length;

                Debug.WriteLine($"[{red}ADMIN{resetColor}]");
                Debug.WriteLine($"{adminCount} whitelisted admins loaded.{resetColor}");
            }
            else
            {
                Debug.WriteLine($"[{red}Admin{resetColor}] {yellow}WARNING:{resetColor} No admins loaded.{resetColor}");
            }
        }

        private bool IsWhitelisted(string license)
        {
            return whitelistedIdentifiers.Contains("license:" + license);
        }

        // =================================================================================
        // OnPlayerConnecting()
        // Runs code when a player connects to the server
        // =================================================================================
        private void OnPlayerConnecting([FromSource] Player player, string playerName, dynamic setKickReason, dynamic deferrals)
        {
            string license = player.Identifiers["license"];
            if (IsWhitelisted(license))
            {

                Debug.WriteLine($"[{green}CONNECTING{resetColor}] Administrator {playerName} connected. ({license}){resetColor}");
                TriggerClientEvent(player, "setPlayerAsAdmin");
            }
            else
            {
                // Do nothing
            }
        }

        public void OnPlayerSpawned([FromSource] Player player)
        {
            string license = player.Identifiers["license"];
            if (IsWhitelisted(license))
            {

                Debug.WriteLine($"[{red}ADMIN{resetColor}] {player.Name} granted admin access.{resetColor}");
                TriggerClientEvent(player, "setPlayerAsAdmin");
            }
            else
            {
                // Do nothing
            }
        }

        private void ListPlayers()
        {
            foreach (var player in Players)
            {
                string playerId = player.ToString();
                string playerName = player.Name;
                string license = player.Identifiers["license"];
                Debug.WriteLine($"Player ID: {playerId}, Name: {playerName}, License: {license}");
            }
        }


        // =================================================================================
        // OnPlayerDropped()
        // Runs code when a player leaves the server
        // =================================================================================
        private void OnPlayerDropped([FromSource] Player player, string reason)
        {
            Debug.WriteLine($"Player {player.Name} disconnected.");
            connectedPlayers.Remove(player); // Removing from list of connected players
        }

        // Event handler for SavePlayerPosition
        private void SavePlayerPosition([FromSource] Player player, string jsonString)
        {
            // Decode the JSON string received from the client
            dynamic positionData = JObject.Parse(jsonString);

            // Extract relevant data
            _ = positionData.street;
            _ = positionData.crossroad;
            _ = positionData.postal;
            _ = positionData.X;
            _ = positionData.Y;
            _ = positionData.Z;

            // Write JSON string to a file
            string filePath = "debug.txt";
            File.AppendAllText(filePath, jsonString + ",");
        }


        private void OnDataReceivedFromClient([FromSource] Player player, IDictionary<string, object> data)
        {
            string clientIP = GetPlayerIP(player);

            if (data.ContainsKey("type"))
            {
                string type = data["type"].ToString();
                Debug.WriteLine($"[{green}DATA{resetColor}] Request of type '{type}' received from: {clientIP}");

                switch (type)
                {
                    case "nameCheck":
                        HandleNameCheckRequest(player, data);
                        break;

                    case "arrestListUpdate":
                        HandleArrestListUpdateRequest(player);
                        break;

                    default:
                        Debug.WriteLine($"[{green}DATA{resetColor}] Invalid request type: {type}");
                        break;
                }
            }
            else
            {
                Debug.WriteLine($"[{green}DATA{resetColor}] 'type' key not found in data.");
            }
        }

        private void HandleNameCheckRequest(Player player, IDictionary<string, object> data)
        {
            if (data.ContainsKey("firstName") && data.ContainsKey("lastName"))
            {
                string firstName = data["firstName"].ToString();
                string lastName = data["lastName"].ToString();

                Identity matchedIdentity = FindIdentity(firstName, lastName, resource);

                string response = matchedIdentity != null ? JsonConvert.SerializeObject(matchedIdentity) : JsonConvert.SerializeObject(new { identityNotFound = true });

                Debug.WriteLine($"[{green}DATA{resetColor}] Sending name check response to player.");

                TriggerClientEvent(player, "nameCheckResponse", response);
            }
            else
            {
                Debug.WriteLine($"[{green}DATA{resetColor}] Missing firstName or lastName in name check request.");
            }
        }

        private void HandleArrestListUpdateRequest(Player player)
        {
            Debug.WriteLine($"[{green}DATA{resetColor}] Arrest list update request received.");

            string arrests = "arrests.json";
            string jsonData = LoadResourceFile(resource, arrests);

            // Create an object to represent the response
            var response2 = new { dataType = "arrests", data = jsonData };

            // Serialize the response object to JSON
            string jsonResponse = JsonConvert.SerializeObject(response2);

            TriggerClientEvent(player, "arrestListUpdateResponse", jsonResponse);
        }

        private string GetPlayerIP(Player player)
        {
            foreach (var identifier in player.Identifiers)
            {
                if (identifier.StartsWith("ip:"))
                {
                    return identifier.Substring(3); // Remove "ip:" prefix
                }
            }
            return "Unknown IP";
        }

  
        // Event handler for the "GetIdentityById" event
        private void OnGetIdentityById([FromSource] Player player, int identityId)
        {
            // Call the method to retrieve the identity by its ID
            Identity requestedIdentity = GetIdentityById(identityId);

            if (requestedIdentity != null)
            {
                // Convert the Identity object to JSON string
                string jsonIdentity = JsonConvert.SerializeObject(requestedIdentity);
                // Trigger an event on the client side with the random identity data
                TriggerClientEvent("serverEvent:RandomIdentityResponse", jsonIdentity);

            }
            else
            {
                // If no identity found, inform the client
                Debug.WriteLine($"No identity found with ID: {identityId}");
                //TriggerClientEvent(player, "clientEvent:IdentityNotFound", identityId);
            }
        }

        // =================================================================================
        // GetIdentityById()
        // Retrieves an identity from identities.json by ID
        // =================================================================================
        public Identity GetIdentityById(int identityId)
        {
            string filePath = "identities.json"; // Update with the correct file path

            if (resource != null)
            {
                string jsonData = LoadResourceFile(resource, filePath);

                if (jsonData != null)
                {
                    try
                    {
                        List<Identity> identities = JsonConvert.DeserializeObject<List<Identity>>(jsonData);

                        // Find the identity with the specified ID
                        Identity requestedIdentity = identities.FirstOrDefault(identity => identity.id == identityId);

                        if (requestedIdentity != null)
                        {
                            return requestedIdentity;
                        }
                        else
                        {
                            Debug.WriteLine($"No identity found with ID: {identityId}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error deserializing JSON: {ex.Message}");
                        // Handle the exception as needed
                    }
                }
                else
                {
                    Debug.WriteLine("Failed to load JSON file or file contents are null.");
                    // Handle the case where jsonData is null
                }
            }
            else
            {
                Debug.WriteLine("Resource name is null.");
                // Handle the case where the resource name is null
            }

            return null; // Return null if there's an issue with reading the file or parsing JSON
        }


        // =================================================================================
        // FindIdentity()
        // Searches and finds matching identity from a received firstname and lastname
        // =================================================================================
        private static Identity FindIdentity(string firstName, string lastName, string resource)
        {
            string filePath = "identities.json"; // Replace with the actual file path

            try
            {
                string jsonData = LoadResourceFile(resource, filePath);

                if (jsonData != null)
                {
                    try
                    {
                        List<Identity> identities = JsonConvert.DeserializeObject<List<Identity>>(jsonData);

                        // Find the matching identity
                        Identity matchedIdentity = identities.FirstOrDefault(
                            identity => identity.firstName == firstName && identity.lastName == lastName
                        );

                        return matchedIdentity;
                    }
                    catch (JsonSerializationException ex)
                    {
                        Debug.WriteLine($"Error deserializing JSON: {ex.Message}");
                        // Handle the error specifically for JSON issues
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error processing JSON data: {ex.Message}");
                        // Handle other unexpected errors
                    }
                }
                else
                {
                    Debug.WriteLine("Failed to load JSON file or file contents are null.");
                    // Handle the case where jsonData is null
                }
            }
            catch (FileNotFoundException ex)
            {
                Debug.WriteLine($"Error: File not found: {ex.Message}");
                // Handle the case where the file is missing
            }
            catch (IOException ex)
            {
                Debug.WriteLine($"Error: I/O error: {ex.Message}");
                // Handle other I/O errors
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unexpected error: {ex.Message}");
                // Handle other unexpected errors
            }

            return null; // Return null if no identity is found or there are errors
        }

        // =================================================================================
        // OnRequestRandomIdentity()
        // Requests a random identity from identities.json with the specified gender
        // =================================================================================
        private void OnRequestRandomIdentity(int requestedGender)
        {
            // Your logic to find a random identity based on the requested gender
            Identity randomIdentity = FindRandomIdentity(requestedGender);

            Debug.WriteLine($"Random identity requested. Given ID: {randomIdentity.id}");

            // Convert the Identity object to JSON string
            string jsonIdentity = JsonConvert.SerializeObject(randomIdentity);

            //Debug.WriteLine($"Data: {jsonIdentity}");

            // Trigger an event on the client side with the random identity data
            TriggerClientEvent("serverEvent:RandomIdentityResponse", jsonIdentity);

        }

        public Identity FindRandomIdentity(int requestedGender)
        {
            string filePath = "identities.json"; // Update with the correct file path

            if (resource != null)
            {
                string jsonData = LoadResourceFile(resource, filePath);

                if (jsonData != null)
                {
                    try
                    {
                        List<Identity> identities = JsonConvert.DeserializeObject<List<Identity>>(jsonData);

                        // Filter identities based on the requested gender
                        List<Identity> filteredIdentities = identities
                            .Where(identity => requestedGender == 0 ? identity.sex.ToLower() == "f" : identity.sex.ToLower() == "m")
                            .ToList();

                        // Check if there are any matching identities
                        if (filteredIdentities.Count > 0)
                        {
                            // Randomly pick an identity from the filtered list
                            Random random = new Random();
                            int randomIndex = random.Next(0, filteredIdentities.Count);

                            return filteredIdentities[randomIndex];
                        }
                        else
                        {
                            Debug.WriteLine($"No identities found for the requested gender: {requestedGender}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error deserializing JSON: {ex.Message}");
                        // Handle the exception as needed
                    }
                }
                else
                {
                    Debug.WriteLine("Failed to load JSON file or file contents are null.");
                    // Handle the case where jsonData is null
                }
            }
            else
            {
                Debug.WriteLine("Resource name is null.");
                // Handle the case where the resource name is null
            }

            return null; // Return null if there's an issue with reading the file or parsing JSON
        }


        private void SubmitArrest([FromSource] Player source, dynamic formData)
        {
            // Extract form data
            string officerName = formData["officerName"];
            string officerID = formData["officerID"];
            string unitNumber = formData["unitNumber"];
            string suspectName = formData["suspectName"];
            string identifiedWith = formData["identifiedWith"];
            string identificationNumber = formData["identificationNumber"];
            string officersReport = formData["officersReport"];

            // Create an object or dictionary with the data to add to arrests.json
            _ = new
            {
                OfficerName = officerName,
                OfficerID = officerID,
                UnitNumber = unitNumber,
                SuspectName = suspectName,
                IdentifiedWith = identifiedWith,
                IdentificationNumber = identificationNumber,
                OfficersReport = officersReport
            };

            // Add the data to arrests.json (You need to implement this part)
            // For example:
            // YourFileHandlingClass.AddArrestData(arrestData);

            // Respond to the client
            source.TriggerEvent("arrestSubmitted", "Arrest report submitted successfully.");
        }


    }
}
