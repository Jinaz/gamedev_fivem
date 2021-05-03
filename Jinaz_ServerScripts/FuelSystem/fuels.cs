#define MANUAL_ENGINE_CUTOFF

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;
using CitizenFX.Core.UI;
using CitizenFX.Core;

using CitizenFX.Core.Native;
using System.Globalization;
using Newtonsoft.Json;
using System.Dynamic;
using System.Drawing;
using TinyTween;

namespace fuels
{
    public class FRHud
    {
        protected Scaleform buttons = new Scaleform("instructional_buttons");
        
        public FRHud()
        {

            }

        public void InstructTurnOffEngine()
        {
            buttons.CallFunction("CLEAR_ALL");
            buttons.CallFunction("TOGGLE_MOUSE_BUTTONS", 0);
            buttons.CallFunction("CREATE_CONTAINER");

            buttons.CallFunction("SET_DATA_SLOT", 0, Function.Call<string>((Hash)0x0499D7B09FC9B407, 2, (int)fuels.engineToggleControl, false), "Turn off engine");

            buttons.CallFunction("DRAW_INSTRUCTIONAL_BUTTONS", -1);
        }
        public void InstructRefuelOrTurnOnEngine()
        {
            buttons.CallFunction("CLEAR_ALL");
            buttons.CallFunction("TOGGLE_MOUSE_BUTTONS", 0);
            buttons.CallFunction("CREATE_CONTAINER");

            buttons.CallFunction("SET_DATA_SLOT", 0, Function.Call<string>((Hash)0x0499D7B09FC9B407, 2, (int)Control.Jump, false), "Add Fuel");
            buttons.CallFunction("SET_DATA_SLOT", 1, Function.Call<string>((Hash)0x0499D7B09FC9B407, 2, (int)fuels.engineToggleControl, 0), "Engine On");

            buttons.CallFunction("DRAW_INSTRUCTIONAL_BUTTONS", -1);
        }
        public void RenderInstructions()
        {
            buttons.Render2D();
        }
        public void InstructManualRefuel(string label)
        {
            buttons.CallFunction("CLEAR_ALL");
            buttons.CallFunction("TOGGLE_MOUSE_BUTTONS", 0);
            buttons.CallFunction("CREATE_CONTAINER");

            buttons.CallFunction("SET_DATA_SLOT", 0, Function.Call<string>((Hash)0x0499D7B09FC9B407, 2, (int)Control.Attack, false), label);

            buttons.CallFunction("DRAW_INSTRUCTIONAL_BUTTONS", -1);
        }
    }
    
    /*
    public class vehMods :BaseScript
    {
        public VehicleToggleModType Turboe = VehicleToggleMod[VehicleToggleModType.Turbo]; this[VehicleToggleModType modType] { get; }
       
        Turbo = 18,
        TireSmoke = 20,
        XenonHeadlights = 22
        public VehicleMod this[VehicleModType modType] { get; }

        
        
        
        public int ColorCombinationCount { get; }
        public Color NeonLightsColor { get; set; }
        public Color CustomPrimaryColor { get; set; }
        public Color CustomSecondaryColor { get; set; }


        public int ColorCombination { get; set; }
        public LicensePlateStyle LicensePlateStyle { get; set; }

        public bool HasNeonLights { get; }
        
        public bool IsPrimaryColorCustom { get; }
        public bool IsSecondaryColorCustom { get; }
        
        
        
        public int LiveryCount { get; }
        public int Livery { get; set; }
        public string LocalizedWheelTypeName { get; }
        public VehicleWheelType[] AllowedWheelTypes { get; }
        public VehicleWheelType WheelType { get; set; }
        public VehicleWindowTint WindowTint { get; set; }
    }*/

    public class fuels : BaseScript
    {
        

        //Timer unlockTimer = new Timer(0);
        //Timer skipTimer = new Timer(1);
        Timer updateTimer = new Timer(2);

        const int maxVehicles = 10;
        List<int> vehicleHistory = new List<int>(maxVehicles);

        protected Blip[] blips;
        
        protected List<Pickup> pickups;

        

        //protected List<Vehicle> personalVehiclesList;
        
        
        public static string carKeys = "_Veh_keys";
        public static string carOwner = "_Veh_Owner";

        protected int currentGasStationIndex;
        
        protected Vehicle lastVehicle = null;
        protected Vehicle LastVehicle { get => lastVehicle; set => lastVehicle = value; }
        protected Vehicle targetVeh = null;


        public static string manualRefuelAnimDict = "weapon@w_sp_jerrycan";

        public static string fuelLevelPropertyName = "_Fuel_Level";

        public static string[] tankBones = new string[] {
            "petrolcap",
            "petroltank",
            "petroltank_r",
            "petroltank_l",
            "wheel_lr"
        };

        protected float fuelTankCapacity = 65f;

        protected float refuelRate = 1f;
        protected float fuelConsumptionRate = 0.5f;
        protected float fuelAccelerationImpact = 0.0002f;
        protected float fuelTractionImpact = 0.0001f;
        protected float fuelRPMImpact = 0.0005f;

