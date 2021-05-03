using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;


using CitizenFX.Core.UI;
using System.Drawing;
namespace lscustoms
{
    public class lsc : BaseScript
    {

        private Vehicle lastVehicle;

        public lsc()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
            lastVehicle = null;

            Tick += OnTick;
        }

        private async Task OnTick()
        {
            //check for Vehicle
            if (Game.PlayerPed.IsInVehicle())
            {
                if (lastVehicle != Game.PlayerPed.CurrentVehicle)
                    lastVehicle = Game.PlayerPed.CurrentVehicle;
            }
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            RegisterCommand("showMods", new Action<int, List<object>, string>(async (source, args, raw) =>
            {

                if (lastVehicle != null && lastVehicle.ClassType != VehicleClass.Boats && lastVehicle.ClassType != VehicleClass.Planes
                && lastVehicle.ClassType != VehicleClass.Cycles && lastVehicle.ClassType != VehicleClass.Emergency
                && lastVehicle.ClassType != VehicleClass.Helicopters && lastVehicle.ClassType != VehicleClass.Industrial
                && lastVehicle.ClassType != VehicleClass.Military && lastVehicle.ClassType != VehicleClass.Motorcycles
                && lastVehicle.ClassType != VehicleClass.Trains && lastVehicle.ClassType != VehicleClass.Utility

                )
                {
                    List<VehicleColor> primarycolros = new List<VehicleColor>();
                    List<VehicleColor> pearlcolors = new List<VehicleColor>();
                    List<VehicleColor> secondarycolors = new List<VehicleColor>();
                    List<VehicleWheelType> wheels = new List<VehicleWheelType>();
                    List<VehicleRoofState> roofstates = new List<VehicleRoofState>();
                    List<VehicleNeonLight> neons = new List<VehicleNeonLight>();
                    List<VehicleWindowTint> wintint = new List<VehicleWindowTint>();
                    List<VehicleModType> modlist = new List<VehicleModType>();
                    List<VehicleToggleModType> toggleModTypes = new List<VehicleToggleModType>();




                    foreach (VehicleColor color in (VehicleColor[])Enum.GetValues(typeof(VehicleColor)))
                    {
                        primarycolros.Add(color);
                        secondarycolors.Add(color);
                        pearlcolors.Add(color);

                        Debug.WriteLine("Color:" + color.ToString());
                    }



                    foreach (VehicleModType modtype in (VehicleModType[])Enum.GetValues(typeof(VehicleModType)))
                    {
                        Debug.WriteLine("VehModType:" + modtype.ToString());

                        Debug.WriteLine("MaxValue:" + (lastVehicle.Mods[modtype].ModCount - 1).ToString());
                        modlist.Add(modtype);
                    }


                    foreach (VehicleToggleModType toggleModtype in (VehicleToggleModType[])Enum.GetValues(typeof(VehicleToggleModType)))
                    {
                        Debug.WriteLine("toggleModType:" + toggleModtype.ToString());
                        //Debug.WriteLine("MaxValue:" + (lastVehicle.Mods[toggleModtype].ModCount - 1).ToString());
                        lastVehicle.Mods.LicensePlate = "S4DMUMMY";
                        toggleModTypes.Add(toggleModtype);
                    }


                    foreach (VehicleWindowTint wintint1 in (VehicleWindowTint[])Enum.GetValues(typeof(VehicleWindowTint)))
                    {
                        Debug.WriteLine("WindowTint:" + wintint1.ToString());
                        lastVehicle.Mods.WindowTint = wintint1;
                        Debug.WriteLine("windtint values:" + wintint1.ToString());
                        wintint.Add(wintint1);
                    }


                    foreach (VehicleRoofState roofstate in (VehicleRoofState[])Enum.GetValues(typeof(VehicleRoofState)))
                    {
                        Debug.WriteLine("roofstate:" + roofstate.ToString());
                        roofstates.Add(roofstate);
                    }


                    foreach (VehicleNeonLight neon in (VehicleNeonLight[])Enum.GetValues(typeof(VehicleNeonLight)))
                    {
                        Debug.WriteLine("neon:" + neon.ToString());
                        //lastVehicle.Mods.neon
                        Debug.WriteLine("neonValues:" + neon.ToString());
                        neons.Add(neon);
                    }


                    foreach (VehicleWheelType wheel in lastVehicle.Mods.AllowedWheelTypes)//(VehicleWheelType[])Enum.GetValues(typeof(VehicleWheelType)))
                    {
                        Debug.WriteLine("wheel:" + wheel.ToString());

                        Debug.WriteLine("allowed Wheels:" + wheel.ToString());
                        wheels.Add(wheel);
                    }


                    Debug.WriteLine(primarycolros.ToString());
                    Debug.WriteLine(pearlcolors.ToString());
                    Debug.WriteLine(secondarycolors.ToString());
                    Debug.WriteLine(wheels.ToString());
                    Debug.WriteLine(roofstates.ToString());
                    Debug.WriteLine(neons.ToString());
                    Debug.WriteLine(wintint.ToString());
                    Debug.WriteLine(modlist.ToString());
                    Debug.WriteLine(toggleModTypes.ToString());

                    lastVehicle.Mods.InstallModKit();

                }
            }), false);
        }


    }




}
