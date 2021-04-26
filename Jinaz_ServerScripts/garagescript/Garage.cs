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

namespace garagescript
{


    public class VehicleInformation : BaseScript
    {

        public VehicleInformation()
        {

        }

        public VehicleColor primcolor;
        public VehicleColor secondcolor;
        public VehicleColor pearlcolor;
        public VehicleWheelType wheeltypes;
        public VehicleNeonLight neon;
        public VehicleWindowTint tint;
        public VehicleModType Spoiler = VehicleModType.Spoilers;
        public VehicleModType FrontBumper = VehicleModType.FrontBumper;
        public VehicleModType RearBumper = VehicleModType.RearBumper;
        public VehicleModType SideSkirt = VehicleModType.SideSkirt;
        public VehicleModType Exhaust = VehicleModType.Exhaust;
        public VehicleModType Frame = VehicleModType.Frame;
        public VehicleModType Grille = VehicleModType.Grille;
        public VehicleModType Hood = VehicleModType.Hood;
        public VehicleModType Fender = VehicleModType.Fender;
        public VehicleModType RightFender = VehicleModType.RightFender;
        public VehicleModType Roof = VehicleModType.Roof;
        public VehicleModType Engine = VehicleModType.Engine;
        public VehicleModType Brakes = VehicleModType.Brakes;
        public VehicleModType Transmission = VehicleModType.Transmission;
        public VehicleModType Horns = VehicleModType.Horns;
        public VehicleModType Suspension = VehicleModType.Suspension;
        public VehicleModType Armor = VehicleModType.Armor;
        public VehicleModType FrontWheel = VehicleModType.FrontWheel;
        public VehicleModType RearWheel = VehicleModType.RearWheel;
        public VehicleModType PlateHolder = VehicleModType.PlateHolder;
        public VehicleModType VanityPlates = VehicleModType.VanityPlates;
        public VehicleModType TrimDesign = VehicleModType.TrimDesign;
        public VehicleModType Ornaments = VehicleModType.Ornaments;
        public VehicleModType Dashboard = VehicleModType.Dashboard;
        public VehicleModType DialDesign = VehicleModType.DialDesign;
        public VehicleModType DoorSpeakers = VehicleModType.DoorSpeakers;
        public VehicleModType Seats = VehicleModType.Seats;
        public VehicleModType SteeringWheels = VehicleModType.SteeringWheels;
        public VehicleModType ColumnShifterLevers = VehicleModType.ColumnShifterLevers;
        public VehicleModType Plaques = VehicleModType.Plaques;
        public VehicleModType Speakers = VehicleModType.Speakers;
        public VehicleModType Trunk = VehicleModType.Trunk;
        public VehicleModType Hydraulics = VehicleModType.Hydraulics;
        public VehicleModType EngineBlock = VehicleModType.EngineBlock;
        public VehicleModType AirFilter = VehicleModType.AirFilter;
        public VehicleModType Struts = VehicleModType.Struts;
        public VehicleModType ArchCover = VehicleModType.ArchCover;
        public VehicleModType Aerials = VehicleModType.Aerials;
        public VehicleModType Trim = VehicleModType.Trim;
        public VehicleModType Tank = VehicleModType.Tank;
        public VehicleModType Windows = VehicleModType.Windows;
        public VehicleModType Livery = VehicleModType.Livery;
        public string licenseplate;
        public VehicleColor RimColor;
        public VehicleColor TrimColor;
        public VehicleColor DashboardColor;
        public System.Drawing.Color TireSmokeColor;
        public LicensePlateType LicensePlateType;




    }
    public class Garage : BaseScript
    {
        protected Blip[] parkingblips;
        public GarageHUD ghud;
        //protected int currentParkingIndex;
        public float showMarkerInRangeSquared = 250f;
        protected Vehicle personalout = null;
        public static string carOwner = "_Veh_Owner";
        public static string carKeys = "_Veh_keys";
        protected Vehicle currentPersonal;
        protected Blip personalCarLocation;

        private void getCarInfoFromServer()
        {
            if (personalout != null)
            {
                Debug.WriteLine(personalout.Mods.GetAllMods().Length.ToString());


                for (int i = 0; i < personalout.Mods.GetAllMods().Length; i++)
                {
                    Debug.WriteLine(personalout.Mods.GetAllMods()[i].ModType.ToString());
                    Debug.WriteLine(personalout.Mods.GetAllMods()[i].Vehicle.ToString());
                    Debug.WriteLine(personalout.Mods.GetAllMods()[i].Variation.ToString());
                    Debug.WriteLine(personalout.Mods.GetAllMods()[i].ModCount.ToString());
                    Debug.WriteLine(personalout.Mods.GetAllMods()[i].LocalizedModTypeName.ToString());
                    Debug.WriteLine(personalout.Mods.GetAllMods()[i].LocalizedModName.ToString());
                    Debug.WriteLine(personalout.Mods.GetAllMods()[i].Index.ToString());
                    //System.Drawing.Color color = System.Drawing.Color.FromArgb(140,255,0,0);
                    VehicleInformation vi = new VehicleInformation();


                }
            }


        }