        protected bool refuelAllowed = true;
        protected float addedFuelCapacitor = 0f;
        protected Random random = new Random();
        protected bool initialized = false;
        public float showMarkerInRangeSquared = 250f;
        protected bool showHud = true;
        protected bool hudActive = false;

        //protected bool justgotintoveh = false;
        //protected bool carSpawned = false;

        public FRHud hud;
        

        
        public int GetGasStationIndexInRange(Vector3 pos, float rangeSquared)
        {
            for (int i = 0; i < blips.Length; i++)
            {
                Blip blip = blips[i];

                if (Vector3.DistanceSquared(GasStations.positions[i], pos) < rangeSquared)
                {
                    return i;
                }
            }

            return -1;
        }
        public void CheckGasStations(Ped playerPed)
        {
            var gasStationIndex = GetGasStationIndexInRange(playerPed.Position, showMarkerInRangeSquared);

            if (gasStationIndex != -1)
            {
                if (gasStationIndex != currentGasStationIndex)
                {
                    // Found gas station in range
                    currentGasStationIndex = gasStationIndex;
                }
            }
            else
            {
                if (currentGasStationIndex != -1)
                {
                    // Lost gas station in range
                    currentGasStationIndex = -1;
                }
            }
            //TODO make a UI-call here
        }
        protected AnimationLoop jerryCanAnimation;

        protected bool currentVehicleFuelLevelInitialized;

        public static Control engineToggleControl = Control.VehicleDuck;

        public fuels()
        {

            hud = new FRHud();
            
            Debug.WriteLine("Gas stations loaded1");
            jerryCanAnimation = new AnimationLoop(
             new Animation(manualRefuelAnimDict, "fire_intro"),
             new Animation(manualRefuelAnimDict, "fire"),
             new Animation(manualRefuelAnimDict, "fire_outro")
           );
            Debug.WriteLine("Gas stations loaded2");
            GasStations.InitViaCode();
            

            //TODO load cars for player

            blips = new Blip[GasStations.positions.Length];
            
            pickups = new List<Pickup>();
            Debug.WriteLine("Gas stations loaded3");

            EntityDecoration.RegisterProperty(fuelLevelPropertyName, DecorationType.Float);
            
            EntityDecoration.RegisterProperty(carKeys, DecorationType.Bool);
            EntityDecoration.RegisterProperty(carjacked, DecorationType.Bool);
            EntityDecoration.RegisterProperty(vehcileUnlocked, DecorationType.Float);
            //EventHandlers["nocarjack:skipThisFrame"] += new Action<int, int>(Skip);
            //EventHandlers["nocarjack:addOwnedVehicle"] += new Action<int, int>(Add);
            //EventHandlers["nocarjack:removeOwnedVehicle"] += new Action<int, int>(Remove);
            
            
            //initialized = false;
            //personalVehiclesList = new List<Vehicle>();

            //lastVehicle = null;
            lastVehicle = null;

            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
            //SetNuiFocus(false, false);


            Tick += OnTick;
            
            Tick += LockVeh;


        }

        private async void blinkLights(Vehicle veh)
        {
            veh.AreBrakeLightsOn = true;
            veh.AreLightsOn = true;

            Wait(10000);

            veh.AreBrakeLightsOn = false;
            veh.AreLightsOn = false;

            Wait(10000);

            veh.AreBrakeLightsOn = true;
            veh.AreLightsOn = true;
            Wait(10000);

            veh.AreBrakeLightsOn = false;
            veh.AreLightsOn = false;
        }

        //protected bool UNLOCKED = false;
        private string carjacked = "_car_jacked";
        private string vehcileUnlocked = "_car_unlocked";

