using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Serialization;


namespace POLSIM.Server
{
    public class DepartmentData
    {
        public string DeptNameFull { get; set; }
        public string DeptNameShort { get; set; }
        public string DeptLogo { get; set; }
        public List<Division> Divisions { get; set; }
        public List<Rank> Ranks { get; set; }
        public string BlipSprite { get; set; }
        public string BlipColour { get; set; }
        public List<Coordinate> StationBlips { get; set; }
        public List<Coordinate> LockerRooms { get; set; }
        public List<Weapon> AllowedWeapons { get; set; }
    }

    public class Division
    {
        public string Id { get; set; }  // Add this property
        public string Name { get; set; }
        public string DivisionIcon { get; set; }
    }

    public class Rank
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }

    public class Coordinate
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }

    public class Weapon
    {
        public string WeaponName { get; set; }
        public uint WeapHash { get; set; }
        public int Ammo { get; set; }
        public int MagSize { get; set; }
    }


    public class User
    {
        public string Username { get; set; }
        public string LicenseIdentifier { get; set; }
        public string DeptNameShort { get; set; }
        public string Rank { get; set; }
        public string Division { get; set; }
        public string Badge { get; set; }
        public int Xp { get; set; }
        public int Arrests { get; set; }
        public int Citations { get; set; }
    }



    public class DepartmentManager : BaseScript
    {
        string green = "\x1b[32m";
        string purple = "\x1b[35m";
        string resetColor = "\x1b[0m";
        private List<DepartmentData> deptData = new List<DepartmentData>();
        private List<User> userData = new List<User>();

        public DepartmentManager()
        {
            EventHandlers["playerSpawned2"] += new Action<Player>(OnPlayerSpawned);

            EventHandlers["serverEvent:SubmitLockerCoordinates"] += new Action<Player, dynamic>(OnSubmitLockerCoordinates);

            LoadDepartments();
            LoadUsers();
        }

        public DepartmentData RetrieveDepartmentData(string deptNameShort)
        {
            try
            {
                // Debug statement indicating that the method is being called
                Debug.WriteLine($"[{purple}Department Manager{resetColor}] Retrieving department data for department: {deptNameShort}...");

                // Attempt to find the department data matching the provided department name
                DepartmentData department = deptData.FirstOrDefault(d => d.DeptNameShort == deptNameShort);

                if (department != null)
                {
                    // Debug statement indicating that department data was found
                    Debug.WriteLine($"[{purple}Department Manager{resetColor}] Department data found for department: {deptNameShort}");
                }
                else
                {
                    // Debug statement indicating that department data was not found
                    Debug.WriteLine($"[{purple}Department Manager{resetColor}] Department data not found for department: {deptNameShort}");
                }
                // Return the retrieved department data
                return department;
            }
            catch (Exception ex)
            {
                // Debug statement indicating that an exception occurred
                Debug.WriteLine($"[{purple}Department Manager{resetColor}] An error occurred while retrieving department data: {ex.Message}");
                // Optionally, log the full exception details
                // Debug.WriteLine($"Exception details: {ex.ToString()}");

                // Return null in case of an exception
                return null;
            }
        }


        private void OnSubmitLockerCoordinates([FromSource] Player player, dynamic lockerCoordinates)
        {
            var lockerList = JsonConvert.DeserializeObject<List<Dictionary<string, float>>>(lockerCoordinates.ToString());
            // Call the existing method to process locker coordinates
            // OnReceiveLockerCoordinates(lockerList);
        }

        private void LoadDepartments()
        {
            try
            {
                string filePath = "departments.json";
                string resource = GetCurrentResourceName();

                if (resource != null && filePath != null)
                {
                    string fileContents = LoadResourceFile(resource, filePath);

                    if (fileContents != null)
                    {
                        var settings = new JsonSerializerSettings();
                        settings.Converters.Add(new Vector3Converter());

                        deptData = JsonConvert.DeserializeObject<List<DepartmentData>>(fileContents, settings);

                        int uniqueDeptCount = deptData.Select(d => d.DeptNameShort).Distinct().Count();
                        Debug.WriteLine($"[{purple}Department Manager{resetColor}] {uniqueDeptCount} agencies have been detected.");
                    }
                    else
                    {
                        Debug.WriteLine("FAIL: departments.json file contents null/file does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occurred while loading/processing the departments.json: {ex.Message}");
            }
        }

        private void LoadUsers()
        {
            try
            {
                string filePath = "users.json";
                string resource = GetCurrentResourceName();

                if (resource != null && filePath != null)
                {
                    string fileContents = LoadResourceFile(resource, filePath);

                    if (fileContents != null)
                    {
                        userData = JsonConvert.DeserializeObject<List<User>>(fileContents);

                        int userCount = userData.Count;
                        Debug.WriteLine($"[{purple}Department Manager{resetColor}] {userCount} whitelisted users detected.");
                    }
                    else
                    {
                        Debug.WriteLine("FAIL: users.json file contents null/file does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occurred while loading/processing the users.json: {ex.Message}");
            }
        }

        public class Vector3Converter : JsonConverter<Vector3>
        {
            public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
            {
                writer.WriteStartArray();
                writer.WriteValue(value.X);
                writer.WriteValue(value.Y);
                writer.WriteValue(value.Z);
                writer.WriteEndArray();
            }

            public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.StartArray)
                {
                    reader.Read();
                    float x = (float)reader.ReadAsDouble();
                    float y = (float)reader.ReadAsDouble();
                    float z = (float)reader.ReadAsDouble();
                    reader.Read();
                    return new Vector3(x, y, z);
                }
                throw new JsonSerializationException("Unexpected token type when parsing Vector3.");
            }
        }

        /*public void SubmitDepartmentData(Player player, DepartmentData departmentData)
        {
            if (departmentData == null)
            {
                Debug.WriteLine("Department data is null.");
                return;
            }

            var divisions = departmentData.Divisions?.Select(d => new
            {
                Name = d.Name,
                Icon = d.DivisionIcon
            }).ToList();

            var ranks = departmentData.Ranks?.Select((r, index) => new
            {
                RankName = $"Rank{index + 1}",
                RankTitle = r,
                Icon = $"Departments/{departmentData.DeptNameShort}/rank_icons/rank{index + 1}.png" // Adjust the path as needed
            }).ToList();

            var departmentDetails = new
            {
                DepartmentName = departmentData.DeptNameFull,
                DeptNameShort = departmentData.DeptNameShort,
                DeptLogo = departmentData.DeptLogo,
                Divisions = divisions,
                Ranks = ranks
            };

            string jsonDepartmentDetails = JsonConvert.SerializeObject(departmentDetails);

            TriggerClientEvent(player, "clientEvent:ReceiveDepartmentData", jsonDepartmentDetails);
        }*/





        public void OnPlayerSpawned([FromSource] Player player)
        {
            try
            {
                if (player == null)
                {
                    Debug.WriteLine("Player is null.");
                    return;
                }

                string license = player.Identifiers["license"];
                string pName = player.Name;

                Debug.WriteLine($"[{purple}Department Manager{resetColor}] Initializing: {pName}.");

                if (string.IsNullOrEmpty(license))
                {
                    Debug.WriteLine($"[{purple}Department Manager{resetColor}] Player license is null or empty.");
                    return;
                }

                if (userData == null)
                {
                    Debug.WriteLine("User data is null.");
                    return;
                }

                var user = userData.FirstOrDefault(u => u.LicenseIdentifier == license);
                if (user != null)
                {
                    string departmentName = user.DeptNameShort;
                    string userRankName = user.Rank;
                    string userDivision = user.Division;
                    string userRankId = user.Rank; // Using Rank ID
                    string userDivisionId = user.Division; // Using Division ID
                    string userBadge = user.Badge;

                    //Debug.WriteLine($"[{purple}Department Manager{resetColor}] User with {departmentName} as {userRankName} in division {userDivision}.");

                    DepartmentData department = RetrieveDepartmentData(departmentName);

                    if (department != null)
                    {
                        Debug.WriteLine($"[{green}DATA{resetColor}] Sending department data to {pName}.");
                        string departmentJson = JsonConvert.SerializeObject(department);
                        TriggerClientEvent(player, "nuiHandler_departmentData", departmentJson); //nuiHandler.lua
                        TriggerClientEvent(player, "departmentData", departmentJson); // client c#


                        // Here we retrieve the user's rank string by matching the user's rank ID with the department's rank IDs.
                        var matchingRank = department.Ranks.FirstOrDefault(r => r.Id == userRankId);
                        if (matchingRank != null)
                        {
                            //Debug.WriteLine($"User's rank: {matchingRank.Name} (ID: {matchingRank.Id})");
                        }
                        else
                        {
                            Debug.WriteLine("No matching rank found for the user.");
                        }

                        // Find matching division by ID
                        var matchingDivision = department.Divisions.FirstOrDefault(d => d.Id == userDivisionId);
                        if (matchingDivision != null)
                        {
                            //Debug.WriteLine($"User's division: {matchingDivision.Name} (ID: {matchingDivision.Id})");
                        }
                        else
                        {
                            Debug.WriteLine("No matching division found for the user.");
                        }

                        // pName (string)
                        // userBadge (string)
                        // matchingRank.Name (string)
                        // matchingDivision.Name (string)

                        var data = new
                        {
                            Dept = departmentName,
                            Name = pName,
                            Badge = userBadge,
                            Rank = matchingRank.Name,
                            Division = matchingDivision.Name,
                            Xp = user.Xp,
                            Arrests = user.Arrests,
                            Citations = user.Citations
                        };

                        Debug.WriteLine($"[{green}DATA{resetColor}] Sending profile to {pName} ({matchingRank.Name}/{matchingDivision.Name}).");

                        string jsonData = JsonConvert.SerializeObject(data);

                        TriggerClientEvent(player, "nuiHandler_updatePlayerProfile", jsonData);

                    }

                }
                else
                {
                    Debug.WriteLine($"Department data not found for department: {user.DeptNameShort}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                // Optionally, you can log the full exception details using ex.ToString()
            }
        }












        private void SubmitPlayerProfile(Player player, string deptNameFull, string deptNameShort, string rank, string division, string badge, string deptLogo)
        {
            var profileData = new
            {
                DeptNameFull = deptNameFull,
                DeptNameShort = deptNameShort,
                Rank = rank,
                Division = division,
                Badge = badge,
                DeptLogo = deptLogo
            };

            string jsonData = JsonConvert.SerializeObject(profileData);

            TriggerClientEvent(player, "serverEvent:SubmitPlayerProfile", jsonData);
        }

        private List<Dictionary<string, float>> RetrieveAgencyLockers(string deptNameShort)
        {
            Debug.WriteLine($"[{purple}Department Manager{resetColor}] Retrieving {deptNameShort} lockers...");

            var department = deptData.FirstOrDefault(d => d.DeptNameShort == deptNameShort);
            if (department != null)
            {
                var lockerRooms = new List<Dictionary<string, float>>();

                for (int i = 0; i < department.LockerRooms.Count; i++)
                {
                    var lockerRoom = department.LockerRooms[i];
                    if (lockerRoom != null)
                    {
                        var lockerRoomDict = new Dictionary<string, float>
                {
                    { "X", lockerRoom.X },
                    { "Y", lockerRoom.Y },
                    { "Z", lockerRoom.Z }
                };
                        lockerRooms.Add(lockerRoomDict);
                    }
                    else
                    {
                        Debug.WriteLine($"[{purple}Department Manager{resetColor}] No locker room found for index {i} in department: {deptNameShort}.");
                        break; // Exit loop if no further locker rooms exist
                    }
                }

                int lockerCount = lockerRooms.Count;
                if (lockerCount == 1)
                {
                    Debug.WriteLine($"[{purple}Department Manager{resetColor}] Sending 1 locker to client.");
                }
                else if (lockerCount > 1)
                {
                    Debug.WriteLine($"[{purple}Department Manager{resetColor}] Sending {lockerCount} lockers to client.");
                }
                else
                {
                    Debug.WriteLine($"[{purple}Department Manager{resetColor}] WARNING: No {deptNameShort} lockers found.");
                }

                return lockerRooms;
            }
            else
            {
                Debug.WriteLine($"[{purple}Department Manager{resetColor}] Department data not found for {deptNameShort}.");
                Debug.WriteLine($"Available departments: {string.Join(", ", deptData.Select(d => d.DeptNameShort))}");
                return new List<Dictionary<string, float>>();
            }
        }

    }
}