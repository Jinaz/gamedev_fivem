using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace CharacterInverntoryUI
{
    public class CharacterInvUI : BaseScript 
    {

        Inventory inv;
        Ped playerped;
        public CharacterInvUI()
        {
            playerped = Game.PlayerPed;
            inv = new Inventory();
            Tick += ItemUsage;
        }


        private void error(IDictionary<string, object> arg1, CallbackDelegate arg2)
        {
            string jsonstring;
            jsonstring = JsonConvert.SerializeObject(new
            {
                toggle = false
            });
            SendNuiMessage(jsonstring);
            SetNuiFocus(false, false);
        }

        private async Task ItemUsage()
        {
            inv.items[0] = ItemsData.SM_WATER;
            //Console.WriteLine(ItemsData.SM_WATER.ToString("g"));
            if (!playerped.IsInVehicle()
                || (playerped.IsInVehicle()
                && playerped.CurrentVehicle.Driver != playerped))
                if (Game.IsControlJustReleased(0, Control.Talk))
                {

                    //open UI

                    string jsonstring;
                    jsonstring = JsonConvert.SerializeObject(new
                    {
                        toggle = true
                    });
                    SendNuiMessage(jsonstring);

                    int slotnumber = 0;
                    SetNuiFocus(true, true);
                    UseItem(inv.items[0].ToString(), slotnumber);
                }
        }

        private void UseItem(string itemname, int slotnumber)
        {
            inv.items[slotnumber] = ItemsData.EMPTY;
            Debug.WriteLine(itemname);
        }
    }
}