        //protected bool UNLOCKUSED = true;
        /*public async Task VehLock()
        {
            Update();

            if (SKIP && skipTimer.Expired)
            {
                SKIP = false;
            }


            if (Game.PlayerPed.Exists() && Game.PlayerPed.IsAlive && Game.PlayerPed.CurrentVehicle != null)
            {
                if (Game.PlayerPed.CurrentVehicle.Driver != null)
                {
                    if (Game.PlayerPed.CurrentVehicle.Driver.Handle == Game.PlayerPed.Handle)
                    {
                        lastVehicle = Game.PlayerPed.CurrentVehicle;
                        Add(Game.Player.ServerId, lastVehicle.GetNetworkID());
                    }
                }
            }

            if (Game.PlayerPed.VehicleTryingToEnter != null && Game.PlayerPed.VehicleTryingToEnter.Exists() && !LOCK && !SKIP)
            {
                Debug.WriteLine(Game.PlayerPed.VehicleTryingToEnter.ToString());
                //Check if vehicle is unlocked and free to be entered
                if (Function.Call<int>(Hash.GET_VEHICLE_DOOR_LOCK_STATUS, Game.PlayerPed.VehicleTryingToEnter) != 2 && Function.Call<int>(Hash.GET_VEHICLE_DOOR_LOCK_STATUS, Game.PlayerPed.VehicleTryingToEnter) != 10)
                {
                    targetVeh = Game.PlayerPed.VehicleTryingToEnter;
                    if (IsVehicleInHistory(targetVeh.GetNetworkID()))
                    {
                        return;
                    }
                    if (!Game.PlayerPed.IsLucky(90))
                    {
                        if (targetVeh.HasDriver())
                        {
                            if (!IsPedInPlayerList(targetVeh.Driver)) //avoid disabling another's player vehicle
                            {
                                Game.PlayerPed.Task.ClearAll();
                                targetVeh.LockStatus = VehicleLockStatus.Locked;
                                Function.Call(Hash.SET_VEHICLE_UNDRIVEABLE, targetVeh, true);
                                targetVeh.IsEngineRunning = true;
                                if (targetVeh.IsPersistent) targetVeh.IsPersistent = false;
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            if (!IsVehiclePlayerListLastVehicle(targetVeh))
                            {
                                if (lastVehicle != null)
                                {
                                    if (lastVehicle.Handle != targetVeh.Handle)
                                    {
                                        Game.PlayerPed.Task.ClearAll();
                                        targetVeh.LockStatus = VehicleLockStatus.CannotBeTriedToEnter;
                                        Function.Call(Hash.SET_VEHICLE_UNDRIVEABLE, targetVeh, true);
                                        if (targetVeh.IsPersistent) targetVeh.IsPersistent = false;
                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                {
                                    Game.PlayerPed.Task.ClearAll();
                                    targetVeh.LockStatus = VehicleLockStatus.CannotBeTriedToEnter;
                                    Function.Call(Hash.SET_VEHICLE_UNDRIVEABLE, targetVeh, true);
                                    if (targetVeh.IsPersistent) targetVeh.IsPersistent = false;
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                    else
                    {

                    }
                }
                unlockTimer.Limit = 500;
                LOCK = true;
            }

            if (unlockTimer.Expired && LOCK)
            {
                if (Game.PlayerPed.VehicleTryingToEnter != null)
                {
                    if (targetVeh.Handle == Game.PlayerPed.VehicleTryingToEnter.Handle)
                    {
                        unlockTimer.Limit = 1000;
                    }
                    else
                    {
                        LOCK = !LOCK;
                    }
                }
                else
                {
                    LOCK = !LOCK;
                }
            }
            await Task.FromResult(0);
        }*/

