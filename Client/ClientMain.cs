/*
   ___  ____  __   __________  ___
  / _ \\/ __ \\/ /  / __/  _/  |/  /
 / ___/ /_/ / /___\\ \\_/ // /|_/ /
/_/   \\____/____/___/___/_/  /_/

 Police Simulator V
 ClientMain.cs
 FiveM ressource

 */

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.Native;
using LemonUI;
using LemonUI.Menus;
using Newtonsoft.Json;
using LemonUI.TimerBars;
using SharedNamespace;
using System.Threading;

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

    public class Event
    {
        public string eventName { get; set; }
        public string eventDescription { get; set; }
        public int eventID { get; set; }
    }

    public class Warrant
    {
        public string warrantType { get; set; }
        public string warrantTitle { get; set; }
        public string warrantDetails { get; set; }
        public int warrantID { get; set; }
    }
}


public struct IdentityPedTexture
{
    public int Handle { get; set; }
    public string TextureString { get; set; }
}

class UniformData
{
    public string Name { get; set; }
    public string Gender { get; set; }
    public string Category { get; set; }
    public string Category2 { get; set; }
    public string Hat { get; set; }
    public string Glasses { get; set; }
    public string Ear { get; set; }
    public string Watch { get; set; }
    public string Mask { get; set; }
    public string Top { get; set; }
    public string UpperSkin { get; set; }
    public string Decal { get; set; }
    public string UnderCoat { get; set; }
    public string Pants { get; set; }
    public string Shoes { get; set; }
    public string Accessories { get; set; }
    public string Armor { get; set; }
    public string Parachute { get; set; }
}


public class StationBlip
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
}

public class LockerRoom
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
}

public class Postal
{
    public double X { get; set; }
    public double Y { get; set; }
    public string Code { get; set; }
}

public class CalloutData
{
    public string Street { get; set; }
    public string Crossroad { get; set; }
    public string Postal { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
}

public class Jail
{
    public string owner { get; set; }
    public string textTag { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
}

public class Item
{
    public string Name { get; set; }
    public string Size { get; set; }
    public string Description { get; set; }
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

public class PlayerProfileData
{
    public string DeptNameFull { get; set; }
    public string DeptNameShort { get; set; }
    public string Rank { get; set; }
    public string Username { get; set; }
    public string LicenseIdentifier { get; set; }
    public string Division { get; set; }
    public string Badge { get; set; }
    public string DeptLogo { get; set; }
}

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



// PedMenu NPC Interaction Data
public class NPCData
{
    public int ID { get; set; }
    public int IsCuffedID { get; set; }
    public bool IsDetained { get; set; }

    public NPCData(int id, int isCuffedID, bool isDetained)
    {
        ID = id;
        IsCuffedID = isCuffedID;
        IsDetained = isDetained;
    }
}

public class SuspectStatus
{
    public bool IsArmed { get; set; }
    public bool HasSurrendered { get; set; }
    public bool HasKneeled { get; set; }

    public SuspectStatus(bool isArmed, bool hasSurrendered, bool hasKneeled)
    {
        IsArmed = isArmed;
        HasSurrendered = hasSurrendered;
        HasKneeled = hasKneeled;
    }
}


namespace POLSIM.Client
{
    public class ClientMain : BaseScript
    {
        private static string version = "0.3.1";
        private static List<ServerSettings> _serverSettings;
        //private static Dictionary<int, bool> npcArmedStatus = new Dictionary<int, bool>();
        private static Dictionary<int, SuspectStatus> npcStatus = new Dictionary<int, SuspectStatus>();

        //private int selectedOption = 0;
        private string[] options = { "Detain", "Ask for ID", "Search", "Handcuff>", "Drag", "Follow Me", "Put In>", "Remove From Vehicle", "Arrest"  };

        private static Random random = new Random();
        public string nearestPostalCode = "ERR";
        public string streetName = "ERR";
        public string crossRoadName = "ERR";
        public string generatedName = "JOHN DOE";
        public bool isNuiFocused = false;
        public bool isMDCDisplaying = false;
        public bool isLicenseDisplayed = false;
        public int cuffs;

        // Define variables outside the loop
        int startIndex = 0;
        int selectedOption = 0;
        const int optionsToShow = 3;

        // move over system
        private const string INVISIBLE_OBJECT_MODEL = "prop_mp_cone_01"; // Example model, replace with a truly invisible model if available
        private int invisibleObjectHandle;
        private Vector3 objectPosition = new Vector3(0, 0, 0); // Set initial position

        private TimerBarProgress timerBar;
        private TimerBarCollection timerBarCollection;
        private bool isEKeyHeld;
        private DateTime keyHoldStartTime;
        private Dictionary<int, float> adjustedVehicles = new Dictionary<int, float>();

        Vehicle towtruckTarget = null;
        Vehicle vehicleMenuTarget = null;

        private readonly Dictionary<Vector3, string> _staticTexts = new Dictionary<Vector3, string>();

        string registeredVehicle = null;
        string registrationFirstName = null;
        string registrationLastName = null;
        string registrationID = null;

        int pedIdentity = -1; // Traffic Stop driver
        int pedIdentityPedInteractionMenu = -1;

        private DateTime last3DTextUpdateTime = DateTime.Now;
        private DateTime lastPedUpdateTime = DateTime.Now;
        private const int textUpdateInterval = 5; // in milliseconds
        private const int pedUpdateInterval = 100; // in milliseconds

        private List<Jail> jails; // Loading jails from .json
        private List<CalloutData> calloutblips; // Loading jails from .json

        private List<Blip> lockerBlips = new List<Blip>();
        private List<Blip> jailBlips = new List<Blip>();
        private List<Blip> calloutDebugBlips = new List<Blip>();

        private List<Item> items; // Loading items from .json
        private Dictionary<int, List<Item>> searchedPedItems; // For keeping track of searched peds and their items
        private Dictionary<int, List<Item>> searchedVehicleItems;

        private bool lockerRoomActive = false;
        private Camera lockerRoomCam;
        List<Vector3> lockerRoomCoordinates = new List<Vector3>(); // list of locker room coordinates, received from server. only contains players own agency

        private static Dictionary<string, string> requests = new Dictionary<string, string>();

        private bool isHoldingE = false;
        private bool progressCompleted = false;
        private DateTime startTime;


        // List of assault rifles hashs, used for locker room animations
        private List<uint> assaultRifles = new List<uint>
    {
        (uint)WeaponHash.AssaultRifle,
        (uint)WeaponHash.CarbineRifle,
        (uint)WeaponHash.AdvancedRifle,
        (uint)WeaponHash.SpecialCarbine,
        (uint)WeaponHash.BullpupRifle,
        (uint)WeaponHash.CompactRifle
    };

        // List of assault rifles hashs, used for locker room animations
        private List<uint> pistols = new List<uint>
    {
        (uint)WeaponHash.Pistol,
        (uint)WeaponHash.Pistol50,
        (uint)WeaponHash.SNSPistol,
        (uint)WeaponHash.PistolMk2,
        (uint)WeaponHash.Revolver,
        (uint)WeaponHash.RevolverMk2,
        (uint)WeaponHash.SNSPistol,
        (uint)WeaponHash.SNSPistolMk2,
        (uint)WeaponHash.StunGun,
        (uint)WeaponHash.VintagePistol
    };



        //List<int> activeBlips = new List<int>(); // List 

        private bool IsPlayerOnDuty()
        {
            return onDuty;
        }

        // Defining traffic event probabilities (percentage)
        Dictionary<string, double> eventProbabilities = new Dictionary<string, double> {
            { "BrokenDownVehicle", 0.10 }, // 10% chance
            { "BrokenWindows", 0.20 },      // 20% chance
            { "FlatTire", 0.20 }, // 20% chance
            { "SmokingEngine", 0.20 }, // 15% chance
            { "Speeding", 0.25 }};      // 25% chance