        public Garage()
        {
            ghud = new GarageHUD();

            ParkingGarages.InitViaCode();
            parkingblips = new Blip[ParkingGarages.positions.Length];


            RegisterNuiCallbackType("carchoice");
            RegisterNuiCallbackType("error");
            RegisterNuiCallbackType("exit");
            EventHandlers["__cfx_nui:carchoice"] += new Action<IDictionary<string, object>, CallbackDelegate>(carsmenuChoice);
            EventHandlers["__cfx_nui:error"] += new Action<IDictionary<string, object>, CallbackDelegate>(exit);
            EventHandlers["__cfx_nui:exit"] += new Action<IDictionary<string, object>, CallbackDelegate>(error);

            EntityDecoration.RegisterProperty(carOwner, DecorationType.Int);

            personalout = null;
            EventHandlers["garage:findcar"] += new Action<float, float, float>(FindCar);

            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
            SetNuiFocus(false, false);

            Tick += DisplayCarUI;
            Tick += VehSpawner;
            



        }
        protected bool initialized = false;
        protected bool displaySeller = false;
        private void ToggleCarSpawnerUI(bool value)
        {
            displaySeller = value;
            SetNuiFocus(value, value);
            string jsonstring = "{ type : \"ui\", status:bool}";
            jsonstring = JsonConvert.SerializeObject(new
            {
                type = "ui",
                status = value
            });
            SendNuiMessage(jsonstring);
            Debug.WriteLine("NUI sent");
        }
        private async Task DisplayCarUI()
        {
            if (displaySeller)
            {
                DisableControlAction(0, 1, displaySeller);
                DisableControlAction(0, 2, displaySeller);
                DisableControlAction(0, 142, displaySeller);
                DisableControlAction(0, 18, displaySeller);
                DisableControlAction(0, 322, displaySeller);
                DisableControlAction(0, 106, displaySeller);
            }
            else
            {
                EnableControlAction(0, 1, displaySeller);
                EnableControlAction(0, 2, displaySeller);
                EnableControlAction(0, 142, displaySeller);
                EnableControlAction(0, 18, displaySeller);
                EnableControlAction(0, 322, displaySeller);
                EnableControlAction(0, 106, displaySeller);
            }

        }

        private async Task VehSpawner()
        {
            if (!initialized)
            {
                CreateParkingBlips();
                initialized = true;
            }


            Ped playerPed = Game.PlayerPed;
            //Debug.WriteLine("Ped close to garage");
            if (IsPedNearParkingStation(playerPed, showMarkerInRangeSquared) && !playerPed.IsSittingInVehicle())
            {
                //Debug.WriteLine("Ped close to garage");
                ghud.InstructOpenMenu();

                if (Game.IsControlJustReleased(0, Control.VehicleFlyRollLeftOnly) && personalout == null)
                {
                    Debug.WriteLine("spawn UI opened");
                    SpawnAdder();

                    //TODO send update to DB

                }

                if (Game.IsControlJustReleased(0, Control.VehicleFlyRollRightOnly))
                {
                    try
                    {
                        if (Vector3.DistanceSquared(
                                ParkingGarages.positions[getClosestParking(
                                    personalout, showMarkerInRangeSquared)]
                                    , personalout.Position) <= 20f && personalout != null)
                        {
                            personalout.Delete();
                            //send update to DB
                            personalout = null;
                        }
                        //carSpawned = true;
                    }
                    catch (EntityDecorationUnregisteredPropertyException e)
                    {
                        TriggerEvent("chat:addMessage", new
                        {
                            color = new[] { 255, 0, 0 },
                            args = new[] { "[Garage]", $"vehicle not owned!" }
                        });
                    }
                }
                ghud.RenderInstructions();
            }

            await Task.FromResult(0);

        }

        private void carsmenuChoice(IDictionary<string, object> data, CallbackDelegate cb)
        {
            //out object value;
            //data.TryGetValue("text", value);


            string carmodel = data["text"].ToString();
            spawnGivenCarModel(carmodel);
            ToggleCarSpawnerUI(false);
        }
        private void error(IDictionary<string, object> data, CallbackDelegate cb)
        {
            Debug.WriteLine("error");
            ToggleCarSpawnerUI(false);
        }