        private async Task LockVeh()
        {

            Ped playerPed = Game.PlayerPed;


            if (Game.PlayerPed.VehicleTryingToEnter != null && Game.PlayerPed.VehicleTryingToEnter.Exists())
            {
                Player pl = Game.Player;
                Vehicle veh = Game.PlayerPed.VehicleTryingToEnter;
                //some code for flyers later on
                /*Debug.WriteLine(veh.ClassType.ToString());

                if (veh.ClassType == VehicleClass.Planes)
                {
                    veh.LockStatus = VehicleLockStatus.Unlocked;
                    Debug.WriteLine("plane");
                }
                if (veh.ClassType == VehicleClass.Military)
                {
                    Debug.WriteLine("milit");
                }
                if (veh.ClassType == VehicleClass.Boats)
                {
                    Debug.WriteLine("boat");
                }
                if (veh.ClassType == VehicleClass.Helicopters)
                {
                    Debug.WriteLine("heli");
                }
                else*/
                if (veh.ClassType == VehicleClass.Commercial
             || veh.ClassType == VehicleClass.Compacts
             || veh.ClassType == VehicleClass.Coupes
             || veh.ClassType == VehicleClass.Emergency
             || veh.ClassType == VehicleClass.Industrial
             || veh.ClassType == VehicleClass.Muscle
             || veh.ClassType == VehicleClass.OffRoad
             || veh.ClassType == VehicleClass.Sedans
             || veh.ClassType == VehicleClass.Service
             || veh.ClassType == VehicleClass.Sports
             || veh.ClassType == VehicleClass.SportsClassics
             || veh.ClassType == VehicleClass.Super
             || veh.ClassType == VehicleClass.SUVs
             || veh.ClassType == VehicleClass.Utility
             || veh.ClassType == VehicleClass.Vans)
                {
                    Debug.WriteLine(veh.LockStatus.ToString());
                    if (IsVehicleInHistory(veh.NetworkId))
                        return;
                    if (!veh.HasDecor(carjacked))
                        veh.SetDecor(carjacked, false);
                    if (!veh.HasDecor(vehcileUnlocked))
                        veh.SetDecor(vehcileUnlocked, -1);

                    if (!veh.GetDecor<bool>(carjacked))
                        //no owner?
                        if (!veh.HasDecor(carOwner))
                        {
                            //someone in car?
                            if (!veh.GetPedOnSeat(VehicleSeat.Driver).IsPlayer && veh.HasDriver())
                            {
                                Debug.WriteLine("stolen npc veh");


                                if (targetVeh == veh)
                                {
                                    if (veh.GetDecor<int>(vehcileUnlocked) == 1)
                                    {
                                        veh.LockStatus = VehicleLockStatus.Unlocked;
                                        veh.SetDecor(carjacked, true);
                                        //veh.SetDecor(vehcileUnlocked, 0);
                                    }

                                }
                                else
                                {
                                    targetVeh = veh;
                                    veh.SetDecor(vehcileUnlocked, 0);
                                    veh.LockStatus = VehicleLockStatus.Locked;
                                }
                            }
                            //empty car
                            else if (!veh.HasDriver())
                            {
                                Debug.WriteLine("no driver");
                                //car keys?
                                if (!veh.HasDecor(carKeys))
                                {
                                    Debug.WriteLine("no keys decor");
                                    if (targetVeh == veh)
                                    {
                                        if (veh.GetDecor<int>(vehcileUnlocked) == 1)
                                        {
                                            veh.LockStatus = VehicleLockStatus.Unlocked;

                                            veh.SetDecor(carjacked, true);
                                            //veh.SetDecor(vehcileUnlocked, 0);
                                            Debug.WriteLine("is unlocked");
                                        }

                                    }
                                    else
                                    {
                                        targetVeh = veh;
                                        veh.SetDecor(vehcileUnlocked, 0);
                                        veh.LockStatus = VehicleLockStatus.Locked;
                                        Debug.WriteLine("is locked");
                                    }
                                }
                                else if (veh.HasDecor(carKeys))
                                {
                                    if (AreKeysInCar(veh))
                                    {
                                        Debug.WriteLine("keys are in car");
                                    }
                                    else
                                    {
                                        if (targetVeh == veh)
                                        {
                                            if (veh.GetDecor<int>(vehcileUnlocked) == 1)
                                            {
                                                veh.LockStatus = VehicleLockStatus.Unlocked;
                                                veh.SetDecor(carjacked, true);
                                                //veh.SetDecor(vehcileUnlocked, false);
                                            }

                                        }
                                        else
                                        {
                                            targetVeh = veh;
                                            veh.SetDecor(vehcileUnlocked, 0);
                                            veh.LockStatus = VehicleLockStatus.Locked;

                                        }
                                    }
                                }
                            }


                        }
                        else if (veh.HasDecor(carOwner))
                        {

                            Debug.WriteLine("carownerDecor Triggered");
                            if (veh.Driver == null)
                            {
                                //car keys?
                                if (!veh.HasDecor(carKeys))
                                {


                                    if (targetVeh == veh)
                                    {
                                        if (veh.GetDecor<int>(vehcileUnlocked) == 1)
                                        {
                                            veh.LockStatus = VehicleLockStatus.Unlocked;
                                            veh.SetDecor(carjacked, true);
                                            //veh.SetDecor(vehcileUnlocked, false);
                                        }

                                    }
                                    else
                                    {
                                        targetVeh = veh;
                                        veh.SetDecor(vehcileUnlocked, 0);
                                        veh.LockStatus = VehicleLockStatus.Locked;
                                    }
                                }
                                else if (veh.HasDecor(carKeys))
                                {
                                    if (AreKeysInCar(veh))
                                    {
                                        Debug.WriteLine("keys are in car");
                                    }
                                    else
                                    {
                                        if (targetVeh == veh)
                                        {
                                            if (veh.GetDecor<int>(vehcileUnlocked) == 1)
                                            {
                                                veh.LockStatus = VehicleLockStatus.Unlocked;
                                                veh.SetDecor(carjacked, true);
                                                //veh.SetDecor(vehcileUnlocked, false);
                                            }

                                        }
                                        else
                                        {
                                            targetVeh = veh;
                                            veh.SetDecor(vehcileUnlocked, 0);
                                            veh.LockStatus = VehicleLockStatus.Locked;
                                        }
                                    }
                                }
                            }
                        }
                }
            }

            if (lastVehicle != null)
                if (playerCloseToVeh(lastVehicle, Game.PlayerPed, 10.0f)
                        && !Game.PlayerPed.IsInVehicle())
                {
                    if (Game.IsControlJustReleased(0, Control.VehicleDuck))
                    {
                        if (lastVehicle.LockStatus == VehicleLockStatus.Unlocked)
                        {
                            //TODO send status to server
                            lastVehicle.LockStatus = VehicleLockStatus.Locked;
                            lastVehicle.IsEngineRunning = false;
                            lastVehicle.IsDriveable = false;
                            blinkLights(lastVehicle);
                            //Wait(1000);
                            Debug.WriteLine("veh locked");

                        }
                        else if (lastVehicle.LockStatus == VehicleLockStatus.Locked)
                        {
                            //TODO send status to server
                            lastVehicle.LockStatus = VehicleLockStatus.Unlocked;
                            lastVehicle.IsEngineRunning = true;
                            lastVehicle.IsDriveable = true;
                            blinkLights(lastVehicle);
                            //Wait(1000);
                            Debug.WriteLine("veh unlocked");
                        }
                    }
                }

            await Task.FromResult(0);

        }