        Dictionary<string, uint> PedHashes = new Dictionary<string, uint>() {
            { "a_c_crow", 0x18012A9F},
            { "a_c_deer", 0xD86B5A95 },
            { "a_c_dolphin", 0x8BBAB455 },
            { "a_c_fish", 0x2FD800B7 },
            { "a_c_hen", 0x6AF51FAF },
            { "a_c_humpback", 0x471BE4B2 },
            { "a_c_husky", 0x4E8F95A2 },
            { "a_c_killerwhale", 0x8D8AC8B9 },
            { "a_c_mtlion", 0x1250D7BA },
            { "a_c_pig", 0xB11BAB56 },
            { "a_c_pigeon", 0x06A20728 },
            { "a_c_poodle", 0x431D501C },
            { "a_c_pug", 0x6D362854 },
            { "a_c_rabbit_01", 0xDFB55C81 },
            { "a_c_rat", 0xC3B52966 },
            { "a_c_retriever", 0x349F33E1 },
            { "a_c_rhesus", 0xC2D06F53 },
            { "a_c_rottweiler", 0x9563221D },
            { "a_c_seagull", 0xD3939DFD },
            { "a_c_sharkhammer", 0x3C831724 },
            { "a_c_sharktiger", 0x06C3F072 },
            { "a_c_shepherd", 0x431FC24C },
            { "a_c_stingray", 0xA148614D },
            { "a_c_westy", 0xAD7844BB },
            { "a_f_m_beach_01", 0x303638A7 }, 
            { "a_f_m_bevhills_01", 0xBE086EFD },
            { "a_f_m_bevhills_02", 0xA039335F },
            { "a_f_m_bodybuild_01", 0x3BD99114 },
            { "a_f_m_business_02", 0x1FC37DBC },
            { "a_f_m_downtown_01", 0x654AD86E },
            { "a_f_m_eastsa_01", 0x9D3DCB7A },
            { "a_f_m_eastsa_02", 0x63C8D891 },
            { "a_f_m_fatbla_01", 0xFAB48BCB },
            { "a_f_m_fatcult_01", 0xB5CF80E4 },
            { "a_f_m_fatwhite_01", 0x38BAD33B },
            { "a_f_m_ktown_01", 0x52C824DE },
            { "a_f_m_ktown_02", 0x41018151 },
            { "a_f_m_prolhost_01", 0x169BD1E1 },
            { "a_f_m_salton_01", 0xDE0E0969 },
            { "a_f_m_skidrow_01", 0xB097523B },
            { "a_f_m_soucent_01", 0x745855A1 },
            { "a_f_m_soucent_02", 0xF322D338 },
            { "a_f_m_soucentmc_01", 0xCDE955D2 },
            { "a_f_m_tourist_01", 0x505603B9 },
            { "a_f_m_tramp_01", 0x48F96F5B },
            { "a_f_m_trampbeac_01", 0x8CA0C266 },
            { "a_f_o_genstreet_01", 0x61C81C85 },
            { "a_f_o_indian_01", 0xBAD7BB80 },
            { "a_f_o_ktown_01", 0x47CF5E96 },
            { "a_f_o_salton_01", 0xCCFF7D8A },
            { "a_f_o_soucent_01", 0x3DFA1830 },
            { "a_f_o_soucent_02", 0xA56DE716 },
            { "a_f_y_beach_01", 0xC79F6928 },
            { "a_f_y_bevhills_01", 0x445AC854 },
            { "a_f_y_bevhills_02", 0x5C2CF7F8 },
            { "a_f_y_bevhills_03", 0x20C8012F },
            { "a_f_y_bevhills_04", 0x36DF2D5D },
            { "a_f_y_business_01", 0x2799EFD8 },
            { "a_f_y_business_02", 0x31430342 },
            { "a_f_y_business_03", 0xAE86FDB4 },
            { "a_f_y_business_04", 0xB7C61032 },
            { "a_f_y_eastsa_01", 0xF5B0079D },
            { "a_f_y_eastsa_02", 0x0438A4AE },
            { "a_f_y_eastsa_03", 0x51C03FA4 },
            { "a_f_y_epsilon_01", 0x689C2A80 },
            { "a_f_y_femaleagent", 0x50610C43 },
            { "a_f_y_fitness_01", 0x457C64FB },
            { "a_f_y_fitness_02", 0x13C4818C },
            { "a_f_y_genhot_01", 0x2F4AEC3E },
            { "a_f_y_golfer_01", 0x7DD8FB58 },
            { "a_f_y_hiker_01", 0x30830813 },
            { "a_f_y_hippie_01", 0x1475B827 },
            { "a_f_y_hipster_01", 0x8247D331 },
            { "a_f_y_hipster_02", 0x97F5FE8D },
            { "a_f_y_hipster_03", 0xA5BA9A16 },
            { "a_f_y_hipster_04", 0x199881DC },
            { "a_f_y_indian_01", 0x092D9CC1 },
            { "a_f_y_juggalo_01", 0xDB134533 },
            { "a_f_y_runner_01", 0xC7496729 },
            { "a_f_y_rurmeth_01", 0x3F789426 },
            { "a_f_y_scdressy_01", 0xDB5EC400 },
            { "a_f_y_skater_01", 0x695FE666 },
            { "a_f_y_soucent_01", 0x2C641D7A },
            { "a_f_y_soucent_02", 0x5A8EF9CF },
            { "a_f_y_soucent_03", 0x87B25415 },
            { "a_f_y_tennis_01", 0x550C79C6 },
            { "a_f_y_topless_01", 0x9CF26183 },
            { "a_f_y_tourist_01", 0x563B8570 },
            { "a_f_y_tourist_02", 0x9123FB40 },
            { "a_f_y_vinewood_01", 0x19F41F65 },
            { "a_f_y_vinewood_02", 0xDAB6A0EB },
            { "a_f_y_vinewood_03", 0x379DDAB8 },
            { "a_f_y_vinewood_04", 0xFAE46146 },
            { "a_f_y_yoga_01", 0xC41B062E },
            { "a_m_m_acult_01", 0x5442C66B },
            { "a_m_m_afriamer_01", 0xD172497E },
            { "a_m_m_beach_01", 0x403DB4FD },
            { "a_m_m_beach_02", 0x787FA588 },
            { "a_m_m_bevhills_01", 0x54DBEE1F },
            { "a_m_m_bevhills_02", 0x3FB5C3D3 },
            { "a_m_m_business_01", 0x7E6A64B7 },
            { "a_m_m_eastsa_01", 0xF9A6F53F },
            { "a_m_m_eastsa_02", 0x07DD91AC },
            { "a_m_m_farmer_01", 0x94562DD7 },
            { "a_m_m_fatlatin_01", 0x61D201B3 },
            { "a_m_m_genfat_01", 0x06DD569F },
            { "a_m_m_genfat_02", 0x13AEF042 },
            { "a_m_m_golfer_01", 0xA9EB0E42 },
            { "a_m_m_hasjew_01", 0x6BD9B68C },
            { "a_m_m_hillbilly_01", 0x6C9B2849 },
            { "a_m_m_hillbilly_02", 0x7B0E452F },
            { "a_m_m_indian_01", 0xDDCAAA2C },
            { "a_m_m_ktown_01", 0xD15D7E71 },
            { "a_m_m_malibu_01", 0x2FDE6EB7 },
            { "a_m_m_mexcntry_01", 0xDD817EAD },
            { "a_m_m_mexlabor_01", 0xB25D16B2 },
            { "a_m_m_og_boss_01", 0x681BD012 },
            { "a_m_m_paparazzi_01", 0xECCA8C15 },
            { "a_m_m_polynesian_01", 0xA9D9B69E },
            { "a_m_m_prolhost_01", 0x9712C38F },
            { "a_m_m_rurmeth_01", 0x3BAD4184 },
            { "a_m_m_salton_01", 0x4F2E038A },
            { "a_m_m_salton_02", 0x60F4A717 },
            { "a_m_m_salton_03", 0xB28C4A45 },
            { "a_m_m_salton_04", 0x964511B7 },
            { "a_m_m_skater_01", 0xD9D7588C },
            { "a_m_m_skidrow_01", 0x01EEA6BD },
            { "a_m_m_socenlat_01", 0x0B8D69E3 },
            { "a_m_m_soucent_01", 0x6857C9B7 },
            { "a_m_m_soucent_02", 0x9F6D37E1 },
            { "a_m_m_soucent_03", 0x8BD990BA },
            { "a_m_m_soucent_04", 0xC2FBFEFE },
            { "a_m_m_stlat_02", 0xC2A87702 },
            { "a_m_m_tennis_01", 0x546A5344 },
            { "a_m_m_tourist_01", 0xC89F0184 },
            { "a_m_m_tramp_01", 0x1EC93FD0 },
            { "a_m_m_trampbeac_01", 0x53B57EB0 },
            { "a_m_m_tranvest_01", 0xE0E69974 },
            { "a_m_m_tranvest_02", 0xF70EC5C4 },
            { "a_m_o_acult_01", 0x55446010 },
            { "a_m_o_acult_02", 0x4BA14CCA },
            { "a_m_o_beach_01", 0x8427D398 },
            { "a_m_o_genstreet_01", 0xAD54E7A8 },
            { "a_m_o_ktown_01", 0x1536D95A },
            { "a_m_o_salton_01", 0x20208E4D },
            { "a_m_o_soucent_01", 0x2AD8921B },
            { "a_m_o_soucent_02", 0x4086BD77 },
            { "a_m_o_soucent_03", 0x0E32D8D0 },
            { "a_m_o_tramp_01", 0x174D4245 },
            { "a_m_y_acult_01", 0xB564882B },
            { "a_m_y_acult_02", 0x80E59F2E },
            { "a_m_y_beach_01", 0xD1FEB884 },
            { "a_m_y_beach_02", 0x23C7DC11 },
            { "a_m_y_beach_03", 0xE7A963D9 },
            { "a_m_y_beachvesp_01", 0x7E0961B8 },
            { "a_m_y_beachvesp_02", 0xCA56FA52 },
            { "a_m_y_bevhills_01", 0x76284640 },
            { "a_m_y_bevhills_02", 0x668BA707 },
            { "a_m_y_breakdance_01", 0x379F9596 },
            { "a_m_y_busicas_01", 0x9AD32FE9 },
            { "a_m_y_business_01", 0xC99F21C4 },
            { "a_m_y_business_02", 0xB3B3F5E6 },
            { "a_m_y_business_03", 0xA1435105 },

            { "a_m_y_cyclist_01", 0xFDC653C7 },
            { "a_m_y_dhill_01", 0xFF3E88AB },
            { "a_m_y_downtown_01", 0x2DADF4AA },
            { "a_m_y_eastsa_01", 0xA4471173 },
            { "a_m_y_eastsa_02", 0x168775F6 },
            { "a_m_y_epsilon_01", 0x77D41A3E },
            { "a_m_y_epsilon_02", 0xAA82FF9B },
            { "a_m_y_gay_01", 0xD1CCE036 },
            { "a_m_y_gay_02", 0xA5720781 },
            { "a_m_y_genstreet_01", 0x9877EF71 },
            { "a_m_y_genstreet_02", 0x3521A8D2 },
            { "a_m_y_golfer_01", 0xD71FE131 },
            { "a_m_y_hasjew_01", 0xE16D8F01 },
            { "a_m_y_hiker_01", 0x50F73C0C },
            { "a_m_y_hippy_01", 0x7D03E617 },
            { "a_m_y_hipster_01", 0x2307A353 },
            { "a_m_y_hipster_02", 0x14D506EE },
            { "a_m_y_hipster_03", 0x4E4179C6 },
            { "a_m_y_indian_01", 0x2A22FBCE },
            { "a_m_y_jetski_01", 0x2DB7EEF3 },
            { "a_m_y_juggalo_01", 0x91CA3E2C },
            { "a_m_y_ktown_01", 0x1AF6542C },
            { "a_m_y_ktown_02", 0x297FF13F },
            { "a_m_y_latino_01", 0x132C1A8E },
            { "a_m_y_methhead_01", 0x696BE0A9 },
            { "a_m_y_mexthug_01", 0x3053E555 },
            { "a_m_y_motox_01", 0x64FDEA7D },
            { "a_m_y_motox_02", 0x77AC8FDA },
            { "a_m_y_musclbeac_01", 0x4B652906 },
            { "a_m_y_musclbeac_02", 0xC923247C },
            { "a_m_y_polynesian_01", 0x8384FC9F },
            { "a_m_y_roadcyc_01", 0xF561A4C6 },
            { "a_m_y_runner_01", 0x25305EEE },
            { "a_m_y_runner_02", 0x843D9D0F },
            { "a_m_y_salton_01", 0xD7606C30 },
            { "a_m_y_skater_01", 0xC1C46677 },
            { "a_m_y_skater_02", 0xAFFAC2E4 },
            { "a_m_y_soucent_01", 0xE716BDCB },
            { "a_m_y_soucent_02", 0xACA3C8CA },
            { "a_m_y_soucent_03", 0xC3F0F764 },
            { "a_m_y_soucent_04", 0x8A3703F1 },
            { "a_m_y_stbla_01", 0xCF92ADE9 },
            { "a_m_y_stbla_02", 0x98C7404F },
            { "a_m_y_stlat_01", 0x8674D5FC },
            { "a_m_y_stwhi_01", 0x2418C430 },
            { "a_m_y_stwhi_02", 0x36C6E98C },
            { "a_m_y_sunbathe_01", 0xB7292F0C },
            { "a_m_y_surfer_01", 0xEAC2C7EE },
            { "a_m_y_vindouche_01", 0xC19377E7 },
            { "a_m_y_vinewood_01", 0x4B64199D },
            { "a_m_y_vinewood_02", 0x5D15BD00 },
            { "a_m_y_vinewood_03", 0x1FDF4294 },
            { "a_m_y_vinewood_04", 0x31C9E669 },
            { "a_m_y_yoga_01", 0xAB0A7155 },
            { "cs_amandatownley", 0x95EF18E3 },
            { "cs_andreas", 0xE7565327 },
            { "cs_ashley", 0x26C3D079 },
            { "cs_bankman", 0x9760192E },
            { "cs_barry", 0x69591CF7 },
            { "cs_beverly", 0xB46EC356 },
            { "cs_brad", 0xEFE5AFE6 },
            { "cs_bradcadaver", 0x7228AF60 },
            { "cs_carbuyer", 0x8CCE790F },
            { "cs_casey", 0xEA969C40 },
            { "cs_chengsr", 0x30DB9D7B },
            { "cs_chrisformage", 0xC1F380E6 },
            { "cs_clay", 0xDBCB9834 },
            { "cs_dale", 0x0CE81655 },
            { "cs_davenorton", 0x8587248C },
            { "cs_debra", 0xECD04FE9 },
            { "cs_denise", 0x6F802738 },
            { "cs_devin", 0x2F016D02 },
            { "cs_dom", 0x4772AF42 },
            { "cs_dreyfuss", 0x3C60A153 },
            { "cs_drfriedlander", 0xA3A35C2F },
            { "cs_fabien", 0x47035EC1 },

            { "cs_fbisuit_01", 0x585C0B52 },
            { "cs_floyd", 0x062547E7 },
            { "cs_guadalope", 0x0F9513F1 },
            { "cs_gurk", 0xC314F727 },
            { "cs_hunter", 0x5B44892C },
            { "cs_janet", 0x3034F9E2 },
            { "cs_jewelass", 0x4440A804 },
            { "cs_jimmyboston", 0x039677BD },
            { "cs_jimmydisanto", 0xB8CC92B4 },
            { "cs_joeminuteman", 0xF09D5E29 },
            { "cs_johnnyklebitz", 0xFA8AB881 },
            { "cs_josef", 0x459762CA },
            { "cs_josh", 0x450EEF9D },
            { "cs_karen_daniels", 0x4BAF381C },
            { "cs_lamardavis", 0x45463A0D },
            { "cs_lazlow", 0x38951A1B },
            { "cs_lestercrest", 0xB594F5C3 },
            { "cs_lifeinvad_01", 0x72551375 },
            { "cs_magenta", 0x5816C61A },
            { "cs_manuel", 0xFBB374CA },
            { "cs_marnie", 0x574DE134 },
            { "cs_martinmadrazo", 0x43595670 },
            { "cs_maryann", 0x0998C7AD },
            { "cs_michelle", 0x70AEB9C8 },
            { "cs_milton", 0xB76A330F },
            { "cs_molly", 0x45918E44 },
            { "cs_movpremf_01", 0x4BBA84D9 },
            { "cs_movpremmale", 0x8D67EE7D },
            { "cs_mrk", 0xC3CC9A75 },
            { "cs_mrs_thornhill", 0x4F921E6E },
            { "cs_mrsphillips", 0xCBFDA3CF },
            { "cs_natalia", 0x4EFEB1F0 },
            { "cs_nervousron", 0x7896DA94 },
            { "cs_nigel", 0xE1479C0B },
            { "cs_old_man1a", 0x1EEC7BDC },
            { "cs_old_man2", 0x98F9E770 },
            { "cs_omega", 0x8B70B405 },
            { "cs_orleans", 0xAD340F5A },
            { "cs_paper", 0x6B38B8F8 },
            { "cs_patricia", 0xDF8B1301 },
            { "cs_priest", 0x4D6DE57E },
            { "cs_prolsec_02", 0x1E9314A2 },
            { "cs_russiandrunk", 0x46521A32 },
            { "cs_siemonyetarian", 0xC0937202 },
            { "cs_solomon", 0xF6D1E04E },
            { "cs_stevehains", 0xA4E0A1FE },
            { "cs_stretch", 0x893D6805 },
            { "cs_tanisha", 0x42FE5370 },
            { "cs_taocheng", 0x8864083D },
            { "cs_taostranslator", 0x53536529 },
            { "cs_tenniscoach", 0x5C26040A },
            { "cs_terry", 0x3A5201C5 },
            { "cs_tom", 0x69E8ABC3 },
            { "cs_tomepsilon", 0x8C0FD4E2 },
            { "cs_tracydisanto", 0x0609B130 },
            { "cs_wade", 0xD266D9D6 },
            { "cs_zimbor", 0xEAACAAF0 },
            { "csb_abigail", 0x89768941 },
            { "csb_agent", 0xD770C9B4 },
            { "csb_anita", 0x0703F106 },
            { "csb_anton", 0xA5C787B6 },
            { "csb_ballasog", 0xABEF0004 },
            { "csb_bride", 0x82BF7EA1 },
            { "csb_burgerdrug", 0x8CDCC057 },
            { "csb_car3guy1", 0x04430687 },
            { "csb_car3guy2", 0x1383A508 },
            { "csb_chef", 0xA347CA8A },
            { "csb_chef2", 0xAE5BE23A },
            { "csb_chin_goon", 0xA8C22996 },
            { "csb_cletus", 0xCAE9E5D5 },
            { "csb_cop", 0x9AB35F63 },
            { "csb_customer", 0xA44F6F8B },
            { "csb_denise_friend", 0xB58D2529 },
            { "csb_fos_rep", 0x1BCC157B },
            { "csb_g", 0xA28E71D7 },
            { "csb_groom", 0x7AAB19D2 },
            { "csb_grove_str_dlr", 0xE8594E22 },
            { "csb_hao", 0xEC9E8F1C },
            { "csb_hugh", 0x6F139B54 },
            { "csb_imran", 0xE3420BDB },
            { "csb_jackhowitzer", 0x44BC7BB1 },
            { "csb_janitor", 0xC2005A40 },

            { "csb_maude", 0xBCC475CB },
            { "csb_money", 0x989DFD9A },
            { "csb_mp_agent14", 0x6DBBFC8B },
            { "csb_mweather", 0x613E626C },
            { "csb_ortega", 0xC0DB04CF },
            { "csb_oscar", 0xF41F399B },
            { "csb_paige", 0x5B1FA0C3 },
            { "csb_popov", 0x617D89E2 },
            { "csb_porndudes", 0x2F4AFE35 },
            { "csb_prologuedriver", 0xF00B49DB },
            { "csb_prolsec", 0x7FA2F024 },
            { "csb_ramp_gang", 0xC2800DBE },
            { "csb_ramp_hic", 0x858C94B8 },
            { "csb_ramp_hipster", 0x21F58BB4 },
            { "csb_ramp_marine", 0x616C97B9 },
            { "csb_ramp_mex", 0xF64ED7D0 },
            { "csb_rashcosvki", 0x188099A9 },
            { "csb_reporter", 0x2E420A24 },
            { "csb_roccopelosi", 0xAA64168C },
            { "csb_screen_writer", 0x8BE12CEC },
            { "csb_stripper_01", 0xAEEA76B5 },
            { "csb_stripper_02", 0x81441B71 },
            { "csb_tonya", 0x6343DD19 },
            { "csb_trafficwarden", 0xDE2937F3 },
            { "csb_undercover", 0xEF785A6A },
            { "csb_vagspeak", 0x48FF4CA9 },
            { "g_f_importexport_01", 0x84A1B11A },
            { "g_f_y_ballas_01", 0x158C439C },
            { "g_f_y_families_01", 0x4E0CE5D3 },
            { "g_f_y_lost_01", 0xFD5537DE },
            { "g_f_y_vagos_01", 0x5AA42C21 },
            { "g_m_importexport_01", 0xBCA2CCEA },
            { "g_m_m_armboss_01", 0xF1E823A2 },
            { "g_m_m_armgoon_01", 0xFDA94268 },
            { "g_m_m_armlieut_01", 0xE7714013 },
            { "g_m_m_chemwork_01", 0xF6157D8F },
            { "g_m_m_chiboss_01", 0xB9DD0300 },
            { "g_m_m_chicold_01", 0x106D9A99 },
            { "g_m_m_chigoon_01", 0x7E4F763F },
            { "g_m_m_chigoon_02", 0xFF71F826 },
            { "g_m_m_korboss_01", 0x352A026F },
            { "g_m_m_mexboss_01", 0x5761F4AD },
            { "g_m_m_mexboss_02", 0x4914D813 },
            { "g_m_y_armgoon_02", 0xC54E878A },
            { "g_m_y_azteca_01", 0x68709618 },
            { "g_m_y_ballaeast_01", 0xF42EE883 },
            { "g_m_y_ballaorig_01", 0x231AF63F },
            { "g_m_y_ballasout_01", 0x23B88069 },
            { "g_m_y_famca_01", 0xE83B93B7 },
            { "g_m_y_famdnf_01", 0xDB729238 },
            { "g_m_y_famfor_01", 0x84302B09 },
            { "g_m_y_korean_01", 0x247502A9 },
            { "g_m_y_korean_02", 0x8FEDD989 },
            { "g_m_y_korlieut_01", 0x7CCBE17A },
            { "g_m_y_lost_01", 0x4F46D607 },
            { "g_m_y_lost_02", 0x3D843282 },
            { "g_m_y_lost_03", 0x32B11CDC },
            { "g_m_y_mexgang_01", 0xBDDD5546 },
            { "g_m_y_mexgoon_01", 0x26EF3426 },
            { "g_m_y_mexgoon_02", 0x31A3498E },
            { "g_m_y_mexgoon_03", 0x964D12DC },
            { "g_m_y_pologoon_01", 0x4F3FBA06 },
            { "g_m_y_pologoon_02", 0xA2E86156 },
            { "g_m_y_salvaboss_01", 0x905CE0CA },
            { "g_m_y_salvagoon_01", 0x278C8CB7 },
            { "g_m_y_salvagoon_02", 0x3273A285 },
            { "g_m_y_salvagoon_03", 0x03B8C510 },
            { "g_m_y_strpunk_01", 0xFD1C49BB },
            { "g_m_y_strpunk_02", 0x0DA1EAC6 },
            { "hc_driver", 0x3B474ADF },
            { "hc_gunman", 0x0B881AEE },
            { "hc_hacker", 0x99BB00F8 },
            { "ig_abigail", 0x400AEC41 },
            { "ig_agent", 0x246AF208 },
            { "ig_amandatownley", 0x6D1E15F7 },
            { "ig_andreas", 0x47E4EEA0 },
            { "ig_ashley", 0x7EF440DB },
            { "ig_avon", 0xFCE270C2 },
            { "ig_ballasog", 0xA70B4A92 },
            { "ig_bankman", 0x909D9E7F },
            { "ig_barry", 0x2F8845A3 },
            { "ig_benny", 0xC4B715D2 },
            { "ig_bestmen", 0x5746CD96 },
            { "ig_beverly", 0xBDA21E5C },
            { "ig_brad", 0xBDBB4922 },
            { "ig_bride", 0x6162EC47 },
            { "ig_car3guy1", 0x84F9E937 },
            { "ig_car3guy2", 0x75C34ACA },
            { "ig_casey", 0xE0FA2554 },
            { "ig_chef", 0x49EADBF6 },
            { "ig_chef2", 0x85889AC3 },
            { "ig_chengsr", 0xAAE4EA7B },
            { "ig_chrisformage", 0x286E54A7 },
            { "ig_clay", 0x6CCFE08A },
            { "ig_claypain", 0x9D0087A8 },
            { "ig_cletus", 0xE6631195 },
            { "ig_dale", 0x467415E9 },
            { "ig_davenorton", 0x15CD4C33 },
            { "ig_denise", 0x820B33BD },
            { "ig_devin", 0x7461A0B0 },
            { "ig_dom", 0x9C2DB088 },
            { "ig_dreyfuss", 0xDA890932 },
            { "ig_drfriedlander", 0xCBFC0DF5 },
            { "ig_fabien", 0xD090C350 },
            { "ig_fbisuit_01", 0x3AE4A33B },
            { "ig_floyd", 0xB1B196B2 },
            { "ig_g", 0x841BA933 },
            { "ig_groom", 0xFECE8B85 },
            { "ig_hao", 0x65978363 },
            { "ig_hunter", 0xCE1324DE },
            { "ig_janet", 0x0D6D9C49 },
            { "ig_jay_norris", 0x7A32EE74 },
            { "ig_jewelass", 0x0F5D26BB },
            { "ig_jimmyboston", 0xEDA0082D },
            { "ig_jimmydisanto", 0x570462B9 },
            { "ig_joeminuteman", 0xBE204C9B },
            { "ig_johnnyklebitz", 0x87CA80AE },
            { "ig_josef", 0xE11A9FB4 },
            { "ig_josh", 0x799E9EEE },
            { "ig_karen_daniels", 0xEB51D959 },
            { "ig_kerrymcintosh", 0x5B3BD90D },
            { "ig_lamardavis", 0x65B93076 },
            { "ig_lazlow", 0xDFE443E5 },
            { "ig_lestercrest_2", 0x6E42FD26 },
            { "ig_lestercrest", 0x4DA6E849 },
            { "ig_lifeinvad_01", 0x5389A93C },
            { "ig_lifeinvad_02", 0x27BD51D4 },
            { "ig_magenta", 0xFCDC910A },
            { "ig_malc", 0xF1BCA919 },
            { "ig_manuel", 0xFD418E10 },
            { "ig_marnie", 0x188232D0 },
            { "ig_maryann", 0xA36F9806 },
            { "ig_maude", 0x3BE8287E },
            { "ig_michelle", 0xBF9672F4 },
            { "ig_milton", 0xCB3059B2 },
            { "ig_molly", 0xAF03DDE1 },
            { "ig_money", 0x37FACDA6 },
            { "ig_mp_agent14", 0xFBF98469 },
            { "ig_mrk", 0xEDDCAB6D },
            { "ig_mrs_thornhill", 0x1E04A96B },
            { "ig_mrsphillips", 0x3862EEA8 },
            { "ig_natalia", 0xDE17DD3B },
            { "ig_nervousron", 0xBD006AF1 },
            { "ig_nigel", 0xC8B7167D },
            { "ig_old_man1a", 0x719D27F4 },
            { "ig_old_man2", 0xEF154C47 },
            { "ig_omega", 0x60E6A7D8 },
            { "ig_oneil", 0x2DC6D3E7 },
            { "ig_orleans", 0x61D4C771 },
            { "ig_ortega", 0x26A562B7 },
            { "ig_paige", 0x154FCF3F },
            { "ig_paper", 0x999B00C6 },
            { "ig_patricia", 0xC56E118C },
            { "ig_popov", 0x267630FE },
            { "ig_priest", 0x6437E77D },
            { "ig_prolsec_02", 0x27B3AD75 },
            { "ig_ramp_gang", 0xE52E126C },
            { "ig_ramp_hic", 0x45753032 },
            { "ig_ramp_hipster", 0xDEEF9F6E },
            { "ig_ramp_mex", 0xE6AC74A4 },
            { "ig_rashcosvki", 0x380C4DE6 },
            { "ig_roccopelosi", 0xD5BA52FF },

            { "ig_russiandrunk", 0x3D0A5EB1 },
            { "ig_screen_writer", 0xFFE63677 },
            { "ig_siemonyetarian", 0x4C7B2F05 },
            { "ig_solomon", 0x86BDFE26 },
            { "ig_stevehains", 0x382121C8 },
            { "ig_stretch", 0x36984358 },
            { "ig_talina", 0xE793C8E8 },
            { "ig_tanisha", 0x0D810489 },
            { "ig_taocheng", 0xDC5C5EA5 },
            { "ig_taostranslator", 0x7C851464 },
            { "ig_tenniscoach", 0xA23B5F57 },
            { "ig_terry", 0x67000B94 },
            { "ig_tomepsilon", 0xCD777AAA },
            { "ig_tonya", 0xCAC85344 },
            { "ig_tracydisanto", 0xDE352A35 },
            { "ig_trafficwarden", 0x5719786D },
            { "ig_tylerdix", 0x5265F707 },
            { "ig_vagspeak", 0xF9FD068C },
            { "ig_wade", 0x92991B72 },
            { "ig_zimbor", 0x0B34D6F5 },
            { "mp_f_boatstaff_01", 0x3293B9CE },
            { "mp_f_cardesign_01", 0x242C34A7 },
            { "mp_f_chbar_01", 0xC3F6E385 },
            { "mp_f_cocaine_01", 0x4B657AF8 },
            { "mp_f_counterfeit_01", 0xB788F1F5 },
            { "mp_f_deadhooker", 0x73DEA88B },
            { "mp_f_execpa_01", 0x432CA064 },
            { "mp_f_execpa_02", 0x5972CCF0 },
            { "mp_f_forgery_01", 0x781A3CF8 },
            { "mp_f_freemode_01", 0x9C9EFFD8 },
            { "mp_f_helistaff_01", 0x19B6FF06 },
            { "mp_f_meth_01", 0xD2B27EC1 },
            { "mp_f_misty_01", 0xD128FF9D },
            { "mp_f_stripperlite", 0x2970A494 },
            { "mp_f_weed_01", 0xB26573A3 },
            { "mp_g_m_pros_01", 0x6C9DD7C9 },
            { "mp_m_avongoon", 0x9C13CB95 },
            { "mp_m_boatstaff_01", 0xC85F0A88 },
            { "mp_m_bogdangoon", 0x4D5696F7 },
            { "mp_m_claude_01", 0xC0F371B7 },
            { "mp_m_cocaine_01", 0x56D38F95 },
            { "mp_m_counterfeit_01", 0x9855C974 },
            { "mp_m_exarmy_01", 0x45348DBB },
            { "mp_m_execpa_01", 0x3E8417BC },
            { "mp_m_famdd_01", 0x33A464E5 },
            { "mp_m_fibsec_01", 0x5CDEF405 },
            { "mp_m_forgery_01", 0x613E709B },
            { "mp_m_freemode_01", 0x705E61F2 },
            { "mp_m_g_vagfun_01", 0xC4A617BD },
            { "mp_m_marston_01", 0x38430167 },
            { "mp_m_meth_01", 0xEDB42F3F },
            { "mp_m_niko_01", 0xEEDACFC9 },
            { "mp_m_securoguard_01", 0xDA2C984E },
            { "mp_m_shopkeep_01", 0x18CE57D0 },
            { "mp_m_waremech_01", 0xF7A74139 },
            { "mp_m_weapexp_01", 0x36EA5B09 },
            { "mp_m_weapwork_01", 0x4186506E },
            { "mp_m_weed_01", 0x917ED459 },
            { "mp_s_m_armoured_01", 0xCDEF5408 },
            { "player_one", 0x9B22DBAF },
            { "player_two", 0x9B810FA2 },
            { "player_zero", 0x0D7114C9 },
            { "s_f_m_fembarber", 0x163B875B },
            { "s_f_m_maid_01", 0xE093C5C6 },
            { "s_f_m_shop_high", 0xAE47E4B0 },
            { "s_f_m_sweatshop_01", 0x312B5BC0 },
            { "s_f_y_airhostess_01", 0x5D71A46F },
            { "s_f_y_bartender_01", 0x780C01BD },
            { "s_f_y_baywatch_01", 0x4A8E5536 },

            { "s_f_y_cop_01", 0x15F8700D },
            { "s_f_y_factory_01", 0x69F46BF3 },
            { "s_f_y_hooker_01", 0x028ABF95 },
            { "s_f_y_hooker_02", 0x14C3E407 },
            { "s_f_y_hooker_03", 0x031640AC },
            { "s_f_y_migrant_01", 0xD55B2BF5 },
            { "s_f_y_movprem_01", 0x2300C816 },
            { "s_f_y_ranger_01", 0x9FC7F637 },
            { "s_f_y_scrubs_01", 0xAB594AB6 },
            { "s_f_y_sheriff_01", 0x4161D042 },
            { "s_f_y_shop_low", 0xA96E2604 },
            { "s_f_y_shop_mid", 0x3EECBA5D },
            { "s_f_y_stripper_01", 0x52580019 },
            { "s_f_y_stripper_02", 0x6E0FB794 },
            { "s_f_y_stripperlite", 0x5C14EDFA },
            { "s_f_y_sweatshop_01", 0x8502B6B2 },
            { "s_m_m_ammucountry", 0x0DE9A30A },
            { "s_m_m_armoured_01", 0x95C76ECD },
            { "s_m_m_armoured_02", 0x63858A4A },
            { "s_m_m_autoshop_01", 0x040EABE3 },
            { "s_m_m_autoshop_02", 0xF06B849D },
            { "s_m_m_bouncer_01", 0x9FD4292D },
            { "s_m_m_ccrew_01", 0xC9E5F56B },
            { "s_m_m_chemsec_01", 0x2EFEAFD5 },
            { "s_m_m_ciasec_01", 0x625D6958 },
            { "s_m_m_cntrybar_01", 0x1A021B83 },
            { "s_m_m_dockwork_01", 0x14D7B4E0 },
            { "s_m_m_doctor_01", 0xD47303AC },
            { "s_m_m_fiboffice_01", 0xEDBC7546 },
            { "s_m_m_fiboffice_02", 0x26F067AD },
            { "s_m_m_fibsec_01", 0x7B8B434B },
            { "s_m_m_gaffer_01", 0xA956BD9E },
            { "s_m_m_gardener_01", 0x49EA5685 },
            { "s_m_m_gentransport", 0x1880ED06 },
            { "s_m_m_hairdress_01", 0x418DFF92 },
            { "s_m_m_highsec_01", 0xF161D212 },
            { "s_m_m_highsec_02", 0x2930C1AB },
            { "s_m_m_janitor", 0xA96BD9EC },
            { "s_m_m_lathandy_01", 0x9E80D2CE },
            { "s_m_m_lifeinvad_01", 0xDE0077FD },
            { "s_m_m_linecook", 0xDB9C0997 },
            { "s_m_m_lsmetro_01", 0x765AAAE4 },
            { "s_m_m_mariachi_01", 0x7EA4FFA6 },
            { "s_m_m_marine_01", 0xF2DAA2ED },
            { "s_m_m_marine_02", 0xF0259D83 },
            { "s_m_m_migrant_01", 0xED0CE4C6 },
            { "s_m_m_movalien_01", 0x64611296 },
            { "s_m_m_movprem_01", 0xD85E6D28 },
            { "s_m_m_movspace_01", 0xE7B31432 },
            { "s_m_m_paramedic_01", 0xB353629E },
            { "s_m_m_pilot_01", 0xE75B4B1C },
            { "s_m_m_pilot_02", 0xF63DE8E1 },
            { "s_m_m_postal_01", 0x62599034 },
            { "s_m_m_postal_02", 0x7367324F },
            { "s_m_m_prisguard_01", 0x56C96FC6 },
            { "s_m_m_scientist_01", 0x4117D39B },
            { "s_m_m_security_01", 0xD768B228 },
            { "s_m_m_snowcop_01", 0x1AE8BB58 },
            { "s_m_m_strperf_01", 0x795AC7A8 },
            { "s_m_m_strpreach_01", 0x1C0077FB },
            { "s_m_m_strvend_01", 0xCE9113A9 },
            { "s_m_m_trucker_01", 0x59511A6C },
            { "s_m_m_ups_01", 0x9FC37F22 },
            { "s_m_m_ups_02", 0xD0BDE116 },
            { "s_m_o_busker_01", 0xAD9EF1BB },
            { "s_m_y_airworker", 0x62018559 },
            { "s_m_y_ammucity_01", 0x9E08633D },
            { "s_m_y_armymech_01", 0x62CC28E2 },
            { "s_m_y_autopsy_01", 0xB2273D4E },
            { "s_m_y_barman_01", 0xE5A11106 },
            { "s_m_y_baywatch_01", 0x0B4A6862 },
            { "s_m_y_blackops_01", 0xB3F3EE34 },
            { "s_m_y_blackops_02", 0x7A05FA59 },
            { "s_m_y_blackops_03", 0x5076A73B },
            { "s_m_y_busboy_01", 0xD8F9CD47 },
            { "s_m_y_chef_01", 0x0F977CEB },
            { "s_m_y_clown_01", 0x04498DDE },
            { "s_m_y_construct_01", 0xD7DA9E99 },
            { "s_m_y_construct_02", 0xC5FEFADE },
            { "s_m_y_cop_01", 0x5E3DA4A4 },
            { "s_m_y_dealer_01", 0xE497BBEF },
            { "s_m_y_devinsec_01", 0x9B557274 },
            { "s_m_y_dockwork_01", 0x867639D1 },
            { "s_m_y_doorman_01", 0x22911304 },
            { "s_m_y_dwservice_01", 0x75D30A91 },
            { "s_m_y_dwservice_02", 0xF5908A06 },
            { "s_m_y_factory_01", 0x4163A158 },
            { "s_m_y_fireman_01", 0xB6B1EDA8 },
            { "s_m_y_garbage", 0xEE75A00F },
            { "s_m_y_grip_01", 0x309E7DEA },
            { "s_m_y_hwaycop_01", 0x739B1EF5 },
            { "s_m_y_marine_01", 0x65793043 },
            { "s_m_y_marine_02", 0x58D696FE },
            { "s_m_y_marine_03", 0x72C0CAD2 },
            { "s_m_y_mime", 0x3CDCA742 },
            { "s_m_y_pestcont_01", 0x48114518 },
            { "s_m_y_pilot_01", 0xAB300C07 },
            { "s_m_y_prismuscl_01", 0x5F2113A1 },
            { "s_m_y_prisoner_01", 0xB1BB9B59 },
            { "s_m_y_ranger_01", 0xEF7135AE },
            { "s_m_y_robber_01", 0xC05E1399 },
            { "s_m_y_sheriff_01", 0xB144F9B9 },
            { "s_m_y_shop_mask", 0x6E122C06 },
            { "s_m_y_strvend_01", 0x927F2323 },
            { "s_m_y_swat_01", 0x8D8F1B10 },
            { "s_m_y_uscg_01", 0xCA0050E9 },
            { "s_m_y_valet_01", 0x3B96F23E },
            { "s_m_y_waiter_01", 0xAD4C724C },
            { "s_m_y_winclean_01", 0x550D8D9D },
            { "s_m_y_xmech_01", 0x441405EC },
            { "s_m_y_xmech_02_mp", 0x69147A0D },
            { "s_m_y_xmech_02", 0xBE20FA04 },
            { "u_f_m_corpse_01", 0x2E140314 },
            { "u_f_m_drowned_01", 0xD7F37609 },
            { "u_f_m_miranda", 0x414FA27B },
            { "u_f_m_promourn_01", 0xA20899E7 },
            { "u_f_o_moviestar", 0x35578634 },
            { "u_f_o_prolhost_01", 0xC512DD23 },
            { "u_f_y_bikerchic", 0xFA389D4F },
            { "u_f_y_comjane", 0xB6AA85CE },
            { "u_f_y_corpse_01", 0x9C70109D },
            { "u_f_y_corpse_02", 0x0D9C72F8 },
            { "u_f_y_hotposh_01", 0x969B6DFE },
            { "u_f_y_jewelass_01", 0xF0D4BE2E },
            { "u_f_y_mistress", 0x5DCA2528 },
            { "u_f_y_poppymich", 0x23E9A09E },
            { "u_f_y_princess", 0xD2E3A284 },
            { "u_f_y_spyactress", 0x5B81D86C },
            { "u_m_m_aldinapoli", 0xF0EC56E2 },
            { "u_m_m_bankman", 0xC306D6F5 },
            { "u_m_m_bikehire_01", 0x76474545 },
            { "u_m_m_doa_01", 0x621E6BFD },
            { "u_m_m_edtoh", 0x2A797197 },
            { "u_m_m_fibarchitect", 0x342333D3 },
            { "u_m_m_filmdirector", 0x2B6E1BB6 },
            { "u_m_m_glenstank_01", 0x45BB1666 },
            { "u_m_m_griff_01", 0xC454BCBB },
            { "u_m_m_jesus_01", 0xCE2CB751 },
            { "u_m_m_jewelsec_01", 0xACCCBDB6 },
            { "u_m_m_jewelthief", 0xE6CC3CDC },
            { "u_m_m_markfost", 0x1C95CB0B },
            { "u_m_m_partytarget", 0x81F74DE7 },
            { "u_m_m_prolsec_01", 0x709220C7 },
            { "u_m_m_promourn_01", 0xCE96030B },
            { "u_m_m_rivalpap", 0x60D5D6DA },
            { "u_m_m_spyactor", 0xAC0EA5D8 },
            { "u_m_m_streetart_01", 0x6C19E962 },
            { "u_m_m_willyfist", 0x90769A8F },
            { "u_m_o_filmnoir", 0x2BACC2DB },
            { "u_m_o_finguru_01", 0x46E39E63 },
            { "u_m_o_taphillbilly", 0x9A1E5E52 },
            { "u_m_o_tramp_01", 0x6A8F1F9B },
            { "u_m_y_abner", 0xF0AC2626 },
            { "u_m_y_antonb", 0xCF623A2C },
            { "u_m_y_babyd", 0xDA116E7E },
            { "u_m_y_baygor", 0x5244247D },
            { "u_m_y_burgerdrug_01", 0x8B7D3766 },
            { "u_m_y_chip", 0x24604B2B },
            { "u_m_y_corpse_01", 0x94C2A03F },
            { "u_m_y_cyclist_01", 0x2D0EFCEB },
            { "u_m_y_fibmugger_01", 0x85B9C668 },
            { "u_m_y_guido_01", 0xC6B49A2F },
            { "u_m_y_gunvend_01", 0xB3229752 },
            { "u_m_y_hippie_01", 0xF041880B },
            { "u_m_y_imporage", 0x348065F5 },
            { "u_m_y_juggernaut_01", 0x90EF5134 },
            { "u_m_y_justin", 0x7DC3908F },
            { "u_m_y_mani", 0xC8BB1E52 },
            { "u_m_y_militarybum", 0x4705974A },
            { "u_m_y_paparazzi", 0x5048B328 },
            { "u_m_y_party_01", 0x36E70600 },
            { "u_m_y_pogo_01", 0xDC59940D },
            { "u_m_y_prisoner_01", 0x7B9B4BC0 },
            { "u_m_y_proldriver_01", 0x855E36A3 },
            { "u_m_y_rsranger_01", 0x3C438CD2 },
            { "u_m_y_sbike", 0x6AF4185D },
            { "u_m_y_staggrm_01", 0x9194CE03 },
            { "u_m_y_tattoo_01", 0x94AE2B8C },
            { "u_m_y_zombie_01", 0xAC4B4506 }
        };

