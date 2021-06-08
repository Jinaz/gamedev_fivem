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
        
        CharacterInterface.CharacterAtts[] cas = new CharacterInterface.CharacterAtts[5];
        public loginscript()
        {
            EventHandlers["loginscr:newPlayer"] += new Action(newPlayer);
            EventHandlers["onClientResourceStart"] += new Action<string>(OnResourceStartAsync);
            //EventHandlers["loginscr:response"] += new Action<string>(responsePrint);
            //EventHandlers["loginscr:respondPeds"] += new Action<Dictionary<string, string>[]>(respPeds);

            EventHandlers["loginscr:displayCharas"] += new Action<string, int>(createUI);
            //SetNuiFocus(true,true);

            RegisterNuiCallbackType("createChar");
            EventHandlers["__cfx_nui:createChar"] += new Action<IDictionary<string, object>, CallbackDelegate>(createTriggered);

            RegisterNuiCallbackType("selectChar");
            EventHandlers["__cfx_nui:selectChar"] += new Action<IDictionary<string, object>, CallbackDelegate>(selectChar);


        }

        private void selectChar(IDictionary<string, object> arg1, CallbackDelegate arg2)
        {

            int choice = Convert.ToInt32(arg1["chosenchar"].ToString());


            TriggerEvent("charinf:setCharInf", JsonConvert.SerializeObject(cas[choice]));
            string steamidid = cas[choice].steamcharid;
            //find skin and apply with steamid_id
            //trigger server event then set skin
            TriggerEvent("loginscr:tpToSpawn");
            throw new NotImplementedException();
            //Trigger tp script after setting chara
        }

        private void createTriggered(IDictionary<string, object> arg1, CallbackDelegate arg2)
        {
            int slotnum = Convert.ToInt32(arg1["slotnum"].ToString());
            TriggerEvent("clths:changeClths", 0 );
        }

        private void newPlayer()
        {
            string jss = JsonConvert.SerializeObject(new
            {
                trigger = true,
                cardnumber = 5
            });

            SendNuiMessage(jss);
            SetNuiFocus(true, true);
        }


        private void createUI(string jsonstring, int index)
        {
            CharacterInterface.CharacterAtts ca = JsonConvert.DeserializeObject<CharacterInterface.CharacterAtts>(jsonstring);
            cas[index] = ca;

            string jss = JsonConvert.SerializeObject(new {
                trigger = true,
                cardnumber = index,
                charactername = ca.CharacterName,
                hand = ca.moneyHand,
                bank = ca.moneyBank,
                job = ca.job

            });

            SendNuiMessage(jss);
            SetNuiFocus(true, true);
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

                defaultModel();
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
                defaultModel();
                TriggerEvent("clths:changeClths", 0 );

                //playerped.Style[PedComponents.Hair].Index = Convert.ToInt32(args[0]);
                //bool valid = playerped.Style[PedComponents.Hair].IsVariationValid(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]));
                //Debug.WriteLine(valid.ToString());
                //if (valid) playerped.Style[PedComponents.Hair].SetVariation(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]));
            }), false);

            RegisterCommand("checkFunctions", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                //defaultModel();
                foreach (string s in args)
                {
                    Debug.WriteLine(s);
                }
                int pedid = Game.PlayerPed.Handle;
                //SetPedComponentVariation();
                int overlayid = Convert.ToInt32(args[0]);
                int maxoverlay = GetNumHeadOverlayValues(overlayid)-1;
                int index = Convert.ToInt32(args[1]);
                float opacity = float.Parse(args[2].ToString());
                SetPedHeadOverlay(pedid, overlayid, index, opacity);
                //SetPedHairColor();
                //SetPedEyeColor();

            }),false);

            RegisterCommand("checkFunctions2", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                //defaultModel();
                foreach (string s in args)
                {
                    Debug.WriteLine(s);
                }
                int pedid = Game.PlayerPed.Handle;
                int overlayid = Convert.ToInt32(args[0]);
                //SetPedHairColor();
                //SetPedEyeColor();
                int colorType = Convert.ToInt32(args[1]);
                int colorid = Convert.ToInt32(args[2]);
                int secondcolorid = Convert.ToInt32(args[3]);
                SetPedHeadOverlayColor(pedid, overlayid, colorType, colorid, secondcolorid);
                //SetPedHeadBlendData();
            }), false);

            RegisterCommand("checkFunctions3", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                //defaultModel();
                foreach (string s in args)
                {
                    Debug.WriteLine(s);
                }
                int pedid = Game.PlayerPed.Handle;
                int componentid = Convert.ToInt32(args[0]);
                //SetPedHairColor();
                //SetPedEyeColor();
                int drawableid = Convert.ToInt32(args[1]);
                int textureid = Convert.ToInt32(args[2]);
                int paletteid = Convert.ToInt32(args[3]);
                //Game.PlayerPed.Style[PedComponents.Hair].SetVariation(drawableid, textureid);
                Debug.WriteLine("Variations:"+Game.PlayerPed.Style[PedComponents.Torso].TextureCount.ToString());
                Debug.WriteLine("Variations:" + Game.PlayerPed.Style[PedComponents.Torso].Count.ToString());
                SetPedComponentVariation(Game.PlayerPed.Handle, componentid, drawableid, textureid, paletteid);
                //SetPedHeadBlendData();
            }), false);

            RegisterCommand("checkFunctions4", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                //defaultModel();
                
                foreach (string s in args)
                {
                    Debug.WriteLine(s);
                }
                int pedid = Game.PlayerPed.Handle;
                int faceShape = Convert.ToInt32(args[0]);
                int skincolor = Convert.ToInt32(args[1]);
                Debug.WriteLine("Count:" + Game.PlayerPed.Style[PedComponents.Face].Count.ToString());
                Debug.WriteLine("TextureCount:" + Game.PlayerPed.Style[PedComponents.Face].TextureCount.ToString());
                //SetPedHeadOverlayColor(pedid, overlayid, colorType, colorid, secondcolorid);
                SetPedHeadBlendData(pedid, 0,0,faceShape,0,0,skincolor,0.0f,0.0f,1.0f,false);
            }), false);
            RegisterCommand("checkFunctions5", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                //defaultModel();

                foreach (string s in args)
                {
                    Debug.WriteLine(s);
                }
                int pedid = Game.PlayerPed.Handle;
                int primcolor = Convert.ToInt32(args[0]);
                int secondcolor = Convert.ToInt32(args[1]);
                //SetPedHeadOverlayColor(pedid, overlayid, colorType, colorid, secondcolorid);
                SetPedHairColor(pedid, primcolor, secondcolor);
                SetPedEyeColor(pedid, primcolor);
            }), false);


            RegisterCommand("dm", new Action<int, List<object>, string>(async (source, args, raw) =>
            {
                //set model to be changable
                defaultModel();
               
                }), false);

            ;
        }
    }
}