        private bool AreKeysInCar(Vehicle veh)
        {

            return veh.GetDecor<bool>(carKeys);

        }

        private void LeaveKeysInCar(Vehicle veh)
        {
            //TODO send status to server
            veh.SetDecor(carKeys, true);
        }
        private void TakeoutKeys(Vehicle veh)
        {
            //TODO send status to server
            veh.SetDecor(carKeys, false);
        }

        private void ToggleCarKeys()
        {
            if (lastVehicle != null)
                if (lastVehicle.Exists() && lastVehicle.IsAlive && playerCloseToVeh(lastVehicle, Game.PlayerPed, 25.0f) && lastVehicle.HasDecor(carKeys))
                    if (AreKeysInCar(lastVehicle))
                    {
                        //instead use a server event
                        TakeoutKeys(lastVehicle);
                        TriggerEvent("chat:addMessage", new
                        {
                            color = new[] { 255, 0, 0 },
                            args = new[] { "[YourVehicle]", $"Keys left in car" }
                        });
                    }
                    else
                    {
                        //instead use a server event
                        LeaveKeysInCar(lastVehicle);
                        TriggerEvent("chat:addMessage", new
                        {
                            color = new[] { 255, 0, 0 },
                            args = new[] { "[YourVehicle]", $"Keys Taken out" }
                        });
                    }
        }

        private void SearchCarKeys()
        {
            if (lastVehicle != null && Game.PlayerPed.IsInVehicle())
                if (!lastVehicle.HasDecor(carKeys) && Game.PlayerPed.CurrentVehicle == lastVehicle)

                    if (
                         lastVehicle.Exists()
                        && lastVehicle.IsAlive
                        && lastVehicle.Driver == Game.PlayerPed)
                    {

                        float min = 1.0f / 3f;
                        float max = 1.0f - (1.0f / 4.0f);


                        if ((float)(random.NextDouble() * (max - min)) + min > 0.3f)
                        {
                            lastVehicle.SetDecor(carKeys, true);
                            TriggerEvent("chat:addMessage", new
                            {
                                color = new[] { 255, 0, 0 },
                                args = new[] { "[Unlocker]", $"Vehicle keys found" }
                            });


                        }
                        else
                        {
                            lastVehicle.SetDecor(carKeys, false);

                            TriggerEvent("chat:addMessage", new
                            {
                                color = new[] { 255, 0, 0 },
                                args = new[] { "[Unlocker]", $"Vehicle NOT keys found" }
                            });
                        }
                    }
        }

        private void UnlockCar()
        {
            if (targetVeh != null)
                if (targetVeh.IsAlive && targetVeh.Exists())
                {



                    if (playerCloseToVeh(targetVeh, Game.PlayerPed, 50.0f))
                    {
                        //triggerminigame?
                        TriggerEvent("chat:addMessage", new
                        {
                            color = new[] { 255, 0, 0 },
                            args = new[] { "[Unlocker]", $"Vehicle unlocking" }
                        });
                    }

                    Wait(10);
                    if (playerCloseToVeh(targetVeh, Game.PlayerPed, 50.0f))
                    {
                        TriggerEvent("chat:addMessage", new
                        {
                            color = new[] { 255, 0, 0 },
                            args = new[] { "[Unlocker]", $"Vehicle unlocked" }
                        });
                        //instead use a server event
                        targetVeh.SetDecor(vehcileUnlocked, 1);
                    }
                }
        }

        public void OnClientResourceStart(string resourceName)
        {

            if (GetCurrentResourceName() != resourceName) return;
            Debug.WriteLine(resourceName);


            RegisterCommand("ToggleCarKeys", new Action<int, List<object>, string>(async (source, args, raw) =>
            {

                ToggleCarKeys();
            }
            ), false);

            RegisterCommand("SearchKeys", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                SearchCarKeys();
            }
            ), false);