        private readonly ObjectPool pool = new ObjectPool();
        private readonly NativeMenu menu = new NativeMenu("Interaction", "Traffic Stop Interaction Menu");
        private readonly NativeMenu pedMenu = new NativeMenu("Interaction", "Pedestrian Interaction Menu");
        private readonly NativeMenu clothingMenu = new NativeMenu("Clothing Menu", "Department outfits");
        private readonly NativeMenu factionClothingMenu = new NativeMenu("Faction Clothing Menu", "My Department's outfits");

        private readonly NativeMenu lockerRoomMenu = new NativeMenu("Locker Room", "Select your uniform and loadout");

        private readonly NativeMenu vehicleMenu = new NativeMenu("Vehicle Menu", "Vehicle Interaction Menu");
        private readonly NativeMenu adminMenu = new NativeMenu("Admin Menu", "POLSIM Admin Menu");

        private readonly NativeMenu factionGear = new NativeMenu("Faction Equipment", "Faction Equipment");

        public Ped playerPed = null;

        //private Queue<Tuple<Vector3, float>> coordinatesAndHeadings = new Queue<Tuple<Vector3, float>>();

        private bool isPulledOver = false;
        private bool pursuitActive = false;

        // Default Traffic Enforcement Settings; these are normally changed when loading server-settings.json later
        private bool trafficEventsEnabled = false;
        private int minTrafficEventTime = 180000; // 3 mins
        private int maxTrafficEventTime = 720000; // 12 mins
        private float pursuitChance = 0.1f;

        private float suspectAggression = 0.1f;
        private float firearmChance = 0.1f;
        private float meleeChance = 0.1f;
        private float fleeChance = 0.1f;

        private bool onDuty = false;
        private string deptShort = null;

        public Ped driver;
        public bool isDriverStoppedAtTrafficStop = false;
        private Vehicle globalTrafficStopVehicle = null;
        Prop prop = null; // move over system
        Prop frontProp = null; // move over system

        public Blip blip = new Blip(0);
        public Blip blipTraffic = new Blip(0);
        private Timer blipTimer;
        public Blip flatbedBlip = new Blip(0);
        public Blip pedInteractionBlip = new Blip(0);


        // Define a variable to keep track of the time the 'E' key has been held
        int eKeyPressedTime = 0;
        //bool isHoldingE = false;
        bool isPlayerAdmin = false;

        // Ped Interaction
        public Ped closestPed = null;
        public Dictionary<Ped, NPCData> pedData = new Dictionary<Ped, NPCData>(); // Dictionary to store data for each ped

        public bool isDraggingPed = false;
        public bool isFollowing = false;

        public bool isPlayerGenderMale = true;
        public string currentPostalCode = "ERR";
        private PostalManager postalManager;
        private int updateInterval = 4500; // Update interval in milliseconds (3 seconds)
        private DateTime lastUpdateTime = DateTime.MinValue;
        // Define a variable to track whether the traffic stop is active
        bool isTrafficStopActive = false;
        DateTime stopStartTime = DateTime.MinValue;

        Vehicle targetVehicle = null;

        public ClientMain()
        {
            Debug.WriteLine("------------------------------------------");
            Debug.WriteLine($"POLSIM v.{version} loaded.");

            /*Vector3 playerPosition2 = Game.PlayerPed.Position;
            AddStaticText(playerPosition2, "TEST");*/

           // LoadInvisibleObjectModel();

            RegisterNuiCallbackType("pedImage:update");
            RegisterNuiCallbackType("mdcExited");

            // WebSocket replacement
            RegisterNuiCallbackType("sendToClient");
            EventHandlers["__cfx_nui:sendToClient"] += new Action<IDictionary<string, object>>(OnDataReceivedFromNUI);
            EventHandlers["nameCheckResponse"] += new Action<string>(OnNameCheckResponse);

            EventHandlers["setPlayerAsAdmin"] += new Action(SetPlayerAsAdmin);

            EventHandlers["arrestListUpdateResponse"] += new Action<string>(OnArrestListUpdateResponse);

            // Initialize dictionary to store searched items for each ped
            searchedPedItems = new Dictionary<int, List<Item>>();
            searchedVehicleItems = new Dictionary<int, List<Item>>();

            // Register an event handler for the button click event from NUI
            EventHandlers["buttonClicked"] += new Action(HandleButtonClicked);
            EventHandlers["mdcExited"] += new Action(GiveControlBackAfterMDCExit);

            // Server Events
            EventHandlers["outboundCoordsUpdate"] += new Action<Vector3>(OnOutboundCoordsUpdate);
            EventHandlers["serverEvent:RandomIdentityResponse"] += new Action<string>(OnRandomIdentityResponse);
            EventHandlers["serverEvent:SubmitPlayerProfile"] += new Action<string>(OnPlayerReceiveProfile);

            EventHandlers["departmentData"] += new Action<string>(OnDepartmentDataReceived);

            //EventHandlers["clientEvent:ReceiveDepartmentData"] += new Action<string>(OnReceiveDepartmentData);

            EventHandlers["playerSpawned"] += new Action(OnPlayerSpawned);
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);

            //EventHandlers["serverEvent:SubmitLockerCoordinates"] += new Action<string>(OnReceiveLockerCoordinates);

            EventHandlers["dispatchData"] += new Action<string, string, int, string, string, int>(OnDispatchDataReceived);
            EventHandlers["updateRecentCallsPage"] += new Action<string>(updateRecentCallsPage);
            EventHandlers["ReceivePedNetIds"] += new Action<string>(ReceivePedNetIds);


            postalManager = new PostalManager();

            // Registering Commands
            RegisterCommand("mdc", new Action<int, List<object>, string>(ToggleMDCCommand), false);
            RegisterCommand("reqtow", new Action<int, List<object>, string>(ToggleRequestTowCommand), false);
            RegisterCommand("toglic", new Action<int, List<object>, string>(ToggleLicenseVisibilityCommand), false);
            RegisterCommand("showeupmenu", new Action(ShowClothingMenu), false);

            //RegisterCommand("polsimadmin", new Action(ShowAdminMenu), false);

            //RegisterCommand("randevent", new Action(HandleGenerateRandomEventCommand), false);
            //RegisterCommand("savepos", new Action<int, List<object>, string>(SavePlayerPosition), false);
            RegisterCommand("forceduty", new Action<int, List<object>, string>((source, args, raw) =>
            {
                ToggleDuty();
            }), false);
            
            /*RegisterCommand("testlocker", new Action<int, List<object>, string>((source, args, raw) =>
            {
                HandleLockerRoom();
            }), false);*/


            try
            {
                string filePath = "server-settings.json";
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

                        _serverSettings = JsonConvert.DeserializeObject<List<ServerSettings>>(fileContents);

                        if (_serverSettings != null && _serverSettings.Count > 0)
                        {
                            foreach (var settings in _serverSettings)
                            {
                                Debug.WriteLine("------------------------------------------");
                                Debug.WriteLine($"Applying server settings to the client:");
                                trafficEventsEnabled = settings.TrafficEventsEnabled;
                                minTrafficEventTime = settings.TrafficMinimumIntervalTime * 60000;
                                maxTrafficEventTime = settings.TrafficMaximumIntervalTime * 60000;
                                pursuitChance = settings.PursuitChance;

                                suspectAggression = settings.SuspectAggression;
                                fleeChance = settings.FleeChance;
                                firearmChance = settings.FirearmChance;
                                meleeChance = settings.MeleeChance;

                                Debug.WriteLine($"Traffic Events Enabled? {trafficEventsEnabled}");
                                Debug.WriteLine($"Traffic Events min time: {minTrafficEventTime}");
                                Debug.WriteLine($"Traffic Events min time: {maxTrafficEventTime}");
                                Debug.WriteLine($"Pursuit Chance: {pursuitChance}%");
                                Debug.WriteLine($"Base aggression: {suspectAggression * 100}%");
                                Debug.WriteLine($"Flee factor: {fleeChance * 100}%");
                                Debug.WriteLine($"Chance to carry firearm: {firearmChance * 100}%");
                                Debug.WriteLine($"Chance to carry melee: {meleeChance * 100}%");
                                Debug.WriteLine("------------------------------------------");
                            }
                        }
                        else
                        {
                            Debug.WriteLine("FAIL: Unable to deserialize server settings.");
                        }

                    }
                    else
                    {
                        Debug.WriteLine("FAIL: server-settings.json file contents null/file does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An exception occurred while loading/processing the server-settings.json: {ex.Message}");
            }

