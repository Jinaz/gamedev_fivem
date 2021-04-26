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
            EventHandlers["charainter:response"] += new Action<string>(responsePrint);

        }
        private void responsePrint(string obj)
        {
            Debug.WriteLine(obj);
        }

        private void OnResourceStart(string obj)
        {
            playerped = Game.PlayerPed;


            if (!initialized)
            {
                TriggerServerEvent("cbc:login");

                Debug.WriteLine("server event triggered");
                //TODO connect SQL on start
                //get player data

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
        }
    }
}