        private void exit(IDictionary<string, object> data, CallbackDelegate cb)
        {
            Debug.WriteLine("exit" + data.ToString());
            ToggleCarSpawnerUI(false);
        }

        private async void spawnGivenCarModel(string model)
        {
            //var model = "adder";
            // check if the model actually exists
            // assumes the directive `using static CitizenFX.Core.Native.API;`

            Vehicle vehicle = null;
            var hash = (uint)GetHashKey(model);
            if (IsModelInCdimage(hash) && IsModelAVehicle(hash))
            {

                // create the vehicle
                vehicle = await World.CreateVehicle(model, Game.PlayerPed.Position, Game.PlayerPed.Heading);

                // set the player ped into the vehicle and driver seat
                Game.PlayerPed.SetIntoVehicle(vehicle, VehicleSeat.Driver);
                vehicle.IsEngineRunning = false;
                vehicle.IsDriveable = false;
                vehicle.LockStatus = VehicleLockStatus.Unlocked;
                vehicle.NeedsToBeHotwired = false;
                //personalout = vehicle;

                //Debug.WriteLine(lastVehicle.ClassDisplayName);
                vehicle.SetDecor(carOwner, Game.Player.Character.NetworkId);
                vehicle.SetDecor(carKeys, true);
                personalout = vehicle;
                personalout.IsPersistent = true;

                // tell the player
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] { 255, 0, 0 },
                    args = new[] { "[CarSpawner]", $"Woohoo! Enjoy your new ^*{model}!" }
                });

            }

            await Task.FromResult(0);
        }

        private void SpawnAdder()
        {
            ToggleCarSpawnerUI(true);

        }

        public void CreateParkingBlips()
        {
            for (int i = 0; i < ParkingGarages.positions.Length; i++)
            {
                var blip = World.CreateBlip(ParkingGarages.positions[i]);
                blip.Sprite = BlipSprite.Garage;
                blip.Color = BlipColor.Blue;
                blip.Scale = 0.6f;
                blip.IsShortRange = true;

                blip.Name = "Garage";

                parkingblips[i] = blip;
            }
        }

        public bool IsPedNearParkingStation(Ped playerped, float rangesquared)
        {
            //var fuelTankPos = GetVehicleTankPos(vehicle);

            int closestparking = getClosestParking(playerped, rangesquared);


            //Debug.WriteLine("AA");
            if (closestparking != -1)
                if (Vector3.DistanceSquared(ParkingGarages.positions[closestparking], playerped.Position) <= 20f)
                {
                    return true;
                }


            return false;
        }

        public int getClosestParking(Ped playerped, float rangesquared)
        {
            int closestparking = -1;
            for (int i = 0; i < parkingblips.Length; i++)
            {
                Blip blip = parkingblips[i];

                if (Vector3.DistanceSquared(ParkingGarages.positions[i], playerped.Position) < rangesquared)
                {
                    closestparking = i;
                }
            }
            return closestparking;
        }

        public int getClosestParking(Vehicle veh, float rangesquared)
        {
            int closestparking = -1;
            for (int i = 0; i < parkingblips.Length; i++)
            {
                Blip blip = parkingblips[i];

                if (Vector3.DistanceSquared(ParkingGarages.positions[i], veh.Position) < rangesquared)
                {
                    closestparking = i;
                }
            }
            return closestparking;
        }

        public void OnClientResourceStart(string resourceName)
        {

            Debug.WriteLine(resourceName);

            RegisterCommand("getInf", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                getCarInfoFromServer();

            }
            ), false);
            RegisterCommand("FindCar", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                if (personalout != null)
                    if (personalout.Exists() && personalout.IsAlive)
                        TriggerEvent("garage:findcar", personalout.Position.X, personalout.Position.Y, personalout.Position.Z);


            }
            ), false);

        }
        private void FindCar(float x, float y, float z)
        {

            {
                if (personalCarLocation != null)
                    if (personalCarLocation.Exists())
                        personalCarLocation.Delete();

                personalCarLocation = World.CreateBlip(new Vector3(x, y, z));
                personalCarLocation.Sprite = BlipSprite.PersonalVehicleCar;
                personalCarLocation.Color = BlipColor.Blue;
                personalCarLocation.Scale = 0.6f;
                personalCarLocation.IsShortRange = true;

                personalCarLocation.Name = "Vehicle";

            }
        }


    }
}