            // Loading items.json with error catching
            try
            {
                string filePath = "items.json";
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

                        items = JsonConvert.DeserializeObject<List<Item>>(fileContents);

                        if (items != null)
                        {
                            int numberOfItems = items.Count; // Count the number of loaded items
                            Debug.WriteLine($"Number of loaded items: {numberOfItems}");
                            Debug.WriteLine("------------------------------------------");
                        }
                        else
                        {
                            Debug.WriteLine("Failed to deserialize JSON into Item list.");
                        }

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


            // Loading coordinates.json
            /*try
            {
                string filePath = "coordinates.json";
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


                        // Deserialize JSON array
                        //var jails = JsonConvert.DeserializeObject<List<Jail>>(fileContents);

                        this.calloutblips = JsonConvert.DeserializeObject<List<CalloutData>>(fileContents);

                        if (calloutblips != null)
                        {
                            int numberOfJails = calloutblips.Count; // Count the number of loaded jails
                            Debug.WriteLine($"Number of loaded coordinates: {numberOfJails}");
                            Debug.WriteLine("------------------------------------------");
                            foreach (var calloutData in calloutblips)
                            {
                                double x = calloutData.X;
                                double y = calloutData.Y;
                                double z = calloutData.Z;

                                Vector3 calloutLocation = new Vector3(calloutData.X, calloutData.Y, calloutData.Z);
                                Blip calloutBlip = World.CreateBlip(calloutLocation);
                                // Set the appropriate blip sprite, name, and any other properties as needed
                                calloutBlip.Sprite = BlipSprite.GTAOMission;
                                calloutBlip.Name = "Callout"; // Set the blip name
                                                              // Add the blip to the list
                                calloutDebugBlips.Add(calloutBlip);
                            }
                        }
                        else
                        {
                            Debug.WriteLine("Failed to deserialize JSON into Jail list.");
                        }

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
            }*/

            // Loading jails.json with error catching
            try
            {
                string filePath = "jails.json";
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


                        // Deserialize JSON array
                        //var jails = JsonConvert.DeserializeObject<List<Jail>>(fileContents);

                        this.jails = JsonConvert.DeserializeObject<List<Jail>>(fileContents);

                        if (jails != null)
                        {
                            int numberOfJails = jails.Count; // Count the number of loaded jails
                            Debug.WriteLine($"Number of loaded jails: {numberOfJails}");
                            Debug.WriteLine("------------------------------------------");
                            foreach (var jail in jails)
                            {
                                double x = jail.X;
                                double y = jail.Y;
                                double z = jail.Z;

                                // Use the x, y, z coordinates as needed
                                //Debug.WriteLine($"Jail Coordinates: X={x}, Y={y}, Z={z}");
                            }
                        }
                        else
                        {
                            Debug.WriteLine("Failed to deserialize JSON into Jail list.");
                        }

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


            // Loading outfits.json with error catching
            try
            {
                string filePath = "outfits.json";
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
                        Debug.WriteLine("------------------------------------------");
                        List<UniformData> uniforms = JsonConvert.DeserializeObject<List<UniformData>>(fileContents);

                        // Extract unique Category2 values
                        HashSet<string> uniqueCategories = new HashSet<string>();

                        int index = 1; // Initialize index counter

                        foreach (var uniform in uniforms)
                        {
                            //Debug.WriteLine($"Retrieved category: {uniform.Category2}");
                            uniqueCategories.Add(uniform.Category2);
                        }


                        foreach (string category in uniqueCategories)
                        {
                            // Adding each categories found in outfits.json to the main clothing menu
                            NativeItem categoryMenuItem = new NativeItem($"{category} {index}");
                            clothingMenu.Add(categoryMenuItem);

                            // Creating a separate menu for each unique categories (i.e LSPD)
                            NativeMenu factionClothingMenu = new NativeMenu($"{category}");
                            pool.Add(factionClothingMenu);

                            // Populate the category menu with items
                            foreach (var uniform in uniforms)
                            {
                                if (uniform.Category2 == category)
                                {
                                    // Create a menu item for each uniform in the category and add it to the category menu
                                    NativeItem uniformMenuItem = new NativeItem(uniform.Name);
                                    factionClothingMenu.Add(uniformMenuItem);

                                    // Add a click event to navigate to the category menu
                                    uniformMenuItem.Activated += (sender, args) =>
                                    {
                                        int ped = Game.Player.Character.Handle;

                                        Debug.WriteLine($"uniformMenuItem.Activated: {sender} - {args}");

                                        // HAT
                                        string[] hatValues = uniform.Hat.Split(':');
                                        int hatValue0 = int.Parse(hatValues[0]) - 1;
                                        int hatValue1 = int.Parse(hatValues[1]) - 1;
                                        Debug.WriteLine($"Hat: {hatValue0}, {hatValue1}");

                                        if (hatValue0 == -1)
                                        {
                                            Debug.WriteLine($"This uniform has no hats. ({hatValue0})");
                                            ClearPedProp(ped, 0);
                                        }
                                        else
                                        {
                                            SetPedPropIndex(ped, 0, hatValue0, hatValue1, true);
                                        }


                                        // GLASSES
                                        string[] glassesValues = uniform.Glasses.Split(':');
                                        int glassesValue0 = int.Parse(glassesValues[0]) - 1;
                                        int glassesValue1 = int.Parse(glassesValues[1]) - 1;
                                        Debug.WriteLine($"Glasses: {glassesValue0}, {glassesValue1}");

                                        if (glassesValue0 == -1)
                                        {
                                            Debug.WriteLine($"This uniform has no glasses. ({glassesValue0})");
                                            ClearPedProp(ped, 6);
                                        }
                                        else
                                        {
                                            SetPedPropIndex(ped, 6, glassesValue0, glassesValue1, true);
                                        }

                                        string[] earValues = uniform.Ear.Split(':');
                                        int earValue0 = int.Parse(earValues[0]) - 1;
                                        int earValue1 = int.Parse(earValues[1]) - 1;
                                        Debug.WriteLine($"Ear: {earValue0}, {earValue1}");

                                        string[] watchValues = uniform.Watch.Split(':');
                                        int watchValue0 = int.Parse(watchValues[0]) - 1;
                                        int watchValue1 = int.Parse(watchValues[1]) - 1;
                                        Debug.WriteLine($"Watch: {watchValue0}, {watchValue1}");

                                        string[] maskValues = uniform.Mask.Split(':');
                                        int maskValue0 = int.Parse(maskValues[0]) - 1;
                                        int maskValue1 = int.Parse(maskValues[1]) - 1;
                                        Debug.WriteLine($"Mask: {maskValue0}, {maskValue1}");

                                        string[] topValues = uniform.Top.Split(':');
                                        int topValue0 = int.Parse(topValues[0]) - 1;
                                        int topValue1 = int.Parse(topValues[1]) - 1;
                                        Debug.WriteLine($"Top: {topValue0}, {topValue1}");

                                        string[] upperSkinValues = uniform.UpperSkin.Split(':');
                                        int upperSkinValue0 = int.Parse(upperSkinValues[0]) - 1;
                                        int upperSkinValue1 = int.Parse(upperSkinValues[1]) - 1;
                                        Debug.WriteLine($"UpperSkin: {upperSkinValue0}, {upperSkinValue1}");

                                        string[] decalValues = uniform.Decal.Split(':');
                                        int decalValue0 = int.Parse(decalValues[0]) - 1;
                                        int decalValue1 = int.Parse(decalValues[1]) - 1;
                                        Debug.WriteLine($"Decal: {decalValue0}, {decalValue1}");

                                        string[] underCoatValues = uniform.UnderCoat.Split(':');
                                        int underCoatValue0 = int.Parse(underCoatValues[0]) - 1;
                                        int underCoatValue1 = int.Parse(underCoatValues[1]) - 1;
                                        Debug.WriteLine($"UnderCoat: {underCoatValue0}, {underCoatValue1}");

                                        string[] pantsValues = uniform.Pants.Split(':');
                                        int pantsValue0 = int.Parse(pantsValues[0]) - 1;
                                        int pantsValue1 = int.Parse(pantsValues[1]) - 1;
                                        Debug.WriteLine($"Pants: {pantsValue0}, {pantsValue1}");

                                        string[] shoesValues = uniform.Shoes.Split(':');
                                        int shoesValue0 = int.Parse(shoesValues[0]) - 1;
                                        int shoesValue1 = int.Parse(shoesValues[1]) - 1;
                                        Debug.WriteLine($"Shoes: {shoesValue0}, {shoesValue1}");

                                        string[] accessoriesValues = uniform.Accessories.Split(':');
                                        int accessoriesValue0 = int.Parse(accessoriesValues[0]) - 1;
                                        int accessoriesValue1 = int.Parse(accessoriesValues[1]) - 1;
                                        Debug.WriteLine($"Accessories: {accessoriesValue0}, {accessoriesValue1}");

                                        string[] armorValues = uniform.Armor.Split(':');
                                        int armorValue0 = int.Parse(armorValues[0]) - 1;
                                        int armorValue1 = int.Parse(armorValues[1]) - 1;
                                        Debug.WriteLine($"Armor: {armorValue0}, {armorValue1}");

                                        string[] parachuteValues = uniform.Parachute.Split(':');
                                        int parachuteValue0 = int.Parse(parachuteValues[0]) - 1;
                                        int parachuteValue1 = int.Parse(parachuteValues[1]) - 1;
                                        Debug.WriteLine($"Parachute: {parachuteValue0}, {parachuteValue1}");

                                        SetPedComponentVariation(ped, 1, maskValue0, maskValue1, 0);
                                        SetPedComponentVariation(ped, 3, upperSkinValue0, upperSkinValue1, 0);
                                        SetPedComponentVariation(ped, 4, pantsValue0, pantsValue1, 0);
                                        SetPedComponentVariation(ped, 5, parachuteValue0, parachuteValue1, 0);
                                        SetPedComponentVariation(ped, 6, shoesValue0, shoesValue1, 0);
                                        SetPedComponentVariation(ped, 7, accessoriesValue0, accessoriesValue1, 0);
                                        SetPedComponentVariation(ped, 8, underCoatValue0, underCoatValue1, 0);
                                        SetPedComponentVariation(ped, 9, armorValue0, armorValue1, 0);
                                        SetPedComponentVariation(ped, 10, decalValue0, decalValue1, 0);
                                        SetPedComponentVariation(ped, 11, topValue0, topValue1, 0);
                                    };
                                }
                            }

                            // Add a click event to navigate to the category menu
                            categoryMenuItem.Activated += (sender, args) =>
                            {
                                // Hide the main menu
                                clothingMenu.Visible = false;

                                // Show the category menu
                                factionClothingMenu.Visible = true;
                            };

                            index++;
                        }

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



            // Traffic Stop Interaction Menu
            //NativeListItem<string> listItemInteract = new NativeListItem<string>("Interact with", "Select the vehicle occupant to interact with", "Driver", "Front Passenger", "Rear Left Passenger", "Rear Right Pasenger");


            NativeItem adminMenuPlayers = new NativeItem("");
            adminMenu.Add(adminMenuPlayers);

            NativeListItem<string> listItemDocuments = new NativeListItem<string>("Take/Give Back", "Ask the vehicle occupant to provide with you a specific document", "Driver's license", "Vehicle registration");
            NativeListItem<string> listItemOrderOut = new NativeListItem<string>("Order out", "Order occupants out of the vehicle", "Driver", "Front Passenger", "Rear Left Passenger", "Rear Right Pasenger");
            NativeItem regularItem = new NativeItem("Dismiss vehicle", "Allow the driver to leave");
            //menu.Add(listItemInteract);
            menu.Add(listItemDocuments);
            menu.Add(listItemOrderOut);
            menu.Add(regularItem);

            NativeItem pedMenuID = new NativeItem("Take/Give Back ID", "Ask the subject to provide a piece of identification or give it back.");
            NativeListItem<string> pedMenuInvestigate = new NativeListItem<string>("Investigate", "Investigate the subject.", "Search", "Other");
            NativeItem pedMenuDetain = new NativeItem("Detain", "Instruct the subject they are being detained.");
            NativeListItem<string> pedMenuCuff = new NativeListItem<string>("Handcuffs", "Manage handcuffs", "Front", "Rear", "Uncuff");


            NativeItem pedMenuDrag = new NativeItem("Drag", "Drag the subject.");
            NativeItem pedMenuFollow = new NativeItem("Follow me", "Instruct the subject to follow you.");
            NativeListItem<string> pedMenuPutIn = new NativeListItem<string>("Put in", "Place the subject in your vehicle.", "Front Right", "Rear Right", "Rear Left");
            NativeItem pedMenuRemoveFrom = new NativeItem("Remove from vehicle", "Remove the subject from your vehicle.");
            NativeItem pedMenuArrest = new NativeItem("Arrest", "Process the subject for custody.");

            // Adding the list items to pedMenu
            pedMenu.Add(pedMenuID);
            pedMenu.Add(pedMenuInvestigate);
            pedMenu.Add(pedMenuDetain);
            pedMenu.Add(pedMenuCuff);
            pedMenu.Add(pedMenuDrag);
            pedMenu.Add(pedMenuFollow);
            pedMenu.Add(pedMenuPutIn);
            pedMenu.Add(pedMenuRemoveFrom);
            pedMenu.Add(pedMenuArrest);

            NativeItem vehMenuSearch = new NativeItem("Search", "Perform a search on the vehicle.");
            NativeItem vehMenuTow = new NativeItem("Mark for towing", "Assign this vehicle to be towed by the towing service.");

            vehicleMenu.Add(vehMenuSearch);
            vehicleMenu.Add(vehMenuTow);

            NativeItem lockerDuty = new NativeItem($"Duty", "Punch in/Punch out");
            NativeItem lockerUniforms = new NativeItem("Uniforms", "See your department's uniforms.");
            NativeItem lockerWeapons = new NativeItem("Equipment", "See your department's approved equipment & weapons.");

            lockerRoomMenu.Add(lockerDuty);
            lockerRoomMenu.Add(lockerUniforms);
            lockerRoomMenu.Add(lockerWeapons);

            // When clicking the Unfiforms button in locker room menu, shows faction clothing menu
            lockerDuty.Activated += async (sender, args) =>
            {
                lockerRoomMenu.Visible = true;
                ToggleDuty();
                await Delay(0);
            };


            // When clicking the Unfiforms button in locker room menu, shows faction clothing menu
            lockerUniforms.Activated += async (sender, args) =>
            {
                lockerRoomMenu.Visible = false;
                factionClothingMenu.Visible = true;
                await Delay(0);
            };

            // When clicking the Equipment button in locker room menu
            lockerWeapons.Activated += async (sender, args) =>
            {
                lockerRoomMenu.Visible = false;

                factionGear.Visible = true;

                await Delay(0);
            };

            // Detects when the faction clothing menu is closed and returns to main locker room menu
            factionClothingMenu.Closed += async (sender, args) =>
            {
                lockerRoomMenu.Visible = true;
                factionClothingMenu.Visible = false;
                await Delay(0);
            };

            // Detects when the whitelisted gear menu is closed and returns to main locker room menu
            factionGear.Closed += async (sender, args) =>
            {
                lockerRoomMenu.Visible = true;
                factionGear.Visible = false;
                await Delay(0);
            };

            // Detects when the whitelisted gear menu is closed and returns to main locker room menu
            lockerRoomMenu.Closed += async (sender, args) =>
            {
                // if the other menus werent opened
                await Delay(100);
                if (factionGear.Visible == false && factionClothingMenu.Visible == false)
                {
                    Debug.WriteLine($"sender: {sender} args: {args}");
                    Game.PlayerPed.Task.ClearAnimation("anim@amb@carmeet@checkout_car@male_c@idles", "idle_a");
                    Game.PlayerPed.Task.ClearAnimation("weapons@first_person@aim_idle@remote_clone@pistol@shared@core", "settle_med");
                    API.FreezeEntityPosition(Game.PlayerPed.Handle, false);

                    lockerRoomActive = false;

                    // Reset the camera
                    World.RenderingCamera = null;
                    lockerRoomCam.Delete();
                    DisplayRadar(true);
                    await Delay(0);
                }
                else
                {

                }
            };


            pedMenuInvestigate.Activated += async (sender, args) =>
            {
                string selectedOption = pedMenuInvestigate.SelectedItem;
                switch (selectedOption)
                {
                    case "Search":
                        DisplayNotification("Searching subject...", 5);

                        // Check if the ped was previously searched
                        if (searchedPedItems.ContainsKey(closestPed.Handle))
                        {
                            Debug.WriteLine("Ped has already been searched.");
                            //DisplayNotification("DEBUG: Items kept in memory:", 5);

                            // Display the items previously found for this ped
                            List<Item> foundItems = searchedPedItems[closestPed.Handle];
                            foreach (var item in foundItems)
                            {
                                Debug.WriteLine($"Found item: {item.Name}, Size: {item.Size}, Description: {item.Description}");
                                DisplayNotification($"Found: {item.Name}", 10);
                                await Delay(100);
                            }
                        }

                        else if (items != null && items.Any())
                        {
                            // Pick 1 to 3 random items from the items list
                            Random random = new Random();
                            int itemCount = random.Next(1, 4);
                            List<Item> pickedItems = items.OrderBy(x => random.Next()).Take(itemCount).ToList();

                            Debug.WriteLine($"itemCount: {itemCount} pickedItems: {pickedItems}");
                            //DisplayNotification($"DEBUG: Ped never searched, fetching new items...", 5);

                            // Store the picked items for this ped
                            searchedPedItems[closestPed.Handle] = pickedItems;

                            // Display the picked items
                            foreach (var item in pickedItems)
                            {
                                Debug.WriteLine($"Picked item: {item.Name}, Size: {item.Size}, Description: {item.Description}");
                                DisplayNotification($"Found: {item.Name}", 10);
                                await Delay(100);
                            }
                        }
                        else
                        {
                            Debug.WriteLine("Items list is null or empty.");
                        }

                        break;
                    default:
                        // Handle other options if needed
                        break;
                }
            };

            // Ped Interaction Menu - Request ID
            pedMenuID.Activated += async (sender, args) =>
            {
                if (closestPed != null)
                {
                    // Check if pedData exists for closestPed
                    if (pedData.ContainsKey(closestPed))
                    {
                        NPCData pedData = this.pedData[closestPed]; // Access the NPCData for the closestPed
                        if (pedData.ID == -1) // -1 is default, wasn't assigned an ID yet
                        {
                            Debug.WriteLine("Random identity not yet assigned...");
                            GetIdentityFromServer(closestPed); // Call GetIdentityFromServer for the closestPed
                            DisplayNotification("Subject retrieving ID...", 5);
                            await Delay(1000);
                            if (!isLicenseDisplayed)
                            {
                                TriggerEvent("ToggleLicenseVisibility");
                                isLicenseDisplayed = true;
                            }
                            DisplayNotification("Handing ID...", 5);

                            // Assigning registrationID to pedData.ID
                            pedData.ID = pedIdentityPedInteractionMenu;
                        }
                        else
                        {
                            if (isLicenseDisplayed)
                            {
                                DisplayNotification("Returning ID.", 5);
                                TriggerEvent("ToggleLicenseVisibility");
                                isLicenseDisplayed = false;
                            }
                            else
                            {
                                DisplayNotification("Subject retrieving ID...", 5);
                                TriggerEvent("ToggleLicenseVisibility");
                                isLicenseDisplayed = true;
                            }
                            Debug.WriteLine("Ped already has an ID assigned.");
                            //pedIdentityPedInteractionMenu = pedData.ID;
                            TriggerServerEvent("GetIdentityById", pedData.ID);
                        }
                    }
                    else
                    {
                        Debug.WriteLine("No pedData found for closestPed");
                    }
                }
                else
                {
                    Debug.WriteLine("No valid closestPed found");
                }
            };

            // Ped Interaction Menu Handcuffing - Types: 0 = Rearcuff, 1 = Frontcuff, 3 = Uncuff
            pedMenuCuff.Activated += async (sender, args) =>
            {
                string animName = "";
                string animDict = "";

                string selectedOption = pedMenuCuff.SelectedItem;
                switch (selectedOption)
                {
                    case "Front":

                        // Should probably put these elsewhere so stuff doesnt get loaded for no reason
                        RequestAnim("anim@move_m@prisoner_cuffed");
                        RequestAnim("mp_arresting");
                        RequestModel(GetHashKey("p_cs_cuffs_02_s"));
                        await Delay(100);

                        ClearPedTasks(closestPed.Handle);

                        if (!IsEntityAMissionEntity(closestPed.Handle)) { Debug.WriteLine("Setting as mission entity");  SetEntityAsMissionEntity(closestPed.Handle, true, true); } // Setting npc as mission entity if it wasnt

                        SetEnableHandcuffs(closestPed.Handle, true);
                        TaskSetBlockingOfNonTemporaryEvents(closestPed.Handle, true);
                        DisplayNotification("Front cuffs", 3); // type 1
                        animName = "idle";
                        animDict = "anim@move_m@prisoner_cuffed";
                        TaskPlayAnim(closestPed.Handle, animDict, animName, 8.0f, -8, -1, 49, 0, false, false, false);
                        ProcessCuffModel(false, 1);
                        break;
                    case "Rear":

                        // Should probably put these elsewhere so stuff doesnt get loaded for no reason
                        RequestAnim("anim@move_m@prisoner_cuffed");
                        RequestAnim("mp_arresting");
                        RequestModel(GetHashKey("p_cs_cuffs_02_s"));
                        await Delay(100);

                        ClearPedTasks(closestPed.Handle);


                        if (!IsEntityAMissionEntity(closestPed.Handle)) { Debug.WriteLine("Setting as mission entity"); SetEntityAsMissionEntity(closestPed.Handle, true, true); } // Setting npc as mission entity if it wasnt

                        SetEnableHandcuffs(closestPed.Handle, true);
                        TaskSetBlockingOfNonTemporaryEvents(closestPed.Handle, true);
                        DisplayNotification("Rear cuffs", 3); // type 0
                        animName = "idle";
                        animDict = "mp_arresting";
                        TaskPlayAnim(closestPed.Handle, animDict, animName, 8.0f, -8, -1, 49, 0, false, false, false);
                        ProcessCuffModel(false, 0);
                        break;
                    case "Uncuff":
                        DisplayNotification("Uncuff", 3);
                        ProcessCuffModel(true, 3);
                        SetEnableHandcuffs(closestPed.Handle, false);
                        ClearPedTasks(closestPed.Handle);
                        break;
                    default:
                        DisplayNotification("Error when selecting cuff option.", 3);
                        break;
                }
            };

            pedMenuDrag.Activated += (sender, args) =>
            {
                Debug.WriteLine("pedMenuDrag");

                if(!isDraggingPed)
                {
                    DisplayNotification("You are dragging the person.", 5);
                    isDraggingPed = true;
                    pedMenu.Visible = false;
                }
                else
                {
                    DisplayNotification("You stopped dragging the person.", 5);
                    isDraggingPed = false;
                    //ClearPedTasksImmediately(closestPed.Handle);
                    DetachEntity(closestPed.Handle, true, true);
                    pedMenu.Visible = false;
                }
            };

            pedMenuFollow.Activated += (sender, args) =>
            {

                Debug.WriteLine("pedMenuFollow");

                if (!isFollowing)
                {


                    // Check if entity already a mission entity
                    if (!IsEntityAMissionEntity(closestPed.Handle))
                    {
                        Debug.WriteLine("Setting this entity as mission entity.");
                        SetEntityAsMissionEntity(closestPed.Handle, true, true);
                        TaskSetBlockingOfNonTemporaryEvents(closestPed.Handle, true);
                        DisplayNotification("Setting as mission entity", 5);

                        DisplayNotification("Person is now following you.", 5);
                        pedMenu.Visible = false;
                        isFollowing = true;
                        TaskFollowToOffsetOfEntity(closestPed.Handle, Game.PlayerPed.Handle, 0.0f, 1.0f, 0.0f, 1.5f, -1, 2.0f, true);
                    }
                    // If not, just make NPC follow:
                    else
                    {
                        DisplayNotification("Person is now following you.", 5);
                        pedMenu.Visible = false;
                        isFollowing = true;
                        TaskFollowToOffsetOfEntity(closestPed.Handle, Game.PlayerPed.Handle, 0.0f, 1.0f, 0.0f, 1.5f, -1, 2.0f, true);
                    }
                }
                else
                {
                    DisplayNotification("Person is no longer following you.", 5);
                    ClearPedTasks(closestPed.Handle);
                    pedMenu.Visible = false;
                    isFollowing = false;
                }
            };

            // PED MENU - PUT IN VEHICLE
            pedMenuPutIn.Activated += (sender, args) =>
            {
                // Get player's coordinates
                Vector3 playerCoords = Game.Player.Character.Position;

                // Initialize closest vehicle variables
                Vehicle closestVehicle = null;
                float closestDistance = float.MaxValue;

                // Loop through all spawned vehicles
                foreach (Vehicle vehicle in World.GetAllVehicles())
                {
                    // Get vehicle coordinates and distance from player
                    Vector3 vehicleCoords = vehicle.Position;
                    float distance = Vector3.Distance(playerCoords, vehicleCoords);

                    // Check if vehicle is within 5 meters and closer than the current closest
                    if (distance <= 5 && distance < closestDistance)
                    {
                        closestVehicle = vehicle;
                        closestDistance = distance;
                    }
                }

                // Check if a closest vehicle was found
                if (closestVehicle != null)
                {
                    // Do something with the closest vehicle, for example, display a message
                    Debug.WriteLine($"Nearest vehicle found: {closestVehicle.DisplayName} at {closestDistance:F2} meters");
                    pedMenu.Visible = false;
                }
                else
                {
                    // No vehicle found within 5 meters
                    DisplayNotification("You are not near any vehicles.", 3);
                    return;
                }


                /*
                    enum eSeatPosition
                    SF_FrontDriverSide = -1,
                    SF_FrontPassengerSide = 0,
                    SF_BackDriverSide = 1,
                    SF_BackPassengerSide = 2,
                    SF_AltFrontDriverSide = 3,
                    SF_AltFrontPassengerSide = 4,
                    SF_AltBackDriverSide = 5,
                    SF_AltBackPassengerSide = 6,
                */

                // If the ped is currently being dragged toggle it off so it doesnt keep sticking to the player
                isDraggingPed = false;

                string selectedDoor = pedMenuPutIn.SelectedItem;
                switch (selectedDoor)
                {
                    case "Front Right":
                        DisplayNotification("The person will use the front right seat.", 3);
                        ClearPedTasks(closestPed.Handle);
                        TaskEnterVehicle(closestPed.Handle, closestVehicle.Handle, 10000, 0, 1.0f, 1, 0);
                        break;
                    case "Rear Right":
                        DisplayNotification("The person will use the rear right seat.", 3);
                        ClearPedTasks(closestPed.Handle);
                        TaskEnterVehicle(closestPed.Handle, closestVehicle.Handle, 10000, 2, 1.0f, 1, 0);
                        break;
                    case "Rear Left":
                        DisplayNotification("The person will use the rear left seat.", 3);
                        ClearPedTasks(closestPed.Handle);
                        TaskEnterVehicle(closestPed.Handle, closestVehicle.Handle, 10000, 1, 1.0f, 1, 0);
                        break;
                    default:
                        DisplayNotification("Error when selecting a door.", 3);
                        break;
                }
            };

            // PED MENU - REMOVE FROM VEHICLE
            pedMenuRemoveFrom.Activated += async (sender, args) =>
            {
                if(IsPedInAnyVehicle(closestPed.Handle, true))
                {
                    DisplayNotification("The subject is exiting the vehicle.", 3);
                    TaskLeaveAnyVehicle(closestPed.Handle, 0, 64);

                    // For some reason the ped will get in the driver seat and drive off, need to fix this
                }
                else
                {
                    DisplayNotification("The subject is not in a vehicle.", 3);
                }
                await Delay(1);
            };

            // PED MENU - DETAIN
            pedMenuDetain.Activated += async (sender, args) =>
            {
                // Setting the NPC as a Mission Entity if it wasn't already set
                if (!IsEntityAMissionEntity(closestPed.Handle))
                {
                    Debug.WriteLine("Setting this entity as mission entity.");
                    SetEntityAsMissionEntity(closestPed.Handle, true, true);
                    TaskSetBlockingOfNonTemporaryEvents(closestPed.Handle, true);
                    DisplayNotification("Notifying subject they are being detained.", 5);

                    Random random = new Random();
                    if (random.NextDouble() < firearmChance)
                    {


                        GiveWeaponToPed(closestPed.Handle, (uint)WeaponHash.Pistol, 50, false, false);
                        DisplayNotification("Subject has been armed with a firearm.", 5);

                        // Check suspect aggression and decide if the ped attacks
                        if (random.NextDouble() < suspectAggression)
                        {
                            // Command the ped to attack
                            TaskCombatPed(closestPed.Handle, Game.PlayerPed.Handle, 0, 16);
                            DisplayNotification("Subject is aggressive and attacking.", 5);
                        }
                        else
                        {
                            DisplayNotification("Subject is armed but not aggressive.", 5);
                        }

                    }
                    else
                    {
                        DisplayNotification("Subject is unarmed.", 5);
                    }

                }
                else
                {
                    ClearPedTasks(closestPed.Handle);
                    int closestPedHandleInt = closestPed.Handle;
                    Debug.WriteLine("Removing this entity as mission entity.");
                    SetEntityAsMissionEntity(closestPed.Handle, false, false);
                    TaskSetBlockingOfNonTemporaryEvents(closestPed.Handle, false);
                    SetPedAsNoLongerNeeded(ref closestPedHandleInt);
                    DisplayNotification("Releasing subject.", 5);
                }
                await Delay(1);
            };

            vehMenuSearch.Activated += async (sender, args) =>
            {
                Vector3 playerPosition = Game.PlayerPed.Position;
                TaskOpenVehicleDoor(Game.PlayerPed.Handle, vehicleMenuTarget.Handle, 5000, -1, 1.0f);
                DisplayNotification("Searching...", 10);

                // Check if the ped was previously searched
                if (searchedVehicleItems.ContainsKey(vehicleMenuTarget.Handle))
                {
                    Debug.WriteLine("Vehicle searched before...");

                    // Display the items previously found for this ped
                    List<Item> foundItems = searchedVehicleItems[vehicleMenuTarget.Handle];
                    foreach (var item in foundItems)
                    {
                        Debug.WriteLine($"Found item: {item.Name}, Size: {item.Size}, Description: {item.Description}");
                        DisplayNotification($"Found: {item.Name}", 10);
                        await Delay(100);
                    }
                }

                else if (items != null && items.Any())
                {
                    // Pick 1 to 6 random items from the items list
                    Random random = new Random();
                    int itemCount = random.Next(1, 7);
                    List<Item> pickedItems = items.OrderBy(x => random.Next()).Take(itemCount).ToList();

                    Debug.WriteLine($"itemCount: {itemCount} pickedItems: {pickedItems}");

                    // Store the picked items for this ped
                    searchedVehicleItems[vehicleMenuTarget.Handle] = pickedItems;

                    // Display the picked items
                    foreach (var item in pickedItems)
                    {
                        Debug.WriteLine($"Picked item: {item.Name}, Size: {item.Size}, Description: {item.Description}");
                        DisplayNotification($"Found: {item.Name}", 10);
                        await Delay(100);
                    }
                }
                else
                {
                    Debug.WriteLine("Items list is null or empty.");
                }


            };

            vehMenuTow.Activated += (sender, args) =>
            {
                Debug.WriteLine("Marking vehicle for towing.");

                if (vehicleMenuTarget != null)
                {
                    towtruckTarget = vehicleMenuTarget;

                    string displayName = GetDisplayNameFromVehicleModel((uint)towtruckTarget.Model.Hash);
                    string plateNum = GetVehicleNumberPlateText(towtruckTarget.Handle);

                    DisplayNotification($"{displayName} with registration {plateNum} marked for towing.", 15);
                }
                else
                {
                    Debug.WriteLine("Error: vehicleMenuTarget is null.");
                    DisplayNotification("Error with vehicle selection.", 15);
                }
            };

            // PED MENU - ARREST
            pedMenuArrest.Activated += async (sender, args) =>
            {
                // Get player's position
                Vector3 playerPosition = Game.PlayerPed.Position;
                double playerX = playerPosition.X;
                double playerY = playerPosition.Y;
                string nearestJail = GetNearestJail(playerX, playerY);

                int pedRef = closestPed.Handle;

                if (nearestJail != string.Empty)
                {
                    DisplayNotification($"Processed subject at: {nearestJail}", 30);
                    pedMenu.Visible = false;

                    if (IsEntityAMissionEntity(closestPed.Handle))
                    {
                        // If it's a mission entity, set it to false
                        SetEntityAsMissionEntity(closestPed.Handle, false, false);
                        DeletePed(ref pedRef);
                        isDraggingPed = false; // set dragging value to false in case player is dragging while arresting
                    }

                    /*DisplayNotification($"If you haven't yet submitted an arrest report, you can do so here.", 30);

                    SendNuiMessage(JsonConvert.SerializeObject(new
                    {
                        type = "toggleMDCVisibility"
                    }));*/
                }
                else
                {
                    DisplayNotification("You are not near any processing facility.", 5);
                }
                await Delay(1);
            };

            listItemDocuments.Activated += (sender, args) =>
            {
                string selectedDocument = listItemDocuments.SelectedItem;
                switch (selectedDocument)
                {
                    case "All":
                        DisplayNotification("Asking for driver's license, vehicle registration and vehicle insurance documents...", 3);
                        break;
                    case "Driver's license":
                        DisplayNotification("Asking/Returning driver's license.", 3);
                        TriggerEvent("ToggleLicenseVisibility");
                        break;
                    case "Vehicle registration":
                        DisplayNotification("Asking/Returning vehicle's registration.", 3);
                        TriggerEvent("ToggleVehicleRegistrationVisibility");
                        break;
                    case "Vehicle insurance":
                        DisplayNotification("Asking/Returning vehicle's insurance.", 3);
                        break;
                    default:
                        break;
                }
            };

            listItemOrderOut.Activated += (sender, args) =>
            {
                string selectedOption = listItemOrderOut.SelectedItem;
                Debug.WriteLine($"Selected option: {selectedOption}");
                blipTraffic?.Delete();
                blip?.Delete();

                switch (selectedOption)
                {
                    case "Driver":
                        // Handle the case for the driver
                        Debug.WriteLine("Handling driver...");

                        if(!IsVehicleSeatFree(targetVehicle.Handle, -1)) // If driver seat is NOT free (occupied by ped)
                        {
                            int tempPedHandle = GetPedInVehicleSeat(targetVehicle.Handle, -1);
                            TaskLeaveAnyVehicle(tempPedHandle, 0, 64);
                        }
                        else
                        {
                            DisplayNotification("No occupants in this seat.", 3);
                        }


                        break;
                    case "Front Passenger":
                        // Handle the case for the front passenger
                        Debug.WriteLine("Handling front passenger...");

                        if (!IsVehicleSeatFree(targetVehicle.Handle, -1)) // If driver seat is NOT free (occupied by ped)
                        {
                            int tempPedHandle = GetPedInVehicleSeat(targetVehicle.Handle, 0);
                            TaskLeaveAnyVehicle(tempPedHandle, 0, 64);
                        }
                        else
                        {
                            DisplayNotification("No occupants in this seat.", 3);
                        }

                        break;
                    case "Rear Left Passenger":
                        // Handle the case for the rear left passenger
                        Debug.WriteLine("Handling rear left passenger...");

                        if (!IsVehicleSeatFree(targetVehicle.Handle, -1)) // If driver seat is NOT free (occupied by ped)
                        {
                            int tempPedHandle = GetPedInVehicleSeat(targetVehicle.Handle, 1);
                            TaskLeaveAnyVehicle(tempPedHandle, 0, 64);
                        }
                        else
                        {
                            DisplayNotification("No occupants in this seat.", 3);
                        }

                        break;
                    case "Rear Right Passenger":
                        // Handle the case for the rear right passenger
                        Debug.WriteLine("Handling rear right passenger...");

                        if (!IsVehicleSeatFree(targetVehicle.Handle, -1)) // If driver seat is NOT free (occupied by ped)
                        {
                            int tempPedHandle = GetPedInVehicleSeat(targetVehicle.Handle, 2);
                            TaskLeaveAnyVehicle(tempPedHandle, 0, 64);
                        }
                        else
                        {
                            DisplayNotification("No occupants in this seat.", 3);
                        }

                        break;
                    default:
                        // Handle default case (if the selected option doesn't match any case)
                        Debug.WriteLine("Invalid option selected.");
                        break;
                }
            };


            regularItem.Activated += async (sender, args) =>
            {
                await ClearTrafficStop();
            };

            // Adding Lemon UI menus to the pool
            pool.Add(menu);
            pool.Add(clothingMenu);
            pool.Add(factionClothingMenu);
            pool.Add(pedMenu);
            pool.Add(vehicleMenu);
            pool.Add(lockerRoomMenu);
            pool.Add(factionGear);

            // Check if trafficEvents are enabled and run ScheduleTrafficEvent()
            if (trafficEventsEnabled) { ScheduleTrafficEvent(); }

            // Initialize the timer bar collection and the timer bar
            timerBarCollection = new TimerBarCollection();
            timerBar = new TimerBarProgress("Timer");
            isEKeyHeld = false;
            Tick += OnTick;
        }


        // =================================================================================
        // GetNearestJail()
        // Checks if the player is near any jail coordinates from jails.json
        // =================================================================================
        public string GetNearestJail(double playerX, double playerY)
        {
            if (jails == null || jails.Count == 0)
            {
                DisplayNotification("There is an error with the jail location database. Contact a server Administrator.", 15);
                Debug.WriteLine("Jail Coordinates list is not initialized or empty.");
                return string.Empty; // or handle the situation accordingly
            }

            double shortestDistance = double.MaxValue;
            string nearestJail = "";

            foreach (Jail jail in jails)
            {
                double distance = CalculateDistance(playerX, playerY, jail.X, jail.Y);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestJail = jail.textTag;
                    Debug.WriteLine($"Nearest jail found: {nearestJail}");
                }
            }
            // Check if the shortest distance is within 10 units before returning the nearest jail
            if (shortestDistance <= 10.0)
            {
                return nearestJail;
            }
            else
            {
                //DisplayNotification("You are not near a processing facility.", 5);
                return string.Empty; // or handle the situation accordingly
            }
        }


        // =================================================================================
        // RequestAnim()
        // Helper function to pre-load animations
        // =================================================================================
        private async void RequestAnim(string dictionary)
        {
            RequestAnimDict(dictionary);
            while (!HasAnimDictLoaded(dictionary))
            {
                await Delay(100);
            }
            await Delay(0);
        }

        // =================================================================================
        // PlayerHasAssaultRifle()
        // Function to check if the player has any assault rifle
        // =================================================================================
        private bool PlayerHasAssaultRifle()
        {
            Ped playerPed = Game.PlayerPed;
            foreach (var weapon in assaultRifles)
            {
                if (API.HasPedGotWeapon(playerPed.Handle, weapon, false))
                {
                    return true;
                }
            }
            return false;
        }

        // =================================================================================
        // PlayerHasPistol()
        // Function to check if the player has any pistols
        // =================================================================================
        private bool PlayerHasPistol()
        {
            Ped playerPed = Game.PlayerPed;
            foreach (var weapon in pistols)
            {
                if (API.HasPedGotWeapon(playerPed.Handle, weapon, false))
                {
                    return true;
                }
            }
            return false;
        }


        private async void HandleLockerRoom()
        {
            Vector3 playerPos = Game.PlayerPed.Position;


            if (IsPlayerNearLockerRoom(playerPos))
            {
                Debug.WriteLine("Player is near a locker room.");
            }
            else
            {
                Debug.WriteLine("Player is not near any locker rooms.");
                return;
            }

            int maxTries = 5;
            int currentTry = 0;

            while (deptShort == null && currentTry < maxTries)
            {
                Debug.WriteLine("deptShort is null. Retrying in 3 seconds...");
                await Delay(3000);
                currentTry++;
            }

            if (deptShort == null)
            {
                Debug.WriteLine("Unable to get deptShort after maximum tries.");
                return;
            }

            // Remove weapons from players hand before applying the animation
            SetCurrentPedWeapon(Game.PlayerPed.Handle, (uint)WeaponHash.Unarmed, true);


            Debug.WriteLine($"This player's department is: {deptShort}");
            // Get the player's ped (character) and its model hash
            int playerPed = API.PlayerPedId();
            uint modelHash = (uint)API.GetEntityModel(playerPed);

            //GiveWeaponToPed(WeaponHash.CarbineRifleMk2);

            // Define the model hashes for mp_f_freemode_01 and mp_m_freemode_01
            uint femaleModelHash = (uint)API.GetHashKey("mp_f_freemode_01");
            uint maleModelHash = (uint)API.GetHashKey("mp_m_freemode_01");

            bool isFemale = modelHash == femaleModelHash;
            bool isMale = modelHash == maleModelHash;

            // If the player model is neither female nor male freemode, return
            if (!isFemale && !isMale)
            {
                DisplayNotification("You need to use the male or female multiplayer character model.", 10);
                return;
            }

            // Check if the player's model matches either of the desired model hashes
            if (isFemale)
            {
                Debug.WriteLine("Player model is freemode female.");
                // Do something if the player model matches
            }
            else if (isMale)
            {
                Debug.WriteLine("Player model is freemode male.");
            }

            lockerRoomActive = !lockerRoomActive;

            if (lockerRoomActive)
            {
                Game.PlayerPed.Heading = 0f;
                // Get player position and 
                Vector3 playerForward = Game.PlayerPed.ForwardVector;

                // Calculate the offset for the camera to be 1 meter in front of the player and at a 45-degree angle
                Vector3 offset = new Vector3(1.5f, 0.0f, -0.1f); // Adjust the 'y' component to raise the camera
                Vector3 camPos = playerPos + playerForward * 1.0f + offset; // Offset by 1 meter in front of the player and raised
                Vector3 targetPos = playerPos + new Vector3(0, 0, 0.4f); // Adjust the 'y' component to raise the camera

                // Create the camera
                lockerRoomCam = World.CreateCamera(camPos, Vector3.Zero, 25f); // Use current field of view
                World.RenderingCamera = lockerRoomCam;

                // Point the camera at the adjusted target position
                lockerRoomCam.PointAt(targetPos);
                lockerRoomCam.Shake(CameraShake.Drunk, 0.05f);

                // Apply the animation
                //API.RequestAnimDict("weapons@first_person@aim_idle@generic@projectile@misc@flare@"); // low ready pistol hold anim

                //anim@amb@range@weapon_test@ : weapon_inspect_02_w_ar_assaultrifle_mag1

                /*if (PlayerHasAssaultRifle())
                {
                    Debug.WriteLine("Player has an assault rifle.");
                    //weapons @first_person@aim_idle @remote_clone@pistol @shared@core: aim_high_loop
                    API.RequestAnimDict("weapons@first_person@aim_idle@remote_clone@pistol@shared@core");
                    while (!API.HasAnimDictLoaded("weapons@first_person@aim_idle@remote_clone@pistol@shared@core"))
                    {
                        await BaseScript.Delay(100);
                    }

                    Game.PlayerPed.Task.PlayAnimation("weapons@first_person@aim_idle@remote_clone@pistol@shared@core", "aim_high_loop", 8.0f, -8.0f, -1, AnimationFlags.StayInEndFrame, 0.0f);
                }
                else if(PlayerHasPistol())
                {
                    Debug.WriteLine("Player has a pistol");

                    API.RequestAnimDict("weapons@first_person@aim_idle@remote_clone@pistol@shared@core");
                    while (!API.HasAnimDictLoaded("weapons@first_person@aim_idle@remote_clone@pistol@shared@core"))
                    {
                        await BaseScript.Delay(100);
                    }

                    Game.PlayerPed.Task.PlayAnimation("weapons@first_person@aim_idle@remote_clone@pistol@shared@core", "settle_med", 8.0f, -8.0f, -1, AnimationFlags.StayInEndFrame, 0.0f);
                }
                else
                {*/

                //}



                //Game.PlayerPed.Task.PlayAnimation("weapons@first_person@aim_idle@generic@projectile@misc@flare@", "aim_low_loop", 8.0f, -8.0f, -1, AnimationFlags.StayInEndFrame, 0.0f);

                // Freeze player position
                API.FreezeEntityPosition(Game.PlayerPed.Handle, true);

                LoadDepartmentClothing(deptShort, isMale);

                // Apply animation (arms crossed)
                API.RequestAnimDict("anim@amb@carmeet@checkout_car@male_c@idles");
                while (!API.HasAnimDictLoaded("weapons@first_person@aim_idle@generic@projectile@misc@flare@"))
                {
                    await BaseScript.Delay(100);
                }
                await Game.PlayerPed.Task.PlayAnimation("anim@amb@carmeet@checkout_car@male_c@idles", "idle_a", 8.0f, -8.0f, -1, AnimationFlags.StayInEndFrame, 0.0f);


                // Show the faction clothing menu
                //factionClothingMenu.Visible = true;

                lockerRoomMenu.Visible = true;


                DisplayRadar(false);

            }
            else
            {
                // Reset the camera
                World.RenderingCamera = null;
                lockerRoomCam.Delete();

                // Clear the animation and unfreeze position
                //Game.PlayerPed.Task.ClearAnimation("weapons@first_person@aim_idle@generic@projectile@misc@flare@", "aim_low_loop");

                Game.PlayerPed.Task.ClearAnimation("anim@amb@carmeet@checkout_car@male_c@idles", "idle_a");

                API.FreezeEntityPosition(Game.PlayerPed.Handle, false);

                DisplayRadar(true);
            }
        }

        private void LoadInvisibleObjectModel()
        {
            int model = API.GetHashKey(INVISIBLE_OBJECT_MODEL);
            API.RequestModel((uint)model);
            while (!API.HasModelLoaded((uint)model))
            {
                Delay(100).Wait();
            }
            invisibleObjectHandle = API.CreateObject(model, objectPosition.X, objectPosition.Y, objectPosition.Z, true, true, false);
            //API.SetEntityVisible(invisibleObjectHandle, false, false); // Make the object invisible
        }


        public void LoadDepartmentClothing(string deptShort, bool isMale)
        {
            try
            {
                string filePath = "outfits.json";
                string resource = GetCurrentResourceName();

                if (resource != null && filePath != null)
                {
                    string fileContents = LoadResourceFile(resource, filePath);

                    if (fileContents != null)
                    {
                        int fileSizeInBytes = fileContents.Length;
                        double fileSizeInKilobytes = fileSizeInBytes / 1024.0;
                        double roundedFileSizeInKilobytes = Math.Round(fileSizeInKilobytes, 2);
                        Debug.WriteLine($"Loaded {filePath} Size: {roundedFileSizeInKilobytes}kB.");
                        Debug.WriteLine("------------------------------------------");
                        List<UniformData> uniforms = JsonConvert.DeserializeObject<List<UniformData>>(fileContents);

                        // Filter uniforms by deptShort and gender
                        string gender = isMale ? "Male" : "Female";
                        var filteredUniforms = uniforms.Where(u => u.Category2 == deptShort && u.Gender == gender).ToList();

                        // Create the faction clothing menu
                        //NativeMenu factionClothingMenu = new NativeMenu($"{deptShort} Clothing");
                        //pool.Add(factionClothingMenu);

                        foreach (var uniform in filteredUniforms)
                        {
                            NativeItem uniformMenuItem = new NativeItem(uniform.Name);
                            factionClothingMenu.Add(uniformMenuItem);

                            uniformMenuItem.Activated += (sender, args) =>
                            {
                                int ped = Game.Player.Character.Handle;

                                Debug.WriteLine($"uniformMenuItem.Activated: {sender} - {args}");

                                // HAT
                                string[] hatValues = uniform.Hat.Split(':');
                                int hatValue0 = int.Parse(hatValues[0]) - 1;
                                int hatValue1 = int.Parse(hatValues[1]) - 1;
                                Debug.WriteLine($"Hat: {hatValue0}, {hatValue1}");

                                if (hatValue0 == -1)
                                {
                                    Debug.WriteLine($"This uniform has no hats. ({hatValue0})");
                                    ClearPedProp(ped, 0);
                                }
                                else
                                {
                                    SetPedPropIndex(ped, 0, hatValue0, hatValue1, true);
                                }

                                // GLASSES
                                string[] glassesValues = uniform.Glasses.Split(':');
                                int glassesValue0 = int.Parse(glassesValues[0]) - 1;
                                int glassesValue1 = int.Parse(glassesValues[1]) - 1;
                                Debug.WriteLine($"Glasses: {glassesValue0}, {glassesValue1}");

                                if (glassesValue0 == -1)
                                {
                                    Debug.WriteLine($"This uniform has no glasses. ({glassesValue0})");
                                    ClearPedProp(ped, 6);
                                }
                                else
                                {
                                    SetPedPropIndex(ped, 6, glassesValue0, glassesValue1, true);
                                }

                                // EAR
                                string[] earValues = uniform.Ear.Split(':');
                                int earValue0 = int.Parse(earValues[0]) - 1;
                                int earValue1 = int.Parse(earValues[1]) - 1;
                                Debug.WriteLine($"Ear: {earValue0}, {earValue1}");

                                // WATCH
                                string[] watchValues = uniform.Watch.Split(':');
                                int watchValue0 = int.Parse(watchValues[0]) - 1;
                                int watchValue1 = int.Parse(watchValues[1]) - 1;
                                Debug.WriteLine($"Watch: {watchValue0}, {watchValue1}");

                                // MASK
                                string[] maskValues = uniform.Mask.Split(':');
                                int maskValue0 = int.Parse(maskValues[0]) - 1;
                                int maskValue1 = int.Parse(maskValues[1]) - 1;
                                Debug.WriteLine($"Mask: {maskValue0}, {maskValue1}");

                                // TOP
                                string[] topValues = uniform.Top.Split(':');
                                int topValue0 = int.Parse(topValues[0]) - 1;
                                int topValue1 = int.Parse(topValues[1]) - 1;
                                Debug.WriteLine($"Top: {topValue0}, {topValue1}");

                                // UPPER SKIN
                                string[] upperSkinValues = uniform.UpperSkin.Split(':');
                                int upperSkinValue0 = int.Parse(upperSkinValues[0]) - 1;
                                int upperSkinValue1 = int.Parse(upperSkinValues[1]) - 1;
                                Debug.WriteLine($"UpperSkin: {upperSkinValue0}, {upperSkinValue1}");

                                // DECAL
                                string[] decalValues = uniform.Decal.Split(':');
                                int decalValue0 = int.Parse(decalValues[0]) - 1;
                                int decalValue1 = int.Parse(decalValues[1]) - 1;
                                Debug.WriteLine($"Decal: {decalValue0}, {decalValue1}");

                                // UNDER COAT
                                string[] underCoatValues = uniform.UnderCoat.Split(':');
                                int underCoatValue0 = int.Parse(underCoatValues[0]) - 1;
                                int underCoatValue1 = int.Parse(underCoatValues[1]) - 1;
                                Debug.WriteLine($"UnderCoat: {underCoatValue0}, {underCoatValue1}");

                                // PANTS
                                string[] pantsValues = uniform.Pants.Split(':');
                                int pantsValue0 = int.Parse(pantsValues[0]) - 1;
                                int pantsValue1 = int.Parse(pantsValues[1]) - 1;
                                Debug.WriteLine($"Pants: {pantsValue0}, {pantsValue1}");

                                // SHOES
                                string[] shoesValues = uniform.Shoes.Split(':');
                                int shoesValue0 = int.Parse(shoesValues[0]) - 1;
                                int shoesValue1 = int.Parse(shoesValues[1]) - 1;
                                Debug.WriteLine($"Shoes: {shoesValue0}, {shoesValue1}");

                                // ACCESSORIES
                                string[] accessoriesValues = uniform.Accessories.Split(':');
                                int accessoriesValue0 = int.Parse(accessoriesValues[0]) - 1;
                                int accessoriesValue1 = int.Parse(accessoriesValues[1]) - 1;
                                Debug.WriteLine($"Accessories: {accessoriesValue0}, {accessoriesValue1}");

                                // ARMOR
                                string[] armorValues = uniform.Armor.Split(':');
                                int armorValue0 = int.Parse(armorValues[0]) - 1;
                                int armorValue1 = int.Parse(armorValues[1]) - 1;
                                Debug.WriteLine($"Armor: {armorValue0}, {armorValue1}");

                                // PARACHUTE
                                string[] parachuteValues = uniform.Parachute.Split(':');
                                int parachuteValue0 = int.Parse(parachuteValues[0]) - 1;
                                int parachuteValue1 = int.Parse(parachuteValues[1]) - 1;
                                Debug.WriteLine($"Parachute: {parachuteValue0}, {parachuteValue1}");

                                SetPedComponentVariation(ped, 1, maskValue0, maskValue1, 0);
                                SetPedComponentVariation(ped, 3, upperSkinValue0, upperSkinValue1, 0);
                                SetPedComponentVariation(ped, 4, pantsValue0, pantsValue1, 0);
                                SetPedComponentVariation(ped, 5, parachuteValue0, parachuteValue1, 0);
                                SetPedComponentVariation(ped, 6, shoesValue0, shoesValue1, 0);
                                SetPedComponentVariation(ped, 7, accessoriesValue0, accessoriesValue1, 0);
                                SetPedComponentVariation(ped, 8, underCoatValue0, underCoatValue1, 0);
                                SetPedComponentVariation(ped, 9, armorValue0, armorValue1, 0);
                                SetPedComponentVariation(ped, 10, decalValue0, decalValue1, 0);
                                SetPedComponentVariation(ped, 11, topValue0, topValue1, 0);
                            };
                        }
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

        private void OnDepartmentDataReceived(string json)
        {
            Debug.WriteLine($"Received json from server: {json}");

            try
            {
                // Deserialize the JSON string into an instance of DepartmentData
                var departmentData = JsonConvert.DeserializeObject<DepartmentData>(json);

                if (departmentData != null)
                {
                    string deptNameShort = departmentData.DeptNameShort;

                    deptShort = deptNameShort;

                    Debug.WriteLine($"deptShort {deptShort}");

                    // Handling locker room coordinates
                    foreach (var coordinate in departmentData.LockerRooms)
                    {
                        Vector3 lockerRoomLocation = new Vector3(coordinate.X, coordinate.Y, coordinate.Z);
                        Debug.WriteLine($"Locker Room Coordinate: {lockerRoomLocation}");

                        lockerRoomCoordinates.Add(lockerRoomLocation);
                        AddStaticText(lockerRoomLocation, $"{deptNameShort} Locker - Press [E]");

                        Blip lockerBlip = World.CreateBlip(lockerRoomLocation);
                        lockerBlip.Sprite = BlipSprite.PoliceStation; // Set the blip sprite to a police station or any other suitable icon
                        lockerBlip.Name = "Faction Locker"; // Set the blip name
                        lockerBlips.Add(lockerBlip); // Add the blip to the list

                    }

                    // Handling whitelisted weapon menu
                    foreach (var WeaponHash in departmentData.AllowedWeapons)
                    {
                        NativeItem lockerFactionWeapons = new NativeItem(WeaponHash.WeaponName, $"Ammo: {WeaponHash.Ammo}, MagSize: {WeaponHash.MagSize}");
                        factionGear.Add(lockerFactionWeapons);

                        lockerFactionWeapons.Activated += async (sender, args) =>
                        {
                            // Hide the current menu
                            lockerRoomMenu.Visible = false;

                            // Log the action for debugging
                            Debug.WriteLine($"Activated: {WeaponHash.WeapHash}");

                            // Load the weapon model first
                            API.RequestWeaponAsset(WeaponHash.WeapHash, 31, 0);
                            while (!API.HasWeaponAssetLoaded(WeaponHash.WeapHash))
                            {
                                await Delay(100);
                            }

                            // Give the player the weapon and put it in his hands
                            GiveWeaponToPed(Game.PlayerPed.Handle, WeaponHash.WeapHash, WeaponHash.Ammo, false, true);
                            SetCurrentPedWeapon(Game.PlayerPed.Handle, WeaponHash.WeapHash, true);
                            DisplayNotification($"Added {WeaponHash.WeaponName} to inventory.", 5);

                            // Play locker room animation
                            API.RequestAnimDict("weapons@first_person@aim_idle@remote_clone@pistol@shared@core");
                            while (!API.HasAnimDictLoaded("weapons@first_person@aim_idle@remote_clone@pistol@shared@core"))
                            {
                                await BaseScript.Delay(100);
                            }
                            await Game.PlayerPed.Task.PlayAnimation("weapons@first_person@aim_idle@remote_clone@pistol@shared@core", "settle_med", 8.0f, -8.0f, -1, AnimationFlags.StayInEndFrame, 0.0f);





                            await Delay(0);
                        };


                        }

                }
                else
                {
                    Debug.WriteLine("Failed to deserialize JSON into DepartmentData.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deserializing JSON: {ex.Message}");
            }
        }

        private void SetPlayerAsAdmin()
        {
            isPlayerAdmin = true;
            Debug.WriteLine($"You are now recognized as an admin: {isPlayerAdmin}");
            DisplayNotification("You have been authentified as a POLSIM Administrator.", 30);
        }

        private void OnDataReceivedFromNUI(IDictionary<string, object> data)
        {
            if (data.ContainsKey("type"))
            {
                Debug.WriteLine("Data received from NUI: " + data["type"]);

                // Example: Handling specific data types
                switch (data["type"].ToString())
                {
                    case "nameCheck":
                        if (data.ContainsKey("firstName") && data.ContainsKey("lastName"))
                        {
                            string firstName = data["firstName"].ToString();
                            string lastName = data["lastName"].ToString();

                            Debug.WriteLine($"First Name: {firstName}, Last Name: {lastName}");

                            // Relaying data to server
                            TriggerServerEvent("sendToServer", data);
                        }
                        else
                        {
                            Debug.WriteLine("Missing firstName or lastName in data.");
                        }
                        break;

                    case "arrestListUpdate":
                            Debug.WriteLine("Requesting arrests list from server...");
                            TriggerServerEvent("sendToServer", data);
                        break;

                    default:
                        Debug.WriteLine("Unknown type in data.");
                        break;
                }
            }
            else
            {
                Debug.WriteLine("Key 'type' not found in data.");
            }
        }


        private void OnNameCheckResponse(string response)
        {
            Debug.WriteLine($"[DATA] Received response from server: {response}");

            // Send the data to NUI (browser)
            SendNUIMessage(new Dictionary<string, object>
        {
            { "type", "nameCheckResponse" },
            { "data", response }
        });
        }

        private void OnArrestListUpdateResponse(string response)
        {
            Debug.WriteLine($"[DATA] Received response from server: {response}");

            // Send the data to NUI (browser)
            SendNUIMessage(new Dictionary<string, object>
        {
            { "type", "arrestListUpdate" },
            { "data", response }
        });
        }

        private void SendNUIMessage(IDictionary<string, object> message)
        {
            // Assuming SendNUIMessage is a method to send messages to NUI in your client-side code
            API.SendNuiMessage(Newtonsoft.Json.JsonConvert.SerializeObject(message));
        }

        /*private void OnReceiveDepartmentData(string departmentDataJson)
        {
            try
            {

                // Deserialize the JSON string into a list of DepartmentData objects
                var departmentDataList = JsonConvert.DeserializeObject<List<DepartmentData>>(departmentDataJson);

                foreach (var departmentData in departmentDataList)
                {
                    // Log the received department data for debugging purposes
                    Debug.WriteLine($"Receiving: {departmentData.DeptNameFull}");

                    // Prepare the data to be sent to NUI
                    var departmentUpdateData = new Dictionary<string, object>
            {
                { "departmentName", departmentData.DeptNameFull },
                { "deptLogo", departmentData.DeptLogo },
                { "divisions", departmentData.Divisions }, // Use the divisions list directly
                { "ranks", departmentData.Ranks } // Use the ranks list directly
            };

                    // Send the data to NUI (browser)
                    SendNUIMessage(new Dictionary<string, object>
            {
                { "type", "myDepartmentUpdate" },
                { "data", departmentUpdateData }
            });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error parsing department data: {ex.Message}");
            }
        }*/










        // =================================================================================
        // RequestModel()
        // Helper function to pre-load models
        // =================================================================================
        private async void RequestModel(int model)
        {
            while (!HasModelLoaded((uint)model))
            {
                await Delay(0);
            }
            await Delay(100);
        }

        private async Task ShowNotification(string text, string color, string textcolor, int time)
        {
            Debug.WriteLine("ShowNotification()");
            // Call the NUI handler to display the notification
            TriggerEvent("fs_dependencies:ShowNotification", text, color, textcolor, time);
            await Delay(0);
        }

        private async Task ShowInteraction(string text, string color, string textcolor)
        {
            Debug.WriteLine("ShowInteraction()");
            // Call the NUI handler to display the notification
            TriggerEvent("ShowInteraction", text, color, textcolor);
            await Delay(0);
        }

        private async Task ShowNotificationAsync(string text, string color, string textColor, int time)
        {
            Debug.WriteLine("ShowNotificationAsync()");

            // Call the NUI handler to display the notification
            TriggerEvent("fs_dependencies:ShowNotification", text, color, textColor, time);

            // Wait for the specified duration
            await Delay(time * 1000); // Convert seconds to milliseconds
        }

        private async Task ShowNotify(string text, int color, int textcolor, int time)
        {
            TriggerEvent("fs_dependencies:ShowNotification", text, color, textcolor, time);
            await Delay(time * 1000);
            HideNotification();
        }

        private void HideNotification()
        {
            TriggerEvent("fs_dependencies:HideNotification");
        }


        // =================================================================================
        // ProcessCuffModel()
        // This handles attaching or detaching handcuff models to NPCs
        // =================================================================================
        private async void ProcessCuffModel(bool remove, int type)
        {
            if (remove)
            {
                DetachEntity(cuffs, true, true);
                DeleteEntity(ref cuffs);
            }
            else
            {
                var coords = GetEntityCoords(closestPed.Handle, false);
                cuffs = CreateObject(GetHashKey("p_cs_cuffs_02_s"), coords.X, coords.Y, coords.Z-10f, true, true, true);
                await Delay(100);
                var networkId = ObjToNet(cuffs);
                SetNetworkIdExistsOnAllMachines(networkId, true);
                SetNetworkIdCanMigrate(networkId, false);
                NetworkSetNetworkIdDynamic(networkId, true);
                if (type == 0)
                {
                    AttachEntityToEntity(cuffs, closestPed.Handle, GetPedBoneIndex(closestPed.Handle, 60309), -0.055f, 0.06f, 0.04f, 265.0f, 155.0f, 80.0f, true, false, false, false, 0, true);
                }
                else if (type == 1)
                {
                    AttachEntityToEntity(cuffs, closestPed.Handle, GetPedBoneIndex(closestPed.Handle, 60309), -0.058f, 0.005f, 0.090f, 290.0f, 95.0f, 120.0f, true, false, false, false, 0, true);
                }
            }
        }

        private void OnPlayerSpawned()
        {
            // Trigger a server event to notify that a player has spawned
            TriggerServerEvent("playerSpawned2");
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() == resourceName)
            {
                // Your code to run on resource start
                Debug.WriteLine("Resource '" + resourceName + "' has been re-started on the client side.");
                //TriggerServerEvent("playerSpawned2");
            }
        }


        // =================================================================================
        // CalculateDistance()
        // Helper function, calculates distance between 2 set of coordinates
        // =================================================================================
        private double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        // =================================================================================
        // OnDispatchDataReceived()
        // Receives dispatching data from the server when a callout has been generated
        // =================================================================================
        public void OnDispatchDataReceived(string currentTime, string locationString, int postalString, string calloutTypeString, string commentString, int calloutPriority)
        {
            if(onDuty)
            {
                DisplayNotification($"{calloutTypeString}", 120);
                DisplayNotification($"Priority: {calloutPriority}", 120);
                DisplayNotification($"{commentString}", 120);
                DisplayNotification($"Postal: {postalString}", 120);
                DisplayNotification($"Location: {locationString}", 120);
            }

            // Update the NUI Dispatch page even if off duty, that way if you go on duty you will still see latest calls
            TriggerEvent("addDispatchEntry", currentTime, locationString, postalString, calloutTypeString, commentString, calloutPriority);

        }


        private void updateRecentCallsPage(string jsonString)
        {
            try
            {
                // Deserialize the JSON string into a custom class or dynamic object
                dynamic data = JsonConvert.DeserializeObject(jsonString);

                // Access and process the data as needed
                string callType = data.callType;
                string callerName = data.callerName;
                string callLocation = data.callLocation;
                int callPriority = data.callPriority;

                // Example: Update UI or perform other actions based on the received data
                Debug.WriteLine($"Received recent call - Type: {callType}, Caller: {callerName}, Location: {callLocation}, Priority: {callPriority}");

                // TriggerEvent javascript for NUI
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error processing recent calls update: {ex.Message}");
            }
        }

        // =================================================================================
        // SavePlayerPosition()
        // HELPER DEBUG FUNCTION
        // =================================================================================
        private void SavePlayerPosition(int source, List<object> args, string rawCommand)
        {
            // Get the player's position
            Vector3 position = Game.PlayerPed.Position;

            // Convert position to string
            //string positionString = $"{position.X}, {position.Y}, {position.Z}";

            // Create a data object to hold the position and additional information
            var data = new
            {
                street = streetName,
                crossroad = crossRoadName,
                postal = nearestPostalCode,
                X = position.X,
                Y = position.Y,
                Z = position.Z
            };

            // Convert data object to JSON string
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);

            // Output JSON string using Debug.WriteLine
            Debug.WriteLine(jsonString);

            // Trigger server event and pass the JSON string as an argument
            TriggerServerEvent("SavePlayerPosition", jsonString);

            DisplayNotification("Position saved.", 2);
        }


        // =================================================================================
        // HandleButtonClicked()
        // This is for the NUI / MDC
        // =================================================================================
        private void HandleButtonClicked()
        {
            // This function will be called when the button in the NUI is clicked
            Debug.WriteLine("Button clicked!");
        }


        // =================================================================================
        // GiveControlBackAfterMDCExit()
        // NUI
        // =================================================================================
        private void GiveControlBackAfterMDCExit()
        {
            // This function will be called when the button in the NUI is clicked
            Debug.WriteLine("Received mdcExit request from NUI.");
        }


        // =================================================================================
        // DisplayNotification()
        // Handles displaying game notifications
        // =================================================================================
        public async void DisplayNotification(string message, int durationInSeconds)
        {
            SetNotificationTextEntry("STRING");
            AddTextComponentString(message);
            int notificationHandle = DrawNotification(false, false);

            await Delay(durationInSeconds * 1000);

            RemoveNotification(notificationHandle);
        }


        // =================================================================================
        // ScheduleTrafficEvent()
        // Picks a random time between minimum & maximum event time then schedules next event
        // =================================================================================
        private async void ScheduleTrafficEvent()
        {
            // Check if the player is in a vehicle and on duty
            if (Game.PlayerPed.IsInVehicle() && onDuty)
            {
                int randomInterval = random.Next(minTrafficEventTime, maxTrafficEventTime);
                double intervalInSeconds = randomInterval / 1000.0; // Convert milliseconds to seconds
                Debug.WriteLine($"Scheduling a traffic event, starting in {intervalInSeconds} seconds.");
                await Delay(randomInterval);

                HandleGenerateRandomEventCommand();
                ScheduleTrafficEvent();
            }
            else
            {
                //Debug.WriteLine("Not in vehicle, traffic events disabled");
                int randomInterval = random.Next(minTrafficEventTime, maxTrafficEventTime);
                await Delay(randomInterval);
                ScheduleTrafficEvent();
            }
        }

        // =================================================================================
        // SelectRandomEvent()
        // This is for the random traffic events system
        // =================================================================================
        private string SelectRandomEvent(Dictionary<string, double> eventProbabilities)
        {
            double randomNumber = random.NextDouble();
            double cumulativeProbability = 0.0;

            foreach (var kvp in eventProbabilities)
            {
                cumulativeProbability += kvp.Value;
                if (randomNumber < cumulativeProbability)
                {
                    return kvp.Key; // Return the selected event
                }
            }

            return eventProbabilities.Keys.Last(); // Return the last event if none is selected
        }

        public Task Draw3dText(float x, float y, float z, string text, float size = 1.0f, int r = 255, int g = 255, int b = 255, int a = 215)
        {
            float _x = 0;
            float _y = 0;
            var onScreen = World3dToScreen2d(x, y, z, ref _x, ref _y);
            Vector3 pCoords = GetGameplayCamCoords();

            var distance = GetDistanceBetweenCoords(pCoords.X, pCoords.Y, pCoords.Z, x, y, z, true);
            var txtScale = (1 / distance) * 2;
            var fov = (1 / GetGameplayCamFov()) * 100;
            var scale = txtScale * fov * size;

            if (onScreen)
            {
                SetTextScale(0.0f, scale);
                SetTextFont(4);
                SetTextProportional(true);
                SetTextColour(r, g, b, a);
                SetTextDropShadow();
                SetTextEdge(0, 0, 0, 0, 150);
                SetTextDropShadow();
                SetTextOutline();
                SetTextEntry("STRING");
                SetTextCentre(true);
                AddTextComponentString(text);
                DrawText(_x, _y);
            }

            return Task.FromResult(0);
        }

        // Method to add a 3D text at a static location
        public void AddStaticText(Vector3 position, string text)
        {
            if (!_staticTexts.ContainsKey(position))
            {
                _staticTexts.Add(position, text);
            }
        }

        // Method to draw 3D texts that are within range
        public async Task DrawStaticTextsInRange()
        {
            //Vector3 playerPosition = GetEntityCoords(PlayerPedId(), true);
            Vector3 playerPosition = Game.PlayerPed.Position;
            foreach (var staticText in _staticTexts)
            {
                Vector3 position = staticText.Key;
                string text = staticText.Value;

                float distance = Vdist(playerPosition.X, playerPosition.Y, playerPosition.Z, position.X, position.Y, position.Z);

                // Adjust this range as needed
                float range = 10f;
                if (distance <= range)
                {
                    await Draw3dText(position.X, position.Y, position.Z, text);
                }
            }
        }

        // Helper method to clear the drawn text from the screen
        private void ClearDraw3dText()
        {
            SetTextEntry("STRING");
            ClearPrints();
        }


        // =================================================================================
        // ToggleDuty()
        // This function is ran when the player goes on or off duty
        // =================================================================================
        private async void ToggleDuty()
        {
            onDuty = !onDuty; // Toggle onDuty status

            if (onDuty)
            {
                //DisplayNotification("You are now on duty.", 5);
                await ShowNotification($"<i class=\"fa fa-gavel\"> You are now on duty.</i>", "#35889e", "##ffff", 5);

                // Draw the 3D text at the player's position
                //await Draw3dText(playerPosition.X, playerPosition.Y, playerPosition.Z, "Test Text", 0.35f, 255, 255, 255, 215, 10.0f);

                //await ShowInteraction($"[E] Open menu", "#9f2fb2", "#ffff");

                // Create blips for jails
                foreach (Jail jail in jails)
                {
                    Vector3 jailLocation = new Vector3(jail.X, jail.Y, jail.Z);
                    Blip jailBlip = World.CreateBlip(jailLocation);
                    jailBlip.Sprite = BlipSprite.Custody; // Set the blip sprite to a police station or any other suitable icon
                    jailBlip.Name = "Jail"; // Set the blip name
                    jailBlips.Add(jailBlip); // Add the blip to the list
                }
            }
            else
            {
                //DisplayNotification("You are now off duty.", 5);
                await ShowNotification($"<i class=\"fa fa-gavel\"> You are now off duty. Goodbye!</i>", "#35889e", "##ffff", 5);

                // Remove blips for jails
                foreach (Blip blip in jailBlips)
                {
                    blip.Delete();
                }

                // Clear the list of jail blips
                jailBlips.Clear();
            }
        }

        // =================================================================================
        // HandleDutyStatusText()
        // Handles displaying text at the bottom of screen such as postal, location etc
        // =================================================================================
        private void HandleDutyStatusText()
        {
            string onDutyText = onDuty ? "On Duty" : "Off Duty";

            // Define fixed text position at the bottom left corner
            float x = 0.175f; // Adjust as needed
            float y = 0.973f; // Adjust as needed

            // if crossroad is null, do not display / crossroad
            if (crossRoadName == "ERR" || crossRoadName == null || crossRoadName == "")
            {
                string displayText = $"| Status: {onDutyText} | Nearest Postal: {nearestPostalCode} | {streetName} |";

                // Display the text at the specified position
                API.SetTextFont(0);
                API.SetTextProportional(true);
                API.SetTextScale(0.25f, 0.25f);
                API.SetTextColour(255, 255, 255, 255);
                API.SetTextEntry("STRING");
                API.SetTextCentre(false);
                API.SetTextOutline();
                API.AddTextComponentString(displayText);
                API.DrawText(x, y);
            }
            else
            {
                string displayText = $"| Status: {onDutyText} | Nearest Postal: {nearestPostalCode} | {streetName} / {crossRoadName} |";
                API.SetTextFont(0);
                API.SetTextProportional(true);
                API.SetTextScale(0.25f, 0.25f);
                API.SetTextColour(255, 255, 255, 255);
                API.SetTextEntry("STRING");
                API.SetTextCentre(false);
                API.SetTextOutline();
                API.AddTextComponentString(displayText);
                API.DrawText(x, y);
            }
        }

        private List<Vehicle> GetNearbyVehicles(Vector3 position, float radius)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            foreach (Vehicle vehicle in World.GetAllVehicles())
            {
                if (vehicle.Position.DistanceToSquared(position) < radius)
                {
                    vehicles.Add(vehicle);
                }
            }
            return vehicles;
        }


        // =================================================================================
        // OnTick()
        // Runs code every frame
        // =================================================================================
        private async Task OnTick()
        {
            // Processing LemonUI menus
            pool.Process();

            Ped[] nearbyPeds = World.GetAllPeds();

            // Traffic slow down when emergency lights are on;
            /*int playerPed = Game.PlayerPed.Handle;
            Ped[] nearbyPeds = World.GetAllPeds();

            if (IsPedInAnyPoliceVehicle(playerPed) && IsVehicleSirenOn(Game.PlayerPed.CurrentVehicle.Handle))
            {
                foreach (Ped ped in nearbyPeds)
                {
                    if (IsPedInAnyVehicle(ped.Handle, false))
                    {
                        int vehicle = GetVehiclePedIsIn(ped.Handle, false);
                        if (GetPedInVehicleSeat(vehicle, -1) == ped.Handle)
                        {
                            if (!adjustedVehicles.ContainsKey(vehicle))
                            {
                                float originalSpeed = GetVehicleEstimatedMaxSpeed(vehicle);
                                float reducedSpeed = originalSpeed * 0.20f;
                                SetDriveTaskMaxCruiseSpeed(ped.Handle, reducedSpeed);
                                adjustedVehicles[vehicle] = originalSpeed;
                                //Debug.WriteLine($"Original speed: {originalSpeed} - Reduced speed: {reducedSpeed}");
                            }
                        }
                    }
                }
            }
            else
            {
                // Reset the speed of vehicles when police lights are turned off
                foreach (var entry in adjustedVehicles)
                {
                    int vehicle = entry.Key;
                    float originalSpeed = entry.Value;

                    int driverPed = GetPedInVehicleSeat(vehicle, -1);
                    if (driverPed != 0)
                    {
                        SetDriveTaskMaxCruiseSpeed(driverPed, originalSpeed);
                        //Debug.WriteLine($"Reset speed for vehicle {vehicle} to original speed: {originalSpeed}");
                    }
                }
                adjustedVehicles.Clear();
            }*/


            // Non-Lethal Stun Gun
            /*Ped character = Game.Player.Character;

            if (character.Exists() && !character.IsInVehicle() && character.IsShooting)
            {

                int weaponHash = GetSelectedPedWeapon(character.Handle);

                //DisplayNotification($"Stun shot 1 weapon: {weaponHash}", 1);

                if (weaponHash == 1171102963)
                {
                    DisplayNotification("Stun shot", 1);
                    foreach (Ped nearbyPed in nearbyPeds)
                    {
                        if (nearbyPed.Exists() && nearbyPed != character && nearbyPed.IsOnScreen) // !Game.IsMissionActive
                        {
                            Function.Call(Hash.SET_PED_SUFFERS_CRITICAL_HITS, nearbyPed, false);
                            if (!Function.Call<bool>(Hash.IS_ENTITY_DEAD, nearbyPed))
                            {
                                nearbyPed.CanWrithe = false;
                                if (nearbyPed.IsBeingStunned)
                                {
                                    nearbyPed.HealthFloat = nearbyPed.HealthFloat;
                                    nearbyPed.ClearBloodDamage();
                                    DisplayNotification("Being stunned", 1);
                                }
                                if (nearbyPed.HealthFloat == 0.0f && nearbyPed.IsBeingStunned)
                                {
                                    nearbyPed.HealthFloat = nearbyPed.MaxHealthFloat;
                                    nearbyPed.ClearBloodDamage();
                                    DisplayNotification("Clear blood damage", 1);
                                }
                                if (nearbyPed.IsInjured)
                                {
                                    DisplayNotification("Reviving ped", 1);
                                    Function.Call(Hash.RESURRECT_PED, nearbyPed);
                                }
                            }
                        }
                    }
                }
            }
            else if (character.Exists() && character.IsInVehicle() && character.IsAiming && character.Weapons.Current.Hash == WeaponHash.StunGun)
            {
                float speed = character.CurrentVehicle.Speed;
                float radius = speed >= 50.0f ? 10f : (speed >= 30.0f ? 20f : 40f);
                foreach (Ped nearbyPed in nearbyPeds)
                {
                    if (nearbyPed.Exists() && nearbyPed != character && nearbyPed.IsOnScreen && !Game.IsMissionActive)
                    {
                        Function.Call(Hash.SET_PED_SUFFERS_CRITICAL_HITS, nearbyPed, false);
                        if (!Function.Call<bool>(Hash.IS_ENTITY_DEAD, nearbyPed))
                        {
                            nearbyPed.CanWrithe = false;
                            if (nearbyPed.IsBeingStunned)
                            {
                                nearbyPed.HealthFloat = nearbyPed.HealthFloat;
                                nearbyPed.ClearBloodDamage();
                            }
                            if (nearbyPed.HealthFloat == 0.0f && nearbyPed.IsBeingStunned)
                            {
                                nearbyPed.HealthFloat = nearbyPed.MaxHealthFloat;
                                nearbyPed.ClearBloodDamage();
                            }
                            if (nearbyPed.IsInjured)
                            {
                                Function.Call(Hash.RESURRECT_PED, nearbyPed);
                            }
                        }
                    }
                }
            }*/


        // Get the player's position
        Vector3 playerPos = Game.PlayerPed.Position;

        // Stop AI Panic, only when on duty
        if(onDuty)
        {
            foreach (Ped ped in nearbyPeds)
            {
                SetBlockingOfNonTemporaryEvents(ped.Handle, true);
            }
        }


        // Ped Takedown Menu (When holding E and aiming at a NPC)
        if (Game.IsControlPressed(0, Control.Context))
        {
            // Check if the player is aiming a weapon
            if (Game.Player.Character.IsAiming)
            {
                // Check if there is a targeted ped
                int entity = 0;
                if (GetEntityPlayerIsFreeAimingAt(Game.Player.Handle, ref entity))
                {
                    if (!isHoldingE)
                    {
                        isHoldingE = true;
                        startTime = DateTime.UtcNow;
                    }

                    // Calculate the progress
                    var elapsed = (DateTime.UtcNow - startTime).TotalSeconds;
                    var progress = Math.Min(elapsed / 1.2, 1.0);

                    // Draw the progress bar
                    DrawProgressBar(0.5f, 0.55f, 0.04f, 0.009f, progress);

                    if (progress >= 1.0 && !progressCompleted)
                    {
                        Ped targetPed = new Ped(entity);
                        DetermineSuspectBehavior(targetPed);
                        progressCompleted = true;
                        isHoldingE = false; // Prevent further progress calculation
                    }
                }
                else
                {
                    isHoldingE = false;
                    progressCompleted = false; // Reset progressCompleted if there's no targeted ped
                }
            }
            else
            {
                isHoldingE = false;
                progressCompleted = false; // Reset progressCompleted if not aiming
            }
        }
        else
        {
            isHoldingE = false;
            progressCompleted = false; // Reset progressCompleted when 'E' is released
        }



            /*
                        var playerPed = Game.Player.Character;
                        var nearbyPeds = World.GetAllPeds();

                        // Variable to hold the closest valid ped
                        //Ped closestPed = null;

                        // Loop through nearbyPeds to find the closest valid one
                        foreach (var ped in nearbyPeds)
                        {
                            // Check if the ped is not the playerPed and within distance
                            if (ped != playerPed && playerPed.Position.DistanceToSquared(ped.Position) < 5.0f) // 5.0f units
                            {
                                closestPed = ped;
                                break; // Break after finding the first valid ped
                            }
                        }

                        // If a valid ped is found, display options
                        if (closestPed != null)
                        {
                            int endIndex = Math.Min(startIndex + optionsToShow, options.Length);

                            // Draw the options
                            for (int i = startIndex; i < endIndex; i++)
                            {
                                Color color = i == selectedOption ? Color.FromArgb(255, 255, 255, 0) : Color.FromArgb(255, 255, 255, 255);
                                Draw3dText(closestPed.Position.X, closestPed.Position.Y, closestPed.Position.Z + 1.1f - ((i - startIndex) * 0.095f), options[i], 0.5f, color.R, color.G, color.B, 175);
                            }

                            // Handle option selection
                            if (Game.IsControlJustReleased(0, Control.Context)) // E key
                            {
                                Debug.WriteLine($"closestPed={closestPed}");
                                HandleOptionSelection(selectedOption);
                            }

                            // Check for Page Up and Page Down key presses
                            if (Game.IsControlJustReleased(0, Control.SpecialAbilityPC)) // CAPS LOCK
                            {
                                Debug.WriteLine("Interaction menu: Up");
                                if (selectedOption > 0)
                                {
                                    selectedOption--;
                                    if (selectedOption < startIndex)
                                    {
                                        startIndex = selectedOption;
                                    }
                                }
                            }

                            if (Game.IsControlJustReleased(0, Control.CreatorMenuToggle)) // LSHIFT
                            {
                                Debug.WriteLine("Interaction menu: Down");
                                if (selectedOption < options.Length - 1)
                                {
                                    selectedOption++;
                                    if (selectedOption >= endIndex)
                                    {
                                        startIndex = selectedOption - optionsToShow + 1;
                                    }
                                }
                            }

                            // Ensure the selected option is highlighted after changing selection
                            endIndex = Math.Min(startIndex + optionsToShow, options.Length);
                            for (int i = startIndex; i < endIndex; i++)
                            {
                                Color color = i == selectedOption ? Color.FromArgb(255, 255, 255, 0) : Color.FromArgb(255, 255, 255, 255);
                                Draw3dText(closestPed.Position.X, closestPed.Position.Y, closestPed.Position.Z + 1.1f - ((i - startIndex) * 0.095f), options[i], 0.5f, color.R, color.G, color.B, 175);
                            }
                        }


                        await Task.FromResult(0);*/

            Vector3 playerLocation = Game.Player.Character.Position;

            if (IsPlayerNearLockerRoom(playerLocation) && Game.IsControlJustPressed(0, Control.Pickup) && !lockerRoomActive) // Control.Context is usually the "E" key
            {
                Debug.WriteLine("Player is near a locker room and pressed [E].");
                // Add your interaction logic here, e.g., open locker room menu
                HandleLockerRoom();
            }

            // Throttle 3D text updates
            if ((DateTime.Now - last3DTextUpdateTime).TotalMilliseconds >= textUpdateInterval)
            {
                await DrawStaticTextsInRange();
                last3DTextUpdateTime = DateTime.Now;
            }

            // If the ped menu isn't displayed and pedInteractionBlip exists, delete it
            if (!pedMenu.Visible)
            {
                pedInteractionBlip?.Delete();
            }

            // Throttle updates for player position, nearest postal code, and street names
            if ((DateTime.Now - lastUpdateTime).TotalMilliseconds >= updateInterval)
            {
                UpdatePlayerInfo();
                lastUpdateTime = DateTime.Now;
            }

            if (onDuty) // Only display bottom text if player is on duty
            {
                HandleDutyStatusText();
            }

            // Ped Interaction Menu stuff that needs to run every frame
            HandlePedDragging();

            // KEYBIND: F1 - Ped Interaction Menu
            if (Game.IsControlJustReleased(0, Control.Detonate) && IsPlayerOnDuty())
            {
                FindClosestPed();
            }

            // Handle Traffic Stop Logic
            await HandleTrafficStop();

            // Vehicle Interaction Menu, checks if E was pressed, if player is on duty and player is not in a vehicle.
            if (Game.IsControlPressed(0, Control.Pickup) && IsPlayerOnDuty() && !Game.Player.Character.IsInVehicle() && !Game.Player.Character.IsAiming)
            {
                HandleVehicleInteractionMenu();
            }

            // Handle Shift Key Press for Traffic Stop
            if (Game.PlayerPed.IsInVehicle() && Game.IsControlPressed(0, Control.Sprint) && !isDriverStoppedAtTrafficStop && IsPlayerOnDuty())
            {
                await FindAndHandleNearbyVehicle();
            }

            // Additional Traffic Stop Behavior
            if (isPulledOver)
            {
                HandlePulledOverDriver();
            }
        }

        // Determine suspect behavior when using takedown menu (E)
        private async void DetermineSuspectBehavior(Ped suspect)
        {
            Random random = new Random();

            // Initialize or get the status of the suspect
            if (!npcStatus.ContainsKey(suspect.Handle))
            {
                //DisplayNotification("Newly encountered NPC, saving data...", 1);
                npcStatus[suspect.Handle] = new SuspectStatus(false, false, false);
            }

            var status = npcStatus[suspect.Handle];

            // Check if the suspect has already surrendered
            if (status.HasSurrendered)
            {
                RequestAnimDict("random@arrests");
                RequestAnim("kneeling_arrest_idle");
                await Delay(100);
                TaskPlayAnim(suspect.Handle, "random@arrests", "kneeling_arrest_idle", 8.0f, -8, -1, (int)AnimationFlags.Loop, 1.0f, false, false, false);
                //DisplayNotification("Foot takedown: Suspect is kneeling. (stage 2)", 5);
                return;
            }

            // If not armed, process the weapon assignment logic
            if (!status.IsArmed)
            {
                ClearPedTasks(suspect.Handle);
                SetEntityAsMissionEntity(suspect.Handle, true, true);
                TaskSetBlockingOfNonTemporaryEvents(suspect.Handle, true);
                //DisplayNotification("First time interacting with ped...", 1);

                bool isArmed = false;

                // Determine if the suspect has a firearm
                if (!isArmed && random.NextDouble() <= firearmChance)
                {
                    // Give the suspect a firearm
                    WeaponHash[] firearms = { WeaponHash.Pistol, WeaponHash.CombatPistol, WeaponHash.MicroSMG };
                    suspect.Weapons.Give(firearms[random.Next(firearms.Length)], 100, true, true);
                    isArmed = true;
                    //DisplayNotification("Suspect is carrying a firearm.", 1);
                }

                // Determine if the suspect has a melee weapon
                if (!isArmed && random.NextDouble() <= meleeChance)
                {
                    // Give the suspect a melee weapon
                    WeaponHash[] meleeWeapons = { WeaponHash.SwitchBlade, WeaponHash.Knife, WeaponHash.KnuckleDuster };
                    suspect.Weapons.Give(meleeWeapons[random.Next(meleeWeapons.Length)], 1, true, true);
                    isArmed = true;
                    //DisplayNotification("Suspect is carrying a melee weapon.", 1);
                }

                // Update the dictionary to mark this NPC as armed
                npcStatus[suspect.Handle].IsArmed = isArmed;
            }

            // The rest of the code runs every time
            // Takedown Menu: Suspect in Vehicle
            if (suspect.IsInVehicle())
            {
                // Determine if suspect is aggressive: (drive-by)
                if (random.NextDouble() <= suspectAggression)
                {
                    //DisplayNotification("Car takedown: Suspect combative.", 5);
                    Vector3 playerPos = Game.PlayerPed.Position;
                    TaskDriveBy(suspect.Handle, Game.PlayerPed.Handle, 0, (int)playerPos.X, (int)playerPos.Y, (int)playerPos.Z, 100f, 100, true, (uint)FiringPattern.FullAuto);
                    return;
                }

                if (random.NextDouble() <= fleeChance)
                {
                    // Suspect flees
                    //DisplayNotification("Car takedown: Suspect is fleeing.", 5);
                    Function.Call(Hash.TASK_SMART_FLEE_PED, suspect.Handle, Game.Player.Character.Handle, 100.0f, -1, false, false);
                    return;
                }
                // Default action: hands up
                TaskLeaveAnyVehicle(suspect.Handle, 0, 256);
                await Delay(500);
                Function.Call(Hash.TASK_HANDS_UP, suspect.Handle, -1, Game.Player.Character.Handle, -1, true);
                //DisplayNotification("Car takedown: Suspect surrendering.", 5);
                npcStatus[suspect.Handle].HasSurrendered = true;
                return;
            }

            // Determine if the suspect is aggressive
            if (random.NextDouble() <= suspectAggression)
            {
                //DisplayNotification("Foot takedown: Suspect combative.", 5);
                TaskCombatPed(suspect.Handle, Game.Player.Character.Handle, 0, 16);
                return;
            }

            // Determine if the suspect flees
            if (random.NextDouble() <= fleeChance)
            {
                // Suspect flees
               // DisplayNotification("Foot takedown: Suspect is fleeing.", 5);
                Function.Call(Hash.TASK_SMART_FLEE_PED, suspect.Handle, Game.Player.Character.Handle, 100.0f, -1, false, false);
                return;
            }

            // Default action: hands up
            Function.Call(Hash.TASK_HANDS_UP, suspect.Handle, -1, Game.Player.Character.Handle, -1, true);
            //DisplayNotification("Foot takedown: Suspect surrendering (stage 1)", 5);

            await Delay(250);

            suspect.Weapons.Drop();

            npcStatus[suspect.Handle].HasSurrendered = true;
        }

        private void DrawProgressBar(float x, float y, float width, float height, double progress)
        {
            // Background (gray bar)
            DrawRect(x, y, width, height, 50, 50, 50, 200);
            // Foreground (light blue bar)
            DrawRect(x - width / 2 + (float)(width * progress) / 2, y, (float)(width * progress), height, 0, 153, 204, 200);
        }

        private void DrawRect(float x, float y, float width, float height, int r, int g, int b, int a)
        {
            CitizenFX.Core.Native.API.DrawRect(x, y, width, height, r, g, b, a);
        }

        private void HandleOptionSelection(int option)
        {
            switch (option)
            {
                case 0: // DETAIN

                    // Setting the NPC as a Mission Entity if it wasn't already set
                    if (!IsEntityAMissionEntity(closestPed.Handle))
                    {
                        Debug.WriteLine("Setting this entity as mission entity.");
                        SetEntityAsMissionEntity(closestPed.Handle, true, true);
                        TaskSetBlockingOfNonTemporaryEvents(closestPed.Handle, true);
                        DisplayNotification("Notifying subject they are being detained.", 5);

                        Random random = new Random();
                        if (random.NextDouble() < firearmChance)
                        {


                            GiveWeaponToPed(closestPed.Handle, (uint)WeaponHash.Pistol, 50, false, false);
                            DisplayNotification("Subject has been armed with a firearm.", 5);

                            // Check suspect aggression and decide if the ped attacks
                            if (random.NextDouble() < suspectAggression)
                            {
                                // Command the ped to attack
                                TaskCombatPed(closestPed.Handle, Game.PlayerPed.Handle, 0, 16);
                                DisplayNotification("Subject is aggressive and attacking.", 5);
                            }
                            else
                            {
                                DisplayNotification("Subject is armed but not aggressive.", 5);
                            }

                        }
                        else
                        {
                            DisplayNotification("Subject is unarmed.", 5);
                        }

                    }
                    else
                    {
                        ClearPedTasks(closestPed.Handle);
                        int closestPedHandleInt = closestPed.Handle;
                        Debug.WriteLine("Removing this entity as mission entity.");
                        SetEntityAsMissionEntity(closestPed.Handle, false, false);
                        TaskSetBlockingOfNonTemporaryEvents(closestPed.Handle, false);
                        SetPedAsNoLongerNeeded(ref closestPedHandleInt);
                        DisplayNotification("Releasing subject.", 5);
                    }

                    break;


                case 1: // ASK FOR ID
                    if (closestPed != null)
                    {
                        // Check if pedData exists for closestPed
                        if (pedData.ContainsKey(closestPed))
                        {
                            NPCData pedData = this.pedData[closestPed]; // Access the NPCData for the closestPed
                            if (pedData.ID == -1) // -1 is default, wasn't assigned an ID yet
                            {
                                Debug.WriteLine("Random identity not yet assigned...");
                                GetIdentityFromServer(closestPed); // Call GetIdentityFromServer for the closestPed
                                DisplayNotification("Subject retrieving ID...", 5);
                                if (!isLicenseDisplayed)
                                {
                                    TriggerEvent("ToggleLicenseVisibility");
                                    isLicenseDisplayed = true;
                                }
                                DisplayNotification("Handing ID...", 5);

                                // Assigning registrationID to pedData.ID
                                pedData.ID = pedIdentityPedInteractionMenu;
                            }
                            else
                            {
                                if (isLicenseDisplayed)
                                {
                                    DisplayNotification("Returning ID.", 5);
                                    TriggerEvent("ToggleLicenseVisibility");
                                    isLicenseDisplayed = false;
                                }
                                else
                                {
                                    DisplayNotification("Subject retrieving ID...", 5);
                                    TriggerEvent("ToggleLicenseVisibility");
                                    isLicenseDisplayed = true;
                                }
                                Debug.WriteLine("Ped already has an ID assigned.");
                                //pedIdentityPedInteractionMenu = pedData.ID;
                                TriggerServerEvent("GetIdentityById", pedData.ID);
                            }
                        }
                        else
                        {
                            Debug.WriteLine("No pedData found for closestPed");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("No valid closestPed found");
                    }
                    break;
                case 2:
                    Debug.WriteLine("Option 3 selected");
                    break;
            }
        }


        private void UpdatePlayerInfo()
        {
            Vector3 playerPosition = Game.PlayerPed.Position;
            double playerX = playerPosition.X;
            double playerY = playerPosition.Y;

            nearestPostalCode = postalManager.GetNearestPostal(playerX, playerY);

            Ped ped = Game.PlayerPed;
            Vector3 coords = ped.Position;

            uint streetHash = 0;
            uint crossingRoadHash = 0;

            GetStreetNameAtCoord(coords.X, coords.Y, coords.Z, ref streetHash, ref crossingRoadHash);

            streetName = GetStreetNameFromHashKey(streetHash);
            crossRoadName = GetStreetNameFromHashKey(crossingRoadHash);
        }

        private void HandlePedDragging()
        {
            Ped playerPed = Game.Player.Character;
            if (closestPed != null && closestPed.Exists() && playerPed != null && playerPed.Exists() && isDraggingPed)
            {
                AttachEntityToEntity(closestPed.Handle, playerPed.Handle, 0, 0.5f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, true, true, false, true, 2, true);
            }
        }

        private async Task HandleTrafficStop()
        {
            if (isDriverStoppedAtTrafficStop)
            {
                if (Game.IsControlJustPressed(0, Control.Pickup) && Game.PlayerPed.IsInVehicle())
                {
                    int playerPedHandle = Game.PlayerPed.Handle;
                    isDriverStoppedAtTrafficStop = false;
                    TaskVehicleMissionPedTarget(globalTrafficStopVehicle.Driver.Handle, globalTrafficStopVehicle.Handle, playerPedHandle, 1, 10f, (int)DrivingStyle.Normal, 1f, 0f, false);
                    await Delay(5000);
                    await TrafficStopPullOverLogic(globalTrafficStopVehicle);
                }

                Vector3 playerPos = Game.PlayerPed.Position;
                Vector3 driverPos = driver.Position;
                float distance = World.GetDistance(playerPos, driverPos);

                if (distance <= 2.0f && Game.IsControlPressed(0, Control.ReplayStartStopRecording))
                {
                    ShowTrafficStopMenuCommand();
                    await Delay(50);
                }
            }
        }

        private void HandlePulledOverDriver()
        {
            if (driver != null && driver.Exists())
            {
                // Implement the pulled-over driver logic here
            }
        }

        private async Task FindAndHandleNearbyVehicle()
        {
            int maxDistance = 15; // Maximum distance to search for nearby vehicles
            const float frontThreshold = 0.95f; // Adjust this threshold as needed
            const float angleThreshold = (float)(Math.PI / 4); // Explicit cast to float

            Vector3 playerPosition = Game.PlayerPed.Position;

            Vehicle[] allVehicles = World.GetAllVehicles();
            foreach (Vehicle vehicle in allVehicles)
            {
                if (vehicle.Handle == Game.PlayerPed.CurrentVehicle.Handle)
                {
                    continue;
                }

                float distance = Vector3.Distance(playerPosition, vehicle.Position);
                if (distance > maxDistance)
                {
                    continue;
                }

                Vector3 directionToVehicle = vehicle.Position - playerPosition;
                directionToVehicle.Normalize();

                float dotProduct = Vector3.Dot(directionToVehicle, Game.PlayerPed.ForwardVector);
                float angle = (float)Math.Acos(Vector3.Dot(directionToVehicle, Game.PlayerPed.ForwardVector));

                if (dotProduct >= frontThreshold && angle < angleThreshold)
                {
                    targetVehicle = vehicle;
                    break;
                }
            }

            if (targetVehicle != null)
            {
                await HandleTrafficStopAsync();
            }
        }

        private void HandleVehicleInteractionMenu()
        {
            Vector3 playerPosition = Game.Player.Character.Position;
            foreach (Vehicle vehicle in World.GetAllVehicles())
            {
                float distance = Vector3.Distance(playerPosition, vehicle.Position);
                if (distance <= 1.5f)
                {
                    vehicleMenuTarget = vehicle;
                    string plateNum = GetVehicleNumberPlateText(vehicle.Handle);
                    vehicleMenu.Visible = true;
                    string displayName = GetDisplayNameFromVehicleModel((uint)vehicle.Model.Hash);
                    DisplayNotification($"MODEL: {displayName}", 15);
                    DisplayNotification($"PLATE: {plateNum}", 15);
                    break;
                }
            }
        }

        /*private async void OnReceiveLockerCoordinates(string serializedData)
        {
            // Deserialize the serializedData into a list of dictionaries containing X, Y, Z coordinates
            List<Dictionary<string, float>> lockerLocations = JsonConvert.DeserializeObject<List<Dictionary<string, float>>>(serializedData);

            // Process the received locker coordinates
            foreach (var lockerLocation in lockerLocations)
            {
                float x = lockerLocation["X"];
                float y = lockerLocation["Y"];
                float z = lockerLocation["Z"];

                // Convert x, y, z coordinates to Vector3
                Vector3 position = new Vector3(x, y, z);

                // Use the X, Y, Z coordinates as needed
                Debug.WriteLine($"Received locker location: X={x}, Y={y}, Z={z} - Vector3: {position}");

                AddStaticText(position, "Locker Room - Press [E]");

            }
            await Delay(0);
        }*/


        // =================================================================================
        // OnPlayerReceiveProfile()
        // Runs code when the player receives his player profile (if whitelisted) upon connection
        // =================================================================================
        private void OnPlayerReceiveProfile(string jsonData)
        {
            try
            {
                var profileData = JsonConvert.DeserializeObject<PlayerProfileData>(jsonData);
                // Process the received player profile data
                Debug.WriteLine("------------------------------------------");
                Debug.WriteLine($"Received player profile:");
                Debug.WriteLine($"Username: {profileData.Username}");
                Debug.WriteLine($"License: {profileData.LicenseIdentifier}");
                Debug.WriteLine($"DeptNameShort: {profileData.DeptNameShort}");
                Debug.WriteLine($"Rank: {profileData.Rank}");
                Debug.WriteLine($"Division: {profileData.Division}");
                Debug.WriteLine($"Badge: {profileData.Badge}");
                Debug.WriteLine("------------------------------------------");

                string deptFull = profileData.DeptNameFull;
                deptShort = profileData.DeptNameShort;
                string pRank = profileData.Rank;
                string pName = Game.Player.Name;
                string pBadge = profileData.Badge;
                string pDivision = profileData.Division;
                string deptLogo = profileData.DeptLogo;

                //Debug.WriteLine($"deptShort is now: {deptShort}");


                // Update the NUI Dispatch page even if off duty, that way if you go on duty you will still see latest calls
                //TriggerEvent("updatePlayerProfile", pName, deptFull, deptShort, pRank, pBadge, pDivision, deptLogo);


            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during handling
                Debug.WriteLine($"ERROR: Error deserializing or handling random identity: {ex.Message}");
            }
        }


            // =================================================================================
            // OnRandomIdentityResponse()
            // Runs code when receiving a random identity string from the server's identities.json
            // =================================================================================
            private void OnRandomIdentityResponse(string randomIdentityJson)
        {
            try
            {
                // Deserialize the JSON string to an Identity object
                Identity randomIdentity = JsonConvert.DeserializeObject<Identity>(randomIdentityJson);

                // Handle the random identity received from the server
                Debug.WriteLine($"Received ID from server: {randomIdentity.id} ({randomIdentity.firstName} {randomIdentity.lastName})");


                TriggerEvent("updateLicenseData", randomIdentity.firstName, randomIdentity.lastName, randomIdentity.sex, randomIdentity.driversLicenseExp, randomIdentity.dob, randomIdentity.driversLicenseNumber, randomIdentity.driversLicenseIssue);

                registeredVehicle = randomIdentity.registeredVehicle;
                registrationFirstName = randomIdentity.firstName;
                registrationLastName = randomIdentity.lastName;
                pedIdentity = randomIdentity.id;
                pedIdentityPedInteractionMenu = randomIdentity.id;
                registrationID = randomIdentity.registeredVehicle;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during handling
                Debug.WriteLine($"ERROR: Error deserializing or handling random identity: {ex.Message}");
            }
        }


        // =================================================================================
        // OnOutboundCoordsUpdate()
        // This function is requested by the server to all clients, checks if local player is near a callout
        // =================================================================================
        private async void OnOutboundCoordsUpdate(Vector3 outboundCoords)
        {

            Debug.WriteLine("OnOutboundCoordsUpdate()");
            // Check if the player's ped is near the outbound coordinates
            if (IsPlayerNearCoords(outboundCoords, 75f)) // Adjust the radius as needed
            {
                int pedNetId = NetworkGetNetworkIdFromEntity(Game.PlayerPed.Handle);
                TriggerServerEvent("clientEvent:ClientIsNearCallout", true, pedNetId); // Tell the server to stop searching for a nearby player
                DisplayNotification("You've triggered the mission.", 5);

                await Delay(0);
            }
        }

        // =================================================================================
        // DeserializePedNetIds()
        // Method to deserialize JSON string containing PedNetIds to List<int>
        // =================================================================================
        private List<int> DeserializePedNetIds(string serializedData)
        {
            return JsonConvert.DeserializeObject<List<int>>(serializedData);
        }

        // =================================================================================
        // ReceivePedNetIds()
        // Event handler to receive serialized JSON string and deserialize it
        // =================================================================================
        private async void ReceivePedNetIds(string serializedData)
        {
            List<int> pedNetIds = DeserializePedNetIds(serializedData);
            // Now you have the deserialized List<int> to work with
            foreach (int pedNetId in pedNetIds)
            {

                // CALLOUT TYPE ID 1 & 2
                // PERSON WITH A GUN

                // SCENARIO 1 - PED(S) WILL ATTACK THE PLAYER WHEN HE GETS IN RANGE.
                Debug.WriteLine($"Received pedNetId: {pedNetId}");

                int localEntity = NetworkGetEntityFromNetworkId(pedNetId); // Get the local ped handle from network ID
                RequestWeaponAsset((uint)WeaponHash.AssaultRifleMk2, 31, 0); // Request weapon model then apply small delay
                await Delay(100);

                // possible guns:
                // WeaponHash.AssaultRifleMk2
                // WeaponHash.CombatPistol
                // WeaponHash.MachinePistol
                // WeaponHash.Pistol
                // WeaponHash.PumpShotgun

                GiveWeaponToPed(localEntity, (uint)WeaponHash.MG, 500, false, false); // Give ped a weapon
                TaskCombatPed(localEntity, Game.PlayerPed.Handle, 0, 16);
            }
        }

        // =================================================================================
        // RunMissionCode()
        // Runs code on the specified NPC for callouts
        // =================================================================================
        public void RunMissionCode(int localEntity)
        {

        }

        // =================================================================================
        // IsPlayerNearCoords()
        // Helper function to check if player is near coordinates with radius, used by OnOutboundCoordsUpdate
        // =================================================================================
        private bool IsPlayerNearCoords(Vector3 coords, float radius)
        {
            // Get the player's ped and its position
            Ped playerPed = Game.PlayerPed;
            Vector3 playerPos = playerPed.Position;

            // Calculate the distance between the player's position and the specified coordinates
            float distance = World.GetDistance(playerPos, coords);

            // Check if the distance is within the specified radius
            return distance <= radius;
        }

        // =================================================================================
        // IsPlayerNearLockerRoom()
        // Function to check if player is near any loaded locker room coordinates (received from server)
        // =================================================================================
        bool IsPlayerNearLockerRoom(Vector3 playerLocation)
        {
            const float proximityThreshold = 15f;

            foreach (var lockerRoomLocation in lockerRoomCoordinates)
            {
                if (Vector3.Distance(playerLocation, lockerRoomLocation) <= proximityThreshold)
                {
                    return true; // Player is within 15 units of a locker room
                }
            }
            return false; // Player is not near any locker room
        }

        // =================================================================================
        // FindClosestPed()
        // Function to get the closest ped from the player, used in ped interaction menu
        // =================================================================================
        private async void FindClosestPed()
        {
            float closestDistance = 5f;
            Ped closestPedFound = null;

            // Hide the license if re-opening interaction menu
            if (isLicenseDisplayed)
            {
                TriggerEvent("ToggleLicenseVisibility");
                isLicenseDisplayed = false;
            }

            foreach (Ped ped in World.GetAllPeds())
            {
                if (ped != Game.PlayerPed)
                {
                    float distance = World.GetDistance(Game.PlayerPed.Position, ped.Position);
                    if (distance < closestDistance)
                    {
                        closestPedFound = ped;
                        closestDistance = distance;
                    }
                }
            }

            if (closestPedFound != null)
            {
                closestPed = closestPedFound;
                Debug.WriteLine($"Closest pedestrian found at distance: {closestDistance}");

                pedInteractionBlip = closestPed.AttachBlip();
                pedInteractionBlip.Sprite = BlipSprite.Crosshair; // You can change the blip sprite if needed


                int mugshot = API.RegisterPedheadshot(closestPed.Handle);
                while (!API.IsPedheadshotReady(mugshot)) await BaseScript.Delay(1);
                string mugtxd = API.GetPedheadshotTxdString(mugshot);

                Debug.WriteLine($"Getting ped mugshot: {mugshot} mugtxd: {mugtxd}");


                //pauseMenu.HeaderPicture = new(mugtxd, mugtxd);

                // Check if pedData exists for closestPed, if not, add it
                if (!pedData.ContainsKey(closestPed))
                {
                    pedData.Add(closestPed, new NPCData(-1, -1, false)); // Initialize with default NPCData
                }

                // Output pedData contents for the closest ped
                //DisplayNotification($"Existing data: Identity: {pedData[closestPed].ID} is Cuffed: {pedData[closestPed].IsCuffedID} is Detained: {pedData[closestPed].IsDetained}", 5);

                ShowPedMenu();




            }
            else
            {
                closestPed = null; // No closest ped found
            }
            await Delay(0);
        }






        // =================================================================================
        // GetIdentityFromServer()
        // Checks a ped's gender then requests a random identity from the server according to gender
        // =================================================================================
        public async void GetIdentityFromServer(Ped ped)
        {
            Debug.WriteLine("Requesting an identity from the server...");
            int hash = GetEntityModel(ped.Handle);
            //int hash = ped.Handle;
            //Debug.WriteLine($"GetEntityModel hash: {hash}");

            // Convert the integer hash to hexadecimal string
            string hexHash = "0x" + hash.ToString("X");
            //Debug.WriteLine($"Hexadecimal hash: {hexHash}");

            uint hash2 = Convert.ToUInt32(hexHash.Replace("0x", ""), 16);
            //Debug.WriteLine($"uint hash: {hash2}");

            Dictionary<uint, string> PedModels = PedHashes.ToDictionary(kv => kv.Value, kv => kv.Key);

            // Check if the PedModels dictionary contains the hash and retrieve the ped model
            if (PedModels.TryGetValue(hash2, out string pedModel))
            {
                //Debug.WriteLine($"Pedmodel for hash {hexHash}: {pedModel}");
            }

            // Determine gender based on ped model prefix and trigger server event
            if (!string.IsNullOrEmpty(pedModel))
            {
                int requestedGender = 0; // Female by default
                if (pedModel.StartsWith("a_m") || pedModel.StartsWith("g_m") || pedModel.StartsWith("s_m") || pedModel.StartsWith("u_m"))
                {
                    requestedGender = 1; // Male
                }

                TriggerServerEvent("clientEvent:RequestRandomIdentity", requestedGender);
                //await GetAndSendMugshotToNUI(ped);

            }
            else
            {
                Debug.WriteLine("ERROR: Pedmodel gender not specified.");
            }
            await Delay(0);
        }

        // =================================================================================
        // TrafficStopPullOverLogic()
        // Main traffic stop logic
        // =================================================================================
        public async Task TrafficStopPullOverLogic(Vehicle vehicle)
        {
            try
            {
                // Check if the vehicle has a driver
                if (vehicle.Exists() && vehicle.Driver != null && vehicle.Driver.Exists())
                {
                    driver = vehicle.Driver;
                    globalTrafficStopVehicle = vehicle;

                    // Set the driver and vehicle as missions entities, that way if the driver dismounts the vehicle wont despawn
                    SetEntityAsMissionEntity(vehicle.Driver.Handle, true, true);
                    SetEntityAsMissionEntity(vehicle.Handle, true, true);
                    Debug.WriteLine("Running Traffic Stop Logic...");

                    // Checking if NPCData exists for driver
                    if (pedData.ContainsKey(driver))
                    {
                        // Existing ped found, output pedData contents
                        Debug.WriteLine("Existing ped data check");
                        foreach (var pair in pedData)
                        {
                            if (pair.Key == driver) // Only print data for the closest ped
                            {
                                // Check if ID is already set, if not, update it with randomIdentity.id
                                if (pair.Value.ID == -1)
                                {
                                    pair.Value.ID = pedIdentity;
                                    //DisplayNotification($"ID set to: {pedIdentity}", 5);
                                    Debug.WriteLine($"ID set to: {pedIdentity}");
                                }
                                Debug.WriteLine($"Existing data: Identity: {pair.Value.ID} is Cuffed: {pair.Value.IsCuffedID} is Detained: {pair.Value.IsDetained}");
                                //DisplayNotification($"Existing data: Identity: {pair.Value.ID} is Cuffed: {pair.Value.IsCuffedID} is Detained: {pair.Value.IsDetained}", 5);

                                // Check if identity is already retrieved
                                if (pair.Value.ID != -1)
                                {
                                    Debug.WriteLine("Requesting ID");
                                    GetIdentityFromServer(driver);
                                }
                                else
                                {
                                    Debug.WriteLine("Requesting ID");
                                    GetIdentityFromServer(driver);
                                }
                                break; // Stop iterating after finding the closest ped data
                            }
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Requesting ID");
                        GetIdentityFromServer(driver);
                        pedData.Add(driver, new NPCData(-1, -1, false));
                    }

                }
                else
                {
                    Debug.WriteLine("Vehicle has no driver.");
                    return; // Exit the method if no driver is found
                }

                Vector3 vehiclePosition = vehicle.Position;
                Vector3 newPosition = vehiclePosition + (vehicle.ForwardVector * 20f);

                int playerVehicleHandle = Game.PlayerPed.CurrentVehicle.Handle;
                int playerPedHandle = Game.PlayerPed.Handle;

                int randomNumber = random.Next(1500, 5000);
                vehicle.Driver.AlwaysKeepTask = true;
                vehicle.Driver.BlockPermanentEvents = true;
                await Delay(randomNumber);

                float currVehSpeed = GetEntitySpeed(vehicle.Handle);

                // Tell the NPC driver to pull over
                TaskVehicleMissionPedTarget(vehicle.Driver.Handle, vehicle.Handle, playerPedHandle, 22, 1f, (int)DrivingStyle.Normal, 1f, 0f, false);

                // Wait for the driver to finish pulling over
                while (GetScriptTaskStatus(vehicle.Driver.Handle, 0xB41F1A34) != 7)
                {
                    await Delay(1);
                }

                DisplayNotification("The driver has come to a stop. Press the horn to make the driver pick another parking position.", 4);
                isDriverStoppedAtTrafficStop = true;
                DisplayNotification("Approach the vehicle and press F1 to interact with the occupant(s).", 5);

                await Delay(5000);

                // Update vehicle registration data / registration card here, response from server should already be received
                string modelName = vehicle.DisplayName;

                if (registeredVehicle != null)
                {
                    Debug.WriteLine("Updating registration card.");

                    // Generate random issue date between 2021 and today
                    DateTime minIssueDate = new DateTime(2021, 1, 1);
                    DateTime maxIssueDate = DateTime.Today;
                    DateTime issueDate = GenerateRandomDate(minIssueDate, maxIssueDate);
                    string formattedRandomIssueDate = issueDate.ToString("MM/dd/yyyy");

                    // Generate expiration date, same as issue date but + 2 years
                    DateTime expirationDate = issueDate.AddYears(2);
                    string formattedRandomExpirationDate = expirationDate.ToString("MM/dd/yyyy");

                    // Generate random vehicle ID
                    //string randomVehicleID = GenerateRandomVehicleID();
                    string plateNum = GetVehicleNumberPlateText(vehicle.Handle);

                    string fullName = registrationFirstName + " " + registrationLastName;
                    TriggerEvent("updateRegistrationData", formattedRandomIssueDate, formattedRandomExpirationDate, modelName, plateNum, registrationID, fullName);
                }
                else
                {
                    Debug.WriteLine($"This vehicle is not registered. Var: {registeredVehicle}");

                    // Wait for a few seconds and try again
                    int retryCount = 0;
                    int maxRetries = 3;

                    while (registeredVehicle is null && retryCount < maxRetries)
                    {
                        Debug.WriteLine($"Retrying after 3 seconds...");
                        await Delay(3000);
                        retryCount++;
                    }

                    if (registeredVehicle is null)
                    {
                        Debug.WriteLine("Retry limit reached. Unable to register the vehicle.");
                    }
                    else
                    {
                        // Try registering again
                        // Make sure to include the registration logic here
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                // Handle the exception as needed
            }
        }


        public static async Task<IdentityPedTexture> GetPedHeadshotTexture(int ped)
        {
            var handle = RegisterPedheadshot_3(ped);

            while (!IsPedheadshotReady(handle))
                await Delay(0);

            var texture = GetPedheadshotTxdString(handle);

            return new IdentityPedTexture { Handle = handle, TextureString = texture };
        }

        // =================================================================================
        // GenerateRandomDate()
        // Generates a random date, used when generating vehicle registration, etc
        // =================================================================================
        private DateTime GenerateRandomDate(DateTime minDate, DateTime maxDate)
        {
            Random random = new Random();
            int range = (maxDate - minDate).Days;
            return minDate.AddDays(random.Next(range));
        }

        // =================================================================================
        // ClearTrafficStop()
        // Function to clear traffic stops
        // =================================================================================
        public async Task ClearTrafficStop()
        {
            try
            {
                // Check if the driver exists
                if (driver != null && driver.Exists())
                {
                    int driverHandle = driver.Handle;
                    SetPedAsNoLongerNeeded(ref driverHandle);
                    Debug.WriteLine("Driver dismissed.");
                }

                // Reset the global traffic stop vehicle
                if (globalTrafficStopVehicle != null && globalTrafficStopVehicle.Exists())
                {
                    globalTrafficStopVehicle = null;
                    Debug.WriteLine("Global traffic stop vehicle reset.");
                }

                // Check if the blip exists
                if (blip != null && blip.Exists())
                {
                    blip.Delete();
                    Debug.WriteLine("Blip removed.");
                }

                // Reset the flag
                isDriverStoppedAtTrafficStop = false;
                Debug.WriteLine("Traffic stop flag reset.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}.");
                // Handle the exception as needed
            }
            await Delay(0);
        }

        // =================================================================================
        // StartPursuit()
        // Changes NPC behavior so they start evading the player
        // =================================================================================
        private async Task StartPursuit(Vehicle targetVehicle)
        {
            pursuitActive = true;
            Debug.WriteLine("Starting vehicle pursuit.");
            //int playerVehicleHandle = Game.PlayerPed.CurrentVehicle.Handle;
            int playerPedHandle = Game.PlayerPed.Handle;
            TaskVehicleMissionPedTarget(targetVehicle.Driver.Handle, targetVehicle.Handle, playerPedHandle, 8, 40f, (int)DrivingStyle.Rushed, 50f, 0f, true);
            await Delay(0);
        }

        // =================================================================================
        // ToggleLicenseVisibilityCommand()
        // For /toglic command
        // =================================================================================
        private async void ToggleLicenseVisibilityCommand(int source, List<object> args, string rawCommand)
        {
            Debug.WriteLine("ToggleLicenseVisibilityCommand");
            DisplayNotification("Toggling license visibility...", 3);
            TriggerEvent("ToggleLicenseVisibility");
            await Delay(0);
        }

        // =================================================================================
        // ToggleMDCCommand()
        // For /mdc command
        // =================================================================================
        private async void ToggleMDCCommand(int source, List<object> args, string rawCommand)
        {
            Debug.WriteLine("ToggleMDCCommand");

            // Check if player is on duty
            if (!onDuty)
            {
                DisplayNotification("You must be on duty to use this command.", 5);
                return; // Exit function if not on duty
            }

            // Check if the player is in a vehicle
            if (Game.PlayerPed.IsInVehicle())
            {
                SendNuiMessage(JsonConvert.SerializeObject(new
                {
                    type = "toggleMDCVisibility"
                }));

                SetNuiFocus(true, true);
                SetNuiFocusKeepInput(false);
                isMDCDisplaying = true;

                // Disable all input except Escape and F
                DisableAllInputs();

            }
            else
            {
                DisplayNotification("You are not near a workstation.", 5);
            }

            await Delay(0);
        }

        // =================================================================================
        // HandleTrafficStopAsync()
        // Handles the traffic stop logic after player pressed LSHIFT, selected a vehicle, etc
        // =================================================================================
        private async Task HandleTrafficStopAsync()
        {
            // Check if the traffic event blip already exists and delete it, before creating a new blip
            blipTraffic?.Delete();

            // Attach a blip to the target vehicle
            blip = targetVehicle.AttachBlip();
            blip.Sprite = BlipSprite.PersonalVehicleCar;
            blip.Color = BlipColor.Yellow;
            blip.Name = "Traffic Stop Target";

            Vehicle playerVehicle = Game.PlayerPed.CurrentVehicle;
            bool sirenOn = IsVehicleSirenOn(playerVehicle.Handle);

            if (!sirenOn)
            {
                DisplayNotification("Turn on your emergency lights to initiate a traffic stop.", 5);

                // Wait until the siren is turned on, with a maximum wait time
                int waitTime = 0;
                while (!IsVehicleSirenOn(playerVehicle.Handle) && waitTime < 10000) // Wait for up to 10 seconds
                {
                    if (IsVehicleSirenOn(playerVehicle.Handle))
                    {
                        await TrafficStopPullOverLogic(targetVehicle);
                    }
                    await Delay(0);
                    waitTime += 100;
                }

                if (!IsVehicleSirenOn(playerVehicle.Handle))
                {
                    DisplayNotification("Traffic stop cancelled: Player didn't activate sirens.", 5);
                    blip?.Delete();
                    return; // Exit if player didn't turn on sirens
                }
            }


            Random random = new Random();
            int randomNumber = random.Next(100);
            int pursuitChancePercentage = (int)(pursuitChance * 100);
            bool isEvading = randomNumber <= pursuitChancePercentage;

            if (isEvading)
            {
                DisplayNotification("The driver is fleeing.", 5);
                blip?.Delete();
                await StartPursuit(targetVehicle);
            }
            else
            {
                await TrafficStopPullOverLogic(targetVehicle);
            }
            await Delay(0);
        }

        // =================================================================================
        // ToggleCancelTowCommand()
        // For /canceltow command
        // =================================================================================
        private async void ToggleCancelTowCommand(int source, List<object> args, string rawCommand)
        {
            // Check if player is on duty
            if (!onDuty)
            {
                DisplayNotification("You must be on duty to use this command.", 5);
                return; // Exit function if not on duty
            }

            // Delete the blip
            if (flatbedBlip.Exists())
            {
                flatbedBlip.Delete();
            }
            await Delay(0);
        }

        // =================================================================================
        // ToggleRequestTowCommand()
        // For /reqtow command
        // =================================================================================
        private async void ToggleRequestTowCommand(int source, List<object> args, string rawCommand)
        {
            Debug.WriteLine("ToggleRequestTowCommand");

            // Check if player is on duty
            if (!onDuty)
            {
                DisplayNotification("You must be on duty to use this command.", 5);
                return; // Exit function if not on duty
            }

            // Save current player position as towParking
            Vector3 towParking = Game.PlayerPed.Position;

            // Calculate a random direction
            float angle = (float)(random.NextDouble() * 2 * Math.PI);
            Vector3 direction = new Vector3((float)Math.Sin(angle), (float)Math.Cos(angle), 0);

            // Calculate the target position 300 meters away from the player position
            Vector3 targetPosition = towParking + direction * 300;


            Vector3 closestNodePosition = new Vector3();
            int nodeType = 0;
            float heading = 0.0f;
            bool success = GetClosestVehicleNodeWithHeading(targetPosition.X, targetPosition.Y, targetPosition.Z, ref closestNodePosition, ref heading, nodeType, 3.0f, 0);

            if (success)
            {
                Debug.WriteLine("Closest vehicle node position: " + closestNodePosition.ToString());
            }
            else
            {
                // Script failed to find a road, abort the tow request
                Debug.WriteLine("Failed to find closest vehicle node.");
                DisplayNotification("Towing dispatch has no available vehicles. Try again later.", 15);
                return;
            }


            // Creating the towtruck
            Vehicle flatbed = await World.CreateVehicle(VehicleHash.Flatbed, closestNodePosition, heading);

            // Wait for towtruck to spawn in
            while (!flatbed.Exists())
            {
                await Delay(100);
            }

            // Check if the flatbed is flipped over and correct its orientation
            if (!flatbed.IsOnAllWheels)
            {
                Debug.WriteLine("Towtruck was flipped. Resetting it on its wheels.");
                flatbed.PlaceOnGround();
                flatbed.Repair();
            }

            // Offset the driver position a bit so he doesnt get crushed by the towtruck
            // Prior testing done with CreatePedInsideVehicle failed, driver wouldnt spawn so we have to spawn the ped first then warp it into the truck
            Vector3 offsetPosition = closestNodePosition + new Vector3(4f, 0f, 0f);
            Ped towDriverPed = await World.CreatePed(PedHash.AirworkerSMY, offsetPosition, heading);

            while (!towDriverPed.Exists())
            {
                await Delay(100);
            }

            // Warp the driver into the driver's seat of the flatbed
            towDriverPed.Task.WarpIntoVehicle(flatbed, VehicleSeat.Driver);

            // Add a blip onto the flatbed position
            flatbedBlip = flatbed.AttachBlip();
            flatbedBlip.Sprite = BlipSprite.JerryCan; // You can change the blip sprite if needed


            // Set the towtruck driver as a mission entity
            SetEntityAsMissionEntity(towDriverPed.Handle, true, true);
            SetDriveTaskDrivingStyle(towDriverPed.Handle, (int)DrivingStyle.Normal);
            TaskVehicleMissionCoorsTarget(towDriverPed.Handle, flatbed.Handle, towParking.X, towParking.Y, towParking.Z, 4, 4, 4, 10.0f, 10.0f, false);

            // Drive the flatbed to the tow parking position
            //TaskVehicleDriveToCoordLongrange(towDriverPed.Handle, flatbed.Handle, towParking.X, towParking.Y, towParking.Z, 25.0f, 447, 30.0f);

            DisplayNotification("The towing company has dispatched a vehicle to your location.", 20);
            DisplayNotification("To cancel this request, type /canceltow", 20);


            int playerPedHandle = Game.PlayerPed.Handle;

            // Check if the vehicle has finished pulling over
            while (GetScriptTaskStatus(towDriverPed.Handle, 0x93A5526E) != 7) // Waiting for TaskVehicleDriveToCoord to finish
            {
                await Delay(1); // Wait for a second before checking again
            }

            TaskVehicleMissionPedTarget(towDriverPed.Handle, flatbed.Handle, playerPedHandle, 22, 10.0f, (int)DrivingStyle.Normal, 1f, 0f, false); // Instruct towtruck to park


            // Check if the vehicle has finished pulling over
            while (GetScriptTaskStatus(towDriverPed.Handle, 0xB41F1A34) != 7) // Waiting for TaskVehicleMissionPedTarget to finish
            {
                await Delay(1); // Wait for a second before checking again
            }

            if (towtruckTarget == null)
            {
                DisplayNotification("The towtruck driver has arrived but no nearby vehicles are marked for towing.", 20);
                DisplayNotification("Use the vehicle interaction menu to mark a vehicle.", 20);
            }

            while (towtruckTarget == null)
            {
                await Delay(1);
            }

            Debug.WriteLine("Attaching target vehicle to towtruck.");
            AttachEntityToEntity(towtruckTarget.Handle, flatbed.Handle, GetEntityBoneIndexByName(flatbed.Handle, "bodyshell"), 0.0f, -2.75f, 1.0f, 0.0f, 0.0f, 0.0f, false, false, false, false, 2, true);

            // Release the resources associated with the tow truck driver once it's no longer needed
            int towDriverPedHandle = towDriverPed.Handle;
            SetPedAsNoLongerNeeded(ref towDriverPedHandle);

            // Release the resources associated with the flatbed vehicle once it's no longer needed
            int flatbedHandle = flatbed.Handle;
            SetVehicleAsNoLongerNeeded(ref flatbedHandle);

            // Delete the blip
            if (flatbedBlip.Exists())
            {
                flatbedBlip.Delete();
            }

            Debug.WriteLine("Ressources released.");
        }

        private bool GetRoadSidePosition(Vector3 targetPosition, ref Vector3 roadSidePosition)
        {
            // Check if the target position is on a road
            if (Function.Call<bool>(Hash.IS_POINT_ON_ROAD, targetPosition.X, targetPosition.Y, targetPosition.Z))
            {
                roadSidePosition = targetPosition;
                return true;
            }

            // If not on a road, find the nearest road point
            Vector3 nearestRoadPoint = Function.Call<Vector3>(Hash.GET_CLOSEST_ROAD, targetPosition.X, targetPosition.Y, targetPosition.Z, 0, 0);
            if (nearestRoadPoint != Vector3.Zero)
            {
                roadSidePosition = nearestRoadPoint;
                return true;
            }

            return false; // Failed to find a suitable road side position
        }

        // =================================================================================
        // DisableAllInputs()
        // Currently unused
        // =================================================================================
        private void DisableAllInputs()
        {
            Debug.WriteLine("DisableAllInputs running");
            // Disable all controls using a loop or a more efficient method based on your framework
            for (int i = 0; i < 256; i++)
            {
                DisableControlAction(0, i, true);
            }

            // Specifically enable Escape and F
            EnableControlAction(0, (int)Control.PhoneCancel, true);
            EnableControlAction(0, (int)Control.VehicleExit, true); // Assuming F is VehicleExit
        }

        // =================================================================================
        // TrafficEventCommand()
        // Used with /trafficevent for debbuging
        // =================================================================================
        private void TrafficEventCommand(int source, List<object> args, string rawCommand)
        {
            Debug.WriteLine($"source: {source} - args: {args} - rawCommand {rawCommand}");
            Debug.WriteLine($"Received command: {rawCommand}");
            Debug.WriteLine($"Number of arguments: {args.Count}");
            if (args.Count > 0)
            {
                if (int.TryParse(args[0].ToString(), out int eventId))
                {
                    // Call your method with the provided eventId
                    HandleTrafficEventCommand(eventId);
                }
                else
                {
                    // Handle incorrect command usage
                    Debug.WriteLine("Usage: /trafficevent [eventId]");
                }
            }
            else
            {
                // Handle incorrect command usage
                Debug.WriteLine("Usage: /trafficevent [eventId]");
            }
        }

        // =================================================================================
        // ShowTrafficStopMenuCommand()
        // Debug command to show traffic stop menu
        // =================================================================================
        private async void ShowTrafficStopMenuCommand()
        {
            if (pool.AreAnyVisible)
            {
                // If any menu is visible, ensure only the intended menu (in this case, 'menu') is visible
                if (menu.Visible)
                {
                    menu.Visible = false;
                }
                else
                {
                    pool.HideAll();
                    menu.Visible = true;
                }
            }
            else
            {
                // If no menu is visible, show the intended menu ('menu')
                menu.Visible = true;
            }
            await Delay(100);
        }

        // =================================================================================
        // ShowPedMenu()
        // Debug command to show ped interaction menu
        // =================================================================================
        private async void ShowPedMenu()
        {
            if (pool.AreAnyVisible)
            {
                // If any menu is visible, ensure only the intended menu (in this case, 'menu') is visible
                if (pedMenu.Visible)
                {
                    pedMenu.Visible = false;
                }
                else
                {
                    pool.HideAll();
                    pedMenu.Visible = true;
                }
            }
            else
            {
                // If no menu is visible, show the intended menu ('menu')
                pedMenu.Visible = true;
            }
            await Delay(100);
        }

        // =================================================================================
        // ShowClothingMenu()
        // For showing EUP clothing menu
        // =================================================================================
        private async void ShowClothingMenu()
        {
            if (pool.AreAnyVisible)
            {
                // If any menu is visible, ensure only the intended menu (in this case, 'menu') is visible
                if (clothingMenu.Visible)
                {
                    clothingMenu.Visible = false;
                }
                else
                {
                    pool.HideAll();
                    clothingMenu.Visible = true;
                }
            }
            else
            {
                // If no menu is visible, show the intended menu ('menu')
                clothingMenu.Visible = true;
            }
            await Delay(100);
        }


        // =================================================================================
        // ShowAdminMenu()
        // For showing EUP clothing menu
        // =================================================================================
        private async void ShowAdminMenu()
        {
            if (pool.AreAnyVisible)
            {
                // If any menu is visible, ensure only the intended menu (in this case, 'menu') is visible
                if (clothingMenu.Visible)
                {
                    clothingMenu.Visible = false;
                }
                else
                {
                    pool.HideAll();
                    clothingMenu.Visible = true;
                }
            }
            else
            {
                // If no menu is visible, show the intended menu ('menu')
                clothingMenu.Visible = true;
            }
            await Delay(100);
        }



        // =================================================================================
        // HandleGenerateRandomEventCommand()
        // Generates a random traffic event with /randevent
        // =================================================================================
        private void HandleGenerateRandomEventCommand()
        {
            if (Game.PlayerPed != null && Game.PlayerPed.IsAlive)
            {
                Debug.WriteLine("HandleGenerateRandomEventCommand()");

                Vehicle[] vehicles = World.GetAllVehicles().ToArray();

                bool foundVehicleWithDriver = false;
                Vehicle randomVehicle = null;

                Vector3 playerPos = Game.PlayerPed.Position;

                while (!foundVehicleWithDriver)
                {
                    if (vehicles.Length > 0)
                    {
                        randomVehicle = vehicles[random.Next(vehicles.Length)];
                        float distance = Vector3.Distance(playerPos, randomVehicle.Position);


                        // Ensure the vehicle has a driver in the driver's seat
                        if (randomVehicle.Driver != null && randomVehicle.Driver.Exists() && randomVehicle.Driver.IsPlayer == false && distance <= 100f)
                        {
                            foundVehicleWithDriver = true;
                        }
                        else
                        {
                            Debug.WriteLine($"The selected vehicle ({randomVehicle.DisplayName}) doesn't have a driver in the driver's seat. Searching for another vehicle...");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("No vehicles found. Exiting the traffic event handling.");
                        return; // No vehicles available, exit the function
                    }
                }


                // Select a random event based on probabilities
                string selectedEvent = SelectRandomEvent(eventProbabilities);

                // Execute the appropriate command based on the selected event
                switch (selectedEvent)
                {
                    case "BrokenDownVehicle":
                        new BrokenDownVehicle(randomVehicle);
                        break;
                    case "BrokenWindows":
                        new BrokenWindows(randomVehicle);
                        break;
                    case "FlatTire":
                        new FlatTire(randomVehicle);
                        break;
                    case "SmokingEngine":
                        new SmokingEngine(randomVehicle);
                        break;
                    case "Speeding":
                        new Speeding(randomVehicle);
                        break;
                    default:
                        break;
                }

                blipTraffic = randomVehicle.AttachBlip();
                blipTraffic.Sprite = BlipSprite.PersonalVehicleCar;
                blipTraffic.Color = BlipColor.Red;
                blipTraffic.Name = "Traffic Event";

                _ = DeleteBlipAfterDelay();

                Debug.WriteLine($"Traffic event triggered! Random vehicle selected: {randomVehicle.DisplayName}");
            }

        }

        private async Task DeleteBlipAfterDelay()
        {
            await Delay(30000); // Delay for 30 seconds
            blipTraffic.Delete(); // Delete the blip after the delay
        }

        // =================================================================================
        // HandleTrafficEventCommand()
        // /trafficevent
        // =================================================================================
        private void HandleTrafficEventCommand(int eventId)
        {
            if (Game.PlayerPed != null && Game.PlayerPed.IsAlive)
            {
                Debug.WriteLine($"Command received: /trafficevent {eventId}");

                Vehicle[] vehicles = World.GetAllVehicles().ToArray();

                bool foundVehicleWithDriver = false;
                Vehicle randomVehicle = null;

                Vector3 playerPos = Game.PlayerPed.Position;

                while (!foundVehicleWithDriver)
                {
                    if (vehicles.Length > 0)
                    {
                        randomVehicle = vehicles[random.Next(vehicles.Length)];
                        float distance = Vector3.Distance(playerPos, randomVehicle.Position);


                        // Ensure the vehicle has a driver in the driver's seat
                        if (randomVehicle.Driver != null && randomVehicle.Driver.Exists() && randomVehicle.Driver.IsPlayer == false && distance <= 100f)
                        {
                            foundVehicleWithDriver = true;
                        }
                        else
                        {
                            Debug.WriteLine($"The selected vehicle ({randomVehicle.DisplayName}) doesn't have a driver in the driver's seat. Searching for another vehicle...");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("No vehicles found. Exiting the traffic event handling.");
                        return; // No vehicles available, exit the function
                    }
                }

                    // Execute traffic event for the found vehicle with a driver
                    switch (eventId)
                    {
                        case 1:
                            new BrokenDownVehicle(randomVehicle);
                            break;

                        case 2:
                            new BrokenWindows(randomVehicle);
                            break;

                        case 3:
                            new FlatTire(randomVehicle);
                                break;

                        case 4:
                            new NoBrakeLights(randomVehicle);
                            break;

                        case 5:
                            new SmokingEngine(randomVehicle);
                            break;

                        case 6:
                            new Speeding(randomVehicle);
                            break;

                        default:
                            Debug.WriteLine($"Invalid traffic event ID: {eventId}");
                            break;
                    }

                    Debug.WriteLine("Attaching debug blip to target vehicle.");
                    blip = randomVehicle.AttachBlip();
                    blip.Sprite = BlipSprite.PersonalVehicleCar;
                    blip.Color = BlipColor.Red;
                    blip.Name = "Debug Blip";

                Debug.WriteLine($"Traffic event triggered! Random vehicle selected: {randomVehicle.DisplayName}");
            }
        }
    }
}