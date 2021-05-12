using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using static CitizenFX.Core.Native.API;


using CitizenFX.Core.UI;
using System.Drawing;
using Newtonsoft.Json;

namespace loginscript
{
    class UIcard
    {
        public string steamCharID;
        public string name;
        public string job;
        public int moneyhand;
        public int moneybank;

        public UIcard(string nameU, string jobU, int moneyhandU, int moneybankU, string steamCharIDU)
        {
            name = nameU;
            job = jobU;
            moneyhand = moneyhandU;
            moneybank = moneybankU;
            steamCharID = steamCharIDU;
        }

    }

    public class loginscript : BaseScript
    {

        Ped playerped;
        bool initialized = false;
        List<UIcard> cards = new List<UIcard>();
        public loginscript()
        {
            EventHandlers["loginscr:newPlayer"] += new Action(newPlayer);
            EventHandlers["onClientResourceStart"] += new Action<string>(OnResourceStartAsync);
            EventHandlers["loginscr:response"] += new Action<string>(responsePrint);
            EventHandlers["loginscr:respondPeds"] += new Action<Dictionary<string, string>[]>(respPeds);

            EventHandlers["loginscr:displayCharas"] += new Action<bool, string, string, string, string, string>(createUI);
            //SetNuiFocus(true,true);
            
        }

        private void newPlayer()
        {
            string jsonstring = "{\"cardscount\":" + 0 + "}";
            Debug.WriteLine(jsonstring);
            //SendNuiMessage(jsonstring);
            Debug.WriteLine(jsonstring);
            //SetNuiFocus(true, true);
            TriggerEvent("loginscr:createChar");
        }

        private void createUI(bool arg1, string arg2, string arg3, string arg4, string arg5, string steamCharID)
        {
            if (arg1)
            {
                cards.Add(new UIcard(arg2, arg3, Convert.ToInt32(arg4), Convert.ToInt32(arg4), steamCharID));
                string jsonstring = "{";
                int ucnumber = 0;
                if (cards.Any())
                {
                    jsonstring += "\"name" + 0 + "\": \"" + cards[0].name + "\", \"job" + 0 + "\":\"" + cards[0].job + "\", \"moneyhand" + 0 + "\":" + cards[0].moneyhand + ",\"moneybank" + 0 + "\":" + cards[0].moneyhand + ",\"charid" + 0 + "\":\"" + cards[0].steamCharID + "\"";
                    for (ucnumber = 1; ucnumber < cards.Count; ucnumber++)
                    {
                        jsonstring += ",\"name" + ucnumber + "\": \"" + cards[ucnumber].name + "\", \"job" + ucnumber + "\":\"" + cards[ucnumber].job + "\", \"moneyhand" + ucnumber + "\":" + cards[ucnumber].moneyhand + ",\"moneybank" + ucnumber + "\":" + cards[ucnumber].moneyhand + ",\"charid" + ucnumber + "\":\"" + cards[ucnumber].steamCharID + "\"";
                    }
                }


                jsonstring += ",\"cardscount\":" + ucnumber + "}";

                //SendNuiMessage(jsonstring);
                Debug.WriteLine(jsonstring);
                //SetNuiFocus(true, true);
            }
            else
            {
                cards.Add(new UIcard(arg2, arg3, Convert.ToInt32(arg4), Convert.ToInt32(arg4), steamCharID));
            }
        }

        private void respPeds(Dictionary<string, string>[] arg2)
        {
            Console.WriteLine("method moved to charcterinterface");

        }

        private void responsePrint(string obj)
        {
            Debug.WriteLine(obj);
        }

        private async Task defaultModel()
        {
            var model = new Model(PedHash.FreemodeMale01);
            bool x = await Game.Player.ChangeModel(model);
            //playerped.Style.SetDefaultClothes();
            Debug.WriteLine(x.ToString());
            foreach (PedComponents pc in (PedComponents[])Enum.GetValues(typeof(PedComponents)))
            {
                Game.PlayerPed.Style[pc].SetVariation(0);
            }
            foreach (PedProps pc in (PedProps[])Enum.GetValues(typeof(PedProps)))
            {
                Game.PlayerPed.Style[pc].SetVariation(0);
            }

        }

        private void OnResourceStartAsync(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            playerped = Game.PlayerPed;


            if (!initialized)
            {
                TriggerServerEvent("cbc:login");

                Debug.WriteLine("server event triggered");

                //set model to be changable
                defaultModel();


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
                TriggerServerEvent("cbc:basicInfo");
            }), false);

            RegisterCommand("setClothes", new Action<int, List<object>, string>(async (source, args, raw) =>
            {

                TriggerEvent("clths:changeClths");

                //playerped.Style[PedComponents.Hair].Index = Convert.ToInt32(args[0]);
                //bool valid = playerped.Style[PedComponents.Hair].IsVariationValid(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]));
                //Debug.WriteLine(valid.ToString());
                //if (valid) playerped.Style[PedComponents.Hair].SetVariation(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]));
            }), false);
        }
    }
}
