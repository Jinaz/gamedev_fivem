using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using static CitizenFX.Core.Native.API;


using CitizenFX.Core.UI;
using System.Drawing;

namespace loginscript
{
    public class loginscript : BaseScript
    {

        Ped playerped;
        bool initialized = false;
        public loginscript()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnResourceStart);
            EventHandlers["loginscr:response"] += new Action<string>(responsePrint);
            EventHandlers["loginscr:respondPeds"] += new Action<Dictionary<string, string>[]>(respPeds);

            //SetNuiFocus(true,true);
        }

        private void respPeds(Dictionary<string, string>[] arg2)
        {
            foreach (Dictionary<string, string> peddict in arg2)
            {
                foreach (string key in peddict.Keys)
                {
                    Ped ped = new Ped(0);

                    foreach (PedComponents pc in (PedComponents[])Enum.GetValues(typeof(PedComponents)))
                    {
                        if (pc.ToString() == key)
                            ped.Style[pc].Index = Int16.Parse(peddict[key]);
                        else if (key.Contains(pc.ToString()) && key.Length > pc.ToString().Length)
                            ped.Style[pc].TextureIndex = Int16.Parse(peddict[key]);
                    }
                }
            }

        }

        private void responsePrint(string obj)
        {
            Debug.WriteLine(obj);
        }

        private void OnResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            playerped = Game.PlayerPed;


            if (!initialized)
            {
                TriggerServerEvent("cbc:login");

                Debug.WriteLine("server event triggered");
                //TODO connect SQL on start
                //get player data


                /* some doc of the enum PedComponent
                 Face = 0,
        Head = 1,
        Hair = 2,
        Torso = 3,
        Legs = 4,
        Hands = 5,
        Shoes = 6,
        Special1 = 7,
        Special2 = 8,
        Special3 = 9,
        Textures = 10,
        Torso2 = 11
                 */


                /*PedProp
                 * 
                 * "Hats = 0,
        Glasses = 1,
        EarPieces = 2,
        Unknown3 = 3,
        Unknown4 = 4,
        Unknown5 = 5,
        Watches = 6,
        Wristbands = 7,
        Unknown8 = 8,
        Unknown9 = 9"*/


                //if first time login
                //if steamid in table then not first time --> open character selection
                //if not empty table
                //
                initialized = true;
            }

            RegisterCommand("getCharaInfo", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                playerped.Style.GetAllProps();

                playerped.Style.GetAllComponents();

                foreach (PedComponent pc in playerped.Style.GetAllComponents())
                {
                    Debug.WriteLine($"{pc} : {pc.Count} : {pc.TextureCount}");
                }

                foreach (PedComponents pc in (PedComponents[])Enum.GetValues(typeof(PedComponents)))
                {
                    PedComponent pedcomp = playerped.Style[pc];
                    Debug.WriteLine($"{pc.ToString()} : {pedcomp.Count.ToString()}");
                    if (pedcomp.HasTextureVariations)
                    {
                        Debug.WriteLine($"{pc.ToString()} : {pedcomp.TextureCount} Variations");
                        for (int txi = 0; txi < pedcomp.TextureCount; txi++)
                        {

                        }
                    }
                }

                foreach (PedProps pc in (PedProps[])Enum.GetValues(typeof(PedProps)))
                {
                    PedProp pedprop = playerped.Style[pc];
                    Debug.WriteLine($"{pc.ToString()} : {pedprop.Count.ToString()}");

                    if (pedprop.HasTextureVariations)
                    {
                        Debug.WriteLine($"{pc.ToString()} : {pedprop.TextureCount} Variations");
                        for (int txi = 0; txi < pedprop.TextureCount; txi++)
                        {

                        }
                    }

                }




            }
            ), false);

            RegisterCommand("triggerlogin", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                Console.WriteLine("login triggered");
                TriggerServerEvent("cbc:login");
            }),false);
        }
    }
}