            RegisterCommand("Unlock", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                //Ped ped = Game.PlayerPed;
                //targetVeh = Get(ped.Position.X, ped.Position.Y, ped.Position.Z,10.0f,)
                UnlockCar();
            }
            ), false);
        }

        


        public bool playerCloseToVeh(Vehicle veh, Ped playerPed, float rangesquared)
        {
            if (Vector3.DistanceSquared(playerPed.Position, veh.Position) < rangesquared)
            {
                return true;
            }

            return false;
        }


        



        /// <summary>
        /// Checks whether the specified ped belongs to a player's ped
        /// </summary>
        /// <param name="ped"></param>
        /// <returns></returns>
        private bool IsPedInPlayerList(Ped ped)
        {
            PlayerList list = base.Players;
            foreach (Player p in list)
            {
                if (p.Character == ped)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks whether the specified vehicle belongs to another player
        /// </summary>
        /// <param name="veh"></param>
        /// <returns></returns>
        private bool IsVehiclePlayerListLastVehicle(Vehicle veh)
        {
            PlayerList list = base.Players;
            foreach (Player p in list)
            {
                if (p.Handle != Game.PlayerPed.Handle)
                {
                    if (p.Character.LastVehicle != null)
                    {
                        if (p.Character.LastVehicle.Handle == veh.Handle)
                        {
                            //CitizenFX.Core.UI.Screen.ShowNotification("~r~PRIVATE VEHICLE");
                            return true;
                        }
                    }
                }
            }
            //CitizenFX.Core.UI.Screen.ShowNotification("~g~PUBLIC VEHICLE");
            return false;
        }

        /// <summary>
        /// Checks if a vehicle already exists as a newtork registered vehicle
        /// </summary>
        /// <param name="veh"></param>
        /// <returns></returns>
        private bool IsVehicleInHistory(int vehNetworkID)
        {
            for (int i = vehicleHistory.Count - 1; i >= 0; i--) // Allows the list to be looped even if it gets changed at runtime
            {
                if (vehicleHistory[i] == vehNetworkID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Add an owned vehicle to the vehicle history list which basically allows the player to get in this networked car
        /// </summary>
        /// <param name="playerId">Targetted player ID</param>
        /// <param name="networkVehicleID">Must be retrieved with NETWORK_GET_NETWORK_ID_FROM_ENTITY, works only with MISSION_ENTITY</param>
        private void Add(int playerId, int vehNetworkID)
        {
            int id = Game.Player.ServerId;
            if (playerId == id)
            {
                if (!vehicleHistory.Contains(vehNetworkID) && vehNetworkID != 0 && vehicleHistory.Count < maxVehicles)
                {
                    vehicleHistory.Add(vehNetworkID);
                }
            }
        }

        /// <summary>
        /// Removes the selected vehicle from the vehicle history list
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="networkVehicleID"></param>
        private void Remove(int playerId, int vehNetworkID)
        {
            int id = Game.Player.ServerId;
            if (playerId == id)
            {
                if (vehicleHistory.Contains(vehNetworkID) && vehNetworkID != 0)
                {
                    vehicleHistory.Remove(vehNetworkID);
                }
            }
        }

        /// <summary>
        /// Remove detroyed vehicles since storing them doesn't make any sense
        /// </summary>
        /// <param name="refreshTime">Let the CPU breathe</param>
        private void Update(int refreshTime = 6000)
        {
            if (updateTimer.Expired)
            {
                if (vehicleHistory.Count > 0)
                {
                    for (int i = vehicleHistory.Count - 1; i >= 0; i--)
                    {
                        Vehicle tempVeh = new Vehicle(Function.Call<int>(Hash.NETWORK_GET_ENTITY_FROM_NETWORK_ID, vehicleHistory[i]));
                        if (tempVeh.Exists() && tempVeh.IsDead)
                        {
                            tempVeh.IsPersistent = false;
                            vehicleHistory.Remove(vehicleHistory[i]);
                        }
                    }
                    updateTimer.Limit = refreshTime;
                }
            }
        }


        public IEnumerable<Vector3> GetNearbyJerryCanPickUpCoordinates(Vector3 position)
        {
            return GasStations.positions.Where(p => p.DistanceToSquared(position) < 100.0f);
        }
        public async Task ManageNearbyJerryCanPickUps()
        {


            Vector3 pos = GetEntityCoords(PlayerPedId(), true);

            int model = 883325847;

            Function.Call(Hash.REQUEST_MODEL, model);

            IEnumerable<Vector3> positions = GetNearbyJerryCanPickUpCoordinates(pos);

            if (positions.Count() == 0 && pickups.Count != 0)
            {
                pickups.ForEach(p => p.Delete());
                pickups.Clear();
            }
            else
            {
                positions.ToList().ForEach(position =>
                {
                    if (!pickups.Any(pickup => position.DistanceToSquared(pickup.Position) < 5f))
                    {
                        // add pickup if one doesn't exist within 5f proximity of it
                        int pickupHandle = CreatePickup(
                            0xc69de3ff, // Petrol Can
                            position.X, position.Y, position.Z - 0.5f,
                            8 | 32, // Place on the ground, local only
                            0,
                            true,
                            (uint)model);

                        Pickup pickup = new Pickup(pickupHandle);

                        pickups.Add(pickup);
                    }
                });
            }
            await Task.FromResult(0);
        }

        public void CreateBlips()
        {
            for (int i = 0; i < GasStations.positions.Length; i++)
            {
                var blip = World.CreateBlip(GasStations.positions[i]);
                blip.Sprite = BlipSprite.JerryCan;
                blip.Color = BlipColor.FranklinGreen;
                blip.Scale = 0.6f;
                blip.IsShortRange = true;
                blip.Name = "Gas Station";

                blips[i] = blip;
            }
        }

        public Vector3 GetVehicleTankPos(Vehicle vehicle)
        {
            EntityBone bone = null;

            foreach (var boneName in tankBones)
            {
                var boneIndex = GetEntityBoneIndexByName(vehicle.Handle, boneName);

                bone = vehicle.Bones[boneIndex];

                if (bone.IsValid)
                {
                    break;
                }
            }

            if (bone == null)
            {
                return vehicle.Position;
            }

            return bone.Position;
        }

        public bool IsVehicleNearAnyPump(Vehicle vehicle)
        {
            var fuelTankPos = GetVehicleTankPos(vehicle);

            foreach (var pump in GasStations.pumps[currentGasStationIndex])
            {
                if (Vector3.DistanceSquared(pump, fuelTankPos) <= 20f)
                {
                    return true;
                }
            }

            return false;
        }

        public bool PlayerVehicleViableForFuel()
        {
            Ped playerPed = Game.PlayerPed;
            Vehicle vehicle = playerPed.CurrentVehicle;

            return playerPed.IsInVehicle() &&
              (
                vehicle.Model.IsCar ||
                vehicle.Model.IsBike ||
                vehicle.Model.IsQuadbike
              ) &&
              vehicle.GetPedOnSeat(VehicleSeat.Driver) == playerPed &&
              vehicle.IsAlive;
        }
        public void InitFuel(Vehicle vehicle)
        {
            currentVehicleFuelLevelInitialized = true;

            fuelTankCapacity = VehicleMaxFuelLevel(vehicle);

            if (!vehicle.HasDecor(fuelLevelPropertyName))
            {
                vehicle.SetDecor(
                    fuelLevelPropertyName,
                    RandomizeFuelLevel(fuelTankCapacity)
                );
            }

            vehicle.FuelLevel = vehicle.GetDecor<float>(fuelLevelPropertyName);
        }


        public float RandomizeFuelLevel(float fuelLevel)
        {
            float min = fuelLevel / 3f;
            float max = fuelLevel - (fuelLevel / 4);

            return (float)((random.NextDouble() * (max - min)) + min);
        }


        

        public async Task OnTick()
        {
            //Game.PlayerPed.IsInVehicle();

            if (!initialized)
            {
                initialized = true;

                //LoadConfig();

                
                CreateBlips();
            }
            //CreateBlips();

            await ManageNearbyJerryCanPickUps();

            Ped playerPed = Game.PlayerPed;
            Player player = Game.Player;
            player.IgnoredByPolice = true;
            player.WantedLevel = 0;

            Update();

            //int vehicleid = GetVehiclePedIsEntering(playerPed.NetworkId);
            //Vehicle enteredveh = (Vehicle)Vehicle.FromNetworkId(vehicleid);
            //Debug.WriteLine(playerPed.IsGettingIntoAVehicle.ToString());




            if (playerPed.IsInVehicle())
            {
                Vehicle vehicle = playerPed.CurrentVehicle;


                if (lastVehicle != vehicle)
                {
                    if (vehicle.Driver == playerPed)
                        Add(Game.Player.ServerId, vehicle.GetNetworkID());
                    foreach (int i in vehicleHistory)
                    {
                        Debug.WriteLine(i.ToString());
                    }
                    lastVehicle = vehicle;
                    currentVehicleFuelLevelInitialized = false;
                }


                if (PlayerVehicleViableForFuel())
                {

                    if (!currentVehicleFuelLevelInitialized)
                    {
                        InitFuel(vehicle);
                    }

                    ConsumeFuel(vehicle);
                    CheckGasStations(playerPed);
                    string jsonstring = "{ type : \"ui\", status:bool}";
                    float maxfuel = VehicleMaxFuelLevel(vehicle);
                    float fuelpercent = VehicleFuelLevel(vehicle) / maxfuel;
                    jsonstring = JsonConvert.SerializeObject(new
                    {
                        visible = true,
                        crfuel = fuelpercent
                    });
                    SendNuiMessage(jsonstring);
                    //DisplayText(.95f, .65f, "fuel:" + VehicleFuelLevel(vehicle).ToString());
                }

                else
                {


                    currentVehicleFuelLevelInitialized = false;
                }
            } else
            {
                string jsonstring = "{ type : \"ui\", status:bool}";
                jsonstring = JsonConvert.SerializeObject(new
                {
                    visible = false,
                    crfuel = 0.0f
                });
                SendNuiMessage(jsonstring);
            }

            await Task.FromResult(0);

            //DisplayText(.95f, .5f, "fuel:" + VehicleFuelLevel(vehicle).ToString());
        }
        public void DisplayText(float x, float y, string text)
        {
            BeginTextCommandDisplayText("STRING");
            AddTextComponentString(text);
            SetTextScale(1f, .5f);
            SetTextCentre(true);
            EndTextCommandDisplayText(x, y);
        }
        public void ControlEngine(Vehicle vehicle)
        {
            // Prevent the player from honking the horn whenever trying to toggle the engine.
            if (engineToggleControl == Control.VehicleDuck)
            {
                Game.DisableControlThisFrame(0, Control.VehicleDuck);

                // Also disable the rocket boost control for DLC cars.
                Game.DisableControlThisFrame(0, (Control)351); // INPUT_VEH_ROCKET_BOOST (E on keyboard, L3 on controller)
            }

            if (Game.IsControlJustReleased(0, engineToggleControl) && !Game.IsControlPressed(0, Control.Jump))
            {
                if (vehicle.IsEngineRunning)
                {
                    vehicle.IsDriveable = false;
                    //vehicle.IsEngineRunning = false;
                    SetVehicleEngineOn(vehicle.Handle, false, true, true); // temporary fix for when the engine keeps turning back on.
                }
                else
                {
                    vehicle.IsDriveable = true;
                    
                    vehicle.IsEngineRunning = true;
                }
            }
        }

        public void ConsumeFuel(Vehicle vehicle)
        {


            if (vehicle.Speed < 0.1f)
            {
                if (Game.IsControlJustReleased(0, Control.VehicleDuck))
                {
                    if (vehicle.IsEngineRunning)
                    {
                        vehicle.IsDriveable = false;
                        //vehicle.IsEngineRunning = false;
                        SetVehicleEngineOn(vehicle.Handle, false, true, true); // temporary fix for when the engine keeps turning back on.
                    }
                    else
                    {
                        vehicle.IsDriveable = true;
                        
                        vehicle.IsEngineRunning = true;
                    }
                }
            }


            float fuel = VehicleFuelLevel(vehicle);

            // Consuming
            if (fuel > 0 && vehicle.IsEngineRunning)
            {
                float normalizedRPMValue = (float)Math.Pow(vehicle.CurrentRPM, 1.5);
                float consumedFuel = 0f;

                consumedFuel += normalizedRPMValue * fuelRPMImpact;
                consumedFuel += vehicle.Acceleration * fuelAccelerationImpact;
                consumedFuel += vehicle.MaxTraction * fuelTractionImpact;

                fuel -= consumedFuel * fuelConsumptionRate;
                fuel = fuel < 0f ? 0f : fuel;
            }

#if MANUAL_ENGINE_CUTOFF
            if (fuel == 0f && vehicle.IsEngineRunning)
            {
                vehicle.IsEngineRunning = false;
            }
#endif

            // Refueling at gas station
            if (
              // If we have gas station near us
              currentGasStationIndex != -1 &&
              // And near any pump
              IsVehicleNearAnyPump(vehicle)
            )
            {
#if MANUAL_ENGINE_CUTOFF
                if (vehicle.Speed < 0.1f && fuel != 0)
#endif
                    if (vehicle.Speed < 0.1f)
                    {
                        //ControlEngine(vehicle);
                    }

                if (vehicle.IsEngineRunning)
                {
                    hud.InstructTurnOffEngine();
                    //Debug.WriteLine("hud.InstructTurnOffEngine();");
                }
                else
                {
                    hud.InstructRefuelOrTurnOnEngine();

                    if (refuelAllowed)
                    {
                        if (Game.IsControlPressed(0, Control.Jump))
                        {
                            if (fuel < fuelTankCapacity)
                            {
                                float fuelPortion = 0.1f * refuelRate;

                                fuel += fuelPortion;
                                addedFuelCapacitor += fuelPortion;
                            }
                        }

                        if (Game.IsControlJustReleased(0, Control.Jump) && addedFuelCapacitor > 0f)
                        {
                            //TriggerEvent("frfuel:fuelAdded", addedFuelCapacitor);
                            //TriggerServerEvent("frfuel:fuelAdded", addedFuelCapacitor);
                            //TODO send to server to calc money
                            addedFuelCapacitor = 0f;
                        }
                    }
                }

                hud.RenderInstructions();
                //PlayHUDAppearSound();
                hudActive = true;

                //Debug.WriteLine("hud stuff");
            }
            else
            {
#if MANUAL_ENGINE_CUTOFF
                if (fuel != 0f && !vehicle.IsEngineRunning)
                {
                    //vehicle.IsEngineRunning = true;
                }
#endif

                hudActive = false;
            }

            VehicleSetFuelLevel(vehicle, fuel);
        }

        public void VehicleSetFuelLevel(Vehicle vehicle, float fuelLevel)
        {
            float max = VehicleMaxFuelLevel(vehicle);

            if (fuelLevel > max)
            {
                fuelLevel = max;
            }

            vehicle.FuelLevel = fuelLevel;
            vehicle.SetDecor(fuelLevelPropertyName, fuelLevel);
        }

        public float VehicleFuelLevel(Vehicle vehicle)
        {
            if (vehicle.HasDecor(fuelLevelPropertyName))
            {
                return vehicle.GetDecor<float>(fuelLevelPropertyName);
            }
            else
            {
                return 65f;
            }
        }

        public float VehicleMaxFuelLevel(Vehicle vehicle)
        {
            return GetVehicleHandlingFloat(vehicle.Handle, "CHandlingData", "fPetrolTankVolume");
        }
    }


}
